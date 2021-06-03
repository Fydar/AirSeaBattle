using System.Collections.ObjectModel;

namespace CodeTest.Game.Simulation
{
	public class WorldPlayer
	{
		public World World { get; }
		public int CurrentScore { get; set; }
		public ObservableCollection<WorldGun> ControlledGuns { get; } = new();
		public SimulationInput Input { get; }

		public WorldPlayer(World world, SimulationInput input)
		{
			World = world;
			Input = input;
		}
	}
}
