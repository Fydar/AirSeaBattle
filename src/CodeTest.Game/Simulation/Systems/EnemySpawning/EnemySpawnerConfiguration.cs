using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation.Systems.EnemySpawning
{
	public class EnemySpawnerConfiguration
	{
		public int MinEnemies { get; set; } = 3;
		public int MaxEnemies { get; set; } = 5;
		public Fixed DelayBetweenRounds { get; set; } = Constants.Half;

		// I took a look at the original game. It looks like 1/12th of the screen was reserved for
		// the players and there was 10 possible spawning altitudes.
		public int LayersCount { get; set; } = 10;
		public Fixed MinimumAltitudePercent { get; set; } = Constants.One / 12;

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
