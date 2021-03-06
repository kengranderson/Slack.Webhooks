using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Slack.Webhooks
{
    /// <summary>
    /// Slack Message
    /// </summary>
    public class SlackMessage
    {
        /// <summary>
        /// This is the text that will be posted to the channel
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Set response to visible to all 'in_channel' or visible to the requester 'ephermeral'
        /// </summary>
        public string ResponseType { get; set; }
        /// <summary>
        /// Used only when creating messages in response to a button action invocation. When set to true, the inciting message will be replaced by this message you're providing. When false, the message you're providing is considered a brand new message.
        /// </summary>
        public bool ReplaceOriginal { get; set; }
        /// <summary>
        /// Used only when creating messages in response to a button action invocation. When set to true, the inciting message will be deleted and if a message is provided, it will be posted as a brand new message.
        /// </summary>
        public bool DeleteOriginal { get; set; }
        /// <summary>
        /// Optional override of destination channel
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// Optional override of the username that is displayed
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Optional emoji displayed with the message
        /// </summary>
        public Emoji IconEmoji { get; set; }
        /// <summary>
        /// Optional url for icon displayed with the message
        /// </summary>
        public Uri IconUrl { get; set; }
        /// <summary>
        /// Optional override markdown mode. Default: true
        /// </summary>
        [JsonPropertyName("mrkdwn")]
        public bool Markdown { get; set; } = true;

        /// <summary>
        /// Enable linkification of channel and usernames
        /// </summary>
        public bool LinkNames { get; set; }
        /// <summary>
        /// Parse mode <see cref="ParseMode"/>
        /// </summary>
        public ParseMode Parse { get; set; }
        /// <summary>
        /// Optional attachment collection
        /// </summary>
        public List<SlackAttachment> Attachments { get; set; }

        public SlackMessage Clone(string newChannel = null)
        {
            return new SlackMessage()
            {
                Attachments = Attachments,
                Text = Text,
                IconEmoji = IconEmoji,
                IconUrl = IconUrl,
                Username = Username,
                Channel = newChannel ?? Channel
            };
        }

        /// <summary>
        /// Conditional serialization of IconEmoji
        /// Overidden by the presence of IconUrl
        /// </summary>
        /// <returns>false when IconUrl is present otherwise true.</returns>
        public bool ShouldSerializeIconEmoji()
        {
            return IconUrl == null && IconEmoji != Emoji.None;
        }

        /// <summary>
        /// Serialize SlackMessage to a JSON string
        /// </summary>
        /// <returns>JSON formatted string</returns>
        public string AsJson()
        {
            var serialized = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                DictionaryKeyPolicy = new SnakeCaseNamingPolicy(),
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true
            });

            return serialized;
        }
    }
}
