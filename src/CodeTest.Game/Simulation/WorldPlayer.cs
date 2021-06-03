using System.Collections.ObjectModel;

namespace CodeTest.Game
{
	public class WorldPlayer
	{
		public int CurrentScore { get; set; }
		public ObservableCollection<WorldGun> ControlledGuns { get; } = new();
		public SimulationInput Input { get; }

		public WorldPlayer(SimulationInput input)
		{
			Input = input;
		}
	}
}
