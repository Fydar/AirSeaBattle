using CodeTest.Game.Math;
using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation;
using CodeTest.Game.Simulation.Systems.EnemyBehaviour;
using CodeTest.Game.Simulation.Systems.EnemySpawning;
using CodeTest.Game.Simulation.Systems.PlayerControl;
using System.Collections;
using UnityEngine;

namespace CodeTestUnity
{
	public class Entrypoint : MonoBehaviour
	{
		[SerializeField] private ControlSchema[] controls;
		[SerializeField] private WorldRenderer worldRenderer;

		private void Start()
		{
			StartCoroutine(RuntimeRoutine());
		}

		private IEnumerator RuntimeRoutine()
		{
			var worldEngine = WorldEngineBuilder.Create()
				.UseWorldSystem(new PlayerControlSystemFactory(new PlayerControlConfiguration()))
				.UseWorldSystem(new EnemySpawnerSystemFactory(new EnemySpawnerConfiguration()))
				.UseWorldSystem(new EnemyBehaviourSystemFactory(new EnemyBehaviourConfiguration()))
				.Build();

			var world = worldEngine.ConstructWorld()
				.UseConfiguration(new FallbackGameplayConfigurationService())
				.Build();

			worldRenderer.Render(world);

			var playerInput = new SimulationInput();
			var playerInputManager = gameObject.AddComponent<UnitySimulationInputManager>();
			playerInputManager.Controls = controls[0];
			playerInputManager.AttachInput(playerInput);

			var player = new LocalPlayer(playerInput);
			world.AddPlayer(player);

			while (true)
			{
				yield return null;
				var deltaTime = Fixed.FromFloat(Time.deltaTime);
				world.Update(deltaTime);
			}
		}
	}
}
