using Microsoft.Extensions.Hosting;
namespace Adnc.Fstorch.File.Application.FileTask
{
    public class CleanTempFileTask : BackgroundService
    {
        // 设置间隔时间
        private readonly TimeSpan CleanupInterval = TimeSpan.FromMinutes(5);
        private readonly string _directoryPath  = $"wwwroot/Temp";
        
        public CleanTempFileTask()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);
            while (!stoppingToken.IsCancellationRequested)
            {
                // 执行清理操作
                DeleteFilesInDirectory(_directoryPath);

                // 等待指定的间隔时间
                await Task.Delay(CleanupInterval, stoppingToken);
            }
        }

        private void DeleteFilesInDirectory(string directoryPath)
        {
            // 获取目录中的所有文件
            var files = Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                // 删除文件
                var creationTime = System.IO.File.GetCreationTime(file);
                var now = DateTime.Now;
                var difference = now - creationTime;
                if (difference.Minutes >= 5)
                    System.IO.File.Delete(file);
            }
        }
    }
}
