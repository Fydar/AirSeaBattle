using AirSeaBattle.Game.Simulation.Models;

namespace AirSeaBattle.Game.Simulation;

/// <summary>
/// A system that drives gameplay and behaviour.
/// </summary>
public interface IWorldSystem
{
    /// <summary>
    /// A callback that allows <see cref="IWorldSystem"/> implementors to modify the <see cref="World"/>
    /// state when a <see cref="WorldPlayer"/> joins the world.
    /// </summary>
    /// <param name="worldPlayer">A model representing the new player that joined the world.</param>
    void OnPlayerJoined(WorldPlayer worldPlayer);

    /// <summary>
    /// A callback that allows <see cref="IWorldSystem"/> implementors to modify the <see cref="World"/>
    /// state when a <see cref="WorldPlayer"/> leaves the world.
    /// </summary>
    /// <param name="worldPlayer">A model representing the player that left the world.</param>
    void OnPlayerRemoved(WorldPlayer worldPlayer);

    /// <summary>
    /// A callback that allows <see cref="IWorldSystem"/> implementors to modify the <see cref="World"/>
    /// state periodically.
    /// </summary>
    /// <param name="parameters">Parameters for the update.</param>
    void OnUpdate(UpdateParameters parameters);
}
