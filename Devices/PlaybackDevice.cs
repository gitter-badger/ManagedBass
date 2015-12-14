﻿using ManagedBass.Dynamics;
using System;

namespace ManagedBass
{
    /// <summary>
    /// Wraps a Bass Playback Device
    /// </summary>
    /// <remarks>All Devices except NoSound and Default need to initialized before use</remarks>
    public class PlaybackDevice : BassDevice
    {
        /// <summary>
        /// Number of available Playback Devices
        /// </summary>
        public static int Count { get { return Bass.DeviceCount; } }

        /// <summary>
        /// Gets an array of Playback Devices
        /// </summary>
        public static PlaybackDevice[] Devices
        {
            get
            {
                int NoOfDevices = Count;

                PlaybackDevice[] Devices = new PlaybackDevice[NoOfDevices];

                for (int i = 0; i < NoOfDevices; ++i) Devices[i] = new PlaybackDevice(i);

                return Devices;
            }
        }

        public static PlaybackDevice NoSoundDevice { get { return new PlaybackDevice(0); } }

        public static PlaybackDevice DefaultDevice { get { return new PlaybackDevice(1); } }

        protected override DeviceInfo DeviceInfo { get { return Bass.GetDeviceInfo(DeviceId); } }

        internal PlaybackDevice(int DeviceId) : base(DeviceId) { }

        public Return<bool> Initialize(int Frequency = 44100) { return Bass.Initialize(DeviceId, Frequency, DeviceInitFlags.Default); }

        public override void Dispose() { Bass.Free(DeviceId); }

        public double Volume
        {
            get
            {
                Bass.CurrentDevice = DeviceId;
                return Bass.Volume;
            }

            set
            {
                Bass.CurrentDevice = DeviceId;
                Bass.Volume = value;
            }
        }
    }
}