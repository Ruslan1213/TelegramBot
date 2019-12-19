using System.IO;

namespace Introspekt_bot.Service
{
    public class DirectoryInformer
    {
        public string GetCurrentDirrectory()
        {
            var currentDirrectoryArray = Directory.GetCurrentDirectory().Split("\\");
            string currentDirectory = string.Empty;

            for (int i = 0; i < currentDirrectoryArray.Length - 3; i++)
            {
                currentDirectory += currentDirrectoryArray[i] + "\\";
            }

            return currentDirectory;
        }
    }
}
