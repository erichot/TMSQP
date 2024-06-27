using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TMSQPWeb.ViewModels
{
    public class EmployeeViewModel:Employee
    {


        public bool IsReadonly { get; set; }


        [ValidateNever]
        public string HtmlAttrDisabled => (m_formEditMode == FormEditModeEnum.Read) ? "disabled" : null;

        [ValidateNever]
        public string HtmlAttrDisabledIfNotTms => (m_formEditMode == FormEditModeEnum.Read == true) ? "disabled" : null;

        private FormEditModeEnum m_formEditMode = FormEditModeEnum.Add;


        [ValidateNever]
        public FormEditModeEnum FormEditMode
        {
            get
            {
                return m_formEditMode;
            }
            set
            {
                m_formEditMode = value;
                if (m_formEditMode == FormEditModeEnum.Read) IsReadonly = true;
            }
        }




        public string CreatedShortDateString => this.CreateDate.ToDateStringForUrl();



        public Employee ToEntity()
        {
            return new Employee()
            {
                EmployeeNo = this.EmployeeNo,
                EmployeeId = this.EmployeeId,
                EmployeeName = this.EmployeeName,
                Email = this.Email,
                Remark = this.Remark,
                CreateDate = this.CreateDate,
                CreatePerson = this.CreatePerson,
                UpdatePerson = this.UpdatePerson,
                UpdateDate = this.UpdateDate,
            };

        } 


    }
}
