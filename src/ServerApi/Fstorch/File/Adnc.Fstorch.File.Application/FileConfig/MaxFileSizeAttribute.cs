using System.ComponentModel.DataAnnotations;

namespace Adnc.Fstorch.File.Application.FileConfig
{
    /// <summary>
    /// 文件大小过滤
    /// </summary>
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize; //字节
        //private readonly string[] _extensions;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null || file.Length == 0)
                return new ValidationResult(GetEmptyMessage());
            else if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
                /*var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetTypeMessage());
                }*/
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"文件不能超过{_maxFileSize / 1024 / 1024}MB.";
        }

        private string GetEmptyMessage()
        {
            return $"文件错误, 当前文件大小为0";
        }

        /*private string GetTypeMessage()
        {
            return $"仅允许上传{string.Join(',', _extensions)}类型";
        }*/
    }
}
