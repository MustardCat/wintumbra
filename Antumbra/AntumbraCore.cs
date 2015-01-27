﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.IO;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Antumbra.Glow.Connector;
using Antumbra.Glow.ExtensionFramework;
using Antumbra.Glow.Utility;
using Antumbra.Glow.Windows;
using System.Reflection;

namespace Antumbra.Glow
{
    public partial class AntumbraCore : MetroFramework.Forms.MetroForm, AntumbraColorObserver
        //Main driver for application
    {
        //private System.Timers.Timer screenTimer;//timer for screen color averaging
        //private Thread fadeThread;//thread for color fades
        private Color color;//newest generated color for displaying
        private Color currentColor;//most recent successfully sent set command color
        private ColorPickerDialog picker;
        //private Thread driverThread;
        //private bool fadeEnabled;
        //private bool screenAvgEnabled;
        public bool gameMode { get; set; }
        private byte lastR, lastG, lastB;
        private int changeThreshold; //difference in colors needed to change
        //bool on;
        private SerialConnector serial;//serial connector
        private SettingsWindow settings;//settings window
        //public AntumbraScreenGrabber screenGrabber { get; set; }
        //public AntumbraDirectXScreenGrabber gameScreenGrabber { get; set; }
        //public AntumbraScreenProcessor screenProcessor { get; set; }
        public int pollingWidth { get; set; }
        public int pollingHeight { get; set; }
        public int pollingX { get; set; }
        public int pollingY { get; set; }
        public int stepSleep { get; set; }
        public int stepSize { get; set; }

        public MEFHelper MEFHelper;

        private GlowDriver GlowDriver;
        private GlowScreenGrabber ScreenGrabber;
        private GlowScreenProcessor ScreenProcessor;
        private List<GlowDecorator> GlowDecorators;
        private List<GlowNotifier> GlowNotifiers;

        private DateTime last;

        //private Thread MainDriverThread;//main driver thread for whole application
        //private Thread DriverThread;//driver thread for driver extensions

        public AntumbraCore()
        {
            this.serial = new SerialConnector(0x03EB, 0x2040);
            Console.WriteLine(this.serial.setup());//sanity check that Glow connects correctly
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.lastR = 0;
            this.lastG = 0;
            this.lastB = 0;
            this.currentColor = Color.Black;//depends on how the Glow starts up
            this.color = Color.Black;
            this.changeThreshold = 10; //see shouldChange(Color, Color) (lower is more sensitive)
            //this.continuous = false;
            //this.fadeEnabled = false;
            //this.gameMode = false;
            //this.fadeThread = new Thread(new ThreadStart(callColorFade));
            //this.screenTimer = new System.Timers.Timer();
            //this.pollingWidth = this.screen.width;
            //this.pollingHeight = this.screen.height;
            this.pollingWidth = Screen.PrimaryScreen.Bounds.Width;
            this.pollingHeight = Screen.PrimaryScreen.Bounds.Height;
            this.pollingX = 0;
            this.pollingY = 0;
            this.stepSleep = 15;
            this.stepSize = 1;
            updateStatus(this.serial.state);
            //this.picker = new ColorPickerDialog(); //TODO investigate crash with color picker
            //this.screenGrabber = new AntumbraScreenGrabber(this);
            //this.gameScreenGrabber = new AntumbraDirectXScreenGrabber(this, this.pollingX, this.pollingY,
            //    this.pollingWidth, this.pollingHeight, 0);//TODO make timeOut a setting
            //this.screenProcessor = new AntumbraScreenProcessor(.45, true, 20, 20);
            //this.driverThread = new Thread(new ThreadStart(setToAvg));
            this.settings = new SettingsWindow(this);
            //this.DriverThread = new Thread(new ThreadStart(run));
            this.MEFHelper = new MEFHelper("./Extensions/");//"/Extensions/");
            if (this.MEFHelper.didFail()) {
                Console.WriteLine("loading extensions failed. See output above.");
            }
            else {
                this.GlowDriver = this.MEFHelper.GetDefaultDriver();
            }
        }

        public void setDriver(GlowDriver driver)
        {
            this.Stop();
            this.GlowDriver = driver;
        }

        public void setScreenGrabber(GlowScreenGrabber screenGrabber)
        {
            this.Stop();
            this.ScreenGrabber = screenGrabber;
        }

        public void setScreenProcessor(GlowScreenProcessor processor)
        {
            this.Stop();
            this.ScreenProcessor = processor;
        }

        public void setDecorators(List<GlowDecorator> decorators)
        {
            this.GlowDecorators = decorators;
        }

        public void setNotifiers(List<GlowNotifier> notifiers)
        {
            this.GlowNotifiers = notifiers;
        }

        void AntumbraColorObserver.NewColorAvail(object sender, EventArgs args)
        {
            //Console.WriteLine();
            double time = DateTime.Now.Subtract(this.last).TotalSeconds;
            string newText = (1.0/time).ToString() + " hz";
            this.Invoke((MethodInvoker)delegate
            {
                this.settings.speed.Text = newText;
            });
            this.last = DateTime.Now;
            SetColorTo((Color)sender);
        }
/*
        public void run()
        {
            while (true) {
                //do stuff
            }
        }*/

        public void SetColorTo(Color newColor)
        {
            //Console.WriteLine(newColor.ToString());
            //fade(newColor, 0, 2);
            byte r = newColor.R;
            byte g = newColor.G;
            byte b = newColor.B;
            changeTo(r, g, b);
        }

        public int getPollingWidth()//TODO offload to drivers settings
        {
            return this.pollingWidth;
        }

        public int getPollingHeight()
        {
            return this.pollingHeight;
        }

        /*private void setToAvg()
        {
            while (true) {
                if (this.screenGrabber.screen == null)//skip
                    continue;
                Color newColor = this.screenProcessor.Process(this.screenGrabber.screen);
                if (newColor.Equals(Color.Empty))//something went wrong
                    continue;
                //fade(newColor, this.screenAvgStepSleep, this.screenAvgStepSize);//fade
                Console.WriteLine(newColor.ToString());
            }
        }

        private void setToGameAvg()
        {
            while (true) {
                Color newColor = this.screenProcessor.Process(this.gameScreenGrabber.screen);
                if (newColor.Equals(Color.Empty))//something went wrong
                    return;
                fade(newColor, this.screenAvgStepSleep, this.screenAvgStepSize);//fade
            }
        }
        */
        private int calcDiff(Color color, Color other)
        {
            int r1 = color.R;
            int g1 = color.G;
            int b1 = color.B;
            int r2 = other.R;
            int g2 = other.G;
            int b2 = other.B;
            int total = 0;//represents the total difference
            total += Math.Abs(r1 - r2);
            total += Math.Abs(g1 - g2);
            total += Math.Abs(b1 - b2);
            return total;
        }
        
        private bool shouldChange(Color color, Color other)
        {
            return calcDiff(color, other) > this.changeThreshold;
        }

        /*
        private void callColorFade()
        {
            while (true)
                colorFade(this.colorFadeStepSleep);
        }

        private void colorFade(int sleep)
        {
            Random rnd = new Random();
            while(true)
                fade(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)), sleep, this.colorFadeStepSize);
        }

        private void callSinFade()
        {
            while (true)
                sinFade(this.sinFadeStepSleep, this.sinFadeStepSize);
        }

        private void sinFade(int sleepTime, double stepSize)
        {
            for (double i = 0; i < Math.PI*2; i += stepSize)
            {
                if (i <= Math.PI) {//in positive half
                    byte byte_i = (byte)(Math.Sin(i) * 255);
                    changeTo(byte_i, byte_i, byte_i);
                }
                Thread.Sleep(sleepTime);
            }
        }

        private void callHsvFade()
        {
            while (true)
                hsvFade(this.HSVstepSleep);
        }

        private void hsvFade(int stepSleep)
        {
            double s = 100;
            double v = 100;
            for (double h = 0; h <= 360; h++)
            {
                int[] rgb = HSVRGGConverter.HSVToRGB(h, s, v);
                fade(Color.FromArgb(rgb[0], rgb[1], rgb[2]), stepSleep, this.HSVstepSize);
            }
        }

        private Color decorate(Color newColor)//'decorate' color to conform to brightness and warmth settings
        {
            int minModifier = (int)(2.55 * this.minBrightness);//min directly adds percentage of color
            double maxBrightnessModifier = this.maxBrightness/100.0;//max reduces color proportional to its value
            int r = (int)(newColor.R * maxBrightnessModifier);
            int g = (int)(newColor.G * maxBrightnessModifier);
            int b = (int)(newColor.B * maxBrightnessModifier);
            r += minModifier;
            g += minModifier;
            b += minModifier;
            if (r > 255)
                r = 255;
            if (g > 255)
                g = 255;
            if (b > 255)
                b = 255;
            return Color.FromArgb(r, g, b);
        }

        private Color addWarmth(Color newColor)//'decorate' color to conform to warmth setting
        {
            int rWarm = 255;
            int gWarm = 190;
            int bWarm = 0;
            int r = newColor.R;
            int g = newColor.G;
            int b = newColor.B;
            r = ((100 - warmth) * r + warmth * rWarm) / (100);
            g = ((100 - warmth) * g + warmth * gWarm) / (100);
            b = ((100 - warmth) * b + warmth * bWarm) / (100);
            return Color.FromArgb(r, g, b);
        }*/

        private void fade(Color newColor, int sleepTime, int stepDivider)
        {
            if (!shouldChange(Color.FromArgb(this.lastR, this.lastG, this.lastB), newColor))
                return;//no update needed*/
            float r = this.lastR;
            float g = this.lastG;
            float b = this.lastB;
            int diff = calcDiff(Color.FromArgb((int)r,(int)g,(int)b), newColor);
            int steps = diff / 3 / stepDivider;
            if (steps <= 0)
                steps = 1;
            int stepSize = diff / steps;
            float rStep = (newColor.R - r) / steps;
            float gStep = (newColor.G - g) / steps;
            float bStep = (newColor.B - b) / steps;
            for (int i = 0; i < steps; i++) {
                r += rStep;
                g += gStep;
                b += bStep;
                changeTo((byte)r, (byte)g, (byte)b);
                if(sleepTime != 0)
                    Thread.Sleep(sleepTime);
            }
        }

        private void changeTo(byte r, byte g, byte b)
        {
            //Console.WriteLine(System.DateTime.Now.ToString());
            Console.WriteLine(r + " " + g + " " + b);
            if (this.serial.send(r, g, b))//sucessful send
                updateLast(r, g, b);
            else {
                this.updateStatus(0);//send failed, device is probably dead
                Console.WriteLine("color send failed!");
            }
            //Console.WriteLine(System.DateTime.Now.ToString() + "        after");
        }

        private void updateStatus(int status)//0 - dead, 1 - idle, 2 - alive
        {
            switch(status) {
                case 0:
                    //dead
                    break;
                case 1:
                    //idle
                    break;
                case 2:
                    //good
                    break;
                default:
                    Console.WriteLine("This should not happen. updateStatus");
                    break;
            }
        }

        private void updateLast(byte r, byte g, byte b)
        {
            //this.antumbraLabel.ForeColor = Color.FromArgb(r, g, b);
            this.lastR = r;
            this.lastG = g;
            this.lastB = b;
        }

        public void updatePollingBounds(int x, int y)
        {
            if (x <= 0 || y <= 0)
                return;//invalid
            this.pollingWidth = x;
            this.pollingHeight = y;
        }

        public void updatePollingBoundsToFull()//assumes primary monitor
        {
            updatePollingBounds(Screen.PrimaryScreen.Bounds.Width, 
                                Screen.PrimaryScreen.Bounds.Height);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenu.Show(Cursor.Position);
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            this.settings.updateValues();
            this.settings.Visible = true;
        }

        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
            this.contextMenu.Visible = false;
            if (this.settings.Visible)
                this.settings.Close();
            this.settings.Dispose();
            Application.Exit();
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Stop();
            this.SetColorTo(Color.Black);
        }

