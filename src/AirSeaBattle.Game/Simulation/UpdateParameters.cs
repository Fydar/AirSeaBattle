using Industry.Simulation.Math;

namespace AirSeaBattle.Game.Simulation
{
	/// <summary>
	/// A set of parameters for an update.
	/// </summary>
	public readonly struct UpdateParameters
	{
		/// <summary>
		/// The amount of the (in seconds) that has past since last update.
		/// </summary>
		public Fixed DeltaTime { get; }

		internal UpdateParameters(Fixed deltaTime)
		{
			DeltaTime = deltaTime;
		}
	}
}
