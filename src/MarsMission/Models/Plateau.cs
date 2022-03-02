using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsMission.Models
{
    public class Plateau
    {
        public Plateau(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
