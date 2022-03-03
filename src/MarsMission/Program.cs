using MarsMission.Models;
using MarsMission.Services;

Console.WriteLine("Welcome to Mars Mission!");

var plateau = PlateauService.GetPlateau();

if (plateau == null)
    return;

var rovers = RoverService.GetRoversAndInstructions(plateau);

RoverService.Discover(rovers, plateau);

