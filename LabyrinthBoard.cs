using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    class LabyrinthBoard
    {
        const int LABYRINTH_SIZE = 7;
        const int PIECE_POSITION = 3;

        private int[,] labyrinth = new int[LABYRINTH_SIZE, LABYRINTH_SIZE];
        private int piecePositionRow = PIECE_POSITION;
        private int piecePositionCol = PIECE_POSITION;

        public LabyrinthBoard()
        {
            this.InitializeLabyrinth();
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
            this.labyrinth = new int[LABYRINTH_SIZE, LABYRINTH_SIZE];

            for (int i = 0; i < LABYRINTH_SIZE; i++)
            {
                for (int j = 0; j < LABYRINTH_SIZE; j++)
                {
                    this.labyrinth[i, j] = random.Next(2);
                }
            }

            this.labyrinth[this.piecePositionCol, this.piecePositionRow] = 0;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < LABYRINTH_SIZE; i++)
            {
                for (int j = 0; j < LABYRINTH_SIZE; j++)
                {
                    if (this.PiecePositionRow == i && this.PiecePositionCol == j)
                    {
                        builder.Append("* ");
                    }
                    else if (this.labyrinth[i, j] == 0)
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
            if (row >= LABYRINTH_SIZE || col >= LABYRINTH_SIZE || row < 0 || col < 0)
            {
                return false;
            }

            if (this.labyrinth[row, col] == 0)
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
                            this.PiecePositionRow == LABYRINTH_SIZE - 1 ||
                            this.piecePositionCol == LABYRINTH_SIZE - 1;
            return isOnEdge;
        }

        public void MovePieceLeft()
        {
            if (this.PiecePositionCol == 0 || !this.IsPossibleCell(this.piecePositionRow, this.piecePositionCol - 1))
            {
                throw new InvalidOperationException("The piece cannot move to the left.");
            }

            this.PiecePositionCol--;
        }

        public void MovePieceRight()
        {
            if (this.PiecePositionCol == LABYRINTH_SIZE - 1 || !this.IsPossibleCell(this.piecePositionRow, this.piecePositionCol + 1))
            {
                throw new InvalidOperationException("The piece cannot move to the right.");
            }

            this.PiecePositionCol++;
        }

        public void MovePieceUp()
        {
            if (this.PiecePositionRow == 0 || !this.IsPossibleCell(this.piecePositionRow - 1, this.piecePositionCol))
            {
                throw new InvalidOperationException("The piece cannot move up.");
            }

            this.PiecePositionRow--;
        }

        public void MovePieceDown()
        {
            if (this.PiecePositionRow == LABYRINTH_SIZE - 1 || !this.IsPossibleCell(this.piecePositionRow + 1, this.piecePositionCol))
            {
                throw new InvalidOperationException("The piece cannot move down.");
            }

            this.PiecePositionRow++;
        }
    }
}
