using Industry.Simulation.Math;

namespace CodeTest.Game.Simulation.Models
{
	/// <summary>
	/// Represents shared characteristics for classifications of enemies.
	/// </summary>
	public class WorldEnemyTemplate
	{
		/// <summary>
		/// The collider width of <see cref="WorldEnemy"/>s that use this template.
		/// </summary>
		public Fixed Width { get; set; } = Constants.One + Constants.Half;

		/// <summary>
		/// The collider width of <see cref="WorldEnemy"/>s that use this template.
		/// </summary>
		public Fixed Height { get; set; } = Constants.One;

		/// <summary>
		/// The speed of <see cref="WorldEnemy"/>s that use this template.
		/// </summary>
		public Fixed Speed { get; set; } = Constants.One;
	}
}
