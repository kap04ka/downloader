using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace downloader
{
    public interface ILink
    {
        public string link { get; set; }
        public int priority { get; set; }
        public string speed { get; set; }
        public float progress { get; set; }
        void speedCalculate(long bytes);

    }
}
