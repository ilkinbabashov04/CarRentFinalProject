namespace Ui.Resources
{
    using System.Reflection;
    using System.Resources;

    public static class Resource
    {
        private static readonly ResourceManager resourceMan =
            new ResourceManager("Ui.Resources.Resource", Assembly.GetExecutingAssembly());

        public static string GetString(string key)
        {
            return resourceMan.GetString(key) ?? $"[{key}] not found";
        }
    }
}
