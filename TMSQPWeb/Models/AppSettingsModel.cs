namespace TMSQPWeb.Models
{
    public class AppSettingsModel
    {
        // 以web client broswer 的角度來解析API url
        public string API_RootUrl { get; set; }

        // 以Web的角度來解析API url
        public string API_FromWeb_RootUrl { get; set; }

        public string WEB_RootUrl { get; set; }

        public int ApiLoginExpireTime { get; set; }

        public int WebLoginExpireTime { get; set; }


    }













    public class SysConfigSettings
    {
        public SysConfigSettings() { }

        public SysConfigSettings(List<SysConfig> _list)
        {
            m_SysConfigs = _list;
        }

        private List<SysConfig> m_SysConfigs { get; set; }


       

    }










    public class SmtpSettingsModel
    {
        public string SenderAddress { get; set; }
        public string SenderName { get; set; }
        public string SmtpServer { get; set; }
        public short Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }





}
