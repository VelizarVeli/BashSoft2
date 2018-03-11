using System;

namespace BashSoft.Exceptions
{
    public class StudentAlreadyEnrolledInGivenCourse : Exception
    {
        private const string StudentAlreadyExists = "The {0} already exists in {1}.";

        public StudentAlreadyEnrolledInGivenCourse(string message)
        :base(string.Format(StudentAlreadyExists,message))
        {
        }
    }
}
