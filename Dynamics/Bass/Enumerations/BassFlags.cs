﻿using System;

namespace ManagedBass.Dynamics
{
    [Flags]
    public enum BassFlags
    {
        /// <summary>
        /// File is a Unicode (16-bit characters) filename
        /// </summary>
        Unicode = -2147483648,

        /// <summary>
        /// 0 = default create stream: 16 Bit, stereo, no Float, hardware mixing, no Loop, no 3D, no speaker assignments...
        /// </summary>
        Default = 0,

        /// <summary>
        /// BASS_FX add-on: If in use, then you can do other stuff while detection's in process.
        /// </summary>
        FxBpmBackground = 1,

        /// <summary>
        /// BASSMIDI add-on: Don't send a WAVE header to the encoder. 
        /// If this flag is used then the sample format (mono 16-bit) 
        /// must be passed to the encoder some other way, eg. via the command-line.
        /// </summary>
        MidiNoHeader = 1,

        /// <summary>
        /// Use 8-bit resolution. If neither this or the BASS_SAMPLE_FLOAT flags are specified, then the stream is 16-bit.
        /// </summary>
        Byte = 1,

        /// <summary>
        /// BASSMIDI add-on: Reduce 24-bit sample data to 16-bit before encoding.
        /// </summary>
        Midi16Bit = 2,

        /// <summary>
        /// Decode/play the stream (MP3/MP2/MP1 only) in mono, reducing the CPU usage (if it was originally stereo).
        /// This flag is automatically applied if BASS_DEVICE_MONO was specified when calling Bass.Init().
        /// </summary>
        Mono = 2,

        /// <summary>
        /// BASS_FX add-on: If in use, then will auto multiply bpm by 2 (if BPM &lt; MinBPM*2)
        /// </summary>
        FXBpmMult2 = 2,

        /// <summary>
        /// Loop the file. This flag can be toggled at any time using Bass.ChannelFlags().
        /// </summary>
        Loop = 4,

        /// <summary>
        /// Use 3D functionality. This is ignored if BASS_DEVICE_3D wasn't specified when calling Bass.Init().
        /// 3D streams must be mono (chans=1). 
        /// The SPEAKER flags can not be used together with this flag.
        /// </summary>
        Bass3D = 8,

        /// <summary>
        /// Force the stream to not use hardware mixing.
        /// </summary>
        SoftwareMixing = 16,

        /// <summary>
        /// Sample: muted at max distance (3D only)
        /// </summary>
        MuteMax = 32,

        /// <summary>
        /// Sample: uses the DX7 voice allocation and management
        /// </summary>
        VAM = 64,

        /// <summary>
        /// Enable the old implementation of DirectX 8 effects. 
        /// See the DX8 effect implementations section for details.
        /// Use Bass.ChannelSetFX() to add effects to the stream. 
        /// Requires DirectX 8 or above.
        /// </summary>
        FX = 128,

        /// <summary>
        /// Use 32-bit floating-point sample data (see Floating-Point Channels for details). 
        /// WDM drivers or the BASS_STREAM_DECODE flag are required to use this flag.
        /// </summary>
        Float = 256,

        /// <summary>
        /// Music: Use "normal" ramping (as used in FastTracker 2).
        /// </summary>
        MusicRamp = 512,

        /// <summary>
        /// BASS_FX add-on (AddOn.Fx.BassFx.BASS_FX_TempoCreate): Uses a linear interpolation mode (simple).
        /// </summary>
        FxTempoAlgorithmLinear = 512,
        
        /// <summary>
        /// BASSCD add-on: Read sub-channel data. 
        /// 96 bytes of de-interleaved sub-channel data will be returned after each 2352 bytes of audio. 
        /// This flag can not be used with the BASS_SAMPLE_FLOAT flag, 
        /// and is ignored if the BASS_STREAM_DECODE flag is not used.
        /// </summary>
        CDSubChannel = 512,
        
