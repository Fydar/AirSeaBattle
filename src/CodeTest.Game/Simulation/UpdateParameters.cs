using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation
{
	public readonly struct UpdateParameters
	{
		public Fixed DeltaTime { get; }

		public UpdateParameters(Fixed deltaTime)
		{
			DeltaTime = deltaTime;
		}
	}
}
