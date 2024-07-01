using AirSeaBattle.Game.Simulation.Models;

namespace AirSeaBattle.Game.Simulation;

/// <summary>
/// A collection of mechanics and systems that a <see cref="World"/> can be created from.
/// </summary>
public class WorldEngine
{
    internal readonly IWorldSystemFactory[] worldSystems;

    internal WorldEngine(IWorldSystemFactory[] worldSystems)
    {
        this.worldSystems = worldSystems;
    }

    /// <summary>
    /// Constructs a new <see cref="World"/> from this <see cref="WorldEnemy"/>.
    /// </summary>
    /// <returns>A <see cref="WorldBuilder"/> that can be used to build a new <see cref="World"/> instance.</returns>
    public WorldBuilder ConstructWorld()
    {
        return new WorldBuilder(this);
    }
}
