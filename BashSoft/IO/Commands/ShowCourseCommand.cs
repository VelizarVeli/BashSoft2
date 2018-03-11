using BashSoft.IO;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    public class ShowCommand : Command
    {
        public ShowCommand(string input, string[] data, Tester judje, StudentsRepository studentsRepository, IOManager iOManager)
            : base(input, data, judje, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string courseName = (this.Data[1]);
                this.Repository.GetAllStudentsFromCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = (this.Data[1]);
                string student = (this.Data[2]);
                this.Repository.GetStudentScoresFromCourse(courseName, student);
            }
            else
            {
                throw new InvalidCommandMessage(this.Input);
            }
        }
    }
}