﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Antumbra.Glow.ExtensionFramework;
using Antumbra.Glow.Utility;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace HSVFade
{
    [Export(typeof(GlowExtension))]
    public class HSVFade : GlowIndependentDriver
    {
        public override Guid id { get; set; }
        public delegate void NewColorAvail(Color newColor, EventArgs args);
        public event NewColorAvail NewColorAvailEvent;
        private Task driver;
        private bool running;

        public override bool IsRunning
        {
            get { return this.running; }
        }

        public override string Name
        {
            get { return "HSV Fade"; }
        }

        public override string Author
        {
            get { return "Team Antumbra"; }
        }

        public override string Description
        {
            get { return "A driver that does a continuous HSV color sweep."; }
        }

        public override Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public override bool IsDefault
        {
            get { return false; }
        }

        public override bool Settings()
        {
            return false;
        }

        public override bool Start()
        {
            this.running = true;
            this.driver = new Task(target);
            this.driver.Start();
            return true;
        }

        private void target()
        {
            int h = 0;
            while (this.IsRunning) {
                h += 1;
                h %= 360;
                HslColor col = new HslColor(h, 1, .5);
                NewColorAvailEvent(col.ToRgbColor(), EventArgs.Empty);
                if (this.stepSleep != 0)
                    Thread.Sleep(this.stepSleep);
            }
        }

        public override void AttachEvent(AntumbraColorObserver observer)
        {
            this.NewColorAvailEvent += new NewColorAvail(observer.NewColorAvail);
        }

        public override bool Stop()
        {
            this.running = false;
            if (this.driver != null) {
                this.driver.Wait(3000);
                if (this.driver.IsCompleted)
                    this.driver.Dispose();
                else
                    return false;
            }
            return true;
        }

        public override void RecmmndCoreSettings()
        {
            this.stepSleep = 1000;
        }

        public override string Website
        {
            get { throw new NotImplementedException(); }
        }
    }
}
