using AirSeaBattle.Game.Services.Configuration;
using AirSeaBattle.Game.Simulation;
using AirSeaBattle.Game.Simulation.Models;
using AirSeaBattle.Game.Simulation.Systems.EnemyBehaviour;
using AirSeaBattle.Game.Simulation.Systems.EnemySpawning;
using AirSeaBattle.Game.Simulation.Systems.PlayerControl;
using AirSeaBattle.Game.Simulation.Systems.ProjectileMovement;
using RPGCore.Events;
using System;
using System.Threading.Tasks;

namespace RPGCore.Documentation.Samples;

public class WorldEventsSample
{
    #region handler
    // A basic event handler for rendering entities in the world.
    public class EntityRendererEventHandler<TValue> : IEventDictionaryHandler<Guid, TValue>
    {
        public void OnAdd(Guid key, TValue value)
        {
            Console.WriteLine($"Starting rendering entity '{key}'.");
        }

        public void OnRemove(Guid key, TValue value)
        {
            Console.WriteLine($"Stopped rendering entity '{key}'.");
        }
    }
    #endregion handler

    public async Task Run()
    {
        // A world engine describes the mechanics and behaviours that run in the world.
        var worldEngine = WorldEngineBuilder.Create()
            .UseWorldSystem(new PlayerControlSystemFactory())
            .UseWorldSystem(new EnemySpawnerSystemFactory())
            .UseWorldSystem(new EnemyBehaviourSystemFactory())
            .UseWorldSystem(new ProjectileMovementSystemFactory())
            .Build();

        var world = await worldEngine.ConstructWorld()
            .UseConfiguration(new FallbackGameplayConfigurationService())
            .Build();

        #region subscribe
        // Create an object to handle the rendering of entities in the world.
        var entityRenderer = new EntityRendererEventHandler<WorldEnemy>();

        // Subscribe to change events from the world.
        // "AddAndInvoke" invokes the "OnAdd" event to every item that may already exist in the collection.
        world.Enemies.Handlers[this].AddAndInvoke(entityRenderer);
        #endregion subscribe
    }
}
