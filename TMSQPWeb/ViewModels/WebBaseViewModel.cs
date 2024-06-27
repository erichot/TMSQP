namespace TMSQPWeb.ViewModels
{
    public class WebBaseViewModel
    {
    }




    public static class WebBaseUI
    {
        public const string Form_Btn_Back = "Back";
        public const string Form_Btn_BackToList = "Back to list";
        public const string Form_Btn_Refresh = "Refresh(R)";
        public const string Form_Btn_Export = "Export(X)";


        public const string Form_Btn_Add = "Add(A)";
        public const string Form_Btn_Edit = "Edit";
        public const string Form_Btn_Remove = "Remove";

        public const string Form_Btn_Delete = "Delete";
        public const string Form_Btn_Insert = "Insert";
        public const string Form_Btn_Update = "Update";


        public const string Form_Btn_SubmitForEdit = "修改並送審";

        public const string Form_Btn_Review = "覆核";
        public const string Form_Btn_Approve = "核准(Enter)";
        public const string Form_Btn_Reject = "駁回";

        public const string Form_Btn_AddDetailEntry = "Add detail(A)";

        public const string Form_Btn_Print = "Print(P)";
        public const string Form_Btn_ExportToPDF = "Export To PDF";
        public const string Form_Btn_ExportToExcel = "Export To Excel";

        public const string ColHeader_DataStatus = "資料狀態";
        public const string ColHeader_ControlField_Edit = "Edit";
        public const string ColHeader_ControlField_Remove = "Remove";
        public const string ColHeader_ControlField_Review = "覆核";
        public const string ColHeader_CreateDate = "建檔日期";
        public const string ColHeader_EditDate = "修改日期";
        public const string ColHeader_ApproveDate = "覆核日期";
        public const string ColHeader_CreatePerson = "建檔人員";
        public const string ColHeader_EditPerson = "修改人員";
        public const string ColHeader_ApprovePerson = "覆核人員";

        // Alert / Sweet alert
        public const string Form_Alt_Deleted_Title = "Complete";
        public const string Form_Alt_Deleted_Msg = "Deleted!";
        public const string Form_Alt_Submitted_Title = "已提交";
        public const string Form_Alt_Submitted_Msg = "送審中";
        public const string Form_Alt_Review_Rejected_Title = "駁回";
        public const string Form_Alt_Review_Rejected_Msg = "已駁回完成";
        public const string Form_Alt_Review_Approved_Title = "核准";
        public const string Form_Alt_Review_Approved_Msg = "已核准完成";
        public const string Form_Alt_Signed_Title = "已簽收";
        public const string Form_Alt_Signed_Msg = "簽收完成";
        public const string Form_Alt_Checkouted_Title = "會計日結作業";
        public const string Form_Alt_Checkouted_Msg = "已完成";
        public const string Form_Alt_DataNotFound_Title = "查無資料";
        public const string Form_Alt_DataNotFound_Msg = "請嘗試再次操作";
        public const string Form_Alt_Prohibit_HasCheckedOutToday_Title = "禁止作業";
        public const string Form_Alt_Prohibit_HasCheckedOutToday_Msg = "本日已結帳";

        public const string Form_Msg_ModelStateInValid = "Please check each field";
        public const string Form_Msg_ModelStateRequired = "欄位不允許空白，請重新輸入";

        //Notification
        public const string Form_Msg_NTF_OperationInserted = "Created";
        public const string Form_Msg_NTF_OperationUpdated = "Updated";
        public const string Form_Msg_NTF_OperationDeleted = "Deleted";
        public const string Form_Msg_NTF_SubmitCompleted = "送審完成";
        public const string Form_Msg_NTF_SubmitError = "送審失敗。";
        public const string Form_Msg_NTF_ReviewCompleted = "覆核完成。";
        public const string Form_Msg_NTF_ReviewThenApproved = "已核准完成。";
        public const string Form_Msg_NTF_ReviewThenRejected = "已駁回完成。";
        public const string Form_Msg_NTF_SignCompleted = "簽收完成";
        public const string Form_Msg_NTF_WarningHasCheckedOutToday = "本日已結帳";

        public const string Form_MainInfo_Label_NoData = "no data";



        public const string Tooltip_Btn_AddWithContractSelector = "開啟另一頁面，並選擇【信託契約】後，再繼續下一步作業";


        public const string PAGEID_Attendances_Outsources_Imports_List = "O011001";
        public const string PAGEID_Attendances_Outsources_Imports_EntityProcess = "O011011";
        public const string PAGEID_Attendances_Outsources_Imports_Upload = "O011008";
        public const string PAGEID_Attendances_Outsources_Daily_ListEdit = "O021002";

        public const string PAGEID_Budgets_GSPImports_EntityProcess = "B011011";
        public const string PAGEID_Budgets_GSPImports_List = "B011001";
        public const string PAGEID_Budgets_GSPImports_Upload = "B011008";

        public const string PAGEID_HeadcountSettings_ListEdit = "B011102";
        public const string PAGEID_HeadcountSettings_YearlyTable = "B011105";

        public const string PAGEID_Employees_EntityProcess = "E010011";
        public const string PAGEID_Employees_List = "E010001";

        public const string PAGEID_Essentials_AssociateTypes_List = "N031001";
        public const string PAGEID_Essentials_Departments_ListEdit = "N021002";
        public const string PAGEID_Essentials_Designations_ListEdit = "N051002";
        public const string PAGEID_Essentials_Divisions_ListEdit = "N011002";
        public const string PAGEID_Essentials_EmploymentTypes_List = "N041001";
        public const string PAGEID_Essentials_LabourTypes_ListEdit = "N061002";

        public const string PAGEID_RoleMenuitems_ListEdit = "S021002";
        public const string PAGEID_Role_ListEdit = "S011002";

        public const string PAGEID_Systems_SysConfigs_CutOffTimeSetting = "S041003";
        public const string PAGEID_Systems_ChangePassword = "S041105";

        public const string PAGEID_Users_EntityProcess = "S031011";
        public const string PAGEID_Users_List = "S031001";

    }




    public class PagePromptSwalEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public string RedirectURL { get; set; }
    }




    public class PageHeaderEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string FullTitle => Title + " " + SubTitle;
        public string PageId { get; set; }


    }



}
