using FreeKassa.Constants;
using FreeKassa.Infrastructure;

namespace FreeKassa.FreeKassaUrls
{
    public class UrlToPayOrder
    {
        public string marketId = WordsAndId.Id;
        public string SecretKey;//ID Вашего магазина:Сумма платежа:Секретное слово:Номер заказа
        private readonly string Url = @"http://www.free-kassa.ru/merchant/cash.php";
        private int Summ;
        private int OrderNumber;

        public UrlToPayOrder(int summ, int orderNumber)
        {
            Summ = summ;
            OrderNumber = orderNumber;
            SecretKey = new Md5Hashing().GetMd5Hash(marketId + ":" + Summ + ":" + WordsAndId.FirstSecretWord + ":" + OrderNumber);
        }

        public string GetUrlOredrPay()
        {
            return Url + "?m=" + marketId + "&oa=" + Summ + "&o=" + OrderNumber + "&s=" + SecretKey;
        }
    }
}
