
namespace TMSQPWeb.BindingServices
{
    public class EmployeeBindingService
    {
        private readonly IMapper m_Mapper;
        private readonly EmployeeService m_EmployeeService;

        public EmployeeBindingService(IMapper mapper, EmployeeService employeeService)
        {
            m_Mapper = mapper;
            m_EmployeeService = employeeService;
        }








        public EmployeeViewModel? GetEntity(short _employeeNo, bool _enableTracking = false, bool _includeDetails = true)
        {            
            return GetEntity(
                new EmployeeViewModel() { EmployeeNo = _employeeNo }, _enableTracking, _includeDetails);
        }
        public async Task<EmployeeViewModel?> GetEntityAsync(short _employeeNo, bool _enableTracking = false, bool _includeDetails = true)
        {
            return await
                GetEntityAsync(
                    new EmployeeViewModel() { EmployeeNo = _employeeNo }, _enableTracking, _includeDetails);
        }
        public EmployeeViewModel? GetEntity(string _employeeId, bool _enableTracking = false, bool _includeDetails = true)
        {
            return GetEntity(
                new EmployeeViewModel() { EmployeeId = _employeeId }, _enableTracking, _includeDetails);
        }
        public async Task<EmployeeViewModel?> GetEntityAsync(string _employeeId, bool _enableTracking = false, bool _includeDetails = true)
        {
            return await
                GetEntityAsync(
                    new EmployeeViewModel() { EmployeeId = _employeeId }, _enableTracking, _includeDetails);
        }
        public EmployeeViewModel? GetEntity(EmployeeViewModel _filterInfo, bool _enableTracking = false, bool _includeDetails = true)
        {
            var info = m_EmployeeService.GetEntity(_filterInfo, _enableTracking, _includeDetails);
            var result = m_Mapper.Map<EmployeeViewModel>(info);
            return result;
        }
        public async Task<EmployeeViewModel?> GetEntityAsync(EmployeeViewModel _filterInfo, bool _enableTracking = false, bool _includeDetails = true)
        {
            var info = await m_EmployeeService.GetEntityAsync(_filterInfo, _enableTracking, _includeDetails);
            var result = m_Mapper.Map<EmployeeViewModel>(info);
            return result;
        }




        public List<EmployeeViewModel> GetList(EmployeeViewModel? _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            var list = m_EmployeeService.GetList(_info, _enableTracking, _includeDetails);
            var result = (
                from item in list
                select m_Mapper.Map<EmployeeViewModel>(item)
                ).ToList();
            return result;
        }
        public async Task<List<EmployeeViewModel>> GetListAsync(EmployeeViewModel? _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            var list = await m_EmployeeService.GetListAsync(_info, _enableTracking, _includeDetails);
            var result = (
                from item in list
                select m_Mapper.Map<EmployeeViewModel>(item)
                ).ToList();
            return result;        
        }






    }
}
