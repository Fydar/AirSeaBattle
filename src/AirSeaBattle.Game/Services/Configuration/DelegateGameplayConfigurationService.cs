using System;
using System.Threading.Tasks;

namespace AirSeaBattle.Game.Services.Configuration
{
	/// <summary>
	/// An implementation of <see cref="IGameplayConfigurationService"/> that provides default values.
	/// </summary>
	public class DelegateGameplayConfigurationService : IGameplayConfigurationService
	{
		private readonly Action<GameplayConfiguration> configureImplementation;

		/// <summary>
		/// Constructs a new instance of the <see cref="DelegateGameplayConfigurationService"/> class.
		/// </summary>
		/// <param name="configureImplementation">A callback used to configure.</param>
		public DelegateGameplayConfigurationService(Action<GameplayConfiguration> configureImplementation)
		{
			this.configureImplementation = configureImplementation;
		}

		/// <inheritdoc/>
		public Task Configure(GameplayConfiguration configuration)
		{
			configureImplementation.Invoke(configuration);

			return Task.CompletedTask;
		}
	}
}
