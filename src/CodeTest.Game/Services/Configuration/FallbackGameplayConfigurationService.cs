using System.Threading.Tasks;

namespace CodeTest.Game.Services.Configuration
{
	/// <summary>
	/// An implementation of <see cref="IGameplayConfigurationService"/> that provides default values.
	/// </summary>
	public class FallbackGameplayConfigurationService : IGameplayConfigurationService
	{
		/// <inheritdoc/>
		public Task Configure(GameplayConfiguration configuration)
		{
			configuration.Id = "default";
			configuration.DefaultHighScore = 100;
			configuration.TimeLimit = 30;
			configuration.PointsPerPlane = 1;

			return Task.CompletedTask;
		}
	}
}
