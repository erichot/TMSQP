
namespace TMSQPWeb.ViewModels
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();


            CreateMap<PayslipImport, PayslipImportViewModel>();
            CreateMap<PayslipImportViewModel, PayslipImport>();

            CreateMap<PayslipImportHead, PayslipImportHeadViewModel>();
            CreateMap<PayslipImportHeadViewModel, PayslipImportHead>();

            CreateMap<PayslipImportDetail, PayslipImportDetailViewModel>();
            CreateMap<PayslipImportDetailViewModel, PayslipImportDetail>();
        }
    }
}
