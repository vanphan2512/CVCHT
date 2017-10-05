namespace Nc.Erp.WorksSupport.Api.Models.WorksSupport
{
    public class WorksSupportAttachmentParam
    {
        public string AttachmentId { get; set; }
        public int WorksSupportId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileId { get; set; }
    }
}