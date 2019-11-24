using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeAI
{
    class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Content { get; set; }

        public Cell(int x, int y, char player)
        {
            X = x;
            Y = y;
            Content = player;
        }
    }
}
