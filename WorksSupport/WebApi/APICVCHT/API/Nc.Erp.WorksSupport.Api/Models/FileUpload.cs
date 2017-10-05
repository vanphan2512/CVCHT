namespace Nc.Erp.WorksSupport.Api.Models
{
    public class FileUpload
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }

        public string FileId { get; set; }

        public string FilePath { get; set; }

        /// <summary>
        /// this should be called by EF only
        /// </summary>
        public FileUpload() { }

        public FileUpload(string fileName, string contentType, long fileSize, byte[] content)
        {
            this.FileName = fileName;
            this.ContentType = contentType;
            this.Size = fileSize;
            this.Content = content;
        }
    }
}