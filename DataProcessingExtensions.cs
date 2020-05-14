using Markdig;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;


namespace MessengerComparison
{
    public static class DataProcessingExtensions
    {
        public static MarkupString ProcessMarkdown(this string s)
        {
            if(!s.Contains("](") && !s.Contains("  \n"))
                return (MarkupString)s;

            var result = Markdown.ToHtml(s);

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
                return (array[0], array[1].ProcessMarkdown());
            }
            else if(array.Length == 1)
            {
                return (string.Empty, array[0].ProcessMarkdown());
            }
            else 
            {
                return (array[0], s.Remove(0, array[0].Length + 1).ProcessMarkdown());
            }
        }

        public static string GetMessengerScore(this string s)
        {
            var array = s.Split('|');
            return array.Length > 1 ? array[0] : string.Empty;
        }

        public static MarkupString GetMessengerValue(this string s)
        {
            var array = s.Split('|');

            if (array.Length == 2)
            {
            
                return array[1].ProcessMarkdown();
            }
            else if(array.Length > 2) 
            {
                int index = s.IndexOf(array[0] + '|');
                return s.Remove(index, array[0].Length + 1).ProcessMarkdown();
            }

            return s.ProcessMarkdown();            
        }
        
    }
}