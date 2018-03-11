using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BashSoft.Exceptions;
using BashSoft.NetCore;

namespace BashSoft.Models
{
    public class Student
    {
        private string userName;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public Student(string userName)
        {
            this.Username = userName;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public IReadOnlyDictionary<string, Course> EnrolledCourses
        {
            get { return enrolledCourses; }
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get { return marksByCourseName; }
        }

        public string Username
        {
            get { return userName; }

            private set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.userName = value;
            }
        }

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCourses.ContainsKey(course.name))
            {
                throw new DuplicateEntryInStructureException(this.userName, course.Name);
            }
            this.enrolledCourses.Add(course.name, course);
        }

        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                throw new CourseNotFoundException(courseName);
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                throw new ArgumentOutOfRangeException($"{scores.Length}", ExceptionMessages.InvalidNumberOfScores);
            }
            this.marksByCourseName.Add(courseName,CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double PercentageOfSolvedExam =
                scores.Sum() / (double) (Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);
            double mark = PercentageOfSolvedExam * 4 + 2;
            return mark;
        }
    }
}
