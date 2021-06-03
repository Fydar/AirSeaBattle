using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeTest.Game.Services.Configuration
{
	/// <inheritdoc/>
	public class RemoteGameplayConfigurationService : IGameplayConfigurationService
	{
		private readonly JsonSerializer serializer;
		private readonly HttpClient httpClient;
		private readonly string url;

		public RemoteGameplayConfigurationService(HttpClient httpClient, string url)
		{
			this.httpClient = httpClient;
			this.url = url;
			serializer = JsonSerializer.Create(
				new JsonSerializerSettings()
				{
					// Allow members from the configuration to be missing
					MissingMemberHandling = MissingMemberHandling.Ignore
				}
			);
		}

		/// <inheritdoc/>
		public async Task Configure(GameplayConfiguration configuration)
		{
			var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

			if (response.IsSuccessStatusCode)
			{
				using var contentStream = await response.Content.ReadAsStreamAsync();
				using var textReader = new StreamReader(contentStream);
				using var jsonReader = new JsonTextReader(textReader);
				var config = serializer.Deserialize<GameplayConfiguration>(jsonReader);

				if (config == null)
				{
					throw new InvalidOperationException("Unable to deserialize configuration.");
				}

				configuration.ConfigureFrom(config);
			}
			else
			{
				throw new InvalidOperationException("Unable to get gameplay configuration from remote URL.");
			}
		}
	}
}
