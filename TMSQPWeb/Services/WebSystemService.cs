

namespace TMSQPWeb.Services
{
    public class WebSystemService
    {
        private IWebHostEnvironment m_Environment;
        private readonly BaseWebPageService m_BaseWebPageService;
        private readonly SysConfigService m_SysConfigService;
       

        public WebSystemService(BaseWebPageService baseWebPageService, SysConfigService sysConfigService, IWebHostEnvironment environment)
        {
            m_BaseWebPageService = baseWebPageService;
            m_SysConfigService = sysConfigService;
            m_Environment = environment;
        }




        public async Task<PageHeaderEntity> GetPageHeaderAsync(string _pageId)
        {
            return new PageHeaderEntity()
            {
                Title = await m_BaseWebPageService.GetPageTitleAsync(_pageId),
                PageId = _pageId
            };
        }
        public async Task<PageHeaderEntity> GetPageHeaderAsync(string _pageId, FormEditModeEnum _formEditModeEnum)
        {
            return new PageHeaderEntity()
            {
                Title = await m_BaseWebPageService.GetPageTitleAsync(_pageId),
                SubTitle = _formEditModeEnum.GetDisplayName(),
                PageId = _pageId
            };
        }


        public string GetPageTitle(string _pageId)
        {
            return m_BaseWebPageService.GetPageTitle(_pageId);
        }
        public string GetPageTitle(string _pageId, FormEditModeEnum _formEditModeEnum)
        {
            var subTitle = _formEditModeEnum.GetDisplayName();

            return m_BaseWebPageService.GetPageTitle(_pageId) + " " + subTitle;
        }






        public string GetImageFilePathName(string _fileName)
        {
            return Path.Combine(m_Environment.ContentRootPath, @"\wwwroot\images\" + _fileName);
        }



        // 需要與 PDFExportService.GetPayslipPdfRepositoryPath 保持一致
        public string GetPayslipPdfRepositoryPath(short _salaryYear, byte _salaryMonth)
        {
            return Path.Combine(
                m_Environment.ContentRootPath
                , AppSystem.GetRelativePathPayslipPdfRepository(_salaryYear, _salaryMonth)
                );
        }



        public string GetTargetFolderForUploadFile()
        {
            var subYearFolder = DateTime.Now.Year.ToString();

            return Path.Combine(m_Environment.ContentRootPath, "Uploads", subYearFolder);
        }








        
        public string GetTempPathForDownload(string? _fileName = null)
        {
            var tempPath = Path.GetTempPath();

            if (string.IsNullOrEmpty(_fileName))
                return tempPath;

            return Path.Combine(tempPath, _fileName);
        }






        public SysConfigSettings GetSysConfigSettings()
        {
            return new SysConfigSettings(m_SysConfigService.GetList(null));
        }




    }
}
