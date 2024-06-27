using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Services
{
    public class BaseWebPageService
    {
        private readonly BaseWebPageRepository m_BaseWebPageRepository;

        public BaseWebPageService(BaseWebPageRepository baseWebPageRepository)
        {
            m_BaseWebPageRepository = baseWebPageRepository;
        }



        public BaseWebPage? GetEntity(BaseWebPage _info, bool _includeDetails = true)
        {
            return  m_BaseWebPageRepository
                .GetQuery(_info, _includeDetails)
                .FirstOrDefault();
        }
        public async Task<BaseWebPage?> GetEntityAsync(BaseWebPage _info, bool _includeDetails = true)
        {
            return await m_BaseWebPageRepository
                .GetQuery(_info, _includeDetails)
                .FirstOrDefaultAsync();
        }









        public string GetPageTitle(string _pageId)
        {
            var pageInfo = GetEntity(
                new BaseWebPage() { PageId = _pageId }
                );

            var result = pageInfo?.PageTitle ?? pageInfo?.BaseWebMenuitem?.NodeName;
            if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(pageInfo?.ParentPageId))
            {
                result = GetPageTitle(pageInfo.ParentPageId);
            }

            return result??string.Empty;
        }





        public async Task<string> GetPageTitleAsync(string _pageId)
        {
            var pageInfo = await GetEntityAsync(
                new BaseWebPage() { PageId = _pageId }
                );

            var result = pageInfo?.PageTitle ?? pageInfo?.BaseWebMenuitem?.NodeName;
            if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(pageInfo?.ParentPageId))
            {
                result = await GetPageTitleAsync(pageInfo.ParentPageId);
            }

            return result ?? string.Empty;
        }






    }
}
