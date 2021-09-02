using System;
using System.Net;

namespace WebParser
{
    public abstract class WebParser
    {
        private readonly WebClient webClient = new WebClient();

        public event Action<ParseResults[]> ParseResultsUpdated;

        public void ParseUrl(String url)
        {        
            string webContent = webClient.DownloadString(url);

            ParseResults[] parseResults = ParseWebContent(webContent);

            if(parseResults == null)
            {
                throw new ArgumentNullException("ParseWebContent function returned null!");
            }

            ParseResultsUpdated?.Invoke(parseResults);
        }

        protected abstract ParseResults[] ParseWebContent(String webContent);
    }
}
