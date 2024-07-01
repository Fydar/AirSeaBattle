using Industry.Simulation.Math;
using RPGCore.Events;
using System;

namespace AirSeaBattle.Game.Simulation.Models;

/// <summary>
/// Represents a moving projectile in the <see cref="World"/>.
/// </summary>
public class WorldProjectile
{
    /// <summary>
    /// A unique identifier for this <see cref="WorldProjectile"/>.
    /// </summary>
    public Guid Identifier { get; }

    /// <summary>
    /// The <see cref="WorldGun"/> that fired this <see cref="WorldProjectile"/>.
    /// </summary>
    public WorldGun Owner { get; }

    /// <summary>
    /// The current position of this projectile.
    /// </summary>
    public EventField<FixedVector2> Position { get; } = new();

    /// <summary>
    /// The velocity of the project (how fast it will move every frame).
    /// </summary>
    public EventField<FixedVector2> Velocity { get; } = new();

    /// <summary>
    /// The world that this entity belongs to.
    /// </summary>
    public World World => Owner.World;

    /// <summary>
    /// Creates a new instance of the <see cref="WorldProjectile"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="WorldGun"/> that shot this projectile.</param>
    public WorldProjectile(WorldGun owner)
    {
        Identifier = Guid.NewGuid();
        Owner = owner;
    }
}
