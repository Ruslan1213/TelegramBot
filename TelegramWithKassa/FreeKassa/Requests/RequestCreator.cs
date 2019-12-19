using System;
using System.IO;
using System.Net;
using FreeKassa.FreeKassaUrls;

namespace FreeKassa.Requests
{
    public class RequestCreator
    {
        public string GetAnythink()
        {
            string url = new GetOrdersUrl().GetUrl();

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();

            if (dataStream != null)
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                dataStream.Dispose();

                return responseFromServer;
            }

            return string.Empty;
        }

        public string DirProject()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string substring = currentDirectory;

            for (int counterSlash = 0; counterSlash < 4; counterSlash++)
            {
                substring = substring.Substring(0, substring.LastIndexOf(@"\", StringComparison.Ordinal));
            }

            return substring + @"\FreeKassa\Files\1.xml";
        }
    }
}
