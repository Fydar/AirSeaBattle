using RPGCore.Events;
using System;

namespace CodeTest.Game.Simulation
{
	public class WorldPlayer
	{
		public Guid Identifier { get; }
		public World World { get; }
		public SimulationInput Input { get; }
		public EventField<int> CurrentScore { get; } = new();
		public EventDictionary<Guid, WorldGun> ControlledGuns { get; } = new();

		public WorldPlayer(World world, SimulationInput input)
		{
			Identifier = Guid.NewGuid();
			World = world;
			Input = input;
		}
	}
}
