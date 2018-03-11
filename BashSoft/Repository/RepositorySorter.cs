namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RepositorySorter
    {
        public void OrderAndTake(Dictionary<string, double> studentMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison.Equals("ascending"))
            {
                this.PrintStudents(studentMarks.OrderBy(x => x.Value).Take(studentsToTake).ToDictionary(x => x.Key, s => s.Value));
            }
            else if (comparison.Equals("descending"))
            {
                PrintStudents(studentMarks.OrderByDescending(x => x.Value).Take(studentsToTake).ToDictionary(x => x.Key, s => s.Value));
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (var kvp in studentsSorted)
            {
                OutputWriter.PrintStudent(kvp);
            }
        }
    }
}