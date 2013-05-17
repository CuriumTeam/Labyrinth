using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscapeFromLabyrinth;

namespace LabyrinthTest
{
    internal class MockLabyrinthBoard : LabyrinthBoard
    {
        public MockLabyrinthBoard(int[,] mockLabyrinth)
            :base()
        {
            this.InitializeMockLabyrinth(mockLabyrinth);
        }

        public void InitializeMockLabyrinth(int[,] mockLabyrinth)
        {
            if (mockLabyrinth.GetLength(0) != LABYRINTH_SIZE || mockLabyrinth.GetLength(1) != LABYRINTH_SIZE)
            {
                throw new ArgumentException("Not a valid Labyrinth representation");
            }

            for (int i = 0; i < mockLabyrinth.GetLength(0); i++)
            {
                for (int u = 0; u < mockLabyrinth.GetLength(1); u++)
                {
                    if (mockLabyrinth[i, u] != 1 && mockLabyrinth[i, u] != 0)
                    {
                        throw new ArgumentException("Not a valid Labyrinth representation");
                    }
                }
            }

            this.Labyrinth = mockLabyrinth;
        }
    }
}
