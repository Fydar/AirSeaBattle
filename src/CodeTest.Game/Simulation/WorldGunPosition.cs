using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation
{
	public class WorldGunPosition
	{
		public string Graphic { get; set; } = string.Empty;
		public Fixed Inclination { get; set; }
		public FixedVector2 BulletOffset { get; set; }
	}
}
