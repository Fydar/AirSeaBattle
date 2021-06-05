using RPGCore.Events;

namespace CodeTest.Game.Simulation
{
	/// <summary>
	/// A model representing a player and their interaction with the game state.
	/// </summary>
	public class LocalPlayer
	{
		/// <summary>
		/// Represents input provided by this <see cref="LocalPlayer"/>
		/// </summary>
		public SimulationInput Input { get; }

		/// <summary>
		/// The players highest achieved score.
		/// </summary>
		public EventField<int> Highscore { get; } = new();

		/// <summary>
		/// Creates a new instance of the <see cref="LocalPlayer"/> class.
		/// </summary>
		/// <param name="input">Input from the player.</param>
		public LocalPlayer(SimulationInput input)
		{
			Input = input;
		}
	}
}
