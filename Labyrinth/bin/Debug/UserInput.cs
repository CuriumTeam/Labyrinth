using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeFromLabyrinth
{
    public class UserInput
    {
        public virtual string GetInput()
        {
           string inputString = Console.ReadLine();
           return inputString;
        }
    }
}
