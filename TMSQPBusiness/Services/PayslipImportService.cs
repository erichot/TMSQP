using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;



namespace TMSQPBusiness.Services
{
    public class PayslipImportService
    {
        private TMSQPDbContext m_TMSQPDbContext;
        private PayslipImportRepository m_PayslipImportRepository;
        private PayslipImportHeadRepository m_PayslipImportHeadRepository;

        public PayslipImportService(TMSQPDbContext tMSQPDbContext)
        {
            m_TMSQPDbContext = tMSQPDbContext;
            m_PayslipImportRepository = new PayslipImportRepository(tMSQPDbContext);
            m_PayslipImportHeadRepository = new PayslipImportHeadRepository(tMSQPDbContext);
        }







        #region "PayslipImport"

        public IQueryable<PayslipImport> GetQuery()
        {
            return m_PayslipImportRepository.GetQuery(null);
        }





        public List<PayslipImport> GetList()
        {
            return m_PayslipImportRepository.GetList(null);
        }
        public async Task<List<PayslipImport>> GetListAsync()
        {
            return await m_PayslipImportRepository.GetListAsync(null);
        }






        public PayslipImport? GetEntity(short _piNo, bool _enableTracking = false, bool _includeDetails = true)
        {
            if (_piNo.IsNullOrDefault())
                return null;

            return m_PayslipImportRepository.GetEntity(
                new PayslipImport() { PINo = _piNo }, _enableTracking, _includeDetails);
        }
        public async Task<PayslipImport?> GetEntityAsync(short _piNo, bool _enableTracking = false, bool _includeDetails = true)
        {
            if (_piNo.IsNullOrDefault())
                return null;

            return await
                m_PayslipImportRepository.GetEntityAsync(
                    new PayslipImport() { PINo = _piNo }, _enableTracking, _includeDetails);
        }







        public async Task<PayslipImport> AddEntityAsync(PayslipImport _info)
        {
            var inserted = m_PayslipImportRepository.AddEntity(_info);
            await m_TMSQPDbContext.SaveChangesAsync();
            return inserted;
        }















        public async Task<BusinessProcessResult> ProcessToImportAsync(PayslipImport _info, List<PayslipImportHead> _heads)
        {
            var result = new BusinessProcessResult();
            //var inserted = m_PayslipImportRepository.AddEntity(_info);
            //await m_PayslipImportRepository.SaveDbChangesAsync();
            var piNo = _heads.FirstOrDefault().PINo;

            //var piNo = inserted.PINo;
            //_heads.ForEach(r => r.PINo = piNo);

            var updating = await m_PayslipImportRepository.GetEntityAsync(piNo, _enableTracking: true);
            if (updating == null)
            {
                result.SetErrorMessage("Payslip Import not found.");
                return result;
            }

            updating.SetForImport();
            updating.NumberOfEmployees = _heads.Count();
            updating.SalaryYearMonth = BusinessCultureHelper.GetDateFromPeriodString(_heads.FirstOrDefault().Period);

            var insertedHeads = m_PayslipImportHeadRepository.AddEntitiesAsync(_heads);


            m_PayslipImportRepository.UpdateEntity(updating);

            
            await m_TMSQPDbContext.SaveChangesAsync();

           
            return result;
        }

        #endregion



















        #region "PayslipImportHead"
        public List<PayslipImportHead> GetHeadList(short _pINo, bool _enableTracking = false, bool _includeDetails = false)
        {
            return GetHeadList(new PayslipImportHead() { PINo = _pINo }, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
        }
        public async Task<List<PayslipImportHead>> GetHeadListAsync(short _pINo, bool _enableTracking = false, bool _includeDetails = false)
        {
            return await GetHeadListAsync(new PayslipImportHead() { PINo = _pINo }, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
        }
        public List<PayslipImportHead> GetHeadList(PayslipImportHead? _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return m_PayslipImportHeadRepository.GetList(_info, _enableTracking:_enableTracking, _includeDetails:_includeDetails);
        }
        public async Task<List<PayslipImportHead>> GetHeadListAsync(PayslipImportHead? _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return await m_PayslipImportHeadRepository.GetListAsync(_info, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
        }

        public PayslipImportHead? GetHeadEntity(short _piNo, short _headerNo, bool _enableTracking = false, bool _includeDetails = false)
        {
            if (_piNo.IsNullOrDefault() || _headerNo.IsNullOrDefault()) return null;
            return m_PayslipImportHeadRepository.GetEntity(_piNo,_headerNo, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
        }
        public async Task<PayslipImportHead?> GetHeadEntityAsync(short _piNo, short _headerNo, bool _enableTracking = false, bool _includeDetails = false)
        {
            if (_piNo.IsNullOrDefault() || _headerNo.IsNullOrDefault()) return null;
            return await m_PayslipImportHeadRepository.GetEntityAsync(_piNo, _headerNo, _enableTracking: _enableTracking, _includeDetails: _includeDetails);
        }
        #endregion






    }
}
