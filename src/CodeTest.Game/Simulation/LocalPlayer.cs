using System;

namespace CodeTest.Game.Simulation
{
	public class LocalPlayer
	{
		public Guid Identifer { get; set; }
		public int HighScore { get; set; }
		public SimulationInput Input { get; }

		public LocalPlayer(SimulationInput input)
		{
			Input = input;
		}
	}
}
