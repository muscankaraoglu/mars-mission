using MarsMission.Models;

namespace MarsMission.Services
{
    public static class PlateauService
    {
        /// <summary>
        /// Gets plateau object by using stdin values.
        /// </summary>
        public static Plateau? GetPlateau(string? widthAndHeightInput = null)
        {
            GetSize:
            //Get Plateau Size
            Console.Write("Please enter the plateau size:");
            bool isInputable = string.IsNullOrEmpty(widthAndHeightInput);
            string? widthAndHeight = string.IsNullOrEmpty(widthAndHeightInput) ? Console.ReadLine() : widthAndHeightInput;
            while (string.IsNullOrEmpty(widthAndHeight))
            {
                Console.WriteLine("Please enter size!");
                if(isInputable)
                    goto GetSize;
                return null;
            }
            string[] dimensions = widthAndHeight.Split(" ");
            bool isValidLengthWidthAndHeight = dimensions.Length == 2;
            while (!isValidLengthWidthAndHeight)
            {
                Console.WriteLine("Please enter valid dimensions e.[X Y]!");
                if (isInputable)
                    goto GetSize;
                return null;
            }
            int width, height;
            bool tryWidth = int.TryParse(dimensions[0], out width);
            bool tryHeight = int.TryParse(dimensions[1], out height);
            while (!tryWidth || !tryHeight)
            {
                Console.WriteLine("Please enter valid dimensions as integer e.[5 5]!");
                if (isInputable)
                    goto GetSize;
                return null;
            }
            while (width < 1 || height < 1)
            {
                Console.WriteLine("Please enter valid dimensions minimum plateau width and height is 1 1!");
                if (isInputable)
                    goto GetSize;
                return null;
            }
            return new Plateau(width, height);
        }
    }
}
