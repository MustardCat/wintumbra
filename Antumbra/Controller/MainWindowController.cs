﻿using Antumbra.Glow.Connector;
using Antumbra.Glow.ExtensionFramework.Management;
using Antumbra.Glow.Observer.Colors;
using Antumbra.Glow.Observer.Configuration;
using Antumbra.Glow.Observer.Connection;
using Antumbra.Glow.Observer.GlowCommands;
using Antumbra.Glow.Observer.GlowCommands.Commands;
using Antumbra.Glow.Observer.Logging;
using Antumbra.Glow.Observer.ToolbarNotifications;
using Antumbra.Glow.Settings;
using Antumbra.Glow.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;

namespace Antumbra.Glow.Controller {

    public class MainWindowController : Loggable, ToolbarNotificationSource, GlowCommandSender, GlowCommandObserver,
                                        ToolbarNotificationObserver, ConfigurationObserver, ConnectionEventObserver,
                                        IDisposable, ConfigurationChangeAnnouncer {

        #region Public Fields

        public const Int32 PID = 0x0A85;
        public const Int32 VID = 0x16D0;

        #endregion Public Fields

        #region Private Fields

        private const int HT_CAPTION = 0x2;

        /// <summary>
        /// Move form dependencies
        /// </summary>
        private const int WM_NCLBUTTONDOWN = 0xA1;

        private readonly Dictionary<int, bool> powerValues = new Dictionary<int, bool> {
            {4, false },
            {7, true},
            {18, false}
        };

        private ConnectionManager connectionManager;

        private Color16Bit controlColor;

        private List<IDisposable> disposables;

        private ExtensionManager extensionManager;

        private int glowCount, id;

        private PollingAreaWindowController pollingWindowController;

        private PreOutputProcessor preOutputProcessor;

        private ManagementEventWatcher pwrWatcher;
        private SettingsManager settingsManager;

        private WhiteBalanceWindowController whiteBalController;

        private MainWindow window;

        #endregion Private Fields

        #region Public Constructors

        public MainWindowController(string productVersion, EventHandler quitHandler) {
            var q = new WqlEventQuery();
            var scope = new ManagementScope("root\\CIMV2");
            q.EventClassName = "Win32_PowerManagementEvent";
            pwrWatcher = new ManagementEventWatcher(scope, q);
            pwrWatcher.EventArrived += PowerEvent;
            pwrWatcher.Start();

            id = -1;//Add drop down for selecting a single or all devices for settings control
            disposables = new List<IDisposable>();

            controlColor.red = 0;
            controlColor.green = 0;
            controlColor.blue = 0;

            AttachObserver((LogMsgObserver)(LoggerHelper.GetInstance()));//attach logger
            // Create Manager instances
            try {
                extensionManager = new ExtensionManager();
            } catch(Exception) {
                throw;
            }
            connectionManager = new ConnectionManager(VID, PID);
            settingsManager = new SettingsManager();
            AttachObserver((ConfigurationChanger)settingsManager);

            preOutputProcessor = new PreOutputProcessor();
            whiteBalController = new WhiteBalanceWindowController(settingsManager, WhiteBalanceWindowClosingHandler);
            pollingWindowController = new PollingAreaWindowController();
            disposables.Add(pollingWindowController);
            // Attach event observers
            connectionManager.AttachObserver((ConnectionEventObserver)settingsManager);
            connectionManager.AttachObserver((ConnectionEventObserver)extensionManager);
            connectionManager.AttachObserver((ConnectionEventObserver)whiteBalController);
            connectionManager.AttachObserver((ConnectionEventObserver)pollingWindowController);
            connectionManager.AttachObserver((ConnectionEventObserver)this);
            preOutputProcessor.AttachObserver((AntumbraColorObserver)connectionManager);
            extensionManager.AttachObserver((AntumbraColorObserver)preOutputProcessor);
            settingsManager.AttachObserver((ConfigurationObserver)extensionManager);
            settingsManager.AttachObserver((ConfigurationObserver)preOutputProcessor);
            settingsManager.AttachObserver((ConfigurationObserver)this);
            whiteBalController.AttachObserver((ConfigurationChanger)settingsManager);
            pollingWindowController.AttachObserver((ConfigurationChanger)settingsManager);
            pollingWindowController.PollingAreaUpdatedEvent += new PollingAreaWindowController.PollingAreaUpdated(UpdatePollingSelection);
            // Find devices
            connectionManager.UpdateDeviceConnections();
            settingsManager.UpdateBoundingBox();

            AttachObserver((GlowCommandObserver)extensionManager);
            pollingWindowController.AttachObserver((GlowCommandObserver)this);

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            this.window = new MainWindow();
            this.window.speedBar_ValueChange += new EventHandler(changeSpeed);
            this.window.outputRateBtn_ClickEvent += new EventHandler(showOutputRate);
            this.window.closeBtn_ClickEvent += new EventHandler(closeBtnClicked);
            this.window.colorWheel_ColorChangedEvent += new EventHandler(colorWheelColorChanged);
            this.window.brightnessTrackBar_ScrollEvent += new EventHandler(brightnessValueChanged);
            this.window.hsvBtn_ClickEvent += new EventHandler(hsvBtnClicked);
            this.window.sinBtn_ClickEvent += new EventHandler(sinBtnClicked);
            this.window.neonBtn_ClickEvent += new EventHandler(neonBtnClicked);
            this.window.mirrorBtn_ClickEvent += new EventHandler(mirrorBtnClicked);
            this.window.augmentBtn_ClickEvent += new EventHandler(augmentBtnClicked);
            this.window.mainWindow_MouseDownEvent += new System.Windows.Forms.MouseEventHandler(mouseDownEvent);
            this.window.quitBtn_ClickEvent += new EventHandler(quitBtnClicked);
            this.quitEventHandler += quitHandler;
            this.window.setPollingBtn_ClickEvent += new EventHandler(setPollingBtnClickHandler);
            this.window.pwrBtn_ClickEvent += new EventHandler(OnOffValueChangedHandler);
            this.window.whiteBalanceBtn_ClickEvent += new EventHandler(whiteBalanceBtnClicked);
            this.window.throttleBar_ValueChange += new EventHandler(throttleBarValueChanged);
            this.window.resetBtn_ClickEvent += new EventHandler(ResetButtonClicked);

            SendStartCommand(-1);
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void NewConfigurationChange(SettingsDelta Delta);

        public delegate void NewGlowCmdAvail(GlowCommand cmd);

        public delegate void NewLogMsgAvail(String source, String msg);

        public delegate void NewToolbarNotif(int time, string title, string msg, int icon);

        #endregion Public Delegates

        #region Public Events

        public event NewConfigurationChange NewConfigurationChangeEvent;

        public event NewGlowCmdAvail NewGlowCmdAvailEvent;

        public event NewLogMsgAvail NewLogMsgAvailEvent;

        public event NewToolbarNotif NewToolbarNotifAvailEvent;

        #endregion Public Events

        #region Private Events

        private event EventHandler quitEventHandler;

        #endregion Private Events

        #region Public Methods

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public void AttachObserver(GlowCommandObserver observer) {
            this.NewGlowCmdAvailEvent += observer.NewGlowCommandAvail;
        }

        public void AttachObserver(ToolbarNotificationObserver observer) {
            this.NewToolbarNotifAvailEvent += observer.NewToolbarNotifAvail;
        }

        public void AttachObserver(LogMsgObserver observer) {
            this.NewLogMsgAvailEvent += observer.NewLogMsgAvail;
        }

        public void AttachObserver(ConfigurationChanger observer) {
            NewConfigurationChangeEvent += observer.ConfigChange;
        }

        public void augmentBtnClicked(object sender, EventArgs args) {
            extensionManager.SetInstance(id, ExtensionManager.MODE.AUGMENT);
            SendStartCommand(id);
        }

        public void brightnessValueChanged(object sender, EventArgs args) {
            if(sender is double) {
                double value = (double)sender;
                for(int i = 0; i < connectionManager.GlowsFound; i += 1) {
                    SettingsDelta Delta = new SettingsDelta(i);
                    Delta.changes[SettingValue.MAXBRIGHTNESS] = value;
                    AnnounceConfigChange(Delta);
                }
            }
        }

        public void closeBtnClicked(object sender, EventArgs args) {
            this.window.Hide();
        }

        public void colorWheelColorChanged(object sender, EventArgs args) {
            if(sender is Utility.HslColor) {
                preOutputProcessor.manualMode = true;
                Utility.HslColor col = (Utility.HslColor)sender;
                Color16Bit manualColor = Color16BitUtil.FromRGBColor(col.ToRgbColor());
                NewGlowCmdAvailEvent(new StopAndSendColorCommand(-1, manualColor));
            }
        }

        /// <summary>
        /// Update UI to match with new config announced
        /// </summary>
        /// <param name="config"></param>
        public void ConfigurationUpdate(Configurable config) {
            if(config is DeviceSettings && window != null) {//settings changed
                DeviceSettings settings = (DeviceSettings)config;
                window.SetCaptureThrottleValue(settings.captureThrottle);
                window.SetBrightnessValue(Convert.ToInt32(settings.maxBrightness * 100));
                window.SetSpeedValue(settings.stepSleep);
                ResendManualColor(settings.id);
            }
        }

        public void ConnectionUpdate(int devCount) {
            glowCount = devCount;
        }

        public void Dispose() {
            foreach(IDisposable disposable in disposables) {
                disposable.Dispose();
            }
            pwrWatcher.Stop();
            pwrWatcher.Dispose();
        }

        public void hsvBtnClicked(object sender, EventArgs args) {
            extensionManager.SetInstance(id, ExtensionManager.MODE.HSV);
            SendStartCommand(id);
        }

        public void mirrorBtnClicked(object sender, EventArgs args) {
            extensionManager.SetInstance(id, ExtensionManager.MODE.MIRROR);
            SendStartCommand(id);
        }

        public void mouseDownEvent(object sender, System.Windows.Forms.MouseEventArgs args) {
            //allows dragging of forms to move them (because of hidden menu bars and window frame)
            if(args.Button == System.Windows.Forms.MouseButtons.Left) {//drag with left mouse btn
                ReleaseCapture();
                SendMessage(this.window.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void neonBtnClicked(object sender, EventArgs args) {
            extensionManager.SetInstance(id, ExtensionManager.MODE.NEON);
            SendStartCommand(id);
        }

        public void NewGlowCommandAvail(GlowCommand cmd) {
            if(NewGlowCmdAvailEvent != null)
                NewGlowCmdAvailEvent(cmd);//pass it up
        }

        public void NewToolbarNotifAvail(int time, string title, string msg, int icon) {
            if(NewToolbarNotifAvailEvent != null)
                NewToolbarNotifAvailEvent(time, title, msg, icon);//pass it up
        }

        public void quitBtnClicked(object sender, EventArgs args) {
            foreach(IDisposable disposable in disposables) {
                disposable.Dispose();
            }
            this.window.Close();
            NewGlowCmdAvailEvent(new PowerOffCommand(-1));//turn all devices off
            connectionManager.Dispose();
            if(quitEventHandler != null)
                quitEventHandler(sender, args);
        }

        public void ResendManualColor(int id) {
            preOutputProcessor.manualMode = true;
            if(whiteBalController.IsOpen()) {
                NewGlowCmdAvailEvent(new SoftSendColorCommand(id, Color16BitUtil.FromRGBColor(new Utility.HslColor(0, 0, .5))));
            } else {
                NewGlowCmdAvailEvent(new SoftSendColorCommand(id, Color16BitUtil.FromRGBColor(window.colorWheel.HslColor.ToRgbColor())));
            }
        }

        public void restartEventHandler(object sender, EventArgs args) {
            this.NewGlowCmdAvailEvent(new StopCommand(-1));
            SendStartCommand(-1);
        }

        public void showOutputRate(object sender, EventArgs args) {
            ShowMessage(2500, "Output Rate(s)", extensionManager.getOutRatesMessage(), 0);
        }

        public void showWindowEventHandler(object sender, EventArgs args) {
            this.window.Activate();
            this.window.Show();
        }

        public void sinBtnClicked(object sender, EventArgs args) {
            extensionManager.SetInstance(id, ExtensionManager.MODE.SIN);
            SendStartCommand(id);
        }

        #endregion Public Methods

        #region Private Methods

        private void AnnounceConfigChange(SettingsDelta delta) {
            if(NewConfigurationChangeEvent != null) {
                NewConfigurationChangeEvent(delta);
            }
        }

        private void changeSpeed(object sender, EventArgs args) {
            if(sender is int) {
                int value = (int)sender;
                for(int i = 0; i < glowCount; i += 1) {
                    SettingsDelta Delta = new SettingsDelta(i);
                    Delta.changes[SettingValue.STEPSLEEP] = value;
                    AnnounceConfigChange(Delta);
                }
            }
        }

        private void Log(string msg) {
            if(NewLogMsgAvailEvent != null)
                NewLogMsgAvailEvent("MainWindowController", msg);
        }

        private void OnOffValueChangedHandler(object sender, EventArgs args) {
            if(sender is bool) {
                bool on = (bool)sender;
                if(on) {
                    SendStartCommand(-1);
                } else {
                    NewGlowCmdAvailEvent(new PowerOffCommand(-1));
                }
            }
        }

        private void PowerEvent(object sender, EventArrivedEventArgs args) {
            foreach(PropertyData data in args.NewEvent.Properties) {
                if(data != null && data.Value != null) {
                    int key = (int)data.Value;
                    bool val;
                    if(powerValues.TryGetValue(key, out val)) {
                        if(val) {
                            Thread.Sleep(2500);//wait for system to be ready
                            SendStartCommand(-1);
                        } else {
                            NewGlowCmdAvailEvent(new PowerOffCommand(-1));
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
        }

        private void ResetButtonClicked(object sender, EventArgs args) {
            this.pollingWindowController.Reset();
        }

        private void SendStartCommand(int id) {
            //Force settings to announce themselves
            for(int i = 0; i < glowCount; i += 1) {
                settingsManager.getSettings(i).Notify();
            }

            // Start
            try {
                NewGlowCmdAvailEvent(new StartCommand(id));
                preOutputProcessor.manualMode = false;
            } catch(Exception) {
                ResendManualColor(id);
            }
        }

        private void setPollingBtnClickHandler(object sender, EventArgs args) {
            preOutputProcessor.manualMode = true;
            for(int i = 0; i < glowCount; i += 1) {
                DeviceSettings settings = settingsManager.getSettings(i);
                pollingWindowController.SetBounds(settings.id, settings.x, settings.y, settings.width, settings.height);
            }
            pollingWindowController.ShowAll();
        }

        private void ShowMessage(int time, string title, string msg, int icon) {
            if(NewToolbarNotifAvailEvent != null)
                NewToolbarNotifAvailEvent(time, title, msg, icon);
        }

        /// <summary>
        /// Event handler for session switching. Used for handling locking and unlocking of the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e) {
            switch(e.Reason) {
                case SessionSwitchReason.SessionLogoff:
                case SessionSwitchReason.SessionLock:
                    NewGlowCmdAvailEvent(new PowerOffCommand(-1));//turn all off
                    break;

                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                    Thread.Sleep(2500);//wait for system to be ready
                    SendStartCommand(-1);//start all
                    break;
            }
        }

        private void throttleBarValueChanged(object sender, EventArgs args) {
            if(sender is int) {
                NewGlowCmdAvailEvent(new StopCommand(-1));
                int value = (int)sender;
                for(var i = 0; i < glowCount; i += 1) {
                    SettingsDelta Delta = new SettingsDelta(i);
                    Delta.changes[SettingValue.CAPTURETHROTTLE] = value;
                    AnnounceConfigChange(Delta);
                }
            }
        }

        private void UpdatePollingSelection(Dictionary<int, SettingsDelta> PollingAreaChanges) {
            foreach(KeyValuePair<int, SettingsDelta> KeyValue in PollingAreaChanges) {
                AnnounceConfigChange(KeyValue.Value);
            }
            settingsManager.UpdateBoundingBox();
        }

        private void whiteBalanceBtnClicked(object sender, EventArgs args) {
            if(glowCount == 0) {
                this.ShowMessage(3000, "No Glows Found",
                    "White balance cannot be opened because no Glow devices were found.", 2);
                return;//can't open
            }
            preOutputProcessor.manualMode = true;
            NewGlowCmdAvailEvent(new StopAndSendColorCommand(-1, this.controlColor));
            window.colorWheel.HslColor = new Utility.HslColor(0, 0, .5);//reset selector to center
            window.colorWheel.Enabled = false;//disable main color wheel until color balance window closed
            whiteBalController.Show();
        }

        private void WhiteBalanceWindowClosingHandler(object sender, System.Windows.Forms.FormClosingEventArgs args) {
            this.window.colorWheel.Enabled = true;//re-enable main wheel
        }

        #endregion Private Methods
    }
}
