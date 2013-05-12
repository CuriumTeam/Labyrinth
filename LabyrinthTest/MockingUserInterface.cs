using System;
using EscapeFromLabyrinth;

namespace LabyrinthTest
{
    public class MockingUserInterface:UserInput
    {
        private string fakeInput = "";
        
        public string FakeInput
        {
            get { return fakeInput; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    fakeInput = value;
                }
                else
                {
                    throw new ArgumentNullException("The user input is null");
                }
            }
        }

        private bool simulateWin = false;

        public bool SimulateWin
        {
            get { return simulateWin; }

            set
            {
                if (value != null)
                {
                    simulateWin = value;
                }
                else
                {
                    throw new AccessViolationException("The user simulation is null");
                }
            }
        }

        private int steps = 0;

        public override string GetInput()
        {
            string output = this.FakeInput;
            if (!simulateWin)
            {
                this.FakeInput = "exit";
            }
            else
            {
                this.steps++;
                if (this.steps == 3)
                {
                    this.FakeInput = "exit";
                }
            }

            return output;
        }
    }
}
