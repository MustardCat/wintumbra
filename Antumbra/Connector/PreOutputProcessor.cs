﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antumbra.Glow.Observer.Colors;
using Antumbra.Glow.Observer.Configuration;
using Antumbra.Glow.Settings;
using Antumbra.Glow.Observer.Logging;

namespace Antumbra.Glow.Connector
{
    public class PreOutputProcessor : ConfigurationObserver, AntumbraColorObserver, Loggable
    {
        public delegate void NewLogMsg(String source, String msg);
        public event NewLogMsg NewLogMsgAvail;
        private Dictionary<int, OutputSettings> AllDeviceSettings;
        private Dictionary<int, long> OutputIndexes;
        public PreOutputProcessor() {
            this.AttachObserver(LoggerHelper.GetInstance());
            AllDeviceSettings = new Dictionary<int,OutputSettings>();
            OutputIndexes = new Dictionary<int, long>();
        }

        public void ConfigurationUpdate(Configurable config)
        {
            if (config is DeviceSettings) {
                DeviceSettings settings = (DeviceSettings)config;

                OutputSettings devSettings;
                devSettings.MaxBrightness = settings.maxBrightness;
                devSettings.redBias = settings.redBias;
                devSettings.greenBias = settings.greenBias;
                devSettings.blueBias = settings.blueBias;
                devSettings.whiteBalanceMin = Convert.ToUInt16((Math.Abs(settings.redBias)
                                                              + Math.Abs(settings.greenBias)
                                                              + Math.Abs(settings.blueBias)
                                                              / 3));
                
                AllDeviceSettings[settings.id] = devSettings;
            }
        }

        public void NewColorAvail(Color16Bit newCol, int id, long index)
        {
            OutputSettings settings;
            if (!AllDeviceSettings.TryGetValue(id, out settings)) {
                throw new Exception("OutputSettings for device not found! id: " + id);
            }

            long prevIndex;
            if (!OutputIndexes.TryGetValue(id, out prevIndex)) {
                OutputIndexes[id] = index;
            }
            else if (prevIndex >= index) {
                // Invalid index
                throw new Exception("Color recieved out of order! Target id: " + id +
                                    " with index " + index + " and last index " + prevIndex);
            }
            // Either first run or valid index
            int red = newCol.red;
            int green = newCol.green;
            int blue = newCol.blue;
            // White balance
            if (newCol.GetAvgBrightness() > settings.whiteBalanceMin) {
                red += settings.redBias;
                green += settings.greenBias;
                blue += settings.blueBias;
            }
            newCol = Color16Bit.FunnelIntoColor(red, green, blue);
            // Scale brightness
            newCol.ScaleColor(settings.MaxBrightness);
        }

        public void AttachObserver(LogMsgObserver observer)
        {
            NewLogMsgAvail += observer.NewLogMsgAvail;
        }

        private void Log(String msg)
        {
            if (NewLogMsgAvail != null) {
                NewLogMsgAvail("PreOutputProcessor", msg);
            }
        }

        private struct OutputSettings {
            public double MaxBrightness;
            public Int16 redBias, greenBias, blueBias;
            public UInt16 whiteBalanceMin;
        }
    }
}