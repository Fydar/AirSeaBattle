namespace CodeTest.Game
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
