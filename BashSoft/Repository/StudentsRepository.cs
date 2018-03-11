using System;
using System.Linq;
using BashSoft.Models;

namespace BashSoft
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public class StudentsRepository
    {
        private bool isDataInitialized;
        private Dictionary<string, Dictionary<string, List<int>>> studentByCourse;
        private RepositoryFilter filter;
        private RepositorySorter sorter;
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;

        public StudentsRepository(RepositoryFilter filter, RepositorySorter sorter)
        {
            this.filter = filter;
            this.sorter = sorter;
            this.studentByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
        }
        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitialisedException);
                return;
            }

            this.students = new Dictionary<string, Student>();
            this.courses = new Dictionary<string, Course>();
            this.ReadData(fileName);
            //OutputWriter.WriteMessageOnNewLine("Reading data..."); не знам дали да го оставя
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            this.students = null;
            this.courses = null;
            //this.studentByCourse = new Dictionary<string, Dictionary<string, List<int>>>(); не знам дали да го оставя
            this.isDataInitialized = false;
        }

        private void ReadData(string path)
        {
            string pattern = @"([A-Z][a-zA-Z#\+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
            Regex regex = new Regex(pattern);

            string[] lines = System.IO.File.ReadAllLines(path);

            try
            {
                foreach (var info in lines)
                {
                    Match match = regex.Match(info);
                    string scoreStr = match.Groups[3].Value;

                    string[] tokens = info.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    string courseName = tokens[0];
                    string username = tokens[1];
                    int marks = int.Parse(tokens[2]);

                    int[] scores = scoreStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                    if (scores.Any(x => x > 100 || x < 0))
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                    }

                    if (scores.Length > Course.NumberOfTasksOnExam)
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                        continue;
                    }

                    if (!this.students.ContainsKey(username))
                    {
                        this.students.Add(username, new Student(username));
                    }

                    if (!this.courses.ContainsKey(courseName))
                    {
                        this.courses.Add(courseName, new Course(courseName));
                    }

                    Course course = this.courses[courseName];
                    Student student = this.students[username];

                    student.EnrollInCourse(course);
                    student.SetMarksInCourse(courseName, scores);
                    course.EnrollStudent(student);
                }
            }
            catch (FormatException fex)
            {
                OutputWriter.DisplayException(fex.Message + $"at line : {lines}");
            }
            isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (studentByCourse.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private bool IsQueryForStudentPossiblе(string courseName, string studentUserName)
        {
            if (this.IsQueryForCoursePossible(courseName) && this.courses[courseName].studentsByName.ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            }
            return false;
        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossiblе(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, this.courses[courseName]
                    .studentsByName[username].marksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");

                foreach (var studentMarksEntry in this.courses[courseName].studentsByName)
                {
                    this.GetStudentScoresFromCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].studentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].studentsByName
                    .ToDictionary(x => x.Key, x => x.Value.marksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].studentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].studentsByName
                    .ToDictionary(x => x.Key, x => x.Value.marksByCourseName[courseName]);

                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }
    }
}