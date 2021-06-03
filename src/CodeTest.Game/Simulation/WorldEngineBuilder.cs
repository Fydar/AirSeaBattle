using System.Collections.Generic;

namespace CodeTest.Game.Simulation
{
	public class WorldEngineBuilder
	{
		private readonly List<IWorldSystemFactory> worldSystems;

		internal WorldEngineBuilder()
		{
			worldSystems = new();
		}

		/// <summary>
		/// Adds a <see cref="IWorldSystemFactory"/> to the world which drives gameplay.
		/// </summary>
		/// <param name="worldSystem">The <see cref="IWorldSystemFactory"/> to add.</param>
		/// <returns>The current instance of <c>this</c> builder.</returns>
		public WorldEngineBuilder UseWorldSystem(IWorldSystemFactory worldSystem)
		{
			worldSystems.Add(worldSystem);
			return this;
		}

		public static WorldEngineBuilder Create()
		{
			return new WorldEngineBuilder();
		}

		public WorldEngine Build()
		{
			return new WorldEngine(worldSystems.ToArray());
		}
	}
}