        private void manualListener(object sender, EventArgs e)
        {
            this.SetColorTo(this.picker.previewPanel.BackColor);
        }

        private void contextMenu_MouseLeave(object sender, EventArgs e)
        {
            contextMenu.Close();
        }

        private bool verifyExtensionChoices()
        {
            this.notifyIcon.ShowBalloonTip(3000, "Verifying Extensions", "Verifying the chosen extensions.", ToolTipIcon.Info);
            if (null == this.GlowDriver) {//sanity check
                this.notifyIcon.ShowBalloonTip(3000, "Info", "Glow Driver not selected.", ToolTipIcon.Info);
                return false;
            }
            if (this.GlowDriver is GlowScreenDriverCoupler) {//screen based driver selected
                if (null == this.ScreenGrabber || null == this.ScreenProcessor)
                    return false;
                this.notifyIcon.ShowBalloonTip(3000, "Screen Based Driver Found", "A screen based driver was found that uses "
                    + this.ScreenGrabber.Name + " & " + this.ScreenProcessor.Name + ".", ToolTipIcon.Info);
                this.GlowDriver = new GlowScreenDriverCoupler(this, this.ScreenGrabber, this.ScreenProcessor);
            }
            return true;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verifyExtensionChoices()) {
                this.last = DateTime.Now;
                if (this.GlowDriver.Start()) {
                    this.notifyIcon.ShowBalloonTip(3000, "Driver Started", this.GlowDriver.Name + " was started successfully.", ToolTipIcon.Info);
                    this.GlowDriver.AttachEvent(this);
                }
                else {
                    this.notifyIcon.ShowBalloonTip(3000, "Driver Issue", this.GlowDriver.Name + " reported that it did not start successfully.",
                        ToolTipIcon.Error);
                }
                //TODO start other extensions as well
            }
        }

        private void Stop()
        {
            if (this.GlowDriver.IsRunning)
                this.GlowDriver.Stop();
            if (this.GlowDriver is GlowScreenDriverCoupler) //maintain status while reseting
                this.GlowDriver = new GlowScreenDriverCoupler(null, null, null);
            //TODO stop everything
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Stop();
        }
    }
}
