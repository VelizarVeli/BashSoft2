using BashSoft.IO;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    public class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string input, string[] data, Tester judje, StudentsRepository studentsRepository, IOManager iOManager) : base(input, data, judje, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                throw new InvalidCommandMessage(this.Input);
            }
            this.Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped");
        }
    }
}