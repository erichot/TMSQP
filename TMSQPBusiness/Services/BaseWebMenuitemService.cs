using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Services
{
    public class BaseWebMenuitemService
    {
        private readonly BaseWebMenuitemRepository m_BaseWebMenuitemRepository;

        public BaseWebMenuitemService(BaseWebMenuitemRepository baseWebMenuitemRepository)
        {
            m_BaseWebMenuitemRepository = baseWebMenuitemRepository;
        }



        //public async Task<List<BaseWebMenuitem>> GetQuery(UserRoleEnum _roleID, short _userNo)
        //{
        //    return await m_Mapper
        //        .ProjectTo<BaseWebMenuitemViewModel>(
        //            m_BaseWebMenuitemRepository.GetQuery(_roleID, _userNo)
        //        ).ToListAsync();
        //}
        //public IQueryable<BaseWebMenuitem> GetQuery(UserRoleEnum _roleID, short _userNo)
        //{
        //    return m_BaseWebMenuitemRepository.GetQuery(_roleID, _userNo);
        //}
        public IQueryable<BaseWebMenuitem> GetQuery(byte _roleNo, short _userNo)
        {
            return m_BaseWebMenuitemRepository.GetQuery(_roleNo, _userNo);
        }



    }
}
