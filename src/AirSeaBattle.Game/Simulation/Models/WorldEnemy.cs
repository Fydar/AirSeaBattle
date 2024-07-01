using Industry.Simulation.Math;
using RPGCore.Events;
using System;

namespace AirSeaBattle.Game.Simulation.Models;

/// <summary>
/// Represents a moving enemy in the <see cref="World"/>.
/// </summary>
public class WorldEnemy
{
    /// <summary>
    /// An event that's fired when this <see cref="WorldEnemy"/> is destroyed.
    /// </summary>
    public event Action? OnDestroyed;

    /// <summary>
    /// A unique identifier for this <see cref="WorldEnemy"/>.
    /// </summary>
    public Guid Identifier { get; }

    /// <summary>
    /// The world that this entity belongs to.
    /// </summary>
    public World World { get; }

    /// <summary>
    /// Provides shared characteristics for this type of <see cref="WorldEnemy"/>.
    /// </summary>
    public WorldEnemyTemplate Template { get; }

    /// <summary>
    /// The current position of the <see cref="WorldEnemy"/> in the <see cref="World"/>.
    /// </summary>
    public EventField<FixedVector2> Position { get; } = new();

    /// <summary>
    /// The horizontal velocity of this <see cref="WorldEnemy"/>.
    /// </summary>
    public EventField<Fixed> VelocityX { get; } = new();

    /// <summary>
    /// Bounds used to detect collisions between this <see cref="WorldEnemy"/> and other entities (e.g. projectiles).
    /// </summary>
    public FixedAABox Bounds => new(Position.Value, new(Template.Width / 2, Template.Height / 2));

    /// <summary>
    /// Creates a new instance of the <see cref="WorldEnemy"/> class.
    /// </summary>
    /// <param name="world">The <see cref="World"/> that this <see cref="WorldEnemy"/> belongs to.</param>
    /// <param name="template">Shared characteristics for the new <see cref="WorldEnemy"/>.</param>
    public WorldEnemy(World world, WorldEnemyTemplate template)
    {
        Identifier = Guid.NewGuid();
        World = world;
        Template = template;
    }

    /// <summary>
    /// Invokes the <see cref="OnDestroyed"/> event.
    /// </summary>
    public void InvokeOnDestroyed()
    {
        OnDestroyed?.Invoke();
    }
}
