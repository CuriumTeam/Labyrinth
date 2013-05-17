using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeFromLabyrinth;
using System.IO;
using System.Text;

namespace LabyrinthTest
{
    [TestClass]
    public class UserInputTests
    {
        static UserInput input;

        [TestMethod]
        public void TestDefaultConstructorForExceptions()
        {
            input = new UserInput();
            // Checking for exceptions.
        }

        [TestMethod]
        public void TestGetInputString()
        {
            MockingUserInterface falseInput = new MockingUserInterface();
            falseInput.FakeInput = "ninja";

            string input = falseInput.GetInput();

            Assert.AreEqual<string>("ninja", input);
        }
    }
}
