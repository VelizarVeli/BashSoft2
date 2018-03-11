using BashSoft.Exceptions;
using BashSoft.IO;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    public class FilterTakeCommands : Command
    {
        public FilterTakeCommands(string input, string[] data, Tester judje, StudentsRepository studentsRepository, IOManager iOManager) : base(input, data, judje, studentsRepository, iOManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 5)
            {
                string courseName = this.Data[1];
                string filter = this.Data[2];
                string takeCommand = this.Data[3].ToLower();
                string takeQuantity = this.Data[4].ToLower();

                TryParamsForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }


        private void TryParamsForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.Repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake = 0;
                    bool isValidNum = int.TryParse(takeQuantity, out studentsToTake);
                    if (isValidNum)
                    {
                        this.Repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);

                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }
    }
}