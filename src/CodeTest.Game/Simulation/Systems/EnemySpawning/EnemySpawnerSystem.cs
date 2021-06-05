using Industry.Simulation.Math;
using CodeTest.Game.Simulation.Models;
using System;

namespace CodeTest.Game.Simulation.Systems.EnemySpawning
{
	public class EnemySpawnerSystem : IWorldSystem
	{
		private readonly World world;

		private readonly Random random;
		private Fixed timeWaitingWithNoEnemies;

		/// <summary>
		/// The height of every layer.
		/// </summary>
		public Fixed LayerHeight
		{
			get
			{
				var minusReserved = world.WorldHeight - (world.Configuration.EnemySpawning.MinimumAltitudePercent * world.WorldHeight);
				return minusReserved / world.Configuration.EnemySpawning.LayersCount;
			}
		}

		public EnemySpawnerSystem(World world)
		{
			this.world = world;
			random = new Random();
		}

		/// <inheritdoc/>
		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{

		}

		/// <inheritdoc/>
		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
		}

		/// <inheritdoc/>
		public void OnUpdate(UpdateParameters parameters)
		{
			if (world.IsGameOver)
			{
				return;
			}

			// Whilst there are no enemies, do nothing.
			if (HasEnemies())
			{
				return;
			}

			timeWaitingWithNoEnemies += parameters.DeltaTime;

			if (timeWaitingWithNoEnemies >= world.Configuration.EnemySpawning.DelayBetweenRounds)
			{
				timeWaitingWithNoEnemies = 0;

				SpawnVerticalWave();
			}
		}

		/// <summary>
		/// Converts and integer layer into a height in the world.
		/// </summary>
		/// <param name="layer">The layer to find the height of.</param>
		/// <returns>The height of the layer.</returns>
		public Fixed GetLayerHeight(int layer)
		{
			if (layer >= world.Configuration.EnemySpawning.LayersCount)
			{
				throw new ArgumentOutOfRangeException(nameof(layer), "Layer is too large.");
			}

			return (world.Configuration.EnemySpawning.MinimumAltitudePercent * world.WorldHeight) + (LayerHeight * (layer - Constants.Half));
		}

		/// <summary>
		/// Spawns a wave of enemies that are vertically stacked ontop of eachother.
		/// </summary>
		private void SpawnVerticalWave()
		{
			int enemiesCount = random.Next(world.Configuration.EnemySpawning.MinEnemies, world.Configuration.EnemySpawning.MaxEnemies);

			int startRow = random.Next(0, world.Configuration.EnemySpawning.LayersCount - enemiesCount);

			for (int i = 0; i < enemiesCount; i++)
			{
				int layer = startRow + i;
				var height = GetLayerHeight(layer);

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
}
