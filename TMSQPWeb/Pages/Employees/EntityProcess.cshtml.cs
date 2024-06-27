using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TMSQPWeb.Pages.Employees
{
    public class EntityProcessModel : PageModel
    {
        private readonly EmployeeBindingService m_EmployeeBindingService;

        public EntityProcessModel(EmployeeBindingService employeeBindingService)
        {
            m_EmployeeBindingService = employeeBindingService;
        }

        public EmployeeViewModel PG_Info { get; set; } 


        public void OnGet(short? _no)
        {
            if (_no.IsNullOrDefault()==false)
            {
                PG_Info = m_EmployeeBindingService.GetEntity(_no.ToShort());

            }
        }
    }
}
