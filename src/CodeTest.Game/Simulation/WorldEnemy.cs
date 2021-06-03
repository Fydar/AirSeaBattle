using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation
{
	public class WorldEnemy
	{
		public WorldEnemyTemplate Template { get; }
		public float PositionX { get; set; }
		public int Layer { get; set; }
		public float VelocityX { get; set; }

		public WorldEnemy(WorldEnemyTemplate template)
		{
			Template = template;
		}
	}
}
