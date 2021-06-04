using CodeTest.Game.Math;
using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation;
using CodeTest.Game.Simulation.Models;
using CodeTest.Game.Simulation.Systems.EnemyBehaviour;
using CodeTest.Game.Simulation.Systems.EnemySpawning;
using CodeTest.Game.Simulation.Systems.PlayerControl;
using CodeTest.Game.Simulation.Systems.ProjectileMovement;
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
				.UseWorldSystem(new PlayerControlSystemFactory(new PlayerControlConfiguration()))
				.UseWorldSystem(new EnemySpawnerSystemFactory(new EnemySpawnerConfiguration()))
				.UseWorldSystem(new EnemyBehaviourSystemFactory(new EnemyBehaviourConfiguration()))
				.UseWorldSystem(new ProjectileMovementSystemFactory())
				.Build();

			var world = worldEngine.ConstructWorld()
				.UseConfiguration(new FallbackGameplayConfigurationService())
				.Build();

			var player = new LocalPlayer(new SimulationInput());

			world.AddPlayer(player);

			world.Update(((Fixed)1) / 5);
			world.Update(((Fixed)1) / 5);
			world.Update(((Fixed)1) / 5);
			world.Update(((Fixed)1) / 5);
			world.Update(((Fixed)1) / 5);
		}
	}
}