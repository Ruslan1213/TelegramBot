namespace FreeKassa.FreeKassaUrls
{
    public abstract class UrlCreator
    {
        public virtual string Name { get; set; }

        public abstract string GetUrl();
    }
}
