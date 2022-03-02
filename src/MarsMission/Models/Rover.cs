using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsMission.Models
{
    public class Rover
    {
        public Rover(int x, int y, char direction,string instructions)
        {
            X = x;
            Y = y;
            Direction = Enum.Parse<Direction>(direction.ToString());
            Instructions = instructions;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
        public string Instructions { get; set; }
    }
}
