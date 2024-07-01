using AirSeaBattle.Game.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirSeaBattle.Game.Simulation;

/// <summary>
/// A builder for <see cref="World"/>.
/// </summary>
public class WorldBuilder
{
    private readonly WorldEngine worldEngine;
    private readonly List<IGameplayConfigurationService> configurationServices;

    internal WorldBuilder(WorldEngine worldEngine)
    {
        this.worldEngine = worldEngine;
        configurationServices = [];
    }

    /// <summary>
    /// Registers an <see cref="IGameplayConfigurationService"/> implementation for configuration.
    /// </summary>
    /// <param name="configurationService">The service to register.</param>
    /// <returns>This current instance of this <see cref="WorldBuilder"/>.</returns>
    public WorldBuilder UseConfiguration(IGameplayConfigurationService configurationService)
    {
        configurationServices.Add(configurationService);
        return this;
    }

    /// <summary>
    /// Registers an implementation for configuration.
    /// </summary>
    /// <param name="callback">A callback used to configure the configuration.</param>
    /// <returns>This current instance of this <see cref="WorldBuilder"/>.</returns>
    public WorldBuilder UseConfiguration(Action<GameplayConfiguration> callback)
    {
        configurationServices.Add(new DelegateGameplayConfigurationService(callback));
        return this;
    }

    /// <summary>
    /// Constructs a new <see cref="World"/> from the current state of this <see cref="WorldBuilder"/>.
    /// </summary>
    /// <returns>The new <see cref="World"/>.</returns>
    public async Task<World> Build()
    {
        var configuration = new GameplayConfiguration();
        foreach (var configurationService in configurationServices)
        {
            await configurationService.Configure(configuration);
        }

        var world = new World(configuration);

        var systems = new IWorldSystem[worldEngine.worldSystems.Length];
        for (int i = 0; i < worldEngine.worldSystems.Length; i++)
        {
            systems[i] = worldEngine.worldSystems[i].Build(world);
        }

        world.systems = systems;

        return world;
    }
}
