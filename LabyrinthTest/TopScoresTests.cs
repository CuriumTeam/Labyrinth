using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeFromLabyrinth;
using System.IO;
using System.Text;

namespace LabyrinthTest
{
    [TestClass]
    public class TopScoresTests
    {
        static TopScores scores;

        [TestMethod]
        public void TestDefaultConstructorForExceptions()
        {
            scores = new TopScores();
            // Checking for exceptions.
        }

        [TestMethod]
        public void TestEnterTopScoreEmptyName()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                scores = new TopScores();
                scores.EnterTopScore("", 0);

                scores.ShowTopScores();
                string expected = "1 -  0\r\n";

                string output = sw.ToString();
                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestEnterTopScoreNotHighEnough()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                scores = new TopScores();
                scores.EnterTopScore("", 3);
                scores.EnterTopScore("", 4);
                scores.EnterTopScore("", 4);
                scores.EnterTopScore("", 4);
                scores.EnterTopScore("", 4);

                string caught = "";

                try
                {
                    scores.EnterTopScore("", 4);
                }
                catch (InvalidOperationException ioe)
                {
                    caught = "exception caught";
                }

                string expected = "exception caught";
                Assert.AreEqual<string>(expected, caught);
            }
        }


        [TestMethod]
        public void TestShowTopScoresEmpty()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                scores = new TopScores();

                scores.ShowTopScores();
                string expected = "The scoreboard is empty.\r\n";

                string output = sw.ToString();
                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestShowTopScoresCorrect()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                scores = new TopScores();
                scores.EnterTopScore("ninja", 6);
                scores.EnterTopScore("ganio", 4);
                scores.EnterTopScore("ganio", 4);
                scores.EnterTopScore("sando", 5);
                scores.EnterTopScore("ninja", 5);
                scores.EnterTopScore("sando", 3);

                scores.ShowTopScores();
                StringBuilder sBuild = new StringBuilder();
                sBuild.Append("1 - sando 3\r\n");
                sBuild.Append("2 - ganio 4\r\n");
                sBuild.Append("3 - ganio 4\r\n");
                sBuild.Append("4 - sando 5\r\n");
                sBuild.Append("5 - ninja 5\r\n");

                string expected = sBuild.ToString();

                string output = sw.ToString();
                Assert.AreEqual<string>(expected, output);
            }
        }

        [TestMethod]
        public void TestCheckIdScoreIsHighEnoughEmptyTopScores()
        {
            scores = new TopScores();
            bool isTrue = scores.CheckIdScoreIsHighEnough(10);

            scores.EnterTopScore("ninja", 6);
            scores.EnterTopScore("ganio", 4);
            scores.EnterTopScore("ganio", 4);
            scores.EnterTopScore("sando", 5);
            scores.EnterTopScore("ninja", 5);

            bool isFalse = scores.CheckIdScoreIsHighEnough(10);
                
            Assert.AreEqual(true, isTrue);
            Assert.AreEqual(false, isFalse);
        }

        [TestMethod]
        public void TestCheckIdScoreIsHighEnoughHighScore()
        {
            scores = new TopScores();

            scores.EnterTopScore("ninja", 6);
            scores.EnterTopScore("ganio", 4);
            scores.EnterTopScore("ganio", 4);
            scores.EnterTopScore("sando", 5);
            scores.EnterTopScore("ninja", 5);

            bool isTrue = scores.CheckIdScoreIsHighEnough(3);

            Assert.AreEqual(true, isTrue);
        }
    }
}
