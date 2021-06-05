using CodeTest.Game.Simulation.Models;
using Industry.Simulation.Math;

namespace CodeTest.Game.Services.Configuration
{
	/// <summary>
	/// Configuration for enemy spawning behaviour
	/// </summary>
	public class EnemySpawnerConfiguration
	{
		/// <summary>
		/// Minimum amount of enemies to be spawned every wave.
		/// </summary>
		public int MinEnemies { get; set; } = 3;

		/// <summary>
		/// Maximum amount of enemies to be spawned every wave.
		/// </summary>
		public int MaxEnemies { get; set; } = 5;

		/// <summary>
		/// Time delay between each round.
		/// </summary>
		public Fixed DelayBetweenRounds { get; set; } = Constants.Half;

		/// <summary>
		/// Amount of layers that enemies can spawn over.
		/// </summary>
		public int LayersCount { get; set; } = 10;

		/// <summary>
		/// The minimum altitude for enemies.
		/// </summary>
		public Fixed MinimumAltitudePercent { get; set; } = Constants.One / 6;

		/// <summary>
		/// The enemy that the spawner should use.
		/// </summary>
		public WorldEnemyTemplate Enemy { get; set; } = new()
		{
			Speed = Constants.One,
			Height = ((Fixed)6) / 10,
			Width = ((Fixed)18) / 10
		};
	}
}
