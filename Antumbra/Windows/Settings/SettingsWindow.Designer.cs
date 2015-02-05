﻿namespace Antumbra.Glow.Windows
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.stepSizeLabel = new MetroFramework.Controls.MetroLabel();
            this.stepSize = new MetroFramework.Controls.MetroTextBox();
            this.stepSleepLabel = new MetroFramework.Controls.MetroLabel();
            this.sleepSize = new MetroFramework.Controls.MetroTextBox();
            this.settingsStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.DriverLabel = new MetroFramework.Controls.MetroLabel();
            this.driverExtensions = new MetroFramework.Controls.MetroComboBox();
            this.screenGrabbers = new MetroFramework.Controls.MetroComboBox();
            this.screenGrabberLabel = new MetroFramework.Controls.MetroLabel();
            this.screenProcessors = new MetroFramework.Controls.MetroComboBox();
            this.screenProcessorLabel = new MetroFramework.Controls.MetroLabel();
            this.notifiersLabel = new MetroFramework.Controls.MetroLabel();
            this.decoratorsLabel = new MetroFramework.Controls.MetroLabel();
            this.apply = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.speedLabel = new MetroFramework.Controls.MetroLabel();
            this.speed = new System.Windows.Forms.Label();
            this.notifiers = new MetroFramework.Controls.MetroComboBox();
            this.decoratorToggle = new MetroFramework.Controls.MetroButton();
            this.notifierToggle = new MetroFramework.Controls.MetroButton();
            this.pollingArea = new MetroFramework.Controls.MetroButton();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.pollingXLabel = new MetroFramework.Controls.MetroLabel();
            this.pollingX = new MetroFramework.Controls.MetroLabel();
            this.pollingYLabel = new MetroFramework.Controls.MetroLabel();
            this.pollingY = new MetroFramework.Controls.MetroLabel();
            this.pollingWidthLabel = new MetroFramework.Controls.MetroLabel();
            this.pollingHeightLabel = new MetroFramework.Controls.MetroLabel();
            this.pollingHeight = new MetroFramework.Controls.MetroLabel();
            this.decorators = new MetroFramework.Controls.MetroComboBox();
            this.pollingWidth = new MetroFramework.Controls.MetroLabel();
            this.shouldChangeLabel = new MetroFramework.Controls.MetroLabel();
            this.changeSensitivity = new MetroFramework.Controls.MetroTextBox();
            this.statusLabel = new MetroFramework.Controls.MetroLabel();
            this.glowStatus = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.offBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.maxStepsLabel = new MetroFramework.Controls.MetroLabel();
            this.maxFadeSteps = new MetroFramework.Controls.MetroTextBox();
            this.fadeEnabledCheck = new MetroFramework.Controls.MetroCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.settingsStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // stepSizeLabel
            // 
            this.stepSizeLabel.AutoSize = true;
            this.stepSizeLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.stepSizeLabel.Location = new System.Drawing.Point(79, 428);
            this.stepSizeLabel.Name = "stepSizeLabel";
            this.stepSizeLabel.Size = new System.Drawing.Size(65, 19);
            this.stepSizeLabel.TabIndex = 7;
            this.stepSizeLabel.Text = "Step Size:";
            // 
            // stepSize
            // 
            this.stepSize.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.stepSize.Lines = new string[0];
            this.stepSize.Location = new System.Drawing.Point(204, 423);
            this.stepSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stepSize.MaxLength = 32767;
            this.stepSize.Name = "stepSize";
            this.stepSize.PasswordChar = '\0';
            this.stepSize.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.stepSize.SelectedText = "";
            this.stepSize.Size = new System.Drawing.Size(102, 31);
            this.stepSize.TabIndex = 13;
            this.stepSize.UseSelectable = true;
            this.stepSize.TextChanged += new System.EventHandler(this.stepSize_TextChanged);
            // 
            // stepSleepLabel
            // 
            this.stepSleepLabel.AutoSize = true;
            this.stepSleepLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.stepSleepLabel.Location = new System.Drawing.Point(55, 479);
            this.stepSleepLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stepSleepLabel.Name = "stepSleepLabel";
            this.stepSleepLabel.Size = new System.Drawing.Size(103, 19);
            this.stepSleepLabel.TabIndex = 17;
            this.stepSleepLabel.Text = "Step Sleep: (ms)";
            // 
            // sleepSize
            // 
            this.sleepSize.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.sleepSize.Lines = new string[0];
            this.sleepSize.Location = new System.Drawing.Point(204, 473);
            this.sleepSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sleepSize.MaxLength = 32767;
            this.sleepSize.Name = "sleepSize";
            this.sleepSize.PasswordChar = '\0';
            this.sleepSize.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.sleepSize.SelectedText = "";
            this.sleepSize.Size = new System.Drawing.Size(102, 31);
            this.sleepSize.TabIndex = 22;
            this.sleepSize.UseSelectable = true;
            this.sleepSize.TextChanged += new System.EventHandler(this.sleepSize_TextChanged);
            // 
            // settingsStyleManager
            // 
            this.settingsStyleManager.Owner = this;
            this.settingsStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // DriverLabel
            // 
            this.DriverLabel.AutoSize = true;
            this.DriverLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.DriverLabel.Location = new System.Drawing.Point(51, 228);
            this.DriverLabel.Name = "DriverLabel";
            this.DriverLabel.Size = new System.Drawing.Size(105, 19);
            this.DriverLabel.TabIndex = 32;
            this.DriverLabel.Text = "Driver Extension:";
            // 
            // driverExtensions
            // 
            this.driverExtensions.FormattingEnabled = true;
            this.driverExtensions.ItemHeight = 23;
            this.driverExtensions.Location = new System.Drawing.Point(186, 225);
            this.driverExtensions.Name = "driverExtensions";
            this.driverExtensions.Size = new System.Drawing.Size(348, 29);
            this.driverExtensions.TabIndex = 33;
            this.driverExtensions.UseSelectable = true;
            // 
            // screenGrabbers
            // 
            this.screenGrabbers.FormattingEnabled = true;
            this.screenGrabbers.ItemHeight = 23;
            this.screenGrabbers.Location = new System.Drawing.Point(186, 259);
            this.screenGrabbers.Name = "screenGrabbers";
            this.screenGrabbers.Size = new System.Drawing.Size(348, 29);
            this.screenGrabbers.TabIndex = 35;
            this.screenGrabbers.UseSelectable = true;
            // 
            // screenGrabberLabel
            // 
            this.screenGrabberLabel.AutoSize = true;
            this.screenGrabberLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.screenGrabberLabel.Location = new System.Drawing.Point(51, 262);
            this.screenGrabberLabel.Name = "screenGrabberLabel";
            this.screenGrabberLabel.Size = new System.Drawing.Size(104, 19);
            this.screenGrabberLabel.TabIndex = 34;
            this.screenGrabberLabel.Text = "Screen Grabber:";
            // 
            // screenProcessors
            // 
            this.screenProcessors.FormattingEnabled = true;
            this.screenProcessors.ItemHeight = 23;
            this.screenProcessors.Location = new System.Drawing.Point(186, 293);
            this.screenProcessors.Name = "screenProcessors";
            this.screenProcessors.Size = new System.Drawing.Size(348, 29);
            this.screenProcessors.TabIndex = 37;
            this.screenProcessors.UseSelectable = true;
            // 
            // screenProcessorLabel
            // 
            this.screenProcessorLabel.AutoSize = true;
            this.screenProcessorLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.screenProcessorLabel.Location = new System.Drawing.Point(43, 296);
            this.screenProcessorLabel.Name = "screenProcessorLabel";
            this.screenProcessorLabel.Size = new System.Drawing.Size(112, 19);
            this.screenProcessorLabel.TabIndex = 36;
            this.screenProcessorLabel.Text = "Screen Processor:";
            // 
            // notifiersLabel
            // 
            this.notifiersLabel.AutoSize = true;
            this.notifiersLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.notifiersLabel.Location = new System.Drawing.Point(96, 363);
            this.notifiersLabel.Name = "notifiersLabel";
            this.notifiersLabel.Size = new System.Drawing.Size(61, 19);
            this.notifiersLabel.TabIndex = 40;
            this.notifiersLabel.Text = "Notifiers:";
            // 
            // decoratorsLabel
            // 
            this.decoratorsLabel.AutoSize = true;
            this.decoratorsLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.decoratorsLabel.Location = new System.Drawing.Point(80, 329);
            this.decoratorsLabel.Name = "decoratorsLabel";
            this.decoratorsLabel.Size = new System.Drawing.Size(76, 19);
            this.decoratorsLabel.TabIndex = 38;
            this.decoratorsLabel.Text = "Decorators:";
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(375, 399);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(159, 23);
            this.apply.TabIndex = 43;
            this.apply.Text = "Apply Extension Changes";
            this.apply.UseSelectable = true;
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.metroLabel1.Location = new System.Drawing.Point(96, 363);
            this.metroLabel1.Name = "notifiersLabel";
            this.metroLabel1.Size = new System.Drawing.Size(61, 19);
            this.metroLabel1.TabIndex = 40;
            this.metroLabel1.Text = "Notifiers:";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.speedLabel.Location = new System.Drawing.Point(281, 114);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(93, 19);
            this.speedLabel.TabIndex = 44;
            this.speedLabel.Text = "Polling Speed:";
            // 
            // speed
            // 
            this.speed.AutoSize = true;
            this.speed.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.speed.Location = new System.Drawing.Point(380, 117);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(13, 13);
            this.speed.TabIndex = 45;
            this.speed.Text = "0";
            // 
            // notifiers
            // 
            this.notifiers.FormattingEnabled = true;
            this.notifiers.ItemHeight = 23;
            this.notifiers.Location = new System.Drawing.Point(186, 363);
            this.notifiers.Name = "notifiers";
            this.notifiers.Size = new System.Drawing.Size(236, 29);
            this.notifiers.TabIndex = 47;
            this.notifiers.UseSelectable = true;
            // 
            // decoratorToggle
            // 
            this.decoratorToggle.Location = new System.Drawing.Point(428, 328);
            this.decoratorToggle.Name = "decoratorToggle";
            this.decoratorToggle.Size = new System.Drawing.Size(106, 29);
            this.decoratorToggle.TabIndex = 48;
            this.decoratorToggle.Text = "Toggle Selected";
            this.decoratorToggle.UseSelectable = true;
            this.decoratorToggle.UseVisualStyleBackColor = true;
            this.decoratorToggle.Click += new System.EventHandler(this.decoratorToggle_Click);
            // 
            // notifierToggle
            // 
            this.notifierToggle.Location = new System.Drawing.Point(428, 363);
            this.notifierToggle.Name = "notifierToggle";
            this.notifierToggle.Size = new System.Drawing.Size(106, 29);
            this.notifierToggle.TabIndex = 49;
            this.notifierToggle.Text = "Toggle Selected";
            this.notifierToggle.UseSelectable = true;
            this.notifierToggle.UseVisualStyleBackColor = true;
            this.notifierToggle.Click += new System.EventHandler(this.notifierToggle_Click);
            // 
            // pollingArea
            // 
            this.pollingArea.Location = new System.Drawing.Point(280, 149);
            this.pollingArea.Name = "pollingArea";
            this.pollingArea.Size = new System.Drawing.Size(138, 35);
            this.pollingArea.TabIndex = 51;
            this.pollingArea.Text = "Set Screen Grabber Area";
            this.pollingArea.UseSelectable = true;
            this.pollingArea.UseVisualStyleBackColor = true;
            this.pollingArea.Click += new System.EventHandler(this.pollingArea_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(101, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 19);
            this.label1.TabIndex = 52;
            this.label1.Text = "Polling Location:";
            // 
            // pollingXLabel
            // 
            this.pollingXLabel.AutoSize = true;
            this.pollingXLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingXLabel.Location = new System.Drawing.Point(33, 114);
            this.pollingXLabel.Name = "pollingXLabel";
            this.pollingXLabel.Size = new System.Drawing.Size(20, 19);
            this.pollingXLabel.TabIndex = 53;
            this.pollingXLabel.Text = "X:";
            // 
            // pollingX
            // 
            this.pollingX.AutoSize = true;
            this.pollingX.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingX.Location = new System.Drawing.Point(56, 114);
            this.pollingX.Name = "pollingX";
            this.pollingX.Size = new System.Drawing.Size(0, 0);
            this.pollingX.TabIndex = 54;
            // 
            // pollingYLabel
            // 
            this.pollingYLabel.AutoSize = true;
            this.pollingYLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingYLabel.Location = new System.Drawing.Point(33, 146);
            this.pollingYLabel.Name = "pollingYLabel";
            this.pollingYLabel.Size = new System.Drawing.Size(20, 19);
            this.pollingYLabel.TabIndex = 55;
            this.pollingYLabel.Text = "Y:";
            // 
            // pollingY
            // 
            this.pollingY.AutoSize = true;
            this.pollingY.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingY.Location = new System.Drawing.Point(56, 146);
            this.pollingY.Name = "pollingY";
            this.pollingY.Size = new System.Drawing.Size(0, 0);
            this.pollingY.TabIndex = 56;
            // 
            // pollingWidthLabel
            // 
            this.pollingWidthLabel.AutoSize = true;
            this.pollingWidthLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingWidthLabel.Location = new System.Drawing.Point(122, 114);
            this.pollingWidthLabel.Name = "pollingWidthLabel";
            this.pollingWidthLabel.Size = new System.Drawing.Size(47, 19);
            this.pollingWidthLabel.TabIndex = 57;
            this.pollingWidthLabel.Text = "Width:";
            // 
            // pollingHeightLabel
            // 
            this.pollingHeightLabel.AutoSize = true;
            this.pollingHeightLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingHeightLabel.Location = new System.Drawing.Point(122, 146);
            this.pollingHeightLabel.Name = "pollingHeightLabel";
            this.pollingHeightLabel.Size = new System.Drawing.Size(50, 19);
            this.pollingHeightLabel.TabIndex = 58;
            this.pollingHeightLabel.Text = "Height:";
            // 
            // pollingHeight
            // 
            this.pollingHeight.AutoSize = true;
            this.pollingHeight.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingHeight.Location = new System.Drawing.Point(169, 146);
            this.pollingHeight.Name = "pollingHeight";
            this.pollingHeight.Size = new System.Drawing.Size(0, 0);
            this.pollingHeight.TabIndex = 60;
            // 
            // decorators
            // 
            this.decorators.FormattingEnabled = true;
            this.decorators.ItemHeight = 23;
            this.decorators.Location = new System.Drawing.Point(186, 328);
            this.decorators.Name = "decorators";
            this.decorators.Size = new System.Drawing.Size(236, 29);
            this.decorators.TabIndex = 46;
            this.decorators.UseSelectable = true;
            // 
            // pollingWidth
            // 
            this.pollingWidth.AutoSize = true;
            this.pollingWidth.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pollingWidth.Location = new System.Drawing.Point(166, 114);
            this.pollingWidth.Name = "pollingWidth";
            this.pollingWidth.Size = new System.Drawing.Size(0, 0);
            this.pollingWidth.TabIndex = 61;
            // 
            // shouldChangeLabel
            // 
            this.shouldChangeLabel.AutoSize = true;
            this.shouldChangeLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.shouldChangeLabel.Location = new System.Drawing.Point(22, 528);
            this.shouldChangeLabel.Name = "shouldChangeLabel";
            this.shouldChangeLabel.Size = new System.Drawing.Size(153, 19);
            this.shouldChangeLabel.TabIndex = 62;
            this.shouldChangeLabel.Text = "Color Change Sensitivity:";
            // 
            // changeSensitivity
            // 
            this.changeSensitivity.Lines = new string[0];
            this.changeSensitivity.Location = new System.Drawing.Point(204, 521);
            this.changeSensitivity.MaxLength = 32767;
            this.changeSensitivity.Name = "changeSensitivity";
            this.changeSensitivity.PasswordChar = '\0';
            this.changeSensitivity.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.changeSensitivity.SelectedText = "";
            this.changeSensitivity.Size = new System.Drawing.Size(102, 31);
            this.changeSensitivity.TabIndex = 63;
            this.changeSensitivity.UseSelectable = true;
            this.changeSensitivity.TextChanged += new System.EventHandler(this.changeSensitivity_TextChanged);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.statusLabel.Location = new System.Drawing.Point(336, 493);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(79, 19);
            this.statusLabel.TabIndex = 64;
            this.statusLabel.Text = "Glow Status:";
            // 
            // glowStatus
            // 
            this.glowStatus.AutoSize = true;
            this.glowStatus.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.glowStatus.Location = new System.Drawing.Point(421, 496);
            this.glowStatus.Name = "glowStatus";
            this.glowStatus.Size = new System.Drawing.Size(0, 13);
            this.glowStatus.TabIndex = 65;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(166, 615);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(76, 29);
            this.startBtn.TabIndex = 66;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // offBtn
            // 
            this.offBtn.Location = new System.Drawing.Point(330, 615);
            this.offBtn.Name = "offBtn";
            this.offBtn.Size = new System.Drawing.Size(76, 29);
            this.offBtn.TabIndex = 67;
            this.offBtn.Text = "Off";
            this.offBtn.UseVisualStyleBackColor = true;
            this.offBtn.Click += new System.EventHandler(this.offBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(248, 615);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(76, 29);
            this.stopBtn.TabIndex = 68;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // maxStepsLabel
            // 
            this.maxStepsLabel.AutoSize = true;
            this.maxStepsLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.maxStepsLabel.Location = new System.Drawing.Point(57, 573);
            this.maxStepsLabel.Name = "maxStepsLabel";
            this.maxStepsLabel.Size = new System.Drawing.Size(105, 19);
            this.maxStepsLabel.TabIndex = 69;
            this.maxStepsLabel.Text = "Max Fade Steps:";
            // 
            // maxFadeSteps
            // 
            this.maxFadeSteps.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maxFadeSteps.Lines = new string[0];
            this.maxFadeSteps.Location = new System.Drawing.Point(204, 566);
            this.maxFadeSteps.MaxLength = 32767;
            this.maxFadeSteps.Name = "maxFadeSteps";
            this.maxFadeSteps.PasswordChar = '\0';
            this.maxFadeSteps.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.maxFadeSteps.SelectedText = "";
            this.maxFadeSteps.Size = new System.Drawing.Size(102, 31);
            this.maxFadeSteps.TabIndex = 70;
            this.maxFadeSteps.UseSelectable = true;
            this.maxFadeSteps.TextChanged += new System.EventHandler(this.maxFadeSteps_TextChanged);
            // 
            // fadeEnabledCheck
            // 
            this.fadeEnabledCheck.AutoSize = true;
            this.fadeEnabledCheck.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.fadeEnabledCheck.Location = new System.Drawing.Point(326, 574);
            this.fadeEnabledCheck.Name = "fadeEnabledCheck";
            this.fadeEnabledCheck.Size = new System.Drawing.Size(98, 17);
            this.fadeEnabledCheck.TabIndex = 71;
            this.fadeEnabledCheck.Text = "Fade Enabled?";
            this.fadeEnabledCheck.UseVisualStyleBackColor = true;
            this.fadeEnabledCheck.CheckedChanged += new System.EventHandler(this.fadeEnabledCheck_CheckedChanged);
            // 
            // SettingsWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(567, 707);
            this.Controls.Add(this.fadeEnabledCheck);
            this.Controls.Add(this.maxFadeSteps);
            this.Controls.Add(this.maxStepsLabel);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.offBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.glowStatus);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.changeSensitivity);
            this.Controls.Add(this.shouldChangeLabel);
            this.Controls.Add(this.pollingWidth);
            this.Controls.Add(this.pollingHeight);
            this.Controls.Add(this.pollingHeightLabel);
            this.Controls.Add(this.pollingWidthLabel);
            this.Controls.Add(this.pollingY);
            this.Controls.Add(this.pollingYLabel);
            this.Controls.Add(this.pollingX);
            this.Controls.Add(this.pollingXLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pollingArea);
            this.Controls.Add(this.notifierToggle);
            this.Controls.Add(this.decoratorToggle);
            this.Controls.Add(this.notifiers);
            this.Controls.Add(this.decorators);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.notifiersLabel);
            this.Controls.Add(this.decoratorsLabel);
            this.Controls.Add(this.screenProcessors);
            this.Controls.Add(this.screenProcessorLabel);
            this.Controls.Add(this.screenGrabbers);
            this.Controls.Add(this.screenGrabberLabel);
            this.Controls.Add(this.driverExtensions);
            this.Controls.Add(this.DriverLabel);
            this.Controls.Add(this.sleepSize);
            this.Controls.Add(this.stepSleepLabel);
            this.Controls.Add(this.stepSize);
            this.Controls.Add(this.stepSizeLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingsWindow";
            this.Padding = new System.Windows.Forms.Padding(30, 92, 30, 31);
            this.Resizable = false;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Text = "Settings";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsWindow_FormClosing);
            this.MouseEnter += new System.EventHandler(this.SettingsWindow_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.settingsStyleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox stepSize;
        private MetroFramework.Controls.MetroTextBox sleepSize;
        private MetroFramework.Components.MetroStyleManager settingsStyleManager;
        private MetroFramework.Controls.MetroLabel stepSizeLabel;
        private MetroFramework.Controls.MetroLabel stepSleepLabel;
        private MetroFramework.Controls.MetroComboBox driverExtensions;
        private MetroFramework.Controls.MetroLabel DriverLabel;
        private MetroFramework.Controls.MetroComboBox screenProcessors;
        private MetroFramework.Controls.MetroLabel screenProcessorLabel;
        private MetroFramework.Controls.MetroComboBox screenGrabbers;
        private MetroFramework.Controls.MetroLabel screenGrabberLabel;
        private MetroFramework.Controls.MetroLabel notifiersLabel;
        private MetroFramework.Controls.MetroLabel decoratorsLabel;
        private MetroFramework.Controls.MetroButton apply;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel speedLabel;
        private MetroFramework.Controls.MetroButton notifierToggle;
        private MetroFramework.Controls.MetroButton decoratorToggle;
        private MetroFramework.Controls.MetroComboBox notifiers;
        private MetroFramework.Controls.MetroButton pollingArea;
        private MetroFramework.Controls.MetroLabel pollingWidth;
        private MetroFramework.Controls.MetroLabel pollingHeight;
        private MetroFramework.Controls.MetroLabel pollingHeightLabel;
        private MetroFramework.Controls.MetroLabel pollingWidthLabel;
        private MetroFramework.Controls.MetroLabel pollingY;
        private MetroFramework.Controls.MetroLabel pollingYLabel;
        private MetroFramework.Controls.MetroLabel pollingX;
        private MetroFramework.Controls.MetroLabel pollingXLabel;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroComboBox decorators;
        private MetroFramework.Controls.MetroTextBox changeSensitivity;
        private MetroFramework.Controls.MetroLabel shouldChangeLabel;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button offBtn;
        private System.Windows.Forms.Button startBtn;
        private MetroFramework.Controls.MetroLabel statusLabel;
        public System.Windows.Forms.Label speed;
        public System.Windows.Forms.Label glowStatus;
        private MetroFramework.Controls.MetroTextBox maxFadeSteps;
        private MetroFramework.Controls.MetroLabel maxStepsLabel;
        private MetroFramework.Controls.MetroCheckBox fadeEnabledCheck;
    }
}