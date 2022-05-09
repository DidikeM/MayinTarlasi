using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game
{
    class MineButton:Button
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int NumberMine { get; set; }
        public bool Clicked { get; set; }
        public int Status { get; set; }
        public int NumberFlag { get; set; }
    }
}
