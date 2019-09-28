using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Slack.Webhooks
{
    public class SlackClient : ISlackClient
    {
        readonly Uri _webhookUri;
        const string POST_SUCCESS = "ok";
        static readonly HttpClient _httpClient = new HttpClient();

        public SlackClient(string webhookUrl)
        {
            if (!Uri.TryCreate(webhookUrl, UriKind.Absolute, out _webhookUri))
                throw new ArgumentException("Please enter a valid webhook url");
        }
        
        public virtual bool Post(SlackMessage slackMessage)
        {
            var result = Task.Run(async () => await PostAsync(slackMessage).ConfigureAwait(false));
            return result.Result;
        }

        public bool PostToChannels(SlackMessage message, IEnumerable<string> channels)
        {
            return channels.DefaultIfEmpty(message.Channel)
                    .Select(message.Clone)
                    .Select(Post).All(r => r);
        }
        
        public IEnumerable<Task<bool>> PostToChannelsAsync(SlackMessage message, IEnumerable<string> channels)
        {
            return channels.DefaultIfEmpty(message.Channel)
                                .Select(message.Clone)
                                .Select(PostAsync);
        }

        public async Task<bool> PostAsync(SlackMessage slackMessage)
        {
            var payload = slackMessage.AsJson();
            using (var request = new HttpRequestMessage
            {
                RequestUri = _webhookUri,
                Method = HttpMethod.Post,
                Content = new StringContent(payload)
            })
            using (var response = await _httpClient.SendAsync(request).ConfigureAwait(false))
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return content.Equals(POST_SUCCESS, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
