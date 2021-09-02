using System;
using Timer = System.Timers.Timer;

namespace WebParser
{
    public class WebParserScheduler
    {
        private WebParser webParser;
        private String    url;
        private Timer     timer;

        public bool Working
        {
            get
            {
                return timer.Enabled;
            }
        }

        public WebParserScheduler(WebParser webParser,
                                  String url,
                                  double timeBetweenUpdatesInMilliseconds)
        {
            this.webParser = webParser;
            this.url = url;

            timer = new Timer(timeBetweenUpdatesInMilliseconds);
            timer.Elapsed += DoWork;
            timer.Enabled = false;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private void DoWork(Object source, System.Timers.ElapsedEventArgs e)
        {
            webParser.ParseUrl(url);
        }
    }
}