        /// <summary>
        /// BASSCD add-on: Read sub-channel data, without using any hardware de-interleaving.
        /// This is identical to the BASS_CD_SUBCHANNEL flag, except that the 
        /// de-interleaving is always performed by BASSCD even if the drive is apparently capable of de-interleaving itself.
        /// </summary>
        CDSubchannelNoHW = 1024,

        /// <summary>
        /// BASS_FX add-on (AddOn.Fx.BassFx.BASS_FX_TempoCreate): Uses a cubic interpolation mode (recommended, default).
        /// </summary>
        FxTempoAlgorithmCubic = 1024,

        /// <summary>
        /// Music: Use "sensitive" ramping.
        /// </summary>
        MuicSensitiveRamping = 1024,

        /// <summary>
        /// BASSMIDI add-on: Ignore system reset events (MIDI_EVENT_SYSTEM) when the system mode is unchanged.
        /// This flag can be toggled at any time using Bass.ChannelFlags().
        /// </summary>
        MidiNoSystemReset = 2048,

        /// <summary>
        /// BASS_FX add-on (AddOn.Fx.BassFx.BASS_FX_TempoCreate): 
        /// Uses a 8-tap band-limited Shannon interpolation (complex, but not much better than cubic).
        /// </summary>
        FxTempoAlgorithmShannon = 2048,

        /// <summary>
        /// Music: Apply XMPlay's surround sound to the music (ignored in mono).
        /// </summary>
        MusicSurround = 2048,
        
        /// <summary>
        /// BASSCD add-on: Include C2 error info. 
        /// 296 bytes of C2 error info is returned after each 2352 bytes of audio (and optionally 96 bytes of sub-channel data).
        /// This flag cannot be used with the BASS_SAMPLE_FLOAT flag, and is ignored if the BASS_STREAM_DECODE flag is not used.
        /// The first 294 bytes contain the C2 error bits (one bit for each byte of audio),
        /// followed by a byte containing the logical OR of all 294 bytes, 
        /// which can be used to quickly check if there were any C2 errors. 
        /// The final byte is just padding.
        /// Note that if you request both sub-channel data and C2 error info, the C2 info will come before the sub-channel data!
        /// </summary>
        CdC2Errors = 2048,

        /// <summary>
        /// BASSmix add-on: only read buffered data.
        /// </summary>
        SplitSlave = 4096,

        /// <summary>
        /// Music: Apply XMPlay's surround sound mode 2 to the music (ignored in mono).
        /// </summary>
        MusicSurround2 = 4096,
        
        /// <summary>
        /// BASSMIDI add-on: Let the sound decay naturally (including reverb) instead of stopping it abruptly at the end of the file.
        /// This flag can be toggled at any time using Bass.BASS_ChannelFlags().
        /// </summary>
        MidiDecayEnd = 4096,

        /// <summary>
        /// BASSmix add-on: resume a stalled mixer immediately upon new/unpaused source
        /// </summary>
        MixerResume = 4096,

        /// <summary>
        /// BASSmix add-on: enable BassMix.ChannelGetPositionEx() support.
        /// </summary>
        MixerPositionEx = 8192,

        /// <summary>
        /// Music: Play .MOD file as FastTracker 2 would.
        /// </summary>
        MusicFT2Mod = 8192,

        /// <summary>
        /// BASSMIDI add-on: Disable the MIDI reverb/chorus processing. 
        /// This flag can be toggled at any time using Bass.ChannelFlags().
        /// </summary>
        MidiNoFx = 8192,

        /// <summary>
        /// BASSmix add-on: buffer source data for BassMix.ChannelGetData() and BassMix.ChannelGetLevel().
        /// </summary>
        MixerBuffer = 8192,
        
        /// <summary>
        /// BASSMIDI add-on: Let the old sound decay naturally (including reverb) when changing the position, including looping.
        /// This flag can be toggled at any time using Bass.ChannelFlags(), and can also be used in Bass.ChannelSetPosition()
        /// calls to have it apply to particular position changes.
        /// </summary>
        MidiDecaySeek = 16384,

