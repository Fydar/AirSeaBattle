namespace AirSeaBattle.Game.Control;

/// <summary>
/// A model for accepting user input in the form of a button.
/// </summary>
public class InputButton
{
    /// <summary>
    /// The current state of this <see cref="InputButton"/>.
    /// </summary>
    public InputButtonState CurrentState { get; private set; }

    /// <summary>
    /// <c>true</c> whilst this <see cref="InputButton"/> is <see cref="InputButtonState.Pressed"/> or <see cref="InputButtonState.Held"/>; otherwise <c>false</c>.
    /// </summary>
    public bool IsDown => CurrentState is InputButtonState.Pressed
        or InputButtonState.Held;

    /// <summary>
    /// Advance the state of the button.
    /// </summary>
    internal void Update()
    {
        if (CurrentState == InputButtonState.Pressed)
        {
            CurrentState = InputButtonState.Held;
        }
        else if (CurrentState == InputButtonState.Released)
        {
            CurrentState = InputButtonState.Unpressed;
        }
    }

    /// <summary>
    /// Inform this <see cref="InputButton"/> that it should represent a <see cref="InputButtonState.Pressed"/> state.
    /// </summary>
    public void SimulateButtonDown()
    {
        CurrentState = InputButtonState.Pressed;
    }

    /// <summary>
    /// Inform this <see cref="InputButton"/> that it should represent a <see cref="InputButtonState.Released"/> state.
    /// </summary>
    public void SimulateButtonUp()
    {
        CurrentState = InputButtonState.Released;
    }
}
