namespace CodeTest.Game.Simulation.Systems.EnemyBehaviour
{
	/// <summary>
	/// A <see cref="IWorldSystemFactory"/> that provides a <see cref="EnemyBehaviourSystem"/> implementation.
	/// </summary>
	public class EnemyBehaviourSystemFactory : IWorldSystemFactory
	{
		/// <inheritdoc/>
		public IWorldSystem Build(World world)
		{
			return new EnemyBehaviourSystem(world);
		}
	}
}
