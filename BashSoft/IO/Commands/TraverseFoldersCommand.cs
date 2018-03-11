using System;
using BashSoft.IO;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    public class TraverseFolderCommands : Command
    {
        public TraverseFolderCommands(string input, string[] data, Tester judje, StudentsRepository studentsRepository, IOManager iOManager) : base(input, data, judje, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                int number;
                bool isNumber = int.TryParse(this.Data[1], out number);

                if (isNumber)
                {
                    this.InputOutputManager.TraverseDirectory(number);
                }
                else
                {
                    throw new InvalidOperationException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                throw new InvalidCommandMessage(this.Input);
            }
        }
    }
}