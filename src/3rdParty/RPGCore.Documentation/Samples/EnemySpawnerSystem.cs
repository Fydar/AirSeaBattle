using CodeTest.Game.Simulation;
using CodeTest.Game.Simulation.Models;
using Industry.Simulation.Math;
using System;

namespace RPGCore.Documentation.Samples
{
	#region system
	// The system responcible for spawning enemy waves.
	public class EnemySpawnerSystem : IWorldSystem
	{
		private readonly World world;
		private readonly Random random;
		private Fixed timeWaitingWithNoEnemies;

		internal EnemySpawnerSystem(World world)
		{
			this.world = world;
			random = new Random();
		}

		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
		}

		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
		}

		public void OnUpdate(UpdateParameters parameters)
		{
			// If the game has ended, do nothing.
			if (world.IsGameOver)
			{
				return;
			}

			// Whilst there are no enemies, do nothing.
			if (HasEnemies())
			{
				return;
			}

			// If we have been waiting long enough, spawn a wave of enemies.
			timeWaitingWithNoEnemies += parameters.DeltaTime;
			if (timeWaitingWithNoEnemies >= world.Configuration.EnemySpawning.DelayBetweenRounds)
			{
				timeWaitingWithNoEnemies = 0;
				SpawnVerticalWave();
			}
		}

		// Spawns a wave of enemies that are vertically stacked ontop of eachother.
		private void SpawnVerticalWave()
		{
			int enemiesCount = random.Next(world.Configuration.EnemySpawning.MinEnemies,
				world.Configuration.EnemySpawning.MaxEnemies);
			int startRow = random.Next(0, world.Configuration.EnemySpawning.LayersCount - enemiesCount);

			for (int i = 0; i < enemiesCount; i++)
			{
				int layer = startRow + i;
				var height = world.GetLayerHeight(layer);

				var newEnemy = new WorldEnemy(world, world.Configuration.EnemySpawning.Enemy);

				newEnemy.Position.Value = new FixedVector2(-newEnemy.Template.Width / 2, height);
				newEnemy.VelocityX.Value = world.Configuration.EnemySpawning.Enemy.Speed;

				world.Enemies.Add(newEnemy.Identifier, newEnemy);
			}
		}

		private bool HasEnemies()
		{
			return world.Enemies.Count > 0;
		}
	}
	#endregion system
}
