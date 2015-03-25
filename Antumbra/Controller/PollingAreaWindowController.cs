﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Antumbra.Glow.Utility;
using Antumbra.Glow.Observer.GlowCommands;
using Antumbra.Glow.Observer.GlowCommands.Commands;
using Antumbra.Glow.Observer.Colors;

namespace Antumbra.Glow.Controller
{
    public class PollingAreaWindowController : GlowCommandSender, AntumbraColorSource
    {
        public delegate void PollingAreaUpdated(int x, int y, int height, int width);
        public event PollingAreaUpdated PollingAreaUpdatedEvent;
        public delegate void NewGlowCommandAvail(GlowCommand cmd);
        public event NewGlowCommandAvail NewGlowCommandAvailEvent;
        public delegate void NewColorAvail(Color16Bit color);
        public event NewColorAvail NewColorAvailEvent;
        private View.pollingAreaSetter pollingWindow;
        private Color color;
        public int id { get; private set; }
        public PollingAreaWindowController()
        {
            this.color = UniqueColorGenerator.GetInstance().GetUniqueColor();
            this.pollingWindow = new View.pollingAreaSetter(this.color);
            this.pollingWindow.formClosingEvent += new EventHandler(UpdatePollingSelectionsEvent);
        }

        public void Show()
        {
            this.pollingWindow.Show();
            SendStopCommand();//stop device
            SendColor(new Color16Bit(this.color));//set to unique color to match its window
        }
        
        private void pollingArea_Click(object sender, EventArgs e)
        {
            if (this.pollingWindow == null || this.pollingWindow.IsDisposed) {
                var back = UniqueColorGenerator.GetInstance().GetUniqueColor();
                this.pollingWindow = new View.pollingAreaSetter(back);
                SendStopCommand();
                SendColor(new Color16Bit(back));
                this.pollingWindow.formClosingEvent += new EventHandler(UpdatePollingSelectionsEvent);
            }
            this.pollingWindow.Show();
        }

        public void AttachObserver(AntumbraColorObserver observer)
        {
            this.NewColorAvailEvent += observer.NewColorAvail;
        }

        private void SendColor(Color16Bit color)
        {
            if (NewColorAvailEvent != null)
                NewColorAvailEvent(color);
        }

        public void AttachObserver(GlowCommandObserver observer)
        {
            this.NewGlowCommandAvailEvent += observer.NewGlowCommandAvail;
        }

        public void RegisterDevice(int id)
        {
            this.id = id;
        }

        private void SendStopCommand()
        {
            if (NewGlowCommandAvailEvent != null)
                NewGlowCommandAvailEvent(new StopCommand(this.id));
        }

        private void UpdatePollingSelectionsEvent(object sender, EventArgs args)
        {
            System.Windows.Forms.Form form = (System.Windows.Forms.Form)sender;
            if (PollingAreaUpdatedEvent != null)
                PollingAreaUpdatedEvent(form.Bounds.X, form.Bounds.Y, form.Width, form.Height);
            UniqueColorGenerator.GetInstance().RetireUniqueColor(form.BackColor);
        }
    }
}
