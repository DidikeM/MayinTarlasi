using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class GameProparties
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int MineCount { get; set; }
        public int CounterLocation { get; set; }
        public GameProparties()
        {
            Row = 16;
            Column = 30;
            MineCount = 99;
            CounterLocation = (Column * 32 / 2) + 50;
        }

        public GameProparties(int row, int column, int mineCount)
        {
            Row = row;
            Column = column;
            MineCount = mineCount;
            CounterLocation = (Column * 32 / 2) + 50;
        }
    }
}
