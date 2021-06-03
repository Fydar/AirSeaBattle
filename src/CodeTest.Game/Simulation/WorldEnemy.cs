using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation
{
	public class WorldEnemy
	{
		public World World { get; }
		public WorldEnemyTemplate Template { get; }
		public Fixed PositionX { get; set; }
		public int Layer { get; set; }
		public Fixed VelocityX { get; set; }

		public WorldEnemy(World world, WorldEnemyTemplate template)
		{
			World = world;
			Template = template;
		}
	}
}
