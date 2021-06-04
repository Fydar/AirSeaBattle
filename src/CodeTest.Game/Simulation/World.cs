using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;
using RPGCore.Events;
using System;
using System.Collections.Generic;

namespace CodeTest.Game.Simulation
{
	public class World
	{
		internal IWorldSystem[] systems = Array.Empty<IWorldSystem>();
		private readonly WorldEngine worldEngine;
		private readonly HashSet<SimulationInput> inputs;

		public FixedVector2 GunSize { get; set; } = FixedVector2.One;
		public Fixed GunHeightPercent { get; set; } = Constants.One / 8;

		/// <summary>
		/// The age of the world in seconds.
		/// </summary>
		public Fixed Age { get; private set; }
		public Fixed Width { get; set; }
		public Fixed Height { get; set; }
		public EventDictionary<Guid, WorldEnemy> Enemies { get; } = new();
		public EventDictionary<Guid, WorldPlayer> Players { get; } = new();
		public EventDictionary<Guid, WorldGun> Guns { get; } = new();
		public EventDictionary<Guid, WorldProjectile> Projectiles { get; } = new();

		public FixedAABox Bounds => new(new(Width / 2, Height / 2), new(Width / 2, Height / 2));

		/// <summary>
		/// The maximum number of projects allowed in this <see cref="World"/>.
		/// </summary>
		/// <remarks>
		/// Could be moved into configuration, but the design brief didn't call for it.
		/// </remarks>
		public int MaximumProjectiles { get; set; } = 5;

		internal World(WorldEngine worldEngine)
		{
			this.worldEngine = worldEngine;

			inputs = new HashSet<SimulationInput>();

			Width = 16;
			Height = 9;
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

			Width = width;
			Height = height;
		}

		/// <summary>
		/// Advances the world forward by a specified amount of time.
		/// </summary>
		/// <param name="deltaTime"></param>
		/// <param name="time"></param>
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

			var worldPlayer = new WorldPlayer(this, localPlayer.Input);
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
			inputs.Remove(worldPlayer.Input);

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
		}
	}
}
