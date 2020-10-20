public class AppConfiguration
{
    public string SymbolFromAppsettings
    {
        get;
        set;
    }

    public string NDayFromAppsettings
    {
        get;
        set;
    }

    public string ApiKeyFromAppsettings
    {
        get;
        set;
    }

    public string SymbolFromKubernetesEnv
    {
        get;
        set;
    }

    public string NDayFromKubernetesEnv
    {
        get;
        set;
    }

    public string ApiKeyFromKubernetesEnv
    {
        get;
        set;
    }
}