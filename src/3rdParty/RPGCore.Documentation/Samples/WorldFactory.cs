using AirSeaBattle.Game.Services.Configuration;
using AirSeaBattle.Game.Simulation;
using AirSeaBattle.Game.Simulation.Systems.EnemyBehaviour;
using AirSeaBattle.Game.Simulation.Systems.EnemySpawning;
using AirSeaBattle.Game.Simulation.Systems.PlayerControl;
using AirSeaBattle.Game.Simulation.Systems.ProjectileMovement;
using System.Threading.Tasks;

namespace RPGCore.Documentation.Samples;

public class WorldFactorySample
{
    public static async Task Run()
    {
        #region factory
        // A world engine describes the mechanics and behaviours that run in the world.
        var worldEngine = WorldEngineBuilder.Create()
            .UseWorldSystem(new PlayerControlSystemFactory())
            .UseWorldSystem(new EnemySpawnerSystemFactory())
            .UseWorldSystem(new EnemyBehaviourSystemFactory())
            .UseWorldSystem(new ProjectileMovementSystemFactory())
            .Build();

        // Multiple worlds can be constructed from the same engine, with different configurations.
        var world = await worldEngine.ConstructWorld()
            .UseConfiguration(new FallbackGameplayConfigurationService())
            .Build();

        // Players can then join the world
        var playerInput = new SimulationInput();
        var player = new LocalPlayer(playerInput);

        world.AddPlayer(player);
        #endregion factory
    }
}
