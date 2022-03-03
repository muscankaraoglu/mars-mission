using MarsMission.Models;
using MarsMission.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsMission.Tests
{
    public class MissionTests
    {
        Plateau? plateau;
        List<Rover>? rovers;
        [SetUp]
        public void Setup()
        {
            rovers = new List<Rover>();
        }

        [Test]
        public void TestPlateauCreation()
        {
            plateau = PlateauService.GetPlateau("5 5");
            Assert.IsNotNull(plateau);
            Assert.IsNull(PlateauService.GetPlateau("a a"));
            Assert.IsNull(PlateauService.GetPlateau("-1 -1"));
            Assert.Pass();
        }

        [Test]
        public void TestRoverCreation()
        {
            rovers = RoverService.GetRoversAndInstructions(new Plateau(5, 5), "2 2 E", "MMRMR");
            Assert.IsNotNull(rovers);
            Assert.AreEqual(1, rovers.Count);
            Assert.AreEqual(1, RoverService.GetRoversAndInstructions(new Plateau(5, 5), "1 1 E", "MM").Count);
            Assert.IsNull(RoverService.GetRoversAndInstructions(new Plateau(5, 5), "6 1 E", "MM"));
            Assert.Pass();
        }
    }
}