using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace downloader
{
    public class LinkForTest : ILink
    {

        public LinkForTest(string link, int priority)
        {
            this.link = link;
            this.priority = priority;
        }

        public float progress
        {
            get { return _progress; }
            set { _progress = value; }
        }

        public string speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public string link { get; set; }
        public int priority { get; set; }
        public string _speed { get; set; }  
        public float _progress { get; set; }

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
