using System.Threading.Tasks;

namespace CodeTest.Game
{
	/// <summary>
	/// A service for providing the game with configuration.
	/// </summary>
	public interface IGameplayConfigurationService
	{
		/// <summary>
		/// Mutate the gameplay configuration to match the behaviour specified by this service.
		/// </summary>
		/// <param name="configuration">The <see cref="GameplayConfiguration"/> to mutate by this service.</param>
		Task Configure(GameplayConfiguration configuration);
	}
}
