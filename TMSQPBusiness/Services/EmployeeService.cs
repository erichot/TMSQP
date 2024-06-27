using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Services
{
    public class EmployeeService
    {
        private TMSQPDbContext m_TMSQPDbContext;
        private EmployeeRepository m_EmployeeRepository;

        public EmployeeService(TMSQPDbContext tMSQPDbContext, EmployeeRepository employeeRepository)
        {
            m_TMSQPDbContext = tMSQPDbContext;
            m_EmployeeRepository = employeeRepository;
        }









        public Employee? GetEntity(short _employeeNo, bool _enableTracking = false, bool _includeDetails = true)
        {
            if (_employeeNo.IsNullOrDefault())
                return null;

            return GetEntity(
                new Employee() { EmployeeNo = _employeeNo }, _enableTracking, _includeDetails);
        }
        public async Task<Employee?> GetEntityAsync(short _employeeNo, bool _enableTracking = false, bool _includeDetails = true)
        {
            if (_employeeNo.IsNullOrDefault())
                return null;

            return await
                GetEntityAsync(
                    new Employee() { EmployeeNo = _employeeNo }, _enableTracking, _includeDetails);
        }
        //public Employee? GetEntity(string _employeeId, bool _enableTracking = false, bool _includeDetails = true)
        //{
        //    return GetEntity(
        //        new Employee() { EmployeeId = _employeeId }, _enableTracking, _includeDetails);
        //}
        //public async Task<Employee?> GetEntityAsync(string _employeeId, bool _enableTracking = false, bool _includeDetails = true)
        //{
        //    return await
        //        GetEntityAsync(
        //            new Employee() { EmployeeId = _employeeId }, _enableTracking, _includeDetails);
        //}
        public Employee? GetEntity(Employee _filterInfo, bool _enableTracking = false, bool _includeDetails = true)
        {
            return m_EmployeeRepository.GetEntity(_filterInfo, _enableTracking, _includeDetails);
        }
        public async Task<Employee?> GetEntityAsync(Employee _filterInfo, bool _enableTracking = false, bool _includeDetails = true)
        {
            return await m_EmployeeRepository.GetEntityAsync(_filterInfo, _enableTracking, _includeDetails);
        }






        public List<Employee> GetList(Employee? _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return m_EmployeeRepository.GetList(_info, _enableTracking, _includeDetails)
                .ToList();
        }
        public async Task<List<Employee>> GetListAsync(Employee? _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return await m_EmployeeRepository.GetListAsync(_info, _enableTracking, _includeDetails);
        }








    }
}
