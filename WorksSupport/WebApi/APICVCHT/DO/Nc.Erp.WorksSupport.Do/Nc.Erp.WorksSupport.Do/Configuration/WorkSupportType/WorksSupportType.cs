﻿using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType
{
    /// <summary>
    /// Luong trung nhan
    /// created: 02/06/2017
    /// Chi tiet loai cong viec can ho tro
    /// </summary>
    public class WorksSupportType
    {
        public int WorksSupportTypeId { get; set; }
        public string WorksSupportTypeName { get; set; }
        public string IconUrl { get; set; }
        public string AddFunctionId { get; set; }
        public string ViewAllFunctionId { get; set; }
        public string EditFunctionId { get; set; }
        public string EditAllFunctionId { get; set; }
        public string DeleteFunctionId { get; set; }
        public string DeleteAllFunctionId { get; set; }
        public string ProcessFunctionId { get; set; }
        public string CommentFunctionId { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        public bool ChooseProcessUserType { get; set; }
        public bool ChooseReferenceUserType { get; set; }
        public bool IsHasSolutionContent { get; set; }
        public string AddProjectFunctionId { get; set; }
        public int IsUsed { get; set; }
        //Độ ưu tiên loại công việc
        public List<WorksSupportTypePriority> ListWorksSupportTypePriority { get; set; }
        //Chất lượng loại công việc
        public List<WorksSupportTypeQuality> ListWorksSupportTypeQuality { get; set; }
        //Bước xử lý
        public List<WorksSupportTypeWorkFlow> ListWorksSupportTypeWorkFlow { get; set; }

        //Quyền theo dự án
        public List<WorksSupportProjectPermis> ListWorksSupportProjectPermis { get; set; }
        //Quyền theo vai trò
        public List<WorksSupportTypeMemberRole> ListWorksSupportTypeMemberRole { get; set; }

    }
}