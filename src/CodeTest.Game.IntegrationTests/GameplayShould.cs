using Industry.Simulation.Math;
using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation;
using CodeTest.Game.Simulation.Models;
using CodeTest.Game.Simulation.Systems.EnemyBehaviour;
using CodeTest.Game.Simulation.Systems.EnemySpawning;
using CodeTest.Game.Simulation.Systems.PlayerControl;
using CodeTest.Game.Simulation.Systems.ProjectileMovement;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CodeTest.Game.IntegrationTests
{
	public class GameplayShould
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task Test1()
		{
			var worldEngine = WorldEngineBuilder.Create()
				.UseWorldSystem(new PlayerControlSystemFactory())
				.UseWorldSystem(new EnemySpawnerSystemFactory())
				.UseWorldSystem(new EnemyBehaviourSystemFactory())
				.UseWorldSystem(new ProjectileMovementSystemFactory())
				.Build();

			var world = await worldEngine.ConstructWorld()
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