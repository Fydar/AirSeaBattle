using CodeTest.Game.Math;
using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation;
using CodeTest.Game.Simulation.Models;
using CodeTest.Game.Simulation.Systems.EnemyBehaviour;
using CodeTest.Game.Simulation.Systems.EnemySpawning;
using CodeTest.Game.Simulation.Systems.PlayerControl;
using CodeTest.Game.Simulation.Systems.ProjectileMovement;
using System.Collections;
using System.Net.Http;
using UnityEngine;

namespace CodeTestUnity
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField] private ControlSchema[] controls;
		[SerializeField] private WorldRenderer worldRenderer;

		[Header("UI")]
		[SerializeField] private LoadingScreen loadingScreen;
		[SerializeField] private MainMenuScreen mainMenuScreen;
		[SerializeField] private HudScreen hudScreen;

		[Header("Configuration")]
		[SerializeField] private float enemySpeed = 1.0f;
		[SerializeField] private string configurationUrl = "http://content.gamefuel.info/api/client_programming_test/air_battle_v1/content/config/config";

		public World CurrentWorld { get; private set; }

		private void Start()
		{
			StartCoroutine(RunGameRoutine());
		}

		private IEnumerator RunGameRoutine()
		{
			loadingScreen.Show();
			mainMenuScreen.Hide();
			hudScreen.Hide();

			var worldEngine = WorldEngineBuilder.Create()
				.UseWorldSystem(new PlayerControlSystemFactory(new PlayerControlConfiguration()))
				.UseWorldSystem(new EnemySpawnerSystemFactory(
					new EnemySpawnerConfiguration()
					{
						Enemy = new WorldEnemyTemplate()
						{
							Speed = Fixed.FromFloat(enemySpeed)
						}
					}))
				.UseWorldSystem(new EnemyBehaviourSystemFactory(
					new EnemyBehaviourConfiguration()))
				.UseWorldSystem(new ProjectileMovementSystemFactory())
				.Build();

			var httpClient = new HttpClient();

			var worldTask = worldEngine.ConstructWorld()
				.UseConfiguration(new FallbackGameplayConfigurationService())
				.UseConfiguration(new RemoteGameplayConfigurationService(httpClient, configurationUrl))
				.Build();

			// Wait until the async task is complete.
			// We may be making web requests to download game configuration.
			while (!worldTask.IsCompleted)
			{
				yield return null;
			}
			CurrentWorld = worldTask.GetAwaiter().GetResult();

			worldRenderer.Render(CurrentWorld);

			var playerInput = new SimulationInput();
			var playerInputManager = gameObject.AddComponent<UnitySimulationInputManager>();
			playerInputManager.Controls = controls[0];
			playerInputManager.AttachInput(playerInput);
			var player = new LocalPlayer(playerInput);

			loadingScreen.Hide();

			while (true)
			{
				mainMenuScreen.Show();
				yield return StartCoroutine(mainMenuScreen.WaitForButtonPressed());
				hudScreen.Show();
				hudScreen.ShowTime();
				mainMenuScreen.Hide();

				var worldPlayer = CurrentWorld.AddPlayer(player);
				hudScreen.RenderTarget = worldPlayer;

				while (true)
				{
					yield return null;
					var deltaTime = Fixed.FromFloat(Time.deltaTime);
					CurrentWorld.Update(deltaTime);

					if (CurrentWorld.IsGameOver)
					{
						break;
					}
				}
				CurrentWorld.Reset();

				hudScreen.HideTime();
				mainMenuScreen.Show();
			}
		}
	}
}
