namespace Adnc.Fstorch.File.Application.FileServicesExtensions.Options
{
    public class FileCleanupServiceOptions
    {
        public TimeSpan CleanupInterval { get; set; } = TimeSpan.FromMinutes(5);
        public string? DirectoryPath { get; set; }
    }
}
