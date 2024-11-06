namespace Adnc.Fstorch.File.Application.FileConfig
{
    public class AllowedExtensionsAndFileSizeAttribute : ValidationAttribute
    {
        private readonly string[] _fileExtension;
        private readonly int _imgFileSize;
        private readonly int _videoFileSize;
        public AllowedExtensionsAndFileSizeAttribute(string[] fileExtension, int ImgFileSize, int VideoFileSize)
        {
            _fileExtension = fileExtension;
            _imgFileSize = ImgFileSize;
            _videoFileSize = VideoFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null || file.Length == 0)
                return new ValidationResult(GetEmptyMessage());
            else if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_fileExtension.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
                else if(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" }.Contains(extension.ToLower()))
                {
                    if (file.Length > _imgFileSize)
                    {
                        return new ValidationResult(GetErrorImgMessage());
                    }
                }
                else if (new string[] { ".mp4", ".mov", ".wmv" }.Contains(extension.ToLower()))
                {
                    if (file.Length > _videoFileSize)
                    {
                        return new ValidationResult(GetErrorVideoMessage());
                    }
                }
                /*var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetTypeMessage());
                }*/
            }
            return ValidationResult.Success;
        }


        private string GetEmptyMessage()
        {
            return $"文件错误, 当前文件大小为0";
        }

        private string GetErrorMessage()
        {
            return $"仅允许上传{string.Join(',', _fileExtension)}类型";
        }

        private string GetErrorImgMessage()
        {
            return $"图片大小不能超过{_imgFileSize / 1024 / 1024}MB";
        }

        private string GetErrorVideoMessage()
        {
            return $"视频大小不能超过{_videoFileSize / 1024 / 1024}MB";
        }
    }
}
