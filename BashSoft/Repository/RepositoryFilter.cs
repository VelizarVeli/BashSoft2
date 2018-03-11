using System.Security.Cryptography.X509Certificates;

namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RepositoryFilter
    {
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake)
        {
            if (wantedFilter == "excellent")
            {
                FilterAndTake(studentsWithMarks, x => x >= 5.00, studentsToTake);
            }
            else if (wantedFilter == "average")
            {
                this.FilterAndTake(studentsWithMarks, x => x < 5 && x >= 3.50, studentsToTake);
            }
            else if (wantedFilter =="poor")
            {
                FilterAndTake(studentsWithMarks, x => x < 3.50, studentsToTake);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
            }
        }

        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter, int studentsToTake)
        {
            int counterForPrinted = 0;

            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(new KeyValuePair<string, double> (studentMark.Key, studentMark.Value));
                    counterForPrinted++;
                }
            }
        }

        private double Average(List<int> scoresOnTask)
        {
            int totalScore = 0;

            foreach (int score in scoresOnTask)
            {
                totalScore += score;
            }

            double percentageOfAll = (double)totalScore / ((double)scoresOnTask.Count * 100);
            double mark = percentageOfAll * 4 + 2;

            return mark;
        }
    }
}