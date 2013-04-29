using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    public class Engine
    {
        LabyrinthBoard ll = new LabyrinthBoard();
        private string enterMove = "Enter your move (L=left, R=right, U=up, D=down):";
        private string welcome = "Welcome to “Labirinth” game. Please try to escape. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.";
        private int i = 0, j = 0, m = 3, n = 3;
        private TopScores topScores = new TopScores();


        private bool _continue = true;


        public void Start()
        {
            Console.WriteLine(welcome);
            Console.Write(ll.ToString());
            Move();
        }

       
        public void Move()
        {
            int steps = 0;

            while (_continue == true)
            {
                Console.Write(enterMove);
                string input = Console.ReadLine();

                if (input.Length > 1 || input.Length == 0)
                {
                    switch (input)
                    {
                        case "exit":
                            Console.WriteLine("Good Bye!");
                            _continue = false;
                            break;
                        case "restart":
                            ll.InitializeLabyrinth();
                            Console.Write(ll.ToString());
                            _continue = true;
                            m = 3;
                            n = 3;
                            Move();
                            break;
                        case "top":
                            topScores.ShowTopScores();
                            _continue = true;
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            _continue = true;
                            break;
                    }//end switch
                }//end if
                else
                {

                    switch (input)
                    {
                        case "L":
                            if (ll.IsPossibleCell(ll.PiecePositionRow, ll.PiecePositionCol-1))
                            {
                                ll.MovePieceLeft();
                                steps++;
                                if (ll.IsPieceOnEdge())
                                {
                                    Console.WriteLine("Congratulations! You escaped in {0} moves.", steps);
                                    ll.InitializeLabyrinth();
                                    Console.WriteLine("\n" + welcome);
                                    Console.WriteLine(enterMove);
                                    Console.Write(ll.ToString());
                                    Move();
                                }
                                else
                                {
                                    Console.Write(ll.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid move!");
                            }
                            break;
                        case "R":
                            if (ll.IsPossibleCell(ll.PiecePositionRow, ll.PiecePositionCol + 1))
                            {
                                ll.MovePieceRight();
                                steps++;
                                if (ll.IsPieceOnEdge())
                                {
                                    Console.WriteLine("Congratulations! You escaped in {0} moves.", steps);
                                    ll.InitializeLabyrinth();



                                    Console.WriteLine("\n" + welcome);
                                    Console.WriteLine(enterMove);
                                    Console.Write(ll.ToString());
                                    Move();
                                }
                                else
                                {
                                    Console.Write(ll.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid move!");
                            }
                            break;
                        case "U":
                            if (ll.IsPossibleCell(ll.PiecePositionRow - 1, ll.PiecePositionCol))
                            {
                                ll.MovePieceUp();



                                steps++;
                                if (ll.IsPieceOnEdge())
                                {
                                    Console.WriteLine("Congratulations! You escaped in {0} moves.", steps);
                                    ll.InitializeLabyrinth();
                                    Console.WriteLine("\n" + welcome);
                                    Console.WriteLine(enterMove);
                                    Console.Write(ll.ToString());
                                    Move();
                                }
                                else
                                {
                                    Console.Write(ll.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid move!");
                            }
                            break;
                        case "D":
                            if (ll.IsPossibleCell(ll.PiecePositionRow + 1, ll.PiecePositionCol))
                            {
                                ll.MovePieceDown();
                                steps++;
                                if (ll.IsPieceOnEdge())
                                {
                                    Console.WriteLine("Congratulations! You escaped in {0} moves.", steps);
                                    ll.InitializeLabyrinth();
                                    Console.WriteLine("\n" + welcome);
                                    Console.WriteLine(enterMove);
                                    Console.Write(ll.ToString());
                                    Move();
                                }
                                else
                                {
                                    Console.Write(ll.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid move!");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid move");
                            break;
                    }//end switch
                }//end else
            }//end while
        }//end Move method
    }//end class Labyrinth
}


