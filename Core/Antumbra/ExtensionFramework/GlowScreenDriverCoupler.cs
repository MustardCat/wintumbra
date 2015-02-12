﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Antumbra.Glow;

namespace Antumbra.Glow.ExtensionFramework
{
    public class GlowScreenDriverCoupler : GlowDriver, AntumbraColorObserver
    //generates color using a GlowScreenGrabber
    //and a GlowScreenProcessor
    {
        public delegate void NewColorAvail(object sender, EventArgs args);
        public event NewColorAvail NewColorAvailEvent;
        private GlowScreenGrabber grabber;
        private GlowScreenProcessor processor;
        private List<IObserver<Color>> observers;
        private Dictionary<string, object> settings;
        public override int id { get; set; }

        public GlowScreenDriverCoupler(GlowScreenGrabber grab, GlowScreenProcessor proc)
        {
            this.grabber = grab;
            this.processor = proc;
            this.observers = new List<IObserver<Color>>();
            this.settings = new Dictionary<string, object>();
        }

        public override bool IsRunning
        {
            get { if (null != this.grabber && null != this.processor)
                    return this.grabber.IsRunning && this.processor.IsRunning;
                return false;
            }
        }
        public sealed override string Name
        { get {
            if (null != this.grabber && null != this.processor)
                return "Glow Screen Driver Coupler (" + this.grabber.Name + " & " + this.processor.Name + ")";
            else
                return "Glow Screen Driver Coupler";
            } }
        public sealed override string Author
        { get { return "Team Antumbra"; } }
        public sealed override string Description
        { get {
            return "A GlowDriver that uses a GlowScreenGrabber and "
                 + "a GlowScreenProcessor to generate colors";
            }
        }

        public sealed override bool IsDefault
        {
            get { return true; }
        }
        public sealed override Version Version
        { get { return new Version("0.0.1"); } }

        public sealed override string Website
        {
            get { return "https://antumbra.io/docs/extensions/framework/GlowScreenDriverCoupler"; }//TODO make docs and change this accordingly
        }

        public sealed override Dictionary<string, object> Settings
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

        public override void AttachEvent(AntumbraColorObserver observer)
        {
            this.NewColorAvailEvent += new NewColorAvail(observer.NewColorAvail);
        }

        void AntumbraColorObserver.NewColorAvail(object sender, EventArgs args)
        {
            NewColorAvailEvent(sender, args);//pass it up
        }

        public override bool Start()
        {
            if (this.grabber != null && this.processor != null) {
                if (this.processor.Start()) {
                    if (this.processor is AntumbraBitmapObserver)
                        this.grabber.AttachEvent((AntumbraBitmapObserver)this.processor);
                    this.processor.AttachEvent(this);
                    if (this.grabber.Start()) {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool Stop()
        {
            this.observers = new List<IObserver<Color>>();//reset observers
            if (this.processor != null)
                this.processor.Stop();
            if (this.grabber != null)
                this.grabber.Stop();
            return true;
        }
    }
}
