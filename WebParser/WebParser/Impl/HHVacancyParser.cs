using System;
using System.Text.RegularExpressions;

namespace WebParser
{
    public class HHVacancyParser : WebParser
    {
        protected override ParseResults[] ParseWebContent(String webContent)
        {
            String[] vacancies = ParseVacancies(webContent);

            ParseResults[] parseResults = ParseVacancies(vacancies);

            return parseResults;
        }

        private String[] ParseVacancies(String webContent)
        {
            String vacancyContent = webContent.ParseBetween(start: "vacancy-serp__results",
                                                            end: "serp-special_under-results");

            String[] vacancies = vacancyContent.ParseArray(itemStart: "vacancy-serp-item ");

            return vacancies;
        }

        private ParseResults[] ParseVacancies(String[] vacancies)
        {
            ParseResults[] parseResultsArray = new ParseResults[vacancies.Length];

            for(int i = 0; i < vacancies.Length; i++)
            {
                String vacancy = vacancies[i];
                String title = ParseTitle(vacancy);
                String salary = ParseSalary(vacancy);

                ParseResults parseResults = new ParseResults();
                parseResults.DataList.Add(title);
                parseResults.DataList.Add(salary);

                parseResultsArray[i] = parseResults;
            }

            return parseResultsArray;
        }

        private String ParseTitle(String vacancy)
        {
            String title = vacancy.ParseBetween(start: "vacancy-serp__vacancy-title",
                                                end: "</a>",
                                                greedy: false);

            title = title.ParseBetween(start: ">", end: "$");

            return title;
        }

        private String ParseSalary(String vacancy)
        {
            String salary = vacancy.ParseBetween(start: "vacancy-serp__vacancy-compensation",
                                                 end: "</span>",
                                                 greedy: false);

            salary = salary.ParseBetween(start: "\b|>", end: "руб");

            salary = Regex.Replace(input: salary,
                                   pattern: @"[^\d^–]",
                                   replacement: String.Empty);

            return salary;
        }
    }
}
