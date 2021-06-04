using CodeTest.Game.Math;
using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation;
using CodeTest.Game.Simulation.Models;
using CodeTest.Game.Simulation.Systems.EnemyBehaviour;
using CodeTest.Game.Simulation.Systems.EnemySpawning;
using CodeTest.Game.Simulation.Systems.PlayerControl;
using CodeTest.Game.Simulation.Systems.ProjectileMovement;
using System.Collections;
using UnityEngine;

namespace CodeTestUnity
{
	public class Entrypoint : MonoBehaviour
	{
		[SerializeField] private ControlSchema[] controls;
		[SerializeField] private WorldRenderer worldRenderer;

		[Header("Configuration")]
		[SerializeField] private float enemySpeed = 1.0f;

		public World CurrentWorld { get; private set; }

		private void Start()
		{
			StartCoroutine(RuntimeRoutine());
		}

		private IEnumerator RuntimeRoutine()
		{
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

			CurrentWorld = worldEngine.ConstructWorld()
				.UseConfiguration(new FallbackGameplayConfigurationService())
				.Build();

			worldRenderer.Render(CurrentWorld);

			var playerInput = new SimulationInput();
			var playerInputManager = gameObject.AddComponent<UnitySimulationInputManager>();
			playerInputManager.Controls = controls[0];
			playerInputManager.AttachInput(playerInput);

			var player = new LocalPlayer(playerInput);
			CurrentWorld.AddPlayer(player);

			while (true)
			{
				yield return null;
				var deltaTime = Fixed.FromFloat(Time.deltaTime);
				CurrentWorld.Update(deltaTime);
			}
		}
	}
}
