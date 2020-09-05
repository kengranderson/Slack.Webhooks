using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Slack.Webhooks
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ParseMode
    {
        [EnumMember(Value = "none")]
        none,
        [EnumMember(Value = "full")]
        full
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SlackActionDataSource
    {
        [EnumMember(Value = "static")]
        @static,
        [EnumMember(Value = "users")]
        users,
        [EnumMember(Value = "channels")]
        channels,
        [EnumMember(Value = "conversations")]
        conversations,
        [EnumMember(Value = "external")]
        external
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SlackActionStyle
    {
        [EnumMember(Value = "default")]
        @default,
        [EnumMember(Value = "primary")]
        primary,
        [EnumMember(Value = "danger")]
        danger
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SlackActionType
    {
        [EnumMember(Value = "button")]
        button,
        [EnumMember(Value = "select")]
        select
    }
}
