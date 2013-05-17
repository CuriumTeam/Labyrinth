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
        private UserInput userInterface = new UserInput();

        public LabyrinthBoard Labyrinth
        {
            get { return labyrinth; }

            set
            {
                if (value != null)
                {
                    labyrinth = value;
                }
                else
                {
                    throw new ArgumentNullException("The labyrinth board is not initialized!");
                }
            }
        }
       
        public TopScores TopScores
        {
            get { return topScores; }

            set
            {
                if (value != null)
                {
                    topScores = value;
                }
                else
                {
                    throw new ArgumentNullException("The top scores are not initialized!");
                }
            }
        }

        public UserInput UserInterface
        {
            get { return userInterface; }

            set
            {
                if (value != null)
                {
                    userInterface = value;
                }
                else
                {
                    throw new ArgumentNullException("The user interface is not initialized!");
                }
            }
        }

        public Engine(LabyrinthBoard labyrinthBoard, TopScores scores, UserInput userInterface)
        {
            this.Labyrinth = labyrinthBoard;
            this.TopScores = scores;
            this.UserInterface = userInterface;
        }

        public Engine()
        {
            labyrinth = new LabyrinthBoard();
            topScores = new TopScores();
            flagContinue = true;
            enterMove = "\nEnter your move (L=left, R=right, U=up, D=down):";
            welcome = "Welcome to “Labirinth” game. Your goal is to escape. \nUse 'top' to view the top scoreboard, \n'restart' to start a new game \nand 'exit' to quit the game.\n";       
        }

        public void Start()
        {
            Console.WriteLine(this.welcome);
            Console.Write(this.labyrinth.ToString());
            this.Move();
        }

        public void Restart()
        {
            this.flagContinue = true;
            this.labyrinth = new LabyrinthBoard();
            this.labyrinth.InitializeLabyrinth();
            Console.WriteLine("\n" + this.welcome);
            Console.Write(this.labyrinth.ToString());
            this.Move();
        }

        public void Move()
        {
            int steps = 0;

            while (this.flagContinue == true)
            {
                Console.Write(enterMove);
                string input = userInterface.GetInput();

                if (input.Length == 1)
                {
                    this.ProcessInputDirection(input, ref steps); 
                }
                else
                {
                    this.ProcessInputCommand(input);
                }
            }
        }

        public void ProcessInputDirection(string input, ref int steps)
        {
            switch (input)
            {
                case "L":
                    if (this.labyrinth.IsPossibleCell(this.labyrinth.PiecePositionRow, this.labyrinth.PiecePositionCol - 1))
                    {
                        this.labyrinth.MovePieceLeft();
                        steps++;
                        this.WalkInLabirinth(ref steps);
                    }
                    else
                    {
                        Console.WriteLine("It is not possible to move left!");
                    }

                    break;
                case "R":
                    if (this.labyrinth.IsPossibleCell(this.labyrinth.PiecePositionRow, this.labyrinth.PiecePositionCol + 1))
                    {
                        this.labyrinth.MovePieceRight();
                        steps++;
                        this.WalkInLabirinth(ref steps);
                    }
                    else
                    {
                        Console.WriteLine("It is not possible to move right!");
                    }

                    break;
                case "U":
                    if (this.labyrinth.IsPossibleCell(this.labyrinth.PiecePositionRow - 1, this.labyrinth.PiecePositionCol))
                    {
                        this.labyrinth.MovePieceUp();
                        steps++;
                        this.WalkInLabirinth(ref steps);
                    }
                    else
                    {
                        Console.WriteLine("It is not possible to move up!");
                    }

                    break;
                case "D":
                    if (this.labyrinth.IsPossibleCell(this.labyrinth.PiecePositionRow + 1, this.labyrinth.PiecePositionCol))
                    {
                        this.labyrinth.MovePieceDown();
                        steps++;
                        this.WalkInLabirinth(ref steps);
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
            if (this.labyrinth.IsPieceOnEdge())
            {
                Console.Write(this.labyrinth.ToString());
                Console.WriteLine("Congratulations! You escaped in {0} moves.", steps);
                if (topScores.CheckIdScoreIsHighEnough(steps))
                {
                    Console.Write("Please, enter your name: ");
                    string name = userInterface.GetInput();
                    topScores.EnterTopScore(name, steps);
                }
                this.Restart();
            }
            else
            {
                Console.Write(this.labyrinth.ToString());
            }
        }

        public void ProcessInputCommand(string input)
        {
            switch (input)
            {
                case "exit":
                    Console.WriteLine("Good Bye!");
                    this.flagContinue = false;
                    break;
                case "restart":
                    this.Restart();
                    break;
                case "top":
                    this.topScores.ShowTopScores();
                    this.flagContinue = true;
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    this.flagContinue = true;
                    break;
            }
        }
    }
}