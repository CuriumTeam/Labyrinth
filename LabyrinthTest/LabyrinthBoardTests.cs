using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeFromLabyrinth;

namespace LabyrinthTest
{
    [TestClass]
    public class LabyrinthBoardTests
    {
        static LabyrinthBoard testBoard;

        [TestMethod]
        public void TestConstructorForExceptions()
        {
            testBoard = new LabyrinthBoard();
            // Checking for exceptions.
        }

        [TestMethod]
        public void TestToConstructorForResult()
        {
            string labyrinthString = testBoard.ToString();
            int pieceCount = 0;

            for (int i = 0; i < labyrinthString.Length; i++)
            {
                if (labyrinthString[i] == '*')
                {
                    pieceCount++;
                }
            }

            int newLineCount = GetNewLineCount(labyrinthString);

            Assert.AreEqual(LabyrinthBoard.LABYRINTH_SIZE, newLineCount);
            Assert.AreEqual(1, pieceCount);
            Assert.AreEqual(LabyrinthBoard.PIECE_INITIAL_POSITION, testBoard.PiecePositionCol);
            Assert.AreEqual(LabyrinthBoard.PIECE_INITIAL_POSITION, testBoard.PiecePositionRow);
        }

        [TestMethod]
        public void TestToString()
        {
            string labyrinthString = testBoard.ToString();

            for (int i = 0; i < labyrinthString.Length; i++)
            {
                if (!(labyrinthString[i] == 'X' ||
                    labyrinthString[i] == '-' ||
                    labyrinthString[i] == '*' ||
                    labyrinthString[i] == ' ' ||
                    Environment.NewLine.Contains(labyrinthString[i].ToString())))
                {
                    Assert.Fail();
                    // No other characters are allowed.
                }
            }
        }

        private int GetNewLineCount(string text)
        {
            int count = 0;
            int currentIndex = 0;
            while (currentIndex + 1 < text.Length && text.IndexOf(Environment.NewLine, currentIndex + 1) >= 0)
            {
                count++;
                currentIndex = text.IndexOf(Environment.NewLine, currentIndex + 1);
            }

            return count;
        }

        static int[,] mockLabyrinth = {{0, 1, 1, 0, 1, 0, 1},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 1, 0, 1, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 1, 0, 1, 1, 0},
{0, 1, 1, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0}};

        static MockLabyrinthBoard mockBoard = new MockLabyrinthBoard(mockLabyrinth);

