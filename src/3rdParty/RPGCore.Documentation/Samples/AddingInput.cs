using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation;
using CodeTest.Game.Simulation.Models;
using CodeTest.Game.Simulation.Systems.EnemyBehaviour;
using CodeTest.Game.Simulation.Systems.EnemySpawning;
using CodeTest.Game.Simulation.Systems.PlayerControl;
using CodeTest.Game.Simulation.Systems.ProjectileMovement;
using Industry.Simulation.Math;
using System.Collections;

namespace RPGCore.Documentation.Samples
{
	public class AddingInputSample
	{
		public static class Time
		{
			public static float deltaTime { get; } = 0.2f;
		}

		public static class Input
		{
			public static bool GetKeyDown(KeyCode keyCode)
			{
				return false;
			}

			public static bool GetKeyUp(KeyCode keyCode)
			{
				return false;
			}
		}

		public enum KeyCode
		{
			W,
			S,
			Up,
			Down,
			Space,
		}

		public static IEnumerable Run()
		{
			// A world engine describes the mechanics and behaviours that run in the world.
			var worldEngine = WorldEngineBuilder.Create()
				.UseWorldSystem(new PlayerControlSystemFactory())
				.UseWorldSystem(new EnemySpawnerSystemFactory())
				.UseWorldSystem(new EnemyBehaviourSystemFactory())
				.UseWorldSystem(new ProjectileMovementSystemFactory())
				.Build();

			// Multiple worlds can be constructed from the same engine, with different configurations.
			var world = worldEngine.ConstructWorld()
				.UseConfiguration(new FallbackGameplayConfigurationService())
				.Build()
				.GetAwaiter().GetResult();

			// Players can then join the world
			var playerInput = new SimulationInput();
			var player = new LocalPlayer(playerInput);

			world.AddPlayer(player);

			#region update
			// Repeat until the game is over
			while (!world.IsGameOver)
			{
				// Mutate the players SimulationInput to control the player in the simulation.
				if (Input.GetKeyDown(KeyCode.Up))
				{
					playerInput.Up.SimulateButtonDown();
				}
				else if (Input.GetKeyUp(KeyCode.Up))
				{
					playerInput.Up.SimulateButtonUp();
				}

				// ... handle the rest of the keys ...

				// Advance the game simulation by a frame.
				world.Update(Fixed.FromFloat(Time.deltaTime));

				// Wait until the next frame.
				yield return null;
			}
			#endregion update
		}
	}
}
