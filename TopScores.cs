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
            for (int i = 0; i < topScores.Length; i++)
            {
                if (topScores[i] != null)
                {
                    Console.WriteLine(i + 1 + " - " + topScores[i]);
                }
            }

            if (topScores[0] == null && topScores[1] == null && topScores[2] == null && topScores[3] == null && topScores[4] == null)
            {

                Console.WriteLine("The scoreboard is empty.");
            }
        }
    }
}
