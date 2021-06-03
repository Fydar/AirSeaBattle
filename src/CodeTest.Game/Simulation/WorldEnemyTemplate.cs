using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation
{
	public class WorldEnemyTemplate
	{
		public Fixed Width { get; set; } = Constants.One + Constants.Half;
		public Fixed Height { get; set; } = Constants.One;
		public Fixed Speed { get; set; } = Constants.One;
	}
}
