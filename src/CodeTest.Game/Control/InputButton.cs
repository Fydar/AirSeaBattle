namespace CodeTest.Game
{
	public class InputButton
	{
		private InputButtonState currentState;

		public InputButtonState CurrentState => currentState;

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
