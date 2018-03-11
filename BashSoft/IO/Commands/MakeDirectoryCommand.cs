using BashSoft.NetCore.Executor.IO.Commands;

namespace BashSoft.IO.Commands
{
    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string input, string[] data, Tester judge, StudentsRepository studentsRepository, IOManager iOManager)
            : base(input, data, judge, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandMessage(this.Input);
            }

            string folderName = this.Data[1];
            this.InputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
