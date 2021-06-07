namespace AirSeaBattle.Game.Simulation.Systems.EnemySpawning
{
	/// <summary>
	/// A <see cref="IWorldSystemFactory"/> that provides a <see cref="EnemySpawnerSystem"/> implementation.
	/// </summary>
	public class EnemySpawnerSystemFactory : IWorldSystemFactory
	{
		/// <inheritdoc/>
		public IWorldSystem Build(World world)
		{
			return new EnemySpawnerSystem(world);
		}
	}
}
