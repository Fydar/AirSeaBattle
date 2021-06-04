using RPGCore.Events;

namespace CodeTest.Game.Simulation
{
	public class LocalPlayer
	{
		public SimulationInput Input { get; }
		public EventField<int> HighScore { get; } = new();

		public LocalPlayer(SimulationInput input)
		{
			Input = input;
		}
	}
}
