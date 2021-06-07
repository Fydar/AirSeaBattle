namespace AirSeaBattle.Game.Control
{
	/// <summary>
	/// A model for accepting user input in the form of a button.
	/// </summary>
	public class InputButton
	{
		private InputButtonState currentState;

		/// <summary>
		/// The current state of this <see cref="InputButton"/>.
		/// </summary>
		public InputButtonState CurrentState => currentState;

		/// <summary>
		/// <c>true</c> whilst this <see cref="InputButton"/> is <see cref="InputButtonState.Pressed"/> or <see cref="InputButtonState.Held"/>; otherwise <c>false</c>.
		/// </summary>
		public bool IsDown => currentState == InputButtonState.Pressed
			|| currentState == InputButtonState.Held;

		/// <summary>
		/// Advance the state of the button.
		/// </summary>
		internal void Update()
		{
			if (currentState == InputButtonState.Pressed)
			{
				currentState = InputButtonState.Held;
			}
			else if (currentState == InputButtonState.Released)
			{
				currentState = InputButtonState.Unpressed;
			}
		}

		/// <summary>
		/// Inform this <see cref="InputButton"/> that it should represent a <see cref="InputButtonState.Pressed"/> state.
		/// </summary>
		public void SimulateButtonDown()
		{
			currentState = InputButtonState.Pressed;
		}

		/// <summary>
		/// Inform this <see cref="InputButton"/> that it should represent a <see cref="InputButtonState.Released"/> state.
		/// </summary>
		public void SimulateButtonUp()
		{
			currentState = InputButtonState.Released;
		}
	}
}
