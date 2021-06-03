using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation.Systems.PlayerControl
{
	public class PlayerControlConfiguration
	{
		public float PlayerHeightPercent { get; set; } = 0.125f;
		public float BulletSpeed { get; set; }
		public float BulletSize { get; set; }
		public float PlayerWidth { get; set; }
		public float PlayerHeight { get; set; }

		public GunPosition DefaultPosition { get; set; } = new GunPosition()
		{
			Graphic = "gun_60",
			Inclination = 60,
			BulletOffsetX = 0.6f,
			BulletOffsetY = 0.6f
		};

		public GunPosition UpPosition { get; set; } = new GunPosition()
		{
			Graphic = "gun_90",
			Inclination = 60,
			BulletOffsetX = 0.3f,
			BulletOffsetY = 0.9f
		};

		public GunPosition DownPosition { get; set; } = new GunPosition()
		{
			Graphic = "gun_30",
			Inclination = 30,
			BulletOffsetX = 0.9f,
			BulletOffsetY = 0.5f
		};
	}
}
