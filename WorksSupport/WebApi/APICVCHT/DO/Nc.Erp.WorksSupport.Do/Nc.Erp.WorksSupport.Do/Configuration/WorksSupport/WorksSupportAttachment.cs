
namespace Nc.Erp.WorksSupport.Do.Configuration.WorksSupport
{
    /// <summary>
	/// Created by 		: Luong Trung Nhan
	/// Created date 	: 17/06/2017 
	/// Danh sách file đính kèm của công việc cần hỗ trợ
	/// </summary>	
    public class WorksSupportAttachment
    {
        public string FileId { get; set; }

		/// <summary>
		/// AttachmentID
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

    }
}
