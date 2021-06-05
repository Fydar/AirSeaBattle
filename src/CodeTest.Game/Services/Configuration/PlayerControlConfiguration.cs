using Industry.Simulation.Math;
using CodeTest.Game.Simulation.Models;

namespace CodeTest.Game.Services.Configuration
{
	/// <summary>
	/// Configuration for player controls.
	/// </summary>
	public class PlayerControlConfiguration
	{
		/// <summary>
		/// Speed that projectiles shot from the player should travel at.
		/// </summary>
		public Fixed BulletSpeed { get; set; } = Constants.One * 4;

		/// <summary>
		/// The default position for guns.
		/// </summary>
		public WorldGunPosition DefaultPosition { get; set; } = new WorldGunPosition()
		{
			Graphic = "gun_60",
			Inclination = 60,
			BulletOffset = new FixedVector2(
				((Fixed)6) / 10,
				((Fixed)6) / 10
			)
		};

		/// <summary>
		/// The upwards position for guns.
		/// </summary>
		public WorldGunPosition UpPosition { get; set; } = new WorldGunPosition()
		{
			Graphic = "gun_90",
			Inclination = 90,
			BulletOffset = new FixedVector2(
				((Fixed)17) / 40,
				((Fixed)6) / 10
			)
		};

		/// <summary>
		/// The downwards position for guns.
		/// </summary>
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
