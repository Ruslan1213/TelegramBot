namespace Introspekt_bot.Abstraction
{
    public abstract class Command
    {
        public virtual string Name { get; }

        public abstract void Execute();
    }
}
