using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game
{
    class Other
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int TopStart { get; set; }
        public int LeftStart { get; set; }
        public Size SizeBlock { get; set; }
        public int CountMine { get; set; }
        public int NumberFlaged { get; set; }
        public int NumberOpened { get; set; }
        public int CheckFinishGame { get; set; }
        public Label FlagCounterLabel { get; set; }
        public bool FinishStartFlag { get; set; }
        public bool LeftClick { get; set; }
        public bool RightClick { get; set; }
        //public bool RunMiddleClick { get; set; }
        public bool FirstClick { get; set; }
    }
}
