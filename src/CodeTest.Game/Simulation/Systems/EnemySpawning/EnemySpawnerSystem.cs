using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;
using System;

namespace CodeTest.Game.Simulation.Systems.EnemySpawning
{
	public class EnemySpawnerSystem : IWorldSystem
	{
		private readonly World world;
		private readonly EnemySpawnerConfiguration configuration;

		private readonly Random random;
		private Fixed timeWaitingWithNoEnemies;

		public Fixed LayerHeight
		{
			get
			{
				var minusReserved = world.Height - (configuration.MinimumAltitudePercent * world.Height);

				return minusReserved / configuration.LayersCount;
			}
		}

		public EnemySpawnerSystem(World world, EnemySpawnerConfiguration configuration)
		{
			this.world = world;
			this.configuration = configuration;
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
			// Whilst there are no enemies, do nothing.
			if (HasEnemies())
			{
				return;
			}

			timeWaitingWithNoEnemies += parameters.DeltaTime;

			if (timeWaitingWithNoEnemies >= configuration.DelayBetweenRounds)
			{
				timeWaitingWithNoEnemies = 0;

				SpawnVerticalWave();
			}
		}

		public Fixed GetLayerCenter(int layer)
		{
			if (layer >= configuration.LayersCount)
			{
				throw new ArgumentOutOfRangeException(nameof(layer), "Layer is too large.");
			}

			return (configuration.MinimumAltitudePercent * world.Height) + (LayerHeight * (layer - Constants.Half));
		}

		/// <summary>
		/// Spawns a wave of enemies that are vertically stacked ontop of eachother.
		/// </summary>
		private void SpawnVerticalWave()
		{
			int enemiesCount = random.Next(configuration.MinEnemies, configuration.MaxEnemies);

			int startRow = random.Next(0, configuration.LayersCount - enemiesCount);

			for (int i = 0; i < enemiesCount; i++)
			{
				int layer = startRow + i;
				var height = GetLayerCenter(layer);

				var newEnemy = new WorldEnemy(world, configuration.Enemy);

				newEnemy.Position.Value = new FixedVector2(-newEnemy.Template.Width / 2, height);
				newEnemy.VelocityX.Value = configuration.Enemy.Speed;

				world.Enemies.Add(newEnemy.Identifier, newEnemy);
			}
		}

		private bool HasEnemies()
		{
			return world.Enemies.Count > 0;
		}
	}
}