        [TestMethod]
        public void TestMovePieceRight()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinth);
            mockBoard.MovePieceRight();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION, LabyrinthBoard.PIECE_INITIAL_POSITION + 1]);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinth);
            mockBoard.MovePieceLeft();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION, LabyrinthBoard.PIECE_INITIAL_POSITION - 1]);
        }

        [TestMethod]
        public void TestMovePieceDown()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinth);
            mockBoard.MovePieceDown();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION + 1, LabyrinthBoard.PIECE_INITIAL_POSITION]);
        }

        [TestMethod]
        public void TestMovePieceUp()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinth);
            mockBoard.MovePieceUp();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION - 1, LabyrinthBoard.PIECE_INITIAL_POSITION]);
        }

        static int[,] mockLabyrinthStuck = {{0, 1, 1, 0, 1, 0, 1},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 1, 1, 1, 0, 0},
{0, 0, 1, 0, 1, 0, 0},
{0, 0, 1, 1, 1, 1, 0},
{0, 1, 1, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0}};

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMovePieceRightImpossible()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinthStuck);
            mockBoard.MovePieceRight();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION + 1, LabyrinthBoard.PIECE_INITIAL_POSITION]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMoveLeftImpossible()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinthStuck);
            mockBoard.MovePieceLeft();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION - 1, LabyrinthBoard.PIECE_INITIAL_POSITION]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMovePieceDownImpossible()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinthStuck);
            mockBoard.MovePieceDown();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION, LabyrinthBoard.PIECE_INITIAL_POSITION + 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMovePieceUpImpossible()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinthStuck);
            mockBoard.MovePieceUp();
            char[,] charRepresentation = GetCharArray(mockBoard);
            Assert.AreEqual('*', charRepresentation[LabyrinthBoard.PIECE_INITIAL_POSITION, LabyrinthBoard.PIECE_INITIAL_POSITION - 1]);
        }

        private char[,] GetCharArray(LabyrinthBoard board)
        {
            string boardToString = board.ToString();
            string[] rows = boardToString.Split(new string[] {Environment.NewLine}, StringSplitOptions.None);
            char[,] charRepresentation = new char[LabyrinthBoard.LABYRINTH_SIZE,LabyrinthBoard.LABYRINTH_SIZE];
            
            for(int i = 0; i < LabyrinthBoard.LABYRINTH_SIZE; i++)
            {
                string[] splitRow = rows[i].Split(' ');
                for(int u = 0; u < LabyrinthBoard.LABYRINTH_SIZE; u++)
                {
                    charRepresentation[i,u] = splitRow[u][0]; 
                    // If all is fine, there should be only one member in this string.
                }
            }

            return charRepresentation;
        }

        static int[,] mockLabyrinthEmpty = {{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0}};

        [TestMethod]
        public void TestIsPieceOnEdgeAllEdges()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinthEmpty);

            // Positioning the piece in the beginning.
            for (int i = 0; i < LabyrinthBoard.PIECE_INITIAL_POSITION; i++)
            {
                mockBoard.MovePieceLeft();
                mockBoard.MovePieceUp();
            }

            for (int i = 0; i < LabyrinthBoard.LABYRINTH_SIZE - 1; i++)
            {
                Assert.IsTrue(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceRight();
            }
            Assert.IsTrue(mockBoard.IsPieceOnEdge());

            for (int i = 0; i < LabyrinthBoard.LABYRINTH_SIZE - 1; i++)
            {
                Assert.IsTrue(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceDown();
            }
            Assert.IsTrue(mockBoard.IsPieceOnEdge());

            for (int i = 0; i < LabyrinthBoard.LABYRINTH_SIZE - 1; i++)
            {
                Assert.IsTrue(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceLeft();
            }
            Assert.IsTrue(mockBoard.IsPieceOnEdge());

            for (int i = 0; i < LabyrinthBoard.LABYRINTH_SIZE - 1; i++)
            {
                Assert.IsTrue(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceUp();
            }
            Assert.IsTrue(mockBoard.IsPieceOnEdge());
        }

        [TestMethod]
        public void TestIsPieceOnEdgeAllInternal()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinthEmpty);

            // Positioning the piece in the beginning, one position away from the edge.
            for (int i = 0; i < LabyrinthBoard.PIECE_INITIAL_POSITION - 1; i++)
            {
                mockBoard.MovePieceLeft();
                mockBoard.MovePieceUp();
            }

            for (int i = 1; i < LabyrinthBoard.LABYRINTH_SIZE - 2; i++)
            {
                Assert.IsFalse(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceRight();
            }
            Assert.IsFalse(mockBoard.IsPieceOnEdge());

            for (int i = 1; i < LabyrinthBoard.LABYRINTH_SIZE - 2; i++)
            {
                Assert.IsFalse(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceDown();
            }
            Assert.IsFalse(mockBoard.IsPieceOnEdge());

            for (int i = 1; i < LabyrinthBoard.LABYRINTH_SIZE - 2; i++)
            {
                Assert.IsFalse(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceLeft();
            }
            Assert.IsFalse(mockBoard.IsPieceOnEdge());

            for (int i = 1; i < LabyrinthBoard.LABYRINTH_SIZE - 2; i++)
            {
                Assert.IsFalse(mockBoard.IsPieceOnEdge());
                mockBoard.MovePieceUp();
            }
            Assert.IsFalse(mockBoard.IsPieceOnEdge());
        }

        [TestMethod]
        public void TestIsPossibleCellOccupied()
        {
            int[,] mockLabyrinth = {{1, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 0}};

            mockBoard = new MockLabyrinthBoard(mockLabyrinth);
            Assert.IsFalse(mockBoard.IsPossibleCell(0, 0));
        }

        [TestMethod]
        public void TestIsPossibleCellOutOfRange()
        {
            Assert.IsFalse(mockBoard.IsPossibleCell(-1, 0));
        }

        [TestMethod]
        public void TestIsPossibleCellAvailable()
        {
            mockBoard = new MockLabyrinthBoard(mockLabyrinth);
            Assert.IsTrue(mockBoard.IsPossibleCell(LabyrinthBoard.LABYRINTH_SIZE - 1, LabyrinthBoard.LABYRINTH_SIZE - 1));
        }
    }
}
