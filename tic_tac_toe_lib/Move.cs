// ------------------------------------------------------------------------------
// j.c. TDD tic tac toe learning example - Oct 2016
// ------------------------------------------------------------------------------
using System;

namespace TicTacToe
{
    // Represents one move in the game.
    public class Move
    {
        public Move(int r, int c)
        {
            row = r;
            col = c;
        }

        // Row property 
        public int Row
        {
            get 
            {
                return row;
            }

            set
            {
                row = value;
            }
        }

        // Column property
        public int Col
        {
            get 
            {
                return col;
            }
            
            set
            {
                col = value;
            }
        }

        private int row;
        private int col;
    }
}
