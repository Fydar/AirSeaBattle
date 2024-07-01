using AirSeaBattle.Game.Control;

namespace AirSeaBattle.Game.Simulation;

/// <summary>
/// A collection of controls for the game simulation.
/// </summary>
public class SimulationInput
{
    /// <summary>
    /// A button for "up" input.
    /// </summary>
    public InputButton Up { get; } = new();

    /// <summary>
    /// A button for "down" input.
    /// </summary>
    public InputButton Down { get; } = new();

    /// <summary>
    /// A button for "fire" input.
    /// </summary>
    public InputButton Fire { get; } = new();
}
