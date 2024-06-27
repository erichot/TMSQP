using System.ComponentModel.DataAnnotations;

namespace TMSQPWeb.Models
{
    public class AppSystemModel
    {

    }

    public class AppSystem
    {

        public const string VD_PageTitle = "v_pg-tle";
        public const string VD_PageSubTitle = "v_pg-subtle";
        public const string VD_PageFullTitle = "v_pg-fultle";
        public const string VD_PageID = "v_pg-id";
        public const string VD_FormID = "v_fm-id";





        // ViewData UserAccount
        public const string VD_USR_Name = "v_user_name";
        public const string VD_USR_Role_Name = "v_user_role_name";


        // ViewData ListField
        public const string VD_LF_Division = "v_lf_division";
        public const string VD_LF_Department = "v_lf_department";
        public const string VD_LF_LabourType = "v_lf_labourtype";
        public const string VD_LF_EmploymentType = "v_lf_employeetype";
        public const string VD_LF_Designation = "v_lf_designation";
        public const string VD_LF_Role = "v_lf_role";
        public const string VD_LF_SlaveIp = "v_lf_slaveip";


        public const string VD_LF_YearSelection = "v_lf_yearselection";
        public const string VD_LF_MonthSelection = "v_lf_monthselection";

        // TempData when load partial

        // TempData send selected value after redirect page
        public const string TD_SEL_DepartmentNo = "td_sel_dept";

        // TempData JsonText from Object
        public const string TD_JT_CouponItem = "td_jt_couponitem";

        //public const string TD_JT_InventForGuarantee = "td_jt_inventforguarantee";
        public const string TD_UI_PagePrompt_Swal = "td_ui_pgpmt_swal";
        public const string TD_UI_OnPageLoad_TriggerButtonID = "td_ui_onpl_tribtnid";
        public const string TD_UI_OnPageLoad_TriggerFunction = "td_ui_onpl_trifunct";
        public const string TD_UI_OnPageLoad_Message_Notification = "td_ui_msg_opageload_ntf";

        // TempData send selected value after redirect page
        public const string TD_UP_PaySlipFileName = "td_up_payslipfilename";
        public const string TD_UP_GSPUploadFileName = "td_up_gspuploadfilename";
        public const string TD_UP_OutsourceAttendanceFileName = "td_up_outsourceatfilename";

        public const string TD_PT_Employee_TMS_IsShow = "td_pt_employee_tms_isshow";
        public const string TD_PT_Employee_HDMS_IsShow = "td_pt_employee_hdms_isshow";


        //public const string PATH_Coupons = "";
        //public const string PATH_Coupons = "";




        // Column Header
        // Table > Column header > Control Field
        public const string UI_TB_CH_CF_Edit = "Edit";
        public const string UI_TB_CH_CF_Remove = "Remove";


        // 
        public const string UI_TB_CH_Remark = "Remark";
        public const string UI_TB_CH_IsDisabled = "In Active";
        public const string UI_TB_CH_IsSystemPreserved = "System Preserved";
        public const string UI_TB_CH_LastModifiedPerson = "Modified Person";
        public const string UI_TB_CH_LastModifiedDate = "Modified Date";
        public const string UI_TB_CH_CreatePerson = "Create Person";
        public const string UI_TB_CH_CreateDate = "Create Date";
        public const string UI_TB_CH_UpdatePerson = "Update Person";
        public const string UI_TB_CH_UpdateDate = "Update Date";
        public const string UI_TB_CH_InActiveOutsource = "Option in Outsource Attendance";
        public const string UI_TB_CH_InActiveOutsourceAbbr = "Opt.in Out.Attend.";





        public const string PATH_PayslipPdfRepository = "PayslipPdfRepository";




        /// <summary>
        /// 需要承接m_Environment.ContentRootPath
        /// </summary>
        /// <param name="_salaryYear"></param>
        /// <param name="_salaryMonth"></param>
        /// <returns></returns>
        public static string GetRelativePathPayslipPdfRepository(short _salaryYear, byte _salaryMonth)
        {
            var salaryYearText = _salaryYear.ToString();
            var salaryYearMonthText = salaryYearText + _salaryMonth.ToString("00");
            return Path.Combine(PATH_PayslipPdfRepository
                , salaryYearText
                , salaryYearMonthText);
        }
    }








    public enum FormEditModeEnum : byte
    {
        [Display(Name = "View")]
        Read = 0,

        [Display(Name = "Add")]
        Add = 1,

        [Display(Name = "Edit")]
        Edit = 2,

        [Display(Name = "Remove")]
        Remove = 3,

        [Display(Name = "List")]
        List = 100
    }





   


}
