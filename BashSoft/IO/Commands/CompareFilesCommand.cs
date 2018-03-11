using BashSoft.IO;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, Tester judje, StudentsRepository studentsRepository, IOManager iOManager)
            : base(input, data, judje, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 3)
            {
                string firstPath = this.Data[1];
                string secondPath = this.Data[2];

                this.Judge.CompareContent(firstPath, secondPath);
            }
            else
            {
                throw new InvalidCommandMessage(this.Input);
            }
        }
    }
}