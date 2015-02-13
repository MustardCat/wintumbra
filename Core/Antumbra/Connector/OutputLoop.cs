﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antumbra.Glow.Utility;
using Antumbra.Glow.ExtensionFramework;
using System.Drawing;

namespace Antumbra.Glow.Connector
{
    public class OutputLoop : AntumbraColorObserver, IDisposable//TODO make output loop manager for getting starting stopping via id
    {
        private Task outputLoopTask;
        private FPSCalc outputFPS = new FPSCalc();
        private DeviceManager mgr;
        public int id { get; private set; }
        private Color color;
        private Color weightedAvg;
        private bool weightingEnabled;//TODO move to decorator?
        private double newColorWeight;

        public double FPS { get { return outputFPS.FPS; } }
        /// <summary>
        /// Synchronisation object
        /// </summary>
        private object sync = new object();

        private bool _active = false;
        /// <summary>
        /// Setting this to false will stop the output thread
        /// </summary>
        /// <remarks>Thread Safe</remarks>
        public bool Active
        {
            get
            {
                lock (sync)
                    return _active;
            }
            set
            {
                lock (sync)
                    _active = value;
            }
        }
        public OutputLoop(DeviceManager mgr, int devId)
        {
            this.id = devId;
            this.mgr = mgr;
        }

        public bool Start(bool weightEnabled, double newColorWeight)
        {
            this.weightingEnabled = weightEnabled;
            this.newColorWeight = newColorWeight;
            this.weightedAvg = Color.Black;
            this.color = Color.Black;
            this._active = true;
            this.outputLoopTask = new Task(target);
            this.outputLoopTask.Start();
            return true;
        }

        private void target()
        {
            try {
                while (Active) {
                    if (this.weightingEnabled) {
                        weightedAvg = Mixer.MixColorPercIn(color, weightedAvg, this.newColorWeight);
                        this.mgr.sendColor(weightedAvg, this.id);
                    }
                    else
                        this.mgr.sendColor(color, this.id);
                }
            }
            catch (Exception e) {
                lock (sync) {
                    Active = false;
                    Console.WriteLine("Exception in outputLoopTarget: " + e.Message);
                }
            }
        }

        private void Stop()
        {
            this._active = false;
            if (this.outputLoopTask != null)
                this.outputLoopTask.Wait(1000);
        }

        void AntumbraColorObserver.NewColorAvail(object sender, EventArgs args)
        {
            outputFPS.Tick();
            lock (sync) {
                color = (Color)sender;
            }
        }

        public void Dispose()
        {
            this.Stop();
            if (outputLoopTask != null)
                outputLoopTask.Dispose();
            this.outputFPS = new FPSCalc();
        }
    }
}
