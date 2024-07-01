using AirSeaBattle.Game.Services.Configuration;
using AirSeaBattle.Game.Simulation;
using AirSeaBattle.Game.Simulation.Models;
using AirSeaBattle.Game.Simulation.Systems.EnemyBehaviour;
using AirSeaBattle.Game.Simulation.Systems.EnemySpawning;
using AirSeaBattle.Game.Simulation.Systems.PlayerControl;
using AirSeaBattle.Game.Simulation.Systems.ProjectileMovement;
using AirSeaBattleUnity.EntityRendering;
using Industry.Simulation.Math;
using System.Collections;
using System.Net.Http;
using UnityEngine;

namespace AirSeaBattleUnity
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
                .UseWorldSystem(new PlayerControlSystemFactory())
                .UseWorldSystem(new EnemySpawnerSystemFactory())
                .UseWorldSystem(new EnemyBehaviourSystemFactory())
                .UseWorldSystem(new ProjectileMovementSystemFactory())
                .Build();

            var httpClient = new HttpClient();

            var worldTask = worldEngine.ConstructWorld()
                .UseConfiguration(new FallbackGameplayConfigurationService())
                .UseConfiguration(new RemoteGameplayConfigurationService(httpClient, configurationUrl))
                .UseConfiguration(configuration =>
                {
                    // Respect values provided by the editor.
                    configuration.EnemySpawning.Enemy.Speed = Fixed.FromFloat(enemySpeed);
                })
                .Build();

            // Wait until the async task is complete.
            // We may be making web requests to download game configuration.
            while (!worldTask.IsCompleted)
            {
                yield return null;
            }
            CurrentWorld = worldTask.GetAwaiter().GetResult();

            worldRenderer.RenderTarget = CurrentWorld;

            var playerInput = new SimulationInput();
            var playerInputManager = gameObject.AddComponent<UnitySimulationInputManager>();
            playerInputManager.Controls = controls[0];
            playerInputManager.AttachInput(playerInput);
            var player = new LocalPlayer(playerInput);

            player.Highscore.Value = CurrentWorld.Configuration.DefaultHighScore;

            loadingScreen.Hide();

            while (true)
            {
                mainMenuScreen.Show();
                yield return StartCoroutine(mainMenuScreen.WaitForButtonPressed());
                hudScreen.Show();
                hudScreen.ShowTime();
                mainMenuScreen.Hide();

                var worldPlayer = CurrentWorld.AddPlayer(player);
                hudScreen.PlayerRenderer.RenderTarget = worldPlayer;

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
