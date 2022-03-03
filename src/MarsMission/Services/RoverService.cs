using MarsMission.Models;

namespace MarsMission.Services
{
    public static class RoverService
    {
        /// <summary>
        /// Gets rover(s) and their instructions.
        /// </summary>
        /// <param name="plateau">The plateau to be discovered.</param>
        /// <returns></returns>
        public static List<Rover> GetRoversAndInstructions(Plateau plateau, string? initialRoverPositionInput = null, string? instructionsInput = null)
        {
            List<Rover> rovers = new();

//Get Initial Rover Position or "GO" Command
GetInitialPosition:
            Console.Write("Please enter the initial rover position or type \"go\" for start discovering the Mars:");
            bool isInputable = string.IsNullOrEmpty(initialRoverPositionInput) || string.IsNullOrEmpty(instructionsInput);
            string? initialRoverPosition = string.IsNullOrEmpty(initialRoverPositionInput) ? Console.ReadLine() : initialRoverPositionInput;
            while (string.IsNullOrEmpty(initialRoverPosition))
            {
                Console.WriteLine("Please enter position!");
                goto GetInitialPosition;
            }
            initialRoverPosition = initialRoverPosition.ToUpper();
            while (initialRoverPosition != "GO")
            {
                string[] positions = initialRoverPosition.Split(" ");
                bool isValidLengthPosition = positions.Length == 3;
                while (!isValidLengthPosition)
                {
                    Console.WriteLine("Please enter valid position e.[X Y Direction]!");
                    if (isInputable)
                        goto Exit;
                    goto GetInitialPosition;
                }
                bool tryLocationX = int.TryParse(positions[0], out int locationX);
                bool tryLocationY = int.TryParse(positions[1], out int locationY);
                bool tryDirection = char.TryParse(positions[2], out char direction);
                while (!tryLocationX || !tryLocationY || !tryDirection)
                {
                    Console.WriteLine("Please enter valid position as integer and char e.[5 5 N]!");
                    if (isInputable)
                        goto Exit;
                    goto GetInitialPosition;
                }
                while (rovers.Any(i => i.X == locationX && i.Y == locationY))
                {
                    Console.WriteLine("There is a rover already existing this coordinates. Please enter another initial location!");
                    if (isInputable)
                        goto Exit;
                    goto GetInitialPosition;
                }
                bool isValidDirectionKey = direction == 'N' || direction == 'S' || direction == 'W' || direction == 'E';
                while (!isValidDirectionKey)
                {
                    Console.WriteLine("Please enter valid direction key e.[E] (N,S,E,W)!");
                    if (isInputable)
                        goto Exit;
                    goto GetInitialPosition;
                }
                bool isInitialLocationValid = locationX <= plateau.Width && locationY <= plateau.Height;
                while (!isInitialLocationValid)
                {
                    Console.WriteLine("Please enter valid position, provided position is out of bounds!");
                    if (isInputable)
                        goto Exit;
                    goto GetInitialPosition;
                }

//Get instructions for the Rover
GetInstructions:
                Console.Write("Please enter the instructions: ");
                string? instructions = string.IsNullOrEmpty(instructionsInput) ? Console.ReadLine() : instructionsInput;
                while (string.IsNullOrEmpty(instructions))
                {
                    Console.WriteLine("Please enter instructions!");
                    if (isInputable)
                        goto Exit;
                    goto GetInstructions;
                }
                instructions = instructions.ToUpper();
                Rover rover = new(locationX, locationY, direction, instructions);
                rovers.Add(rover);
                if (!isInputable)
                    goto Exit;
                goto GetInitialPosition;
            }
Exit:
            return rovers;
        }

        /// <summary>
        /// The discover command for rovers.
        /// </summary>
        /// <param name="rovers">Deployed rovers.</param>
        /// <param name="plateau">The plateau to be discovered.</param>
        public static void Discover(List<Rover> rovers, Plateau plateau)
        {
            if (rovers.Count == 0)
            {
                Console.WriteLine("You have not send any rovers to Mars");
                return;
            }
            //Start the discovery
            for (int i = 0; i < rovers.Count; i++)
            {
                Rover rover = rovers[i];
                string instructions = rover.Instructions;
                int currentX = rover.X, currentY = rover.Y;
                Direction currentDirection = rover.Direction;
                bool incomplete = false;
                for (int j = 0; j < instructions.Length; j++)
                {
                    switch (instructions[j])
                    {
                        case 'R':
                            if ((int)currentDirection <= 2)
                                currentDirection++;
                            else
                                currentDirection = Direction.N;
                            break;
                        case 'L':
                            if ((int)currentDirection >= 1)
                                currentDirection--;
                            else
                                currentDirection = Direction.W;
                            break;
                        case 'M':
                            switch (currentDirection)
                            {
                                case Direction.N:
                                    currentY += 1;
                                    if (currentY > plateau.Height)
                                    {
                                        Fall(i);
                                        return;
                                    }
                                    break;
                                case Direction.E:
                                    currentX += 1;
                                    if (currentX > plateau.Width)
                                    {
                                        Fall(i);
                                        return;
                                    }
                                    break;
                                case Direction.S:
                                    currentY -= 1;
                                    if (currentY < 0)
                                    {
                                        Fall(i);
                                        return;
                                    }
                                    break;
                                case Direction.W:
                                    currentX -= 1;
                                    if (currentY < 0)
                                    {
                                        Fall(i);
                                        return;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            if (rovers.Any(i => i.X == currentX && i.Y == currentY))
                            {
                                Console.Error.WriteLine("Your rovers has crashed :( There is a rover at these coordinates {0} {1}",currentX, currentY);
                                incomplete = true;
                                return;
                            }
                            rovers[i].X = currentX;
                            rovers[i].Y = currentY;
                            rovers[i].Direction = currentDirection;
                            break;
                        default:
                            Console.Error.WriteLine("Instructions has invalid command character. It's ignored.");
                            break;
                    }
                }
                if (!incomplete)
                {
                    //Place up the rover
                    Console.WriteLine("Instruction {0} has completed, Rover #{0} latest location is:{1} {2} {3}", i, currentX, currentY, currentDirection);
                }
            }
        }
        private static void Fall(int indexOfRover)
        {
            Console.Error.WriteLine("Rover #{0} has fall :( Mission Failed", indexOfRover);
        }
    }
}