        /// <summary>
        /// Music: Play .MOD file as ProTracker 1 would.
        /// </summary>
        MusicPT1Mod = 16384,

        /// <summary>
        /// BASSmix add-on: Limit mixer processing to the amount available from this source.
        /// </summary>
        MixerLimit = 16384,

        /// <summary>
        /// Music: Stop all notes when seeking (using Bass.ChannelSetPosition()).
        /// </summary>
        MusicPositionReset = 32768,

        /// <summary>
        /// Recording: Start the recording paused. Use Bass.ChannelPlay() to start it.
        /// </summary>
        RecordPause = 32768,

        /// <summary>
        /// BASSMIDI add-on: Do not remove empty space (containing no events) from the end of the file.
        /// </summary>
        MidiNoCrop = 32768,

        /// <summary>
        /// Music: Use non-interpolated mixing.
        /// This generally reduces the sound quality, but can be good for chip-tunes.
        /// </summary>
        MusicNonInterpolated = 65536,
        
        /// <summary>
        /// BASSMIDI add-on: Only release the oldest instance upon a note off event (MIDI_EVENT_NOTE with velocity=0)
        /// when there are overlapping instances of the note.
        /// Otherwise all instances are released. 
        /// This flag can be toggled at any time using Bass.ChannelFlags().
        /// </summary>
        MidiNoteOff1 = 65536,

        /// <summary>
        /// BASS_FX add-on: Free the source handle as well?
        /// </summary>
        FxFreeSource = 65536,

        /// <summary>
        /// Sample: override lowest volume
        /// </summary>
        SampleOverrideLowestVolume = 65536,

        /// <summary>
        /// BASSmix add-on: end the stream when there are no sources
        /// </summary>
        MixerEnd = 65536,

        /// <summary>
        /// BASSmix add-on: Matrix mixing
        /// </summary>
        MixerMatrix = 65536,
        //
        // Summary:
        //     Enable pin-point accurate seeking (to the exact byte) on the MP3/MP2/MP1
        //     stream.
        //     This also increases the time taken to create the stream, due to the entire
        //     file being pre-scanned for the seek points.  Note: BASS_STREAM_PRESCAN is
        //     ONLY needed for files with a VBR, files with a CBR are always accurate.
        //
        // Summary:
        //     Music: Calculate the playback length of the music, and enable seeking in
        //     bytes. This slightly increases the time taken to load the music, depending
        //     on how long it is.
        //     In the case of musics that loop, the length until the loop occurs is calculated.
        //     Use Un4seen.Bass.Bass.BASS_ChannelGetLength(System.Int32,Un4seen.Bass.BASSMode)
        //     to retrieve the length.
        Prescan = 131072,
        
        /// <summary>
        /// BASSMIDI add-on: Map the file into memory. 
        /// This flag is ignored if the soundfont is packed as the sample data cannot be played directly from a mapping;
        /// it needs to be decoded. 
        /// This flag is also ignored if the file is too large to be mapped into memory.
        /// </summary>
        MidiFontMemoryMap = 131072,

        /// <summary>
        /// BASSmix add-on: don't stall when there are no sources
        /// </summary>
        MixerNonStop = 131072,

        /// <summary>
        /// Sample: override longest playing
        /// </summary>
        SampleOverrideLongestPlaying = 131072,

        /// <summary>
        /// BASSmix add-on: don't process the source
        /// </summary>
        MixerPause = 131072,

