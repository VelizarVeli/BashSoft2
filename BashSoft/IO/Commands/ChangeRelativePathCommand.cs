using BashSoft.IO;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    public class ChangePathRelativelyCommand : Command
    {
        public ChangePathRelativelyCommand(string input, string[] data, Tester judje, StudentsRepository studentsRepository, IOManager iOManager) : base(input, data, judje, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string path = this.Data[1];
                this.InputOutputManager.ChangeCurrentDirectoryRelative(path);
            }
            else
            {
                throw new InvalidCommandMessage(this.Data[1]);
            }
        }
    }
}