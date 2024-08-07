﻿using Newtonsoft.Json;

namespace AirSeaBattle.Game.Services.Configuration.Remote;

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
public class RemoteGameplayConfiguration
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
}
