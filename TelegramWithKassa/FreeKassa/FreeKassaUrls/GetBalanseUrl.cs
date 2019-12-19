using FreeKassa.Constants;

namespace FreeKassa.FreeKassaUrls
{
    public class GetBalanseUrl : UrlCreator
    {
        private string Url = @"http://www.free-kassa.ru/api.php?merchant_id="+ WordsAndId.Id + "&s=";

        private const string action = @"&action=get_balance";

        public string Option;

        public override string Name { get => "GetBalanse"; }

        public GetBalanseUrl(string option)
        {
            Option = option;
        }

        public override string GetUrl()
        {
            return Url + Option + action;
        }
    }
}
