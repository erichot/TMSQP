


using TMSQPData.Repositories;

namespace TMSQPWeb.BindingServices
{
    public class PayslipImportBindingService
    {
        private readonly IMapper m_Mapper; 
        private readonly PayslipImportService m_PayslipImportService;

        public PayslipImportBindingService(IMapper mapper, PayslipImportService payslipImportService)
        {
            m_Mapper = mapper;
            m_PayslipImportService = payslipImportService;
        }









        #region "Operation"



        public async Task<BusinessProcessResult> ProcessToImportAsync(PayslipImportViewModel _info)
        {
            List<PayslipImportHeadViewModel> _list = _info.PayslipImportHeads;


            var result = new BusinessProcessResult();
            var payslipImportHeads = (
                from item in _list
                select m_Mapper.Map<PayslipImportHead>(item)
                )
                .ToList();

            var inserting = new PayslipImport();
            inserting.PayslipImportHeads = payslipImportHeads;

            var info = m_Mapper.Map<PayslipImport>(_info);

            await m_PayslipImportService.ProcessToImportAsync(info, payslipImportHeads); ;
            return result;
        }


        #endregion













        #region "Payslip Import"

        public List<PayslipImportViewModel> GetList()
        {
            //var query = m_PayslipImportService.GetQuery();
            //var list = await m_Mapper.ProjectTo<PayslipImportViewModel>(query).ToListAsync();
            var list = m_PayslipImportService.GetList().OrderByDescending(c => c.SalaryYearMonth);

            var result = (
                from item in list
                select m_Mapper.Map<PayslipImportViewModel>(item)
                ).ToList();

            return result;
        }
        public async Task<List<PayslipImportViewModel>> GetListAsync()
        {
            //var query = m_PayslipImportService.GetQuery();
            //var list = await m_Mapper.ProjectTo<PayslipImportViewModel>(query).ToListAsync();
            var list = (await m_PayslipImportService.GetListAsync()).OrderByDescending(c => c.SalaryYearMonth); 

            var result = (
                from item in list
                select m_Mapper.Map<PayslipImportViewModel>(item)
                ).ToList();

            return result;
        }







        public async Task<PayslipImport> AddEntityAsync(PayslipImportViewModel? _info)
        {
            var inserting = m_Mapper.Map<PayslipImport>(_info);
            return await m_PayslipImportService.AddEntityAsync(inserting);
        }



        public PayslipImportViewModel? GetEntity(short _piNo, bool _enableTracking = false, bool _includeDetails = true)
        {
            return m_Mapper.Map<PayslipImportViewModel>(
                m_PayslipImportService.GetEntity( _piNo , _enableTracking)
                );
        }
        public async Task<PayslipImportViewModel?> GetEntityAsync(short _piNo, bool _enableTracking = false, bool _includeDetails = true)
        {
            return m_Mapper.Map<PayslipImportViewModel>(
                await
                    m_PayslipImportService.GetEntityAsync( _piNo , _enableTracking, _includeDetails)
                );
        }

        #endregion












        public List<PayslipImportHeadViewModel> GetHeadList(short _pINo, bool _enableTracking = false, bool _includeDetails = false)
        {
            var list = m_PayslipImportService.GetHeadList(_pINo, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
            var result = (
                from item in list
                select m_Mapper.Map<PayslipImportHeadViewModel>(item)
                ).ToList(); 
            return result;
        }
        public async Task<List<PayslipImportHeadViewModel>> GetHeadListAsync(short _pINo, bool _enableTracking = false, bool _includeDetails = false)
        {
            var list = await m_PayslipImportService.GetHeadListAsync(_pINo, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
            var result = (
                from item in list
                select m_Mapper.Map<PayslipImportHeadViewModel>(item)
                ).ToList();
            return result;
        }


        public PayslipImportHeadViewModel? GetHeadEntity(short _piNo, short _headerNo, bool _enableTracking = false, bool _includeDetails = false)
        {
            var info = m_PayslipImportService.GetHeadEntity(_piNo, _headerNo, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
            var result = m_Mapper.Map<PayslipImportHeadViewModel>(info);
            return result;
        }
        public async Task<PayslipImportHeadViewModel?> GetHeadEntityAsync(short _piNo, short _headerNo, bool _enableTracking = false, bool _includeDetails = false)
        {
            var info = await m_PayslipImportService.GetHeadEntityAsync(_piNo, _headerNo, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
            var result = m_Mapper.Map<PayslipImportHeadViewModel>(info);
            return result;
        }









    }
}
