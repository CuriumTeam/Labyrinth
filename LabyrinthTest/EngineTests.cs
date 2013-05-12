using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeFromLabyrinth;
using System.IO;
using System.Text;

namespace LabyrinthTest
{
    [TestClass]
    public class EngineTests
    {
        static Engine testEngine;

        [TestMethod]
        public void TestDefaultConstructorForExceptions()
        {
            testEngine = new Engine();
            // Checking for exceptions.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUserDefinedConstructorForExceptions()
        {
            testEngine = new Engine(null, null, null);
        }

        static int[,] mockLabyrinth = {{0, 1, 1, 0, 1, 0, 1},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 1, 0, 1, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 1, 0, 1, 1, 0},
                {0, 1, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0}};

        [TestMethod]
        public void TestStartMethodForCorrectOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(mockLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "exit";
                testEngine = new Engine(testBoard,scores,userInterface);
                testEngine.Start();

                string welcomeString = "Welcome to “Labirinth” game. Your goal is to escape. \nUse 'top' to view the top scoreboard, \n'restart' to start a new game \nand 'exit' to quit the game.\n";
                string boardString = testBoard.ToString();
                string enterMoveString = "\nEnter your move (L=left, R=right, U=up, D=down):";

                StringBuilder startMethodOutput = new StringBuilder();
                startMethodOutput.Append(welcomeString);
                startMethodOutput.Append(Environment.NewLine);
                startMethodOutput.Append(boardString);
                startMethodOutput.Append(enterMoveString);
                startMethodOutput.Append("Good Bye!");
                startMethodOutput.Append(Environment.NewLine);

                string expected = startMethodOutput.ToString();
                string output = sw.ToString();
                
                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestRestartMethodForRandomBoard()
        {
             using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(mockLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "exit";
                testEngine = new Engine(testBoard,scores,userInterface);
                testEngine.Restart();

                string welcomeString = "Welcome to “Labirinth” game. Your goal is to escape. \nUse 'top' to view the top scoreboard, \n'restart' to start a new game \nand 'exit' to quit the game.\n";
                string boardString = testBoard.ToString();
                string enterMoveString = "\nEnter your move (L=left, R=right, U=up, D=down):";

                StringBuilder startMethodOutput = new StringBuilder();
                startMethodOutput.Append(welcomeString);
                startMethodOutput.Append(Environment.NewLine);
                startMethodOutput.Append(boardString);
                startMethodOutput.Append(enterMoveString);
                startMethodOutput.Append("Good Bye!");
                startMethodOutput.Append(Environment.NewLine);

                string expected = startMethodOutput.ToString();
                string output = sw.ToString();

                Assert.AreNotEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputDirectionLeft()
        {
            TopScores scores = new TopScores();
            MockLabyrinthBoard testBoard = new MockLabyrinthBoard(mockLabyrinth);
            int positionX = testBoard.PiecePositionCol;
            MockingUserInterface userInterface = new MockingUserInterface();
            userInterface.FakeInput = "L";
            testEngine = new Engine(testBoard, scores, userInterface);
            testEngine.Start();
            
            Assert.AreEqual<int>(positionX - 1, testEngine.Labyrinth.PiecePositionCol);
        }

        [TestMethod]
        public void TestProcessInputDirectionRight()
        {
            TopScores scores = new TopScores();
            MockLabyrinthBoard testBoard = new MockLabyrinthBoard(mockLabyrinth);
            int positionX = testBoard.PiecePositionCol;
            MockingUserInterface userInterface = new MockingUserInterface();
            userInterface.FakeInput = "R";
            testEngine = new Engine(testBoard, scores, userInterface);
            testEngine.Start();

            Assert.AreEqual<int>(positionX + 1, testEngine.Labyrinth.PiecePositionCol);
        }

        [TestMethod]
        public void TestProcessInputDirectionUp()
        {
            TopScores scores = new TopScores();
            MockLabyrinthBoard testBoard = new MockLabyrinthBoard(mockLabyrinth);
            int positionY = testBoard.PiecePositionRow;
            MockingUserInterface userInterface = new MockingUserInterface();
            userInterface.FakeInput = "U";
            testEngine = new Engine(testBoard, scores, userInterface);
            testEngine.Start();

            Assert.AreEqual<int>(positionY - 1, testEngine.Labyrinth.PiecePositionRow);
        }

        [TestMethod]
        public void TestProcessInputDirectionDown()
        {
            TopScores scores = new TopScores();
            MockLabyrinthBoard testBoard = new MockLabyrinthBoard(mockLabyrinth);
            int positionY = testBoard.PiecePositionRow;
            MockingUserInterface userInterface = new MockingUserInterface();
            userInterface.FakeInput = "D";
            testEngine = new Engine(testBoard, scores, userInterface);
            testEngine.Start();

            Assert.AreEqual<int>(positionY + 1, testEngine.Labyrinth.PiecePositionRow);
        }

        static int[,] closedLabyrinth = {{0, 1, 1, 0, 1, 0, 1},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
                {0, 0, 1, 0, 1, 0, 0},
                {0, 0, 1, 1, 1, 1, 0},
                {0, 1, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0}};

        [TestMethod]
        public void TestProcessInputDirectionLeftImposible()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "L";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("It is not possible to move left!");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("\nEnter your move (L=left, R=right, U=up, D=down):");
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 94);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputDirectionRightImposible()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "R";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("It is not possible to move right!");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("\nEnter your move (L=left, R=right, U=up, D=down):");
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 95);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputDirectionUpImposible()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "U";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("It is not possible to move up!");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("\nEnter your move (L=left, R=right, U=up, D=down):");
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 92);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputDirectionDownImposible()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "D";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("It is not possible to move down!");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("\nEnter your move (L=left, R=right, U=up, D=down):");
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 94);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputDirectionInvalid()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "A";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("Invalid command to move");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("\nEnter your move (L=left, R=right, U=up, D=down):");
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 85);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputCommandExit()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "exit";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 11);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputCommandTop()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "top";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("The scoreboard is empty.");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("\nEnter your move (L=left, R=right, U=up, D=down):");
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 86);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputCommandRestart()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "restart";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                string expected = testBoard.ToString();
                string output = testEngine.Labyrinth.ToString();

                Assert.AreNotEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestProcessInputCommandInvalid()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(closedLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "ninja";
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("Invalid command");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("\nEnter your move (L=left, R=right, U=up, D=down):");
                expectedString.Append("Good Bye!");
                expectedString.Append(Environment.NewLine);

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 77);

                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestWalkInLabirinth()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                TopScores scores = new TopScores();
                MockLabyrinthBoard testBoard = new MockLabyrinthBoard(mockLabyrinth);
                MockingUserInterface userInterface = new MockingUserInterface();
                userInterface.FakeInput = "L";
                userInterface.SimulateWin = true;
                testEngine = new Engine(testBoard, scores, userInterface);
                testEngine.Start();

                StringBuilder expectedString = new StringBuilder();
                expectedString.Append("Congratulations! You escaped in 3 moves.");
                expectedString.Append(Environment.NewLine);
                expectedString.Append("Please, enter your name:");

                string expected = expectedString.ToString();
                string output = sw.ToString();
                output = output.Substring(output.Length - 395,66);

                Assert.AreEqual<string>(expected, output);
            }
        }
    }
}


