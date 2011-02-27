namespace System
{
    public interface IConfigurable<TConfiguration, TNext>
    {
        TNext Configure(Action<TConfiguration> configurator);
    }
}