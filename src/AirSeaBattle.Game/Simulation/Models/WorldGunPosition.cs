using Industry.Simulation.Math;

namespace AirSeaBattle.Game.Simulation.Models
{
	/// <summary>
	/// Represents an angle that the <see cref="WorldGun"/> can be positioned in.
	/// </summary>
	public class WorldGunPosition
	{
		/// <summary>
		/// A string used to determine what graphics to be used to render the gun.
		/// </summary>
		public string Graphic { get; set; } = string.Empty;

		/// <summary>
		/// An angle used to calculate the vector of the fired projectile.
		/// </summary>
		public Fixed Inclination { get; set; }

		/// <summary>
		/// The starting position of the bullet (in normalised space of the gun bounds).
		/// </summary>
		public FixedVector2 BulletOffset { get; set; }
	}
}
