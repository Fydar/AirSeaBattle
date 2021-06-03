using CodeTest.Game.Control;

namespace CodeTest.Game.Simulation
{
	public class SimulationInput
	{
		public InputButton Up { get; } = new();
		public InputButton Down { get; } = new();
		public InputButton Fire { get; } = new();
	}
}
