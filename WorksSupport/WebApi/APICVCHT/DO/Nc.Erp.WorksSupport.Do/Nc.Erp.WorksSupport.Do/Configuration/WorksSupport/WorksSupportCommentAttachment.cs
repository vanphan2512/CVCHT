namespace Nc.Erp.WorksSupport.Do.Configuration.WorksSupport
{
    using System;

    public class WorksSupportCommentAttachment
    {
        /// <summary>
        /// CommandId
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// FileId
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// AttachmentId
        /// 
        /// </summary>
        public string AttachmentId { get; set; }

        /// <summary>
        /// WorksSupportId
        /// Mã công việc cần hỗ trợ
        /// </summary>
        public int WorksSupportId { get; set; }

        /// <summary>
        /// FilePath
        /// Đường dẫn URL file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// FileName
        /// Tên file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// CommentDate
        /// </summary>
        public DateTime? CommentDate { get; set; }
    }
}
