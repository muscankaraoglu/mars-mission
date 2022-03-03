﻿using System;
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

        /// <summary>
        /// Gets plateau object by using stdin values.
        /// </summary>
        Plateau GetPlateau()
        {
            GetSize:
            //Get Plateau Size
            Console.Write("Please enter the plateau size:");
            string? widthAndHeight = Console.ReadLine();
            while (string.IsNullOrEmpty(widthAndHeight))
            {
                Console.WriteLine("Please enter size!");
                goto GetSize;
            }
            string[] dimensions = widthAndHeight.Split(" ");
            bool isValidLengthWidthAndHeight = dimensions.Length == 2;
            while (!isValidLengthWidthAndHeight)
            {
                Console.WriteLine("Please enter valid dimensions e.[X Y]!");
                goto GetSize;
            }
            int width, height;
            bool tryWidth = int.TryParse(dimensions[0], out width);
            bool tryHeight = int.TryParse(dimensions[1], out height);
            while (!tryWidth || !tryHeight)
            {
                Console.WriteLine("Please enter valid dimensions as integer e.[5 5]!");
                goto GetSize;
            }
            while (width < 1 || height < 1)
            {
                Console.WriteLine("Please enter valid dimensions minimum plateau width and height is 1 1!");
                goto GetSize;
            }
            return new Plateau(width, height);
        }
    }
}
