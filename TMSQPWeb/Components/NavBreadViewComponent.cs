using Microsoft.AspNetCore.Mvc;

namespace TMSQPWeb.Component
{
    public class NavBreadViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;
        //private readonly IdentityClaimEntity m_CurrentLoginClaimInfo;
        //private readonly UserManagementService m_UserManagementService;

        //private readonly UserMenuitemService m_UserMenuitemService;

        public NavBreadViewComponent(IHttpContextAccessor httpContextAccessor
            //,UserMenuitemService userMenuitemService
            //, UserManagementService userManagementService
             )
        {
            m_HttpContextAccessor = httpContextAccessor;
            //m_UserMenuitemService = userMenuitemService;
            //m_UserManagementService = userManagementService;
            //m_CurrentLoginClaimInfo = m_UserManagementService.GetCurrentLoginClaim(m_HttpContextAccessor.HttpContext.User);
        }


        public IViewComponentResult Invoke(string pageID = null, string formID = null)
        {
            ViewData["id"] = pageID;
            
           
            return View();
        }



    }
}
