﻿using CodeTest.Game.Math;

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
				Constants.One * 6 / 10,
				Constants.One * 6 / 10
			)
		};

		public WorldGunPosition UpPosition { get; set; } = new WorldGunPosition()
		{
			Graphic = "gun_90",
			Inclination = 90,
			BulletOffset = new FixedVector2(
				Constants.One * 3 / 10,
				Constants.One * 9 / 10
			)
		};

		public WorldGunPosition DownPosition { get; set; } = new WorldGunPosition()
		{
			Graphic = "gun_30",
			Inclination = 30,
			BulletOffset = new FixedVector2(
				Constants.One * 9 / 10,
				Constants.One * 3 / 10
			)
		};
	}
}
