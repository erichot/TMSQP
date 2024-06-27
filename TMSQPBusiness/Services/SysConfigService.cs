using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Services
{
    public class SysConfigService
    {
        private readonly TMSQPDbContext m_TMSQPDbContext;
        private readonly SysConfigRepository m_SysConfigRepository;

        public SysConfigService(TMSQPDbContext TMSQPDbContext)
        {
            m_TMSQPDbContext = TMSQPDbContext;
            m_SysConfigRepository = new SysConfigRepository(TMSQPDbContext);
        }







        public List<SysConfig> GetList(SysConfig _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return m_SysConfigRepository
                .GetQuery(_info, _enableTracking)
                .ToList();
        }
        public async Task<List<SysConfig>> GetListAsync(SysConfig _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return await m_SysConfigRepository
                .GetQuery(_info, _enableTracking, _includeDetails)
                .ToListAsync();
        }




        public  SysConfig? GetEntity(SysConfigEnum _scSysConfigNo, bool _enableTracking = false, bool _includeDetails = false)
        {
            
            return m_SysConfigRepository
                .GetQuery(_scSysConfigNo, _enableTracking, _includeDetails)
                .FirstOrDefault();
        }
        public async Task<SysConfig?> GetEntityAsync(SysConfigEnum _scSysConfigNo, bool _enableTracking = false, bool _includeDetails = false)
        {
            return await m_SysConfigRepository
                .GetQuery(_scSysConfigNo, _enableTracking, _includeDetails)
                .FirstOrDefaultAsync();
        }
        public async Task<SysConfig?> GetEntityAsync(SysConfig _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return await m_SysConfigRepository
                .GetQuery(_info, _enableTracking, _includeDetails)
                .FirstOrDefaultAsync();
        }




























        public async Task<BusinessProcessResult> ProcessToUpdateAsync(List<SysConfig> _info)
        {
            var result = new BusinessProcessResult();

            foreach (var postData in _info)
            {
                var updating = GetEntity(postData.scSysConfigNo, _enableTracking:true);
                updating.MergeFrom(postData);
                m_SysConfigRepository.UpdateEntity(updating);
            }

            await m_TMSQPDbContext.SaveChangesAsync();

            return result;

        }






    }
}
