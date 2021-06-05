using System.Collections.Generic;

namespace CodeTest.Game.Simulation.Models
{
	/// <summary>
	/// A factory for <see cref="WorldEngine"/>
	/// </summary>
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

		/// <summary>
		/// Begins construction of a <see cref="WorldEngine"/>.
		/// </summary>
		/// <returns></returns>
		public static WorldEngineBuilder Create()
		{
			return new WorldEngineBuilder();
		}

		/// <summary>
		/// Constructs a new <see cref="WorldEngine"/> from the current state of this <see cref="WorldEngineBuilder"/>.
		/// </summary>
		/// <returns>The new <see cref="WorldEngine"/>.</returns>
		public WorldEngine Build()
		{
			return new WorldEngine(worldSystems.ToArray());
		}
	}
}
