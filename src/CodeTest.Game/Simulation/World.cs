using CodeTest.Game.Services.Configuration;
using CodeTest.Game.Simulation.Models;
using Industry.Simulation.Math;
using RPGCore.Events;
using System;
using System.Collections.Generic;

namespace CodeTest.Game.Simulation
{
	/// <summary>
	/// A model representing the persistant state of the game world.
	/// </summary>
	public class World
	{
		internal IWorldSystem[] systems = Array.Empty<IWorldSystem>();
		private readonly HashSet<SimulationInput> inputs;

		/// <summary>
		/// Configuration used to contro gameplay systems.
		/// </summary>
		public GameplayConfiguration Configuration { get; }

		/// <summary>
		/// The age of the world in seconds.
		/// </summary>
		public Fixed Age { get; private set; }

		/// <summary>
		/// The horizontal playspace that is available.
		/// </summary>
		public Fixed WorldWidth { get; set; }

		/// <summary>
		/// The vertical playspace that is available.
		/// </summary>
		public Fixed WorldHeight { get; set; }

		/// <summary>
		/// All <see cref="WorldEnemy"/> in the game world.
		/// </summary>
		public EventDictionary<Guid, WorldEnemy> Enemies { get; } = new();

		/// <summary>
		/// All <see cref="WorldPlayer"/> currently contributing to the game world.
		/// </summary>
		public EventDictionary<Guid, WorldPlayer> Players { get; } = new();

		/// <summary>
		/// All <see cref="WorldGun"/> in the game world.
		/// </summary>
		public EventDictionary<Guid, WorldGun> Guns { get; } = new();

		/// <summary>
		/// All <see cref="WorldProjectile"/> in the game world.
		/// </summary>
		public EventDictionary<Guid, WorldProjectile> Projectiles { get; } = new();

		/// <summary>
		/// Bounds used to determine whether entities are contained within the playspace.
		/// </summary>
		public FixedAABox Bounds => new(new(WorldWidth / 2, WorldHeight / 2), new(WorldWidth / 2, WorldHeight / 2));

		/// <summary>
		/// <c>true</c> whilst the <see cref="TimeRemaining"/> is less than or equal to <c>0.0</c>.
		/// </summary>
		public bool IsGameOver => Age >= Configuration.TimeLimit;

		/// <summary>
		/// The time remaining (in seconds) until the game is over.
		/// </summary>
		public Fixed TimeRemaining => Configuration.TimeLimit - Age;

		/// <summary>
		/// The maximum number of projects allowed in this <see cref="World"/>.
		/// </summary>
		/// <remarks>
		/// Could be moved into configuration, but the design brief didn't call for it.
		/// </remarks>
		public int MaximumProjectiles { get; set; } = 5;

		/// <summary>
		/// The height of every layer.
		/// </summary>
		public Fixed SingleLayerHeight
		{
			get
			{
				var minusReserved = WorldHeight - (Configuration.EnemySpawning.MinimumAltitudePercent * WorldHeight);
				return minusReserved / Configuration.EnemySpawning.LayersCount;
			}
		}

		internal World(GameplayConfiguration configuration)
		{
			Configuration = configuration;

			inputs = new HashSet<SimulationInput>();

			WorldWidth = 16;
			WorldHeight = 9;
		}

		/// <summary>
		/// Resizes the game world.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void Resize(Fixed width, Fixed height)
		{
			// Ahhhhh gameplay that is fundemantally effected by the screen size? Makes me sad; but it's
			// what the design document asks for. Got to make it so the world can be resized as I rather
			// have control over this than let it be an implicit fact of the game.

			WorldWidth = width;
			WorldHeight = height;
		}

		/// <summary>
		/// Advances the world forward by a specified amount of time.
		/// </summary>
		/// <param name="deltaTime">The time since the last update.</param>
		public void Update(Fixed deltaTime)
		{
			Age += deltaTime;

			var parameters = new UpdateParameters(deltaTime);

			for (int i = 0; i < systems.Length; i++)
			{
				var system = systems[i];
				system.OnUpdate(parameters);
			}

			foreach (var input in inputs)
			{
				input.Up.Update();
				input.Down.Update();
				input.Fire.Update();
			}
		}

		/// <summary>
		/// Adds a player to the game world.
		/// </summary>
		/// <param name="localPlayer">An object representing the player that joined.</param>
		public WorldPlayer AddPlayer(LocalPlayer localPlayer)
		{
			inputs.Add(localPlayer.Input);

			var worldPlayer = new WorldPlayer(this, localPlayer);
			Players.Add(worldPlayer.Identifier, worldPlayer);

			for (int i = 0; i < systems.Length; i++)
			{
				var system = systems[i];
				system.OnPlayerJoined(worldPlayer);
			}
			return worldPlayer;
		}

		/// <summary>
		/// Removes a player from the game world.
		/// </summary>
		/// <param name="worldPlayer">An object representing the player that left.</param>
		public void RemovePlayer(WorldPlayer worldPlayer)
		{
			inputs.Remove(worldPlayer.Player.Input);

			Players.Remove(worldPlayer.Identifier);

			for (int i = 0; i < systems.Length; i++)
			{
				var system = systems[i];
				system.OnPlayerRemoved(worldPlayer);
			}
		}

		/// <summary>
		/// Resets the world to the default state (minus mutations to the screen size).
		/// </summary>
		public void Reset()
		{
			Age = 0;
			Guns.Clear();
			Enemies.Clear();
			Players.Clear();
			Projectiles.Clear();
		}

		/// <summary>
		/// Converts and integer layer into a height in the world.
		/// </summary>
		/// <param name="layer">The layer to find the height of.</param>
		/// <returns>The height of the layer.</returns>
		public Fixed GetLayerHeight(int layer)
		{
			if (layer >= Configuration.EnemySpawning.LayersCount)
			{
				throw new ArgumentOutOfRangeException(nameof(layer), "Layer is too large.");
			}

			return (Configuration.EnemySpawning.MinimumAltitudePercent * WorldHeight) + (SingleLayerHeight * (layer - Constants.Half));
		}
	}
}
