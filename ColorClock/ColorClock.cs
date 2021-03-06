﻿using Antumbra.Glow.ExtensionFramework;
using Antumbra.Glow.ExtensionFramework.Types;
using Antumbra.Glow.Observer.Colors;
using Antumbra.Glow.Utility;
using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ColorClock {

    [Export(typeof(GlowExtension))]
    public class ColorClock : GlowDriver {

        #region Private Fields

        private int deviceId;

        private Task driver;

        private long index;

        private bool running = false;

        #endregion Private Fields

        #region Public Delegates

        public delegate void NewColorAvail(Color16Bit newColor, int id, long index);

        #endregion Public Delegates

        #region Public Events

        public event NewColorAvail NewColorAvailEvent;

        #endregion Public Events

        #region Public Properties

        public override string Author {
            get { return "Team Antumbra"; }
        }

        public override string Description {
            get {
                return "A time based color clock with unique colors "
                     + "for every second of the day.";
            }
        }

        public override int devId {
            get {
                return deviceId;
            }
            set {
                deviceId = value;
            }
        }

        public override Guid id {
            get { return Guid.Parse("53f16938-2642-44a2-8d6e-954a1a9e2ac7"); }
        }

        public override bool IsDefault {
            get { return false; }
        }

        public override bool IsRunning {
            get { return this.running; }
        }

        public override string Name {
            get { return "Color Clock"; }
        }

        public override Version Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public override string Website {
            get { return "https://antumbra.io/docs/extensions/drivers/colorClock"; }
        }

        #endregion Public Properties

        #region Public Methods

        public override void AttachColorObserver(AntumbraColorObserver observer) {
            this.NewColorAvailEvent += new NewColorAvail(observer.NewColorAvail);
        }

        public override GlowDriver Create() {
            return new ColorClock();
        }

        public override void Dispose() {
            if(driver != null) {
                driver.Dispose();
            }
        }

        public override void RecmmndCoreSettings() {
            stepSleep = 1000;
        }

        public override bool Settings() {
            return false;//no custom settings
        }

        public override bool Start() {
            running = true;
            index = long.MinValue;
            driver = new Task(driverTarget);
            driver.Start();
            return true;
        }

        public override bool Stop() {
            this.running = false;
            if(null != driver) {
                if(driver.IsCompleted)
                    driver.Dispose();
                else {
                    driver.Wait(2000);
                    if(driver.IsCompleted)
                        driver.Dispose();
                }
            }
            return true;
        }

        #endregion Public Methods

        #region Private Methods

        private void driverTarget() {
            while(running) {
                if(NewColorAvailEvent == null) { }//no one is listening, do nothing...
                else
                    NewColorAvailEvent(getTimeColor(DateTime.Now), deviceId, index++);
                Thread.Sleep(stepSleep);
            }
        }

        private Color16Bit getTimeColor(DateTime time) {
            double secondsFromStart = (time - DateTime.Today).TotalSeconds;//0-86400
            double fraction = secondsFromStart / 86400.0;
            double[] hsv = new double[3];
            hsv[0] = (double)(secondsFromStart % 3600);//12 rotations over half a day
            hsv[1] = (double)(2.0 * Math.Abs(fraction - .5));//0 -> 1 -> 0 over a day
            if(fraction < .25)//first 4th
                hsv[2] = fraction / .25;//up to 1
            else if(fraction > .75)//last 4th
                hsv[2] = (1 - fraction) / .25;//down to 0
            else//middle 8ths
                hsv[2] = 1;
            int[] values = HSVRGGConverter.HSVToRGB(hsv[0], hsv[1], hsv[2]);
            return Color16BitUtil.FromRGBColor(System.Drawing.Color.FromArgb(values[0], values[1], values[2]));
        }

        #endregion Private Methods
    }
}
