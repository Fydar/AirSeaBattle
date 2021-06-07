using AirSeaBattle.Game.Services.Configuration;
using AirSeaBattle.Game.Simulation;
using AirSeaBattle.Game.Simulation.Models;
using AirSeaBattle.Game.Simulation.Systems.EnemyBehaviour;
using AirSeaBattle.Game.Simulation.Systems.EnemySpawning;
using AirSeaBattle.Game.Simulation.Systems.PlayerControl;
using AirSeaBattle.Game.Simulation.Systems.ProjectileMovement;
using System.Net.Http;
using System.Threading.Tasks;

namespace RPGCore.Documentation.Samples
{
	public class AdvancedConfigurationSample
	{
		public static async Task Run()
		{
			var httpClient = new HttpClient();

			// A world engine describes the mechanics and behaviours that run in the world.
			var worldEngine = WorldEngineBuilder.Create()
				.UseWorldSystem(new PlayerControlSystemFactory())
				.UseWorldSystem(new EnemySpawnerSystemFactory())
				.UseWorldSystem(new EnemyBehaviourSystemFactory())
				.UseWorldSystem(new ProjectileMovementSystemFactory())
				.Build();

			#region advanced_config
			// Multiple worlds can be constructed from the same engine, with different configurations.
			var world = await worldEngine.ConstructWorld()
				.UseConfiguration(new FallbackGameplayConfigurationService())

				// Some configuration services may source their data from the web.
				.UseConfiguration(new RemoteGameplayConfigurationService(httpClient, "https://config.url/get"))
				
				// Configuration layers mutate results from the previous.
				.UseConfiguration(configuration =>
				{
					configuration.PointsPerPlane += 5;
					configuration.TimeLimit += 10;

					configuration.EnemySpawning.Enemy = new WorldEnemyTemplate()
					{
						Speed = 2
					};
				})
				.Build();
			#endregion advanced_config
		}
	}
}
