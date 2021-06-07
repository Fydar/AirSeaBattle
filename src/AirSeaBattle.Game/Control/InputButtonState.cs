namespace AirSeaBattle.Game.Control
{
	/// <summary>
	/// An enum representing the states of an <see cref="InputButton"/>.
	/// </summary>
	public enum InputButtonState
	{
		/// <summary>
		/// The button is not pressed.
		/// </summary>
		Unpressed,

		/// <summary>
		/// The button has recently been pressed.
		/// </summary>
		Pressed,

		/// <summary>
		/// The button was previously <see cref="Pressed"/> and is now being held down.
		/// </summary>
		Held,

		/// <summary>
		/// The button was previously <see cref="Pressed"/> and is now being released.
		/// </summary>
		Released
	}
}
