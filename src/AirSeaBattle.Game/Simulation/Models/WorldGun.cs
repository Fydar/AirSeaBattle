using Industry.Simulation.Math;
using RPGCore.Events;
using System;

namespace AirSeaBattle.Game.Simulation.Models;

/// <summary>
/// Represents a gun in the <see cref="World"/>.
/// </summary>
public class WorldGun
{
    /// <summary>
    /// A unique identifier for this <see cref="WorldGun"/>.
    /// </summary>
    public Guid Identifier { get; }

    /// <summary>
    /// The world that this entity belongs to.
    /// </summary>
    public WorldPlayer Player { get; }

    /// <summary>
    /// The current <see cref="WorldGunPosition"/> that this <see cref="WorldGun"/> is in.
    /// </summary>
    public EventField<WorldGunPosition> Angle { get; } = new();

    /// <summary>
    /// The horizontal position of the gun in the <see cref="World"/>.
    /// </summary>
    public EventField<Fixed> PercentX { get; } = new();

    /// <summary>
    /// The position of this <see cref="WorldGun"/> in the <see cref="World"/>.
    /// </summary>
    public FixedVector2 Position => new(PercentX.Value * World.WorldWidth, World.Configuration.GunHeightPercent * World.WorldHeight);

    /// <summary>
    /// Bounds used to calculate intersections between this <see cref="WorldGun"/> and other entities.
    /// </summary>
    public FixedAABox Bounds => new(Position, World.Configuration.GunSize);

    /// <summary>
    /// The world that this entity belongs to.
    /// </summary>
    public World World => Player.World;

    /// <summary>
    /// Creates a new instance of the <see cref="WorldGun"/> class.
    /// </summary>
    /// <param name="player">The <see cref="WorldPlayer"/> that owns this <see cref="WorldGun"/>.</param>
    /// <param name="angle">The default angle of the gun.</param>
    public WorldGun(WorldPlayer player, WorldGunPosition angle)
    {
        Identifier = Guid.NewGuid();
        Player = player;
        Angle.Value = angle;
    }
}
