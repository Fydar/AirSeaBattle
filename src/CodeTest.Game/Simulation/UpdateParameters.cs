using CodeTest.Game.Math;

namespace CodeTest.Game.Simulation
{
	public readonly struct UpdateParameters
	{
		public float DeltaTime { get; }

		public UpdateParameters(float deltaTime)
		{
			DeltaTime = deltaTime;
		}
	}
}
