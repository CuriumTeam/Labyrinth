using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    public class TopScores
    {
        const int NumberOfTopScores = 5;
        private List<KeyValuePair<string, int>> topScores = new List<KeyValuePair<string, int>>(NumberOfTopScores);

        public void ShowTopScores()
        {
            for (int i = 0; i < this.topScores.Count; i++)
            {
                Console.WriteLine(i + 1 + " - " + this.topScores[i].Key + " " + this.topScores[i].Value);
            }

            if (topScores.Count == 0)
            {
                Console.WriteLine("The scoreboard is empty.");
            }
        }

        public void EnterTopScore(string name, int moves)
        {
            bool inserted = false;

            for (int i = 0; i < this.topScores.Count; i++)
            {
                if (moves < this.topScores[i].Value)
                {
                    this.topScores.Insert(i, new KeyValuePair<string,int>(name, moves));
                    if (this.topScores.Count > NumberOfTopScores)
                    {
                        this.topScores.RemoveAt(NumberOfTopScores);
                    }

                    inserted = true;
                    break;
                }
            }

            if (inserted == false && this.topScores.Count < NumberOfTopScores)
            {
                this.topScores.Add(new KeyValuePair<string, int>(name, moves));
                inserted = true;
            }

            if (!inserted)
            {
                throw new InvalidOperationException("The score is not high enough to be entered");
            }
        }

        public bool CheckIdScoreIsHighEnough(int moves)
        {
            if (this.topScores.Count < NumberOfTopScores)
            {
                return true;
            }

            for (int i = 0; i < this.topScores.Count; i++)
            {
                if (moves < this.topScores[i].Value)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
