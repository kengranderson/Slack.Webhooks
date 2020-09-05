using System.Linq;
using System.Text.Json;

namespace Slack.Webhooks
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => Utils.ToSnakeCase(name);
    }

    static class Utils
    {
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }

    //public class JsonUtils
    //{

    //    private enum SeparatedCaseState
    //    {
    //        Start,
    //        Lower,
    //        Upper,
    //        NewWord
    //    }

    //    public static string ToSnakeCase(string s) => ToSeparatedCase(s, '_');

    //    static string ToSeparatedCase(string s, char separator)
    //    {
    //        if (string.IsNullOrEmpty(s))
    //        {
    //            return s;
    //        }

    //        var sb = new StringBuilder();
    //        var state = SeparatedCaseState.Start;

    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            if (s[i] == ' ')
    //            {
    //                if (state != SeparatedCaseState.Start)
    //                {
    //                    state = SeparatedCaseState.NewWord;
    //                }
    //            }
    //            else if (char.IsUpper(s[i]))
    //            {
    //                switch (state)
    //                {
    //                    case SeparatedCaseState.Upper:
    //                        bool hasNext = (i + 1 < s.Length);
    //                        if (i > 0 && hasNext)
    //                        {
    //                            char nextChar = s[i + 1];
    //                            if (!char.IsUpper(nextChar) && nextChar != separator)
    //                            {
    //                                sb.Append(separator);
    //                            }
    //                        }
    //                        break;

    //                    case SeparatedCaseState.Lower:
    //                    case SeparatedCaseState.NewWord:
    //                        sb.Append(separator);
    //                        break;
    //                }

    //                sb.Append(char.ToLowerInvariant(s[i]));

    //                state = SeparatedCaseState.Upper;
    //            }
    //            else if (s[i] == separator)
    //            {
    //                sb.Append(separator);
    //                state = SeparatedCaseState.Start;
    //            }
    //            else
    //            {
    //                if (state == SeparatedCaseState.NewWord)
    //                {
    //                    sb.Append(separator);
    //                }

    //                sb.Append(s[i]);
    //                state = SeparatedCaseState.Lower;
    //            }
    //        }

    //        return sb.ToString();
    //    }
    //}
}
