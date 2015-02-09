﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antumbra.Glow.ExtensionFramework;
using System.ComponentModel.Composition;
using System.Threading;
using System.Drawing;

namespace ManualColorSelector
{
    [Export(typeof(GlowExtension))]
    public class ManualColorSelector : GlowIndependentDriver
    {
        private Dictionary<string, object> settings;
        private bool running;
        private MainForm picker;
        private Thread driver;
        public delegate void NewColorAvail(object sender, EventArgs args);
        public event NewColorAvail NewColorAvailEvent;
        public override void AttachEvent(AntumbraColorObserver observer)
        {
            this.NewColorAvailEvent += new NewColorAvail(observer.NewColorAvail);
        }
        public override Dictionary<string, object> Settings
        {
            get
            {
                return this.settings;
            }
            set
            {
                this.settings = Settings;
            }
        }
        public override int id { get; set; }
        public override string Name
        {
            get { return "Manual Color Selector"; }
        }

        public override string Author
        {
            get { return "Team Antumbra"; }
        }

        public override string Description
        {
            get { return "A manual color selector for Glow"; }
        }

        public override bool IsDefault
        {
            get { return false; }
        }

        public override Version Version
        {
            get { return new Version("0.0.1"); }
        }

        public override string Website
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsRunning
        {
            get { return this.running; }
        }

        public override bool Start()
        {
            this.picker = new MainForm();
            this.picker.Show();
            this.driver = new Thread(new ThreadStart(target));
            this.running = true;
            return true;
        }

        private void target()
        {
            while (this.picker.WindowState == System.Windows.Forms.FormWindowState.Normal) {//while open
                NewColorAvailEvent(this.picker.sampleColor, EventArgs.Empty);//send current color
            }
        }

        public override bool Stop()
        {
            if (this.driver != null && this.driver.IsAlive)
                this.driver.Abort();
            if (this.picker != null)
                this.picker.Close();
            this.running = false;
            return true;
        }
    }
}