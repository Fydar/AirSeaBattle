using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirSeaBattle.Game.Services.Configuration.Remote;

/// <summary>
/// An implementation of <see cref="IGameplayConfigurationService"/> that downloads
/// configuration values from a remote server.
/// </summary>
public class RemoteGameplayConfigurationService : IGameplayConfigurationService
{
    private readonly JsonSerializer serializer;
    private readonly HttpClient httpClient;
    private readonly string url;

    /// <summary>
    /// Creates a new instance of the <see cref="RemoteGameplayConfiguration"/> class.
    /// </summary>
    /// <param name="httpClient">A <see cref="HttpClient"/> to perform HTTP requests with.</param>
    /// <param name="url">A remote URL to issue GET requests to.</param>
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
            var config = serializer.Deserialize<RemoteGameplayConfiguration>(jsonReader);

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
