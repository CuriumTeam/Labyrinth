using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    public class Engine
    {
        LabyrinthBoard labyrinth = new LabyrinthBoard();
        private TopScores topScores = new TopScores();
        private bool flagContinue = true;
        private readonly string enterMove = "\nEnter your move (L=left, R=right, U=up, D=down):";
        private readonly string welcome = "Welcome to “Labirinth” game. Your goal is to escape. \nUse 'top' to view the top scoreboard, \n'restart' to start a new game \nand 'exit' to quit the game.\n";       
 
        public void Start()
        {
            Console.WriteLine(welcome);
            Console.Write(labyrinth.ToString());
            Move();
        }

        public void Restart()
        {
            flagContinue = true;
            labyrinth = new LabyrinthBoard();
            labyrinth.InitializeLabyrinth();
            Console.WriteLine("\n" + welcome);
            Console.Write(labyrinth.ToString());
            Move();
        }
       
        public void Move()
        {
            int steps = 0;

            while (flagContinue == true)
            {
                Console.Write(enterMove);
                string input = Console.ReadLine();

                if (input.Length == 1)
                {
                    ProcessInputDirection(input, ref steps); 
                }
                else
                {
                    ProcessInputCommand(input);
                }
            }
        }

        public void ProcessInputDirection(string input, ref int steps)
        {
            switch (input)
            {
                case "L":
                    if (labyrinth.IsPossibleCell(labyrinth.PiecePositionRow, labyrinth.PiecePositionCol - 1))
                    {
                        labyrinth.MovePieceLeft();
                        steps++;
                        WalkInLabirinth(ref steps);
                    }
                    else
                    {
                        Console.WriteLine("It is not possible to move left!");
                    }
                    break;
                case "R":
                    if (labyrinth.IsPossibleCell(labyrinth.PiecePositionRow, labyrinth.PiecePositionCol + 1))
                    {
                        labyrinth.MovePieceRight();
                        steps++;
                        WalkInLabirinth(ref steps);
                    }
                    else
                    {
                        Console.WriteLine("It is not possible to move right!");
                    }
                    break;
                case "U":
                    if (labyrinth.IsPossibleCell(labyrinth.PiecePositionRow - 1, labyrinth.PiecePositionCol))
                    {
                        labyrinth.MovePieceUp();
                        steps++;
                        WalkInLabirinth(ref steps);
                    }
                    else
                    {
                        Console.WriteLine("It is not possible to move up!");
                    }

                    break;
                case "D":
                    if (labyrinth.IsPossibleCell(labyrinth.PiecePositionRow + 1, labyrinth.PiecePositionCol))
                    {
                        labyrinth.MovePieceDown();
                        steps++;
                        WalkInLabirinth(ref steps);
                    }
                    else
                    {
                        Console.WriteLine("It is not possible to move down!");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid command to move");
                    break;
            }
        }

        private void WalkInLabirinth(ref int steps)
        {   
            if (labyrinth.IsPieceOnEdge())
            {
                Console.Write(labyrinth.ToString());
                Console.WriteLine("Congratulations! You escaped in {0} moves.", steps);
                Restart();
            }
            else
            {
                Console.Write(labyrinth.ToString());
            }
        }

        public void ProcessInputCommand(string input)
        {
            switch (input)
            {
                case "exit":
                    Console.WriteLine("Good Bye!");
                    flagContinue = false;
                    break;
                case "restart":
                    Restart();
                    break;
                case "top":
                    topScores.ShowTopScores();
                    flagContinue = true;
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    flagContinue = true;
                    break;
            }
        }
    }
}