        /// <summary>
        /// Sample: override furthest from listener (3D only)
        /// </summary>
        SampleOverrideDistance = 196608,
        //
        // Summary:
        //     Automatically free the stream's resources when it has reached the end, or
        //     when Un4seen.Bass.Bass.BASS_ChannelStop(System.Int32) (or Un4seen.Bass.Bass.BASS_Stop())
        //     is called.
        //
        // Summary:
        //     Music: Automatically free the music when it ends. This allows you to play
        //     a music and forget about it, as BASS will automatically free the music's
        //     resources when it has reached the end or when Un4seen.Bass.Bass.BASS_ChannelStop(System.Int32)
        //     (or Un4seen.Bass.Bass.BASS_Stop()) is called.
        //     Note that some musics never actually end on their own (ie. without you stopping them).
        AutoFree = 262144,
        
        /// <summary>
        /// Restrict the download rate of the file to the rate required to sustain playback.
        /// If this flag is not used, then the file will be downloaded as quickly as possible. 
        /// This flag has no effect on "unbuffered" streams (buffer=false).
        /// </summary>
        RestrictDownloadRate = 524288,
        //
        // Summary:
        //     Music: Stop the music when a backward jump effect is played. This stops musics
        //     that never reach the end from going into endless loops.
        //     Some MOD musics are designed to jump all over the place, so this flag would
        //     cause those to be stopped prematurely. If this flag is used together with
        //     the BASS_SAMPLE_LOOP flag, then the music would not be stopped but any BASS_SYNC_END
        //     sync would be triggered.
        MusicStopBack = 524288,
        //
        // Summary:
        //     Download and play the file in smaller chunks.
        //     Uses a lot less memory than otherwise, but it's not possible to seek or loop
        //     the stream - once it's ended, the file must be opened again to play it again.
        //     This flag will automatically be applied when the file length is unknown.
        //     This flag also has the effect of resticting the download rate. This flag
        //     has no effect on "unbuffered" streams (buffer=false).
        StreamDownloadBlocks = 1048576,
        //
        // Summary:
        //     Music: Don't load the samples. This reduces the time taken to load the music,
        //     notably with MO3 files, which is useful if you just want to get the name
        //     and length of the music without playing it.
        MusicNoSample = 1048576,
        //
        // Summary:
        //     Decode the sample data, without outputting it. Use Un4seen.Bass.Bass.BASS_ChannelGetData(System.Int32,System.IntPtr,System.Int32)
        //     to retrieve decoded sample data.
        //     BASS_SAMPLE_SOFTWARE/3D/FX/AUTOFREE are all ignored when using this flag,
        //     as are the SPEAKER flags.
        //
        // Summary:
        //     Music: Decode the music into sample data, without outputting it.
        //     Use Un4seen.Bass.Bass.BASS_ChannelGetData(System.Int32,System.IntPtr,System.Int32)
        //     to retrieve decoded sample data. BASS_SAMPLE_SOFTWARE/3D/FX/AUTOFREE are
        //     ignored when using this flag, as are the SPEAKER flags.
        Decode = 2097152,

        /// <summary>
        /// Music: Stop all notes and reset bpm/etc when seeking.
        /// </summary>
        MusicPositionResetEx = 4194304,

        /// <summary>
        /// BASSmix add-on: downmix to stereo (or mono if mixer is mono)
        /// </summary>
        MixerDownMix = 4194304,
        //
        // Summary:
        //     BASSMIDI add-on: Use sinc interpolated sample mixing. This increases the
        //     sound quality, but also requires more CPU. Otherwise linear interpolation
        //     is used.
        //
        // Summary:
        //     Music: Sinc interpolated sample mixing.  This increases the sound quality,
        //     but also requires quite a bit more processing. If neither this or the BASS_MUSIC_NONINTER
        //     flag is specified, linear interpolation is used.
        SincInterpolation = 8388608,

        /// <summary>
        /// BASSmix add-on: don't ramp-in the start
        /// </summary>
        MixerNoRampin = 8388608,
        //
        // Summary:
        //     Pass status info (HTTP/ICY tags) from the server to the DOWNLOADPROC callback
        //     during connection.
        //     This can be useful to determine the reason for a failure.
        StreamStatus = 8388608,

