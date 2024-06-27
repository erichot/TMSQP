using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TMSQPWeb.Pages.Payslips
{
    public class ListModel : PageModel
    {

        private readonly PayslipImportBindingService m_PayslipImportService;

        public ListModel(PayslipImportBindingService payslipImportService)
        {
            m_PayslipImportService = payslipImportService;
        }


        public List<PayslipImportViewModel> PG_List { get; set; }


        public async void OnGet()
        {
            //PG_List = await m_PayslipImportService.GetListAsync();
            PG_List = m_PayslipImportService.GetList();

        }








        public async Task<IActionResult> OnGetRemoveAsync(short _piNo)
        {
            if (_piNo.IsNullOrDefault())
            {
                return Page();
            }




            return Page();
        }







    }
}
