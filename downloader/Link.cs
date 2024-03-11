using System;
using System.Windows;

namespace downloader
{
    public class Link : DependencyObject, ILink
    {
        public Link(string link, int priority)
        {
            this.link = link;
            this.priority = priority;
        }

        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("progress", typeof(float), typeof(Link));
        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("speed", typeof(string), typeof(Link));

        public float progress
        {
            get { return (float)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, (float)value); }
        }

        public string speed
        {
            get { return (string)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, (string)value); }
        }

        public string link { get; set; }
        public int priority { get; set; }

        private DateTime lastUpdate;
        private long lastBytes = 0;
        public void speedCalculate(long bytes)
        {
            if (lastBytes == 0)
            {
                lastUpdate = DateTime.Now;
                lastBytes = bytes;
                return;
            }

            var now = DateTime.Now;
            var timeSpan = now - lastUpdate;
            if (timeSpan.Milliseconds != 0)
            {
                var bytesChange = bytes - lastBytes;
                var bytesPerMillisecond = ((bytesChange / timeSpan.Milliseconds) / 1024d / 1024d * 1000).ToString("0.00");
                speed = $"Скорость: {bytesPerMillisecond} Mb/s";
            }
            lastBytes = bytes;
            lastUpdate = now;
        }
    }
}

