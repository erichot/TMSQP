using TMSQPData.Repositories;

namespace TMSQPWeb.BindingServices
{
    public class NotificationBindingService
    {
        private readonly IMapper m_Mapper;
        private readonly NotificationService m_NotificationService;

        public NotificationBindingService(IMapper mapper, NotificationService notificationService)
        {
            m_Mapper = mapper;
            m_NotificationService = notificationService;
        }


















        public async Task<BusinessProcessResult> UpdateEntityAsync(NotificationHead _info)
        {
            return await
                m_NotificationService.UpdateEntityAsync(_info);
        }








        /// <summary>
        /// 單期／單人。建立Notification資料。用於後續sendmail
        /// </summary>
        /// <param name="_info"></param>
        /// <returns></returns>
        public async Task<BusinessProcessResult> ProcessToInsertAsync(NotificationHead _info)
        {
            return await 
                m_NotificationService.ProcessToInsertAsync(_info);
        }






















        public async Task<NotificationHead?> GetHeadEntityAsync(int _notificationNo, bool _enableTracking = false, bool _includeDetails = false)
        {  
            return await m_NotificationService.GetHeadEntityAsync(_notificationNo 
                , _enableTracking, _includeDetails);
        }
        public async Task<NotificationHead?> GetHeadEntityAsync(NotificationHead _info, bool _enableTracking = false, bool _includeDetails = false)
        {
            return await m_NotificationService.GetHeadEntityAsync(_info, _enableTracking, _includeDetails);
        }





    }
}
