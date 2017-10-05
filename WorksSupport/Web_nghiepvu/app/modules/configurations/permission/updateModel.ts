import { ValidationException } from "../../../common/models/exception";
export class UpdateWorksTypeModel {
    public Id: number = 0;
    public WorksSupportTypeName: string = "";
    public IconUrL: string = "";
   // public AddFunctionId: string = "";
   // public ViewAllFunctionId: string = "";
   // public EditFunctionId: string = "";
   // public EditAllFunctionId: string = "";
  // public DeleteFunctionId: string = "";
  // public DeleteAllFunctionId: string = "";
    // public ProcessFunctionId: string = "";
    // public CommentFunctionId: string = "";
    public Description: string = "";
    public OrderIndex: number = -1;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public User: string = "";
    // public AddProjectFunctionId : string ="";


    public import(item: any) {
        if (!item) { return; }
        this.Id = item.WorksSupportTypeId;
        this.WorksSupportTypeName = item.WorksSupportTypeName;
        // this.AddFunctionId = item.AddFunctionId;
        // this.ViewAllFunctionId = item.ViewAllFunctionId;
        // this.EditFunctionId = item.EditFunctionId;
        // this.EditAllFunctionId = item.EditAllFunctionId;
        // this.DeleteFunctionId = item.DeleteFunctionId;
        // this.DeleteAllFunctionId = item.DeleteAllFunctionId;
        // this.ProcessFunctionId = item.ProcessFunctionId;
        // this.CommentFunctionId = item.CommentFunctionId;
        this.Description = item.Description;
        this.OrderIndex = item.OrderIndex;
        // this.AddProjectFunctionId = item.AddProjectFunctionId;

        if (item.IsActived === 1 || item.IsActived) {
            this.IsActived = 1;
        }
        else{
            this.IsActived = 0;
        }
        if (item.IsSystem === true) {
            this.IsSystem = 1;
        }
        else {
            this.IsSystem = 0;
        }
        if (item.IconUrl !== null && item.IconUrl !== "") {
            this.IconUrL = item.IconUrl;
        }

        this.User = "administrator";
    }
}