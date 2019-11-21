namespace CocktailWizard.Services.Providers
{
    public interface IFileServiceProvider
    {
        bool FileExists(string filePath);
        (bool result, string message) CreateFolder(string filePath);
    }
}