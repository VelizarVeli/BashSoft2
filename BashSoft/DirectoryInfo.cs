using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft
{
    static class DirectoryInfo
    {
        public static void TraverseDirectory(string path)
        {
            OutputWriter.WriteEmptyLine();
            int initialIndentation = path.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(path);

            while (subFolders.Count != 0)
            {

            }
        }
    }
}
