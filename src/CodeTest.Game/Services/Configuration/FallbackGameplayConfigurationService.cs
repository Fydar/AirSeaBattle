using System.Threading.Tasks;

namespace CodeTest.Game.Services.Configuration
{
	/// <inheritdoc/>
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
