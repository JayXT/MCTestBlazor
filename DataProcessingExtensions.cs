using CommonMark;
using Microsoft.AspNetCore.Components;

namespace MessengerComparison
{
    public static class DataProcessingExtensions
    {
        public static MarkupString ToHtml(this string s)
        {
            if(!s.Contains("](") && !s.Contains("  \n"))
                return (MarkupString)s;

            var result = CommonMarkConverter.Convert(s);

            if (result.StartsWith("<p>"))
            {
                result = result.Remove(0, 3);
                result = result.Remove(result.LastIndexOf("</p>"), 4);
            }

            return (MarkupString)result;
        }

        public static (string, MarkupString) GetScoreValue(this string s) 
        {
            var array = s.Split('|');

            if(array.Length == 2) 
            {
                return (array[0], array[1].ToHtml());
            }
            else if(array.Length == 1)
            {
                return (string.Empty, array[0].ToHtml());
            }
            else 
            {
                return (array[0], s.Remove(0, array[0].Length + 1).ToHtml());
            }
        }

        public static bool AreTelegramFeaturesPresent(this Aspect aspect) 
        {
            foreach(var feature in aspect.Features)
                if(!string.IsNullOrEmpty(feature.Values.Telegram))
                    return true;
            return false;
        }

        public static bool AreViberFeaturesPresent(this Aspect aspect) 
        {
            foreach(var feature in aspect.Features)
                if(!string.IsNullOrEmpty(feature.Values.Viber))
                    return true;
            return false;
        }

        public static bool AreWhatsAppFeaturesPresent(this Aspect aspect) 
        {
            foreach(var feature in aspect.Features)
                if(!string.IsNullOrEmpty(feature.Values.WhatsApp))
                    return true;
            return false;
        }
        
    }
}