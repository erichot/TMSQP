using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TMSQPWeb.Pages.Payslips
{
    public class ListEmpModel : PageModel
    {
        private readonly PayslipImportBindingService m_PayslipImportService;

        public ListEmpModel(PayslipImportBindingService payslipImportService)
        {
            m_PayslipImportService = payslipImportService;
        }


        public PayslipImportViewModel? PG_Info { get; set; }
        public List<PayslipImportHeadViewModel> PG_List { get; set; }



        public void OnGet(short? _pn)
        {
            if (_pn.IsNullOrDefault() == false)
            {
                PG_Info = m_PayslipImportService.GetEntity(_pn.ToShort());
                PG_List = m_PayslipImportService.GetHeadList(_pn.ToShort());
            }
        }



    }
}
