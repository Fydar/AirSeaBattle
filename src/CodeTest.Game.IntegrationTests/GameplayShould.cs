using NUnit.Framework;

namespace CodeTest.Game.IntegrationTests
{
	public class GameplayShould
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			var worldEngine = WorldEngineBuilder.Create()
				.Build();

			var world = worldEngine.ConstructWorld()
				.UseConfiguration(new FallbackGameplayConfigurationService())
				.Build();

			var player = new LocalPlayer(new SimulationInput());

			world.AddPlayer(player);

			world.Update(0.2f);
			world.Update(0.2f);
			world.Update(0.2f);
			world.Update(0.2f);
			world.Update(0.2f);
		}
	}
}