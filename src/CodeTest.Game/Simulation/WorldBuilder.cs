using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation.Models;
using System.Collections.Generic;

namespace CodeTest.Game.Simulation
{
	public class WorldBuilder
	{
		private readonly WorldEngine worldEngine;
		private readonly List<IGameplayConfigurationService> configurationServices;

		internal WorldBuilder(WorldEngine worldEngine)
		{
			this.worldEngine = worldEngine;
			configurationServices = new();
		}

		public WorldBuilder UseConfiguration(IGameplayConfigurationService configurationService)
		{
			configurationServices.Add(configurationService);
			return this;
		}

		/// <summary>
		/// Constructs a new <see cref="World"/> from the current state of this <see cref="WorldBuilder"/>.
		/// </summary>
		/// <returns>The new <see cref="World"/>.</returns>
		public World Build()
		{
			var world = new World(worldEngine);

			var systems = new IWorldSystem[worldEngine.worldSystems.Length];
			for (int i = 0; i < worldEngine.worldSystems.Length; i++)
			{
				systems[i] = worldEngine.worldSystems[i].Build(world);
			}

			world.systems = systems;

			return world;
		}
	}
}
