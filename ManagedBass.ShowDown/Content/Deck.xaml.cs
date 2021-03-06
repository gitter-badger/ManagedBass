﻿using ManagedBass;
using ManagedBass.Effects;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ManagedBass.ShowDown
{
    public partial class Deck : UserControl, INotifyPropertyChanged
    {
        #region Fields
        ReverseDecoder ReverseDecoder;
        public TempoPlayer Player;
        ReverbEffect ReverbEffect;
        DispatcherTimer ProgressBarTimer;
        string FilePath;

        public static readonly DependencyProperty ReadyProperty = DependencyProperty.Register("Ready", typeof(bool), typeof(Deck), new UIPropertyMetadata(false));
        #endregion

        #region Properties
        public bool Ready
        {
            get { return (bool)GetValue(ReadyProperty); }
            set { SetValue(ReadyProperty, value); }
        }

        public double Position
        {
            get { return Player != null ? Player.Position : 0; }
            set
            {
                if (Player != null) Player.Position = value;

                OnPropertyChanged("Position");
            }
        }

        public double Duration
        {
            get { return Player != null ? Player.Duration : 0; }
            set { OnPropertyChanged("Duration"); }
        }

        public double Volume { set { Player.Volume = value; } }

        public bool Reverse
        {
            get { return ReverseDecoder != null ? ReverseDecoder.Reverse : false; }
            set
            {
                if (ReverseDecoder != null) ReverseDecoder.Reverse = value;

                OnPropertyChanged("Reverse");
            }
        }

        public bool Loop
        {
            get { return Player != null ? Player.Loop : false; }
            set
            {
                if (Player != null) Player.Loop = value;

                OnPropertyChanged("Loop");
            }
        }

        public double Tempo
        {
            get { return Player != null ? Player.Tempo : 0; }
            set
            {
                if (Player != null) Player.Tempo = value;

                OnPropertyChanged("Tempo");
            }
        }

        public double Frequency
        {
            get { return Player != null ? Player.Frequency : 44100; }
            set
            {
                if (Player != null) Player.Frequency = value;

                OnPropertyChanged("Frequency");
            }
        }

        public double Pitch
        {
            get { return Player != null ? Player.Pitch : 0; }
            set
            {
                if (Player != null) Player.Pitch = value;

                OnPropertyChanged("Pitch");
            }
        }

        public double Balance
        {
            get { return Player != null ? Player.Balance : 0; }
            set
            {
                if (Player != null) Player.Balance = value;

                OnPropertyChanged("Balance");
            }
        }

        public bool Reverb
        {
            get { return ReverbEffect != null ? ReverbEffect.IsActive : false; }
            set
            {
                if (ReverbEffect != null) ReverbEffect.IsActive = value;

                OnPropertyChanged("Reverb");
            }
        }
        #endregion

        public Deck()
        {
            InitializeComponent();

            DataContext = this;

            ProgressBarTimer = new DispatcherTimer(DispatcherPriority.Send) { Interval = TimeSpan.FromMilliseconds(100)};
            ProgressBarTimer.Tick += (s, e) =>
            {
                if (Player.IsPlaying && !IsDragging)
                    OnPropertyChanged("Position");
            };
        }

        void Load(string FilePath)
        {
            this.FilePath = FilePath;

            //try
            //{
            if (Player != null) Player.Dispose();
            if (ReverseDecoder != null) ReverseDecoder.Dispose();
            if (ReverbEffect != null) ReverbEffect = null;

            ReverseDecoder = new ReverseDecoder(new FileDecoder(FilePath)) { Reverse = false };

            Player = new TempoPlayer(ReverseDecoder);

            ReverbEffect = new ReverbEffect(Player) { IsActive = false };

            Ready = true;

            Reverse = false;

            Title.Content = Path.GetFileNameWithoutExtension(FilePath);

            OnPropertyChanged("Duration");
            OnPropertyChanged("Position");

            BPlay.Content = "/Resources/Play.png";

            Frequency = 44100;
            Balance = Pitch = Tempo = 0;

            Player.MediaEnded += (s, t) => Dispatcher.Invoke(() =>
                {
                    Status.Content = "Stopped";
                    Position = Reverse ? Duration : 0;
                    ProgressBarTimer.Stop();
                    BPlay.Content = "/Resources/Play.png";
                });

            if (MusicLoaded != null) MusicLoaded();
            //}
            //catch { Error(); }
        }

        public event Action MusicLoaded;

        void Error()
        {
            Title.Content = "Error";
            Status.Content = "Error";
            Ready = false;
        }
        
        public void Play(object sender = null, RoutedEventArgs e = null)
        {
            if (Player != null)
            {
                if (BPlay.Content.ToString().Contains("Play"))
                {
                    if (Reverse && Position == 0)
                        Position = Duration;

                    Player.Start();

                    Status.Content = "Playing";
                    BPlay.Content = "/Resources/Pause.png";
                    ProgressBarTimer.Start();
                }

                else if (BPlay.Content.ToString().Contains("Pause"))
                {
                    Player.Pause();

                    Status.Content = "Paused";
                    BPlay.Content = "/Resources/Play.png";
                    ProgressBarTimer.Stop();
                }
            }
        }

        public void Stop(object sender = null, RoutedEventArgs e = null)
        {
            if (Player != null && Player.Stop())
            {
                Status.Content = "Stopped";
                BPlay.Content = "/Resources/Play.png";
                Position = Reverse ? Duration : 0;
                ProgressBarTimer.Stop();
            }
        }

        #region Progress Slider
        bool IsDragging = false;

        void Slider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsDragging)
            {
                Player.Position = ((Slider)sender).Value;

                IsDragging = false;
            }
        }

        void Slider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) { IsDragging = true; }

        void Slider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) { Player.Position = ((Slider)sender).Value; }
        #endregion
        
        void UserControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                Load((string)e.Data.GetData(DataFormats.StringFormat));
        }

        #region Reset
        void ResetPitch(object sender, MouseButtonEventArgs e) { Pitch = 0; }

        void ResetFrequency(object sender, MouseButtonEventArgs e) { Frequency = 44100; }

        void ResetBalance(object sender, MouseButtonEventArgs e) { Balance = 0; }

        void ResetTempo(object sender, MouseButtonEventArgs e) { Tempo = 0; }
        #endregion

        #region INotifyPropertyChanged
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}