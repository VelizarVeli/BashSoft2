using System.IO;
using BashSoft.IO;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    public class ReadDatabaseCommand : Command
    {
        public ReadDatabaseCommand(string input, string[] data, Tester judje, StudentsRepository studentsRepository, IOManager iOManager) : base(input, data, judje, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string path = Directory.GetCurrentDirectory() + "\\Data" + "\\" + this.Data[1];

                if (File.Exists(path))
                {
                    this.Repository.LoadData(path);
                }
                else
                {
                    throw new InvalidCommandMessage(this.Input);
                }
            }

        }
    }
}