using Newtonsoft.Json;

namespace CodeTest.Game.Services.Configuration
{
	/// <summary>
	/// A class representing web-hosted game configuration.
	/// </summary>
	/// <example>
	/// When serialized, this data looks something like:
	/// <code lang="json">
	/// {
	///   "time_limit": 60,
	///   "default_high_score": 35,
	///   "points_per_plane": 5,
	///   "id": "config"
	/// }
	/// </code>
	/// </example>
	public class GameplayConfiguration
	{
		/// <summary>
		/// How long (in seconds) the player has per game.
		/// </summary>
		[JsonProperty("time_limit")]
		public int? TimeLimit { get; set; } = 60;

		/// <summary>
		/// A default value for the game highscore.
		/// </summary>
		[JsonProperty("default_high_score")]
		public int? DefaultHighScore { get; set; } = 35;

		/// <summary>
		/// How many points should be awarded to the player apon destroying a plane.
		/// </summary>
		[JsonProperty("points_per_plane")]
		public int? PointsPerPlane { get; set; } = 5;

		/// <summary>
		/// An identifier for this gameplay configuration.
		/// </summary>
		[JsonProperty("id")]
		public string? Id { get; set; } = "config";

		/// <summary>
		/// Overwrites configuration values that are not <c>null</c> in <c>this</c> <see cref="GameplayConfiguration"/>.
		/// </summary>
		/// <param name="other">The <see cref="GameplayConfiguration"/> to source values from.</param>
		public void ConfigureFrom(GameplayConfiguration other)
		{
			TimeLimit = other.TimeLimit ?? TimeLimit;
			DefaultHighScore = other.DefaultHighScore ?? DefaultHighScore;
			PointsPerPlane = other.PointsPerPlane ?? PointsPerPlane;
			Id = other.Id ?? Id;
		}
	}
}
