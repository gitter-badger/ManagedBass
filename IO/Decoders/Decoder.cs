﻿using System.IO;
using System.Runtime.InteropServices;
using ManagedBass.Dynamics;
using System;

namespace ManagedBass
{
    public class Decoder : Channel
    {
        static Decoder() { PlaybackDevice.NoSoundDevice.Initialize(); }

        protected Decoder(Resolution BufferKind = Resolution.Short) : base(BufferKind) { Bass.CurrentDevice = 0; }

        internal Decoder(int Handle, Resolution BufferKind = Resolution.Short) : this(BufferKind) { this.Handle = Handle; }

        public bool HasData { get { return Bass.IsChannelActive(Handle) == PlaybackState.Playing; } }

        /// <summary>
        /// Position in Bytes
        /// </summary>
        public long Position
        {
            get { return Bass.GetChannelPosition(Handle); }
            set { Bass.SetChannelPosition(Handle, value); }
        }

        public void Reset() { Position = 0; }

        /// <summary>
        /// Writes all the Data in the decoder to a file
        /// </summary>
        /// <param name="Writer">Audio File Writer to write to</param>
        /// <param name="Offset">+ve for forward, -ve for backward</param>
        public void Write(IAudioFileWriter Writer, int Offset = 0)
        {
            long InitialPosition = Position;

            Position += Offset;

            int BlockLength = (int)Seconds2Bytes(2);

            byte[] Buffer = new byte[BlockLength];
            
            var gch = GCHandle.Alloc(Buffer, GCHandleType.Pinned);

            while (HasData)
            {
                Bass.ChannelGetData(Handle, gch.AddrOfPinnedObject(), BlockLength);
                Writer.Write(Buffer, BlockLength);
            }

            gch.Free();

            Writer.Dispose();

            Position = InitialPosition;
        }
    }
}