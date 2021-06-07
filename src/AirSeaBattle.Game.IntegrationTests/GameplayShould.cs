using AirSeaBattle.Game.Services.Configuration;
using AirSeaBattle.Game.Simulation;
using AirSeaBattle.Game.Simulation.Models;
using AirSeaBattle.Game.Simulation.Systems.EnemyBehaviour;
using AirSeaBattle.Game.Simulation.Systems.EnemySpawning;
using AirSeaBattle.Game.Simulation.Systems.PlayerControl;
using AirSeaBattle.Game.Simulation.Systems.ProjectileMovement;
using Industry.Simulation.Math;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AirSeaBattle.Game.IntegrationTests
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