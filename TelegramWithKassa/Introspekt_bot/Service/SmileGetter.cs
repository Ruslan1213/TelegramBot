using System.Text;

namespace Introspekt_bot.Service
{
    public class SmileGetter
    {
        private readonly Encoding encoding;

        public SmileGetter()
        {
            encoding = new UTF8Encoding(true, true);
        }

        public string GetSmile(string smile)
        {
            return encoding.GetString(encoding.GetBytes(smile));
        }
    }
}