        /// <summary>
        /// Front speakers (channel 1/2)
        /// </summary>
        SpeakerFront = 16777216,

        /// <summary>
        /// Speakers Pair 1
        /// </summary>
        SpeakerPair1 = 16777216,

        /// <summary>
        /// Speakers Pair 2
        /// </summary>
        SpeakerPair2 = 33554432,

        /// <summary>
        /// Rear/Side speakers (channel 3/4)
        /// </summary>
        SpeakerRear = 33554432,

        /// <summary>
        /// Speakers Pair 3
        /// </summary>
        SpeakerPair3 = 50331648,

        /// <summary>
        /// Center and LFE speakers (5.1, channel 5/6)
        /// </summary>
        SpeakerCenterLFE = 50331648,

        /// <summary>
        /// Speakers Pair 4
        /// </summary>
        SpeakerPair4 = 67108864,

        /// <summary>
        /// Rear Center speakers (7.1, channel 7/8)
        /// </summary>
        SpeakerRearCenter = 67108864,

        /// <summary>
        /// Speakers Pair 5
        /// </summary>
        SpeakerPair5 = 83886080,

        /// <summary>
        /// Speakers Pair 6
        /// </summary>
        SpeakerPair6 = 100663296,

        /// <summary>
        /// Speakers Pair 7
        /// </summary>
        SpeakerPair7 = 117440512,
        //
        // Summary:
        //     Speakers Pair 8
        SpeakerPair8 = 134217728,
        //
        // Summary:
        //     Speakers Pair 9
        SpeakerPair9 = 150994944,
        //
        // Summary:
        //     Speakers Pair 10
        SpeakerPair10 = 167772160,
        //
        // Summary:
        //     Speakers Pair 11
        SpeakerPair11 = 184549376,
        //
        // Summary:
        //     Speakers Pair 12
        SpeakerPair12 = 201326592,
        //
        // Summary:
        //     Speakers Pair 13
        SpeakerPair13 = 218103808,
        //
        // Summary:
        //     Speakers Pair 14
        SpeakerPair14 = 234881024,
        //
        // Summary:
        //     Speakers Pair 15
        SpeakerPair15 = 251658240,
        //
        // Summary:
        //     Speaker Modifier: left channel only
        SpeakerLeft = 268435456,
        //
        // Summary:
        //     Front Left speaker only (channel 1)
        SpeakerFrontLeft = 285212672,
        //
        // Summary:
        //     Rear/Side Left speaker only (channel 3)
        SpeakerRearLeft = 301989888,
        //
        // Summary:
        //     Center speaker only (5.1, channel 5)
        SpakerCenter = 318767104,
        //
        // Summary:
        //     Rear Center Left speaker only (7.1, channel 7)
        SpeakerRearCenterLeft = 335544320,
        //
        // Summary:
        //     Speaker Modifier: right channel only
        SpeakerRight = 536870912,
        //
        // Summary:
        //     Front Right speaker only (channel 2)
        SpeakerFrontRight = 553648128,
        //
        // Summary:
        //     Rear/Side Right speaker only (channel 4)
        SpeakerRearRight = 570425344,
        //
        // Summary:
        //     LFE speaker only (5.1, channel 6)
        SpeakerLFE = 587202560,
        //
        // Summary:
        //     Rear Center Right speaker only (7.1, channel 8)
        SpeakerRearCenterRight = 603979776,
        //
        // Summary:
        //     Use an async look-ahead cache.
        AsyncFile = 1073741824,

        MidiFontMemory = 0x10000,
        
        /// <summary>
        /// Use bank 127 in the soundfont for XG drum kits. 
        /// When an XG drum kit is needed, bank 127 in soundfonts that have this flag set will be checked first, 
        /// before falling back to bank 128 (the standard SF2 drum kit bank) if it is not available there. 
        /// </summary>
        MidiFontXGDRUMS = 0x40000
    }
}