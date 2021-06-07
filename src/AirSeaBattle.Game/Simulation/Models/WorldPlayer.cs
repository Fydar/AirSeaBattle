using RPGCore.Events;
using System;

namespace AirSeaBattle.Game.Simulation.Models
{
	/// <summary>
	/// Represents a moving projectile in the <see cref="World"/>.
	/// </summary>
	public class WorldPlayer
	{
		/// <summary>
		/// A unique identifier for this <see cref="WorldPlayer"/>.
		/// </summary>
		public Guid Identifier { get; }

		/// <summary>
		/// The world that this entity belongs to.
		/// </summary>
		public World World { get; }

		/// <summary>
		/// A model representing the input and state of the local player.
		/// </summary>
		public LocalPlayer Player { get; }

		/// <summary>
		/// The current score of the player.
		/// </summary>
		public EventField<int> CurrentScore { get; } = new();

		/// <summary>
		/// A collection of all <see cref="WorldGun"/>s that are controlled by this <see cref="WorldPlayer"/>.
		/// </summary>
		public EventDictionary<Guid, WorldGun> ControlledGuns { get; } = new();

		/// <summary>
		/// Creates a new instance of the <see cref="WorldPlayer"/> class.
		/// </summary>
		/// <param name="world">The <see cref="World"/> that this player belongs to.</param>
		/// <param name="player">The external player to provide input.</param>
		public WorldPlayer(World world, LocalPlayer player)
		{
			Identifier = Guid.NewGuid();
			World = world;
			Player = player;
		}
	}
}
