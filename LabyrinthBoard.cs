using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    class LabyrinthBoard
    {
        const int LABYRINTHSIZE = 7;
        const int PIECEPOSITION = 3;

        private int[,] labyrinth = new int[LABYRINTHSIZE, LABYRINTHSIZE];
        private int piecePositionRow = PIECEPOSITION;
        private int piecePositionCol = PIECEPOSITION;

        public LabyrinthBoard()
        {
            InitializeLabyrinth();
        }

        public int PiecePositionCol
        {
            get
            {
                return piecePositionCol;
            }
            private set
            {
                piecePositionCol = value;
            }
        }

        public int PiecePositionRow
        {
            get
            {
                return piecePositionRow;
            }
            private set
            {
                piecePositionRow = value;
            }
        }

        public void InitializeLabyrinth()
        {
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    labyrinth[i, j] = random.Next(2);
                }
            }

            labyrinth[piecePositionCol, piecePositionRow] = 0;
        }

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
                    else if (labyrinth[i, j] == 0)
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
        }

        public bool IsPossibleCell(int row, int col)
        {
            if (row >= LABYRINTHSIZE || col >= LABYRINTHSIZE || row < 0 || col < 0)
            {
                return false;
            }

            if (labyrinth[row, col] == 0)
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
                            this.PiecePositionRow == LABYRINTHSIZE - 1 ||
                            this.piecePositionCol == LABYRINTHSIZE - 1;
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
            if (this.PiecePositionCol == LABYRINTHSIZE - 1 || !this.IsPossibleCell(piecePositionRow, piecePositionCol + 1))
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
            if (this.PiecePositionRow == LABYRINTHSIZE - 1 || !this.IsPossibleCell(piecePositionRow + 1, piecePositionCol))
            {
                throw new InvalidOperationException("The piece cannot move down.");
            }

            this.PiecePositionRow++;
        }
    }
}
