// ------------------------------------------------------------------------------
// j.c. TDD tic tac toe learning example - Oct 2016
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace TicTacToe
{
    // Represents a board for a game of tic tac toe.
    public class Board
    {
        // The default side length of the square board - 3 is the standard value
        private const int DEFAULT_BOARD_SIZE = 3;

        private int boardSize;

        public int GetSize()
        {
            return boardSize;
        }

        // TODO perhaps pass in size here?
        public Board(int boardSize_ = DEFAULT_BOARD_SIZE)
        {
            boardSize = boardSize_;
            m_squares = new SquareContents[boardSize, boardSize];

            // Initialise squares to empty
            for (int i = 0; i < boardSize; i++) 
            {
                for (int j = 0; j < boardSize; j++) 
                {
                    m_squares [i, j] = SquareContents.EMPTY;
                }
            }
        }

        public SquareContents GetContentsAtSquare(int i, int j)
        {
            ThrowOnBadCoord(i, j);

            return m_squares [i, j];
        }

        // Return 1 if board position is a win for player p. 
        // Return -1 if a lost position for player p.
        // Return 0 if there is no winner.
        //[Pure] 
        public int CalcScore(Player p)
        {
            if (CalcScoreInternal (p) == 1) 
            {
                return 1;
            }

            if (CalcScoreInternal (PlayerUtils.OtherPlayer (p)) == 1) 
            {
                return -1;
            }

            return 0;
        }

        // Sets square to given value.
        public void MakeMove(Move m)
        {
            ThrowOnBadCoord(m.Row, m.Col);

            // TODO throw if not empty?

            m_squares[m.Row, m.Col] = PlayerUtils.SquareContentsFromPlayer(m.Player);
        }

        public bool IsLegal(Move m)
        {
            ThrowOnBadCoord(m.Row, m.Col);

            // For tic tac toe it's easy - a move is legal if the destination square is empty
            return (m_squares[m.Row, m.Col] == SquareContents.EMPTY);
        }

        public List<Move> GetMoves(Player p)
        {
            List<Move> list = new List<Move>();
            for (int i = 0; i < boardSize; i++) 
            {
                for (int j = 0; j < boardSize; j++) 
                {
                    if (m_squares [i, j] == SquareContents.EMPTY)
                    {
                        list.Add(new Move(i, j, p));
                    }
                }
            }
            return list;
        }
                
        public void Draw()
        {
            for (int i = 0; i < boardSize; i++) 
            {
                string line = "";
                string horizontal = "";
                for (int j = 0; j < boardSize; j++) 
                {
                    SquareContents s = m_squares [i, j];
                    switch (s) {
                    case SquareContents.EMPTY:
                        line += "   ";
                        break;
                    case SquareContents.X:
                        line += " X ";
                        break;
                    case SquareContents.O:
                        line += " O ";
                        break;
                    }

                    horizontal += "---";

                    if (j < (boardSize - 1)) 
                    {
                        line += "|";
                        horizontal += "+";
                    }
                }
                Console.WriteLine (line);
                if (i < (boardSize - 1)) 
                {
                    Console.WriteLine (horizontal);
                }
            }
        }

        // Return 1 if a winning position for player p
        // Else return 0.
        private int CalcScoreInternal(Player p)
        {
            bool diag1 = true;
            bool diag2 = true;
            for (int i = 0; i < boardSize; i++) 
            {
                bool column = true;
                bool row = true;
                // Check rows and columns
                for (int j = 0; j < boardSize; j++) 
                {
                    if (!DoesSquareBelongToPlayer (i, j, p)) 
                    {
                        column = false;
                    }

                    if (!DoesSquareBelongToPlayer (j, i, p)) 
                    {
                        row = false;
                    }
                }

                if (column || row) {
                    return 1;
                }

                // Check diagonals
                if (!DoesSquareBelongToPlayer (i, i, p)) {
                    diag1 = false;
                }

                if (!DoesSquareBelongToPlayer (i, boardSize - i - 1, p)) {
                    diag2 = false;
                }
            }

            if (diag1 || diag2) {
                return 1;
            }

            return 0;
        }

        private bool DoesSquareBelongToPlayer(int i, int j, Player p)
        {
            ThrowOnBadCoord(i, j);

            SquareContents s = m_squares[i, j];

            if (p == Player.O)
            {
                return (s == SquareContents.O);
            }

            return (s == SquareContents.X);
        }

        private void ThrowOnBadCoord(int i, int j)
        {
            // Table-style: see this, item 13 http://www.viva64.com/en/b/0391/#ID0E5CAC
            if (   i < 0 
                || j < 0
                || i >= GetSize() 
                || j >= GetSize())
            {
                throw new ArgumentException("Coord out of range");
            }   
        }

        private SquareContents[,] m_squares;
    }
}

