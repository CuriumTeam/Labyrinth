using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    class LabyrinthBoard
    {
        const int LabyrinthSize = 7;

        private int[,] ll = new int[LabyrinthSize, LabyrinthSize];
        private int piecePositionRow = 3;
        private int piecePositionCol = 3;

        public LabyrinthBoard()
        {
            InitializeLabyrinth();
        }

        public int PiecePositionCol
        {
            get
            {
                return this.piecePositionCol;
            }
            private set
            {
                this.piecePositionCol = value;
            }
        }

        public int PiecePositionRow
        {
            get
            {
                return this.piecePositionRow;
            }
            private set
            {
                this.piecePositionRow = value;
            }
        }

        public void InitializeLabyrinth()
        {
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    ll[i, j] = random.Next(2);
                }
            }
            ll[piecePositionCol, piecePositionRow] = 0;
        }//end InitializeLabyrinth method

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (this.PiecePositionRow == i && this.PiecePositionCol == j)
                    {
                        builder.Append("* ");
                    }
                    else if (ll[i, j] == 0)
                    {


                        builder.Append("- ");
                    }
                    else
                    {
                        builder.Append("X ");
                    }
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }//end ShowLabyrinth method

        public bool IsPossibleCell(int row, int col)
        {
            if (row >= LabyrinthSize || col >= LabyrinthSize || row < 0 || col < 0)
            {
                return false;
            }

            if (ll[row, col] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPieceOnEdge()
        {
            bool isOnEdge = this.PiecePositionRow == 0 ||
                            this.piecePositionCol == 0 ||
                            this.PiecePositionRow == LabyrinthSize - 1 ||
                            this.piecePositionCol == LabyrinthSize - 1;
            return isOnEdge;
        }

        public void MovePieceLeft()
        {
            if(this.PiecePositionCol == 0 || !this.IsPossibleCell(piecePositionRow, piecePositionCol - 1))
            {
                throw new InvalidOperationException("The piece cannot move to the left.");
            }

            this.PiecePositionCol--;
        }

        public void MovePieceRight()
        {
            if (this.PiecePositionCol == LabyrinthSize - 1 || !this.IsPossibleCell(piecePositionRow, piecePositionCol + 1))
            {
                throw new InvalidOperationException("The piece cannot move to the right.");
            }

            this.PiecePositionCol++;
        }

        public void MovePieceUp()
        {
            if (this.PiecePositionRow == 0 || !this.IsPossibleCell(piecePositionRow - 1, piecePositionCol))
            {
                throw new InvalidOperationException("The piece cannot move up.");
            }

            this.PiecePositionRow--;
        }

        public void MovePieceDown()
        {
            if (this.PiecePositionRow == LabyrinthSize - 1 || !this.IsPossibleCell(piecePositionRow + 1, piecePositionCol))
            {
                throw new InvalidOperationException("The piece cannot move down.");
            }

            this.PiecePositionRow++;
        }
    }
}
