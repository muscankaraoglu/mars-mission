using MarsMission.Models;
//TODO: Seperate program by methods
Console.WriteLine("Welcome to Mars Mission!");
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
Plateau plateau = new Plateau(width, height);

List<Rover> rovers = new List<Rover>();

//Get Initial Rover Position or Discover Command
GetInitialPosition:
Console.Write("Please enter the initial rover position or type \"go\" for start discovering the Mars:");
string? initialRoverPosition = Console.ReadLine();
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
        goto GetInitialPosition;
    }
    int locationX, locationY;
    char direction;
    bool tryLocationX = int.TryParse(positions[0], out locationX);
    bool tryLocationY = int.TryParse(positions[1], out locationY);
    bool tryDirection = char.TryParse(positions[2], out direction);
    while (!tryLocationX || !tryLocationY || !tryDirection)
    {
        Console.WriteLine("Please enter valid position as integer and char e.[5 5 N]!");
        goto GetInitialPosition;
    }
    while (rovers.Any(i => i.X == locationX && i.Y == locationY))
    {
        Console.WriteLine("There is already a rover existing. Please enter another initial location e.[5 5 N]!");
        goto GetInitialPosition;
    }
    bool isValidDirectionKey = direction == 'N' || direction == 'S' || direction == 'W' || direction == 'E';
    while (!isValidDirectionKey)
    {
        Console.WriteLine("Please enter valid direction key e.[E] (N,S,E,W)!");
        goto GetInitialPosition;
    }
    bool isInitialLocationValid = locationX <= plateau.Width && locationY <= plateau.Height;
    while (!isInitialLocationValid)
    {
        Console.WriteLine("Please enter valid position, provided position is out of bounds!");
        goto GetInitialPosition;
    }

//Get instructions for the Rover
GetInstructions:
    Console.Write("Please enter the instructions: ");
    string? instructions = Console.ReadLine();
    while (string.IsNullOrEmpty(instructions))
    {
        Console.WriteLine("Please enter instructions!");
        goto GetInstructions;
    }
    instructions = instructions.ToUpper();
    Rover rover = new(locationX, locationY, direction, instructions);
    rovers.Add(rover);
    goto GetInitialPosition;
}
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
                    currentDirection = currentDirection + 1;
                else
                    currentDirection = Direction.N;
                break;
            case 'L':
                if ((int)currentDirection >= 1)
                    currentDirection = currentDirection - 1;
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
                            Console.Error.WriteLine("Rover #{0} has fall :( Mission Failed", i);
                            incomplete = true;
                            break;
                        }
                        break;
                    case Direction.E:
                        currentX += 1;
                        if (currentX > plateau.Width)
                        {
                            Console.Error.WriteLine("Rover #{0} has fall :( Mission Failed", i);
                            incomplete = true;
                            break;
                        }
                        break;
                    case Direction.S:
                        currentY -= 1;
                        if (currentY < 0)
                        {
                            Console.Error.WriteLine("Rover #{0} has fall :( Mission Failed", i);
                            incomplete = true;
                            break;
                        }
                        break;
                    case Direction.W:
                        currentX -= 1;
                        if (currentY < 0)
                        {
                            Console.Error.WriteLine("Rover #{0} has fall :( Mission Failed", i);
                            incomplete = true;
                            break;
                        }
                        break;
                    default:
                        break;
                }
                if (rovers.Any(i => i.X == currentX && i.Y == currentY))
                {
                    Console.Error.WriteLine("Your rovers has crashed :(");
                    incomplete = true;
                    break;
                }
                break;
            default:
                Console.Error.WriteLine("Instructions has invalid command character. It's ignored.");
                break;
        }
    }
    if (!incomplete)
    {
        //Place up the rover
        rovers[i].X = currentX;
        rovers[i].Y = currentY;
        Console.WriteLine("{0}. instructions has completed, latest location is:{1} {2} {3}", i, currentX, currentY, currentDirection);
    }
}