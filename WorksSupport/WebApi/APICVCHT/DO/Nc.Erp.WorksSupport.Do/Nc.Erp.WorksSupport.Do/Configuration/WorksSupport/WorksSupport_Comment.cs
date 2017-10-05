using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nc.Erp.WorksSupport.Do.Configuration.WorksSupport
{
    /// <summary>
    /// Created by 		: Luong Trung Nhan 
    /// Created date 	: 15/06/2017 
    /// 
    /// </summary>	
    public class WorksSupportComment
    {
        public int CommentId { get; set; }

        public int WorksSupportId { get; set; }

        public DateTime? CommentDate { get; set; }

        public string CommentContent { get; set; }

        public string CommentUser { get; set; }

        public string FullName { get; set; }
        public string PositionName { get; set; }

        public string DepartmentName { get; set; }

        public string DefaultPictureUrl { get; set; }

        //List file đính kèm theo comment
        public List<WorksSupportCommentAttachment> ListCommentAttachment { get; set; }
    }


}
