using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    class TopScores
    {
        private string[] topScores = new string[5];

        public void ShowTopScores()
        {
            for (int i = 0; i < this.topScores.Length; i++)
            {
                if (this.topScores[i] != null)
                {
                    Console.WriteLine(i + 1 + " - " + this.topScores[i]);
                }
            }

            if (this.topScores[0] == null && this.topScores[1] == null && this.topScores[2] == null && this.topScores[3] == null && this.topScores[4] == null)
            {
                Console.WriteLine("The scoreboard is empty.");
            }
        }
    }
}
