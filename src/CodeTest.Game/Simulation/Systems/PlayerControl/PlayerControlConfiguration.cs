using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;

namespace CodeTest.Game.Simulation.Systems.PlayerControl
{
	public class PlayerControlConfiguration
	{
		public Fixed BulletSpeed { get; set; } = Constants.One * 4;
		public Fixed BulletSize { get; set; } = Constants.One / 8;

		public WorldGunPosition DefaultPosition { get; set; } = new WorldGunPosition()
		{
			Graphic = "gun_60",
			Inclination = 60,
			BulletOffset = new FixedVector2(
				((Fixed)6) / 10,
				((Fixed)6) / 10
			)
		};

		public WorldGunPosition UpPosition { get; set; } = new WorldGunPosition()
		{
			Graphic = "gun_90",
			Inclination = 90,
			BulletOffset = new FixedVector2(
				((Fixed)17) / 40,
				((Fixed)6) / 10
			)
		};

		public WorldGunPosition DownPosition { get; set; } = new WorldGunPosition()
		{
			Graphic = "gun_30",
			Inclination = 30,
			BulletOffset = new FixedVector2(
				((Fixed)6) / 10,
				((Fixed)5) / 10
			)
		};
	}
}
