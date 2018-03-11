using System;
using System.Collections.Generic;
using BashSoft.Exceptions;
using BashSoft.NetCore;

namespace BashSoft.Models
{
   public class Course
   {
       public string name;
       private Dictionary<string, Student> studentsByName;
       public const int NumberOfTasksOnExam = 5;
       public const int MaxScoreOnExamTask = 100;

       public Course(string name)
       {
           this.name = name;
           this.studentsByName = new Dictionary<string, Student>();
       }

       public string Name
       {
           get { return name; }
           set
           {
               if (String.IsNullOrEmpty(value))
               {
                   throw new InvalidStringException();
               }
           }
       }
       public IReadOnlyDictionary<string, Student> StudentsByName
       {
           get { return studentsByName; }
       }

       public void EnrollStudent(Student student)
       {
           if (this.studentsByName.ContainsKey(student.Username))
           {
               throw new DuplicateEntryInStructureException(student.Username,this.Name);
           }
           this.studentsByName.Add(student.Username, student);
       }
   }
}
