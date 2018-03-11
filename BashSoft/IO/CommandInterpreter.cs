﻿using System;
using System.IO;

namespace BashSoft
{
    using System.Diagnostics;

    class CommandInterpreter
    {
        private Tester judge;
        private StudentsRepository repository;
        private IOManager inputOutputManager;

        public CommandInterpreter(Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpredCommand(string input)
        {
            string[] data = input.Split(' ');
            string command = data[0];

            try
            {
                this.ParseCommand(input, command, data);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                OutputWriter.DisplayException(dnfe.Message);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                OutputWriter.DisplayException(aoore.Message);
            }
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private void ParseCommand(string input, string command, string[] data)
        {
            switch (command)
            {
                case "open":
                    TryOpenFile(input, data);
                    break;

                case "mkdir":
                    TryCreateDirectory(input, data);
                    break;

                case "ls":
                    TryTraverseFolders(input, data);
                    break;

                case "cmp":
                    TryCompareFiles(input, data);
                    break;

                case "cdRel":
                    TryChangePathRelatively(input, data);
                    break;

                case "cdAbs":
                    TryChangePathAbsolute(input, data);
                    break;

                case "readDb":
                    TryReadDatabaseFromFile(input, data);
                    break;

                case "show":
                    TryShowWantedData(input, data);
                    break;

                case "help":
                    TryGetHelp(input, data);
                    break;

                case "filter":
                    TryFilterAndTake(input, data);
                    break;

                case "order":
                    TryOrderAndTake(input, data);
                    break;

                case "decOrder":
                    // TODO
                    break;

                case "download":
                    // TODO
                    break;

                case "downloadAsynch":
                    // TODO
                    break;

                case "dropdb":
                    TryDropDB(input, data);
                    break;

                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private void TryDropDB(string input, string[] data)
        {
            if (data.Length != 1)
            {
                this.DisplayInvalidCommandMessage(input);
                return;
            }
            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }

        private void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string folderName = data[1];
                this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1)
            {
                this.inputOutputManager.TraverseDirectory(0);
            }
            else if (data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(data[1], out depth);

                if (hasParsed)
                {
                    this.inputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                string firstPath = data[1];
                string secondPath = data[2];

                this.judge.CompareContent(firstPath, secondPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryChangePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string relPath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string absolutePath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                this.repository.LoadData(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string courseName = data[1];
                this.repository.GetAllStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                string courseName = data[1];
                string username = data[2];

                this.repository.GetStudentScoresFromCourse(courseName, username);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryGetHelp(string input, string[] data)
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 122)}");
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "make directory - mkdir path "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "traverse directory - ls depth "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "comparing files - cmp path1 path2"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "change directory - cdRel relative path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "change directory - cdAbs absolute path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "read students data base - readDb path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "show courseName (username) – user name may be omitted"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "download file - download: path of file (saved in current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -121}|", "get help – help"));
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 122)}");
            OutputWriter.WriteEmptyLine();
        }

        private void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string filter = data[2].ToLower();
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand.Equals("take"))
            {
                if (takeQuantity.Equals("all"))
                {
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake = 0;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);

                    if (hasParsed)
                    {
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
            }
        }

        private void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string comparison = data[2];
                string orderCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForOrderAndTake(orderCommand, takeQuantity, courseName, comparison);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForOrderAndTake(string orderCommand, string takeQuantity, string courseName, string comparison)
        {
            if (orderCommand.Equals("take"))
            {
                if (takeQuantity.Equals("all"))
                {
                    this.repository.OrderAndTake(courseName, comparison);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);

                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, comparison, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
            }
        }

        private void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.DisplayException($"The command '{input}' is invalid");
        }
    }
}