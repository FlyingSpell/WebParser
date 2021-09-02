using System;

namespace WebParser
{
    class Program
    {
        private static WebParserScheduler webParserScheduler;
        private static int                totalUpdates = 0;

        static void Main(string[] args)
        {
            String hhSearchUrl = "https://spb.hh.ru/search/vacancy?clusters=true&area=2&ored_clusters=true&enable_snippets=true&salary=&st=searchVacancy&text=%D0%9A%D1%83%D1%80%D1%8C%D0%B5%D1%80&from=suggest_post";

            HHVacancyParser hhVacancyParser = new HHVacancyParser();
            hhVacancyParser.ParseResultsUpdated += OnDatabaseUpdate;

            webParserScheduler = new WebParserScheduler(webParser: hhVacancyParser,
                                                        url: hhSearchUrl,
                                                        timeBetweenUpdatesInMilliseconds: 5000);

            webParserScheduler.Start();

            WaitScheduler();
        }

        private static void OnDatabaseUpdate(ParseResults[] parseResultsArray)
        {
            Console.WriteLine("Updating database....");

            totalUpdates++;

            if(totalUpdates == 3)
            {
                webParserScheduler.Stop();
            }
        }

        private static void WaitScheduler()
        {
            while(webParserScheduler.Working)
            { 
            }
        }
    }
}
