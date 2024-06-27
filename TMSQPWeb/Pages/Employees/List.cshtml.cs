using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TMSQPWeb.Pages.Employees
{
    public class ListModel : PageModel
    {
        private readonly EmployeeBindingService m_EmployeeBindingService;

        public ListModel(EmployeeBindingService employeeBindingService)
        {
            m_EmployeeBindingService = employeeBindingService;
        }


        public List<EmployeeViewModel> PG_List { get; set; }


        public void OnGet()
        {
            PG_List = m_EmployeeBindingService.GetList(null, false, false);
        }

    }
}
