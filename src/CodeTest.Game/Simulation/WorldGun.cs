using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation
{
	public class WorldGun
	{
		public World World { get; }
		public WorldGunPosition Angle { get; }
		public Fixed PositionX { get; set; }
		public bool IsFlipped { get; set; }

		public WorldGun(World world, WorldGunPosition angle)
		{
			World = world;
			Angle = angle;
		}
	}
}
