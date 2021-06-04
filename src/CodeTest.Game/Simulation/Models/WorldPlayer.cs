using RPGCore.Events;
using System;

namespace CodeTest.Game.Simulation.Models
{
	public class WorldPlayer
	{
		public Guid Identifier { get; }
		public World World { get; }
		public LocalPlayer Player { get; }
		public SimulationInput Input { get; }
		public EventField<int> CurrentScore { get; } = new();
		public EventDictionary<Guid, WorldGun> ControlledGuns { get; } = new();

		public WorldPlayer(World world, LocalPlayer player)
		{
			Identifier = Guid.NewGuid();
			World = world;
			Player = player;
			Input = player.Input;
		}
	}
}
