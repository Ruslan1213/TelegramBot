using FreeKassa.Constants;
using FreeKassa.Infrastructure;

namespace FreeKassa.FreeKassaUrls
{
    public class GetOrdersUrl : UrlCreator
    {
        public string MarketId = WordsAndId.Id;

        public override string Name { get => "GetOrders";}

        public string SecretKey { get; }

        public string Action = "get_orders";

        private readonly string Url = @"http://www.free-kassa.ru/export.php";

        public GetOrdersUrl()
        {
            SecretKey = new Md5Hashing().GetMd5Hash(MarketId + WordsAndId.SecondSecretWord);
        }

        public override string GetUrl()
        {
            return Url + "?merchant_id=" + MarketId + "&s=" + SecretKey + "&action=" + Action;
        }
    }
}
