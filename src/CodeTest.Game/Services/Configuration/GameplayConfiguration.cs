using Industry.Simulation.Math;
using CodeTest.Game.Simulation.Systems.EnemySpawning;

namespace CodeTest.Game.Services.Configuration
{
	/// <summary>
	/// A class representing game configuration.
	/// </summary>
	public class GameplayConfiguration
	{
		/// <summary>
		/// How long (in seconds) the player has per game.
		/// </summary>
		public Fixed TimeLimit { get; set; } = 60;

		/// <summary>
		/// A default value for the game highscore.
		/// </summary>
		public int DefaultHighScore { get; set; } = 35;

		/// <summary>
		/// How many points should be awarded to the player apon destroying a plane.
		/// </summary>
		public int PointsPerPlane { get; set; } = 5;

		/// <summary>
		/// An identifier for this gameplay configuration.
		/// </summary>
		public string Id { get; set; } = "config";

		/// <summary>
		/// Diamentions for the gun.
		/// </summary>
		public FixedVector2 GunSize { get; set; } = FixedVector2.One;

		/// <summary>
		/// Height in the world (as a percent) that player guns are placed.
		/// </summary>
		public Fixed GunHeightPercent { get; set; } = Constants.One / 8;

		/// <summary>
		/// Configuration for enemy spawning behaviour.
		/// </summary>
		public EnemySpawnerConfiguration EnemySpawning { get; set; } = new();

		/// <summary>
		/// Configuration for player controls.
		/// </summary>
		public PlayerControlConfiguration PlayerControl { get; set; } = new();

		/// <summary>
		/// Overwrites configuration values that are not <c>null</c> in <c>this</c> <see cref="GameplayConfiguration"/>.
		/// </summary>
		/// <param name="other">The <see cref="GameplayConfiguration"/> to source values from.</param>
		public void ConfigureFrom(RemoteGameplayConfiguration other)
		{
			TimeLimit = other.TimeLimit ?? TimeLimit;
			DefaultHighScore = other.DefaultHighScore ?? DefaultHighScore;
			PointsPerPlane = other.PointsPerPlane ?? PointsPerPlane;
			Id = other.Id ?? Id;
		}
	}
}
