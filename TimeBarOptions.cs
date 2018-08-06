using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeBar
{
    class TimeBarOptions
    {
        [Option("EndTime")]
        public string EndTime { get; set; }

        [Option("Minutes", Default = 30)]
        public int Minutes { get; set; }

        [Option("ColorElapsed", Default = "LightGray")]
        public string ColorElapsed { get; set; }

        public Color ColorValueElapsed
        {
            get
            {
                var color = (Color)ColorConverter.ConvertFromString(ColorElapsed);
                color.A = Transparency;
                return color;
            }
        }

        [Option("ColorRemaining", Default = "RoyalBlue")]
        public string ColorRemaining { get; set; }

        public Color ColorValueRemaining
        {
            get
            {
                var color = (Color)ColorConverter.ConvertFromString(ColorRemaining);
                color.A = Transparency;
                return color;
            }
        }


        [Option("ColorBorder", Default = "Black")]
        public string ColorBorder { get; set; }

        public Color ColorValueBorder
        {
            get
            {
                var color = (Color)ColorConverter.ConvertFromString(ColorBorder);
                color.A = Transparency;
                return color;
            }
        }

        [Option("Transparency", Default = (byte)100)]
        public byte Transparency { get; set; }

        [Option("Width", Default = 150)]
        public int Width { get; set; }

        [Option("Height", Default = 30)]
        public int Height { get; set; }

        [Option("Format", Default = "hh\\:mm\\:ss")]
        public string Format { get; set; }

        [Option("UpdateIntervalSec", Default = 1)]
        public int UpdateIntervalSec { get; set; }

        [Option("TopMost", Default = true)]
        public bool TopMost { get; set; }
    }
}
