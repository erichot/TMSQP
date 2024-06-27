using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Services
{
    public class NotificationService
    {
        private readonly TMSQPDbContext m_TMSQPDbContext;
        
        private readonly NotificationHeadRepository m_NotificationHeadRepository;
        //private readonly NotificationDetailRepository m_NotificationDetailRepository;
        //private readonly NotificationRecipientRepository m_NotificationRecipientRepository;




        public string WEB_RootUrl { get; set; }

        public string SenderAddress { get; set; }
        public string SenderName { get; set; }








        public NotificationService(TMSQPDbContext TMSQPDbContext)
        {
            m_TMSQPDbContext = TMSQPDbContext;
            m_NotificationHeadRepository = new NotificationHeadRepository(TMSQPDbContext);
            //m_NotificationDetailRepository = new NotificationDetailRepository(TMSQPDbContext);
            //m_NotificationRecipientRepository = new NotificationRecipientRepository(TMSQPDbContext);

        }









        public async Task<NotificationHead?> GetHeadEntityAsync(int _notificationNo, bool _enableTracking = false, bool _includeDetails = false)
        {
            if (_notificationNo.IsNullOrDefault()) return null;

            return await GetHeadEntityAsync(
                new NotificationHead() { NotificationNo = _notificationNo }
                , _enableTracking, _includeDetails);
        }
        public async Task<NotificationHead?> GetHeadEntityAsync(NotificationHead _info, bool _enableTracking = false, bool _includeDetails = false)
        {   
            return await m_NotificationHeadRepository
                .GetEntityAsync(_info, _enableTracking, _includeDetails);
        }





        public async Task<BusinessProcessResult> UpdateEntityAsync(NotificationHead _info)
        {
            var result = new BusinessProcessResult();
            m_NotificationHeadRepository.UpdateEntity(_info);
            await m_NotificationHeadRepository.SaveDbChangesAsync();

            return result;
        }










        /// <summary>
        /// 單期／單人。建立Notification資料。用於後續sendmail
        /// </summary>
        /// <param name="_info"></param>
        /// <returns></returns>
        /// <remarks>
        /// PayslipImportHead要包含Employee的 relative entity
        /// </remarks>
        public async Task<BusinessProcessResult> ProcessToInsertAsync(NotificationHead _info)
        {
            var result = new BusinessProcessResult();


            var inserting = _info;
            

            var inserted = await m_NotificationHeadRepository.AddEntityAsync(inserting);


            await m_TMSQPDbContext.SaveChangesAsync();

            result.ResultNo = inserted.NotificationNo;

            return result;
        }















        private string GeneralMessage(Employee _inserted)
        {

            string cssTableTh = @"""border-top: 1px solid #ddd;background:#fff;text-align:center;background-color:#d0e1ef;""";
            string cssTableTd = @"""border: 1px solid #bbc2c4;text-align:center;""";


            bool IsMergeCurrentTableRow = false;
            int RowCountInEmpGroup = 0;
            string HtmlTableRowSpan = "";



            var sbTb = new StringBuilder();
            sbTb.AppendLine(@"<html><head>");
            sbTb.AppendLine(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">");
            sbTb.Append(@"<style type=""text/css"">");
            sbTb.Append(@"
                    body { font-family: ""Verdana"", Arial, Helvetica, sans-serif !important; font-weight: 400; color: #000; }"
                    );
            sbTb.Append(@"</style>");
            sbTb.AppendLine(@"</head>");
            sbTb.AppendLine(@"<body>");
            sbTb.AppendLine(@"<table style=""border-collapse: collapse;width:1000px"">");
            sbTb.AppendLine(@"  <tr>");
            sbTb.AppendLine(@"      <td style=""text-align:center;font-family:SimSun;font-size:22px; "">");
            sbTb.AppendLine(@"          Manpower Dashboard System");
            sbTb.AppendLine(@"      </td>");
            sbTb.AppendLine(@"  </tr>");
            sbTb.AppendLine(@"</table>");
            sbTb.AppendLine(@"<table style=""collapse;width:1000px"">");
            sbTb.AppendLine(@"  <tr>");
            sbTb.AppendLine(@"      <td style=""width:300px;text-align:left;"">");
            sbTb.AppendLine(@"      </td>");
            sbTb.AppendLine(@"      <td style=""width:50%;"">");
            sbTb.AppendLine(@"      </td>");
            sbTb.AppendLine(@"      <td style=""width:300px;text-align:left;"">");
            sbTb.AppendLine(@"      </td>");
            sbTb.AppendLine(@"  </tr>");
            sbTb.AppendLine(@"</table>");
            sbTb.AppendLine(@"<table style=""border-collapse: collapse;width:1000px"">");
            sbTb.AppendLine(@"  <tr>");
            sbTb.AppendLine(@"      <th style=""text-align:left;width:150px;border-top: 1px solid #ddd;background-color: #ccffcc;border: 0px solid #bbc2c4 ;"">");
            sbTb.AppendLine(@"          Notification time");
            sbTb.AppendLine(@"      </th>");
            sbTb.AppendLine(@"      <td style=""width:160px;"">" );
            sbTb.AppendLine(@"          " + DateTime.Now.ToShortDateTimeString());
            sbTb.AppendLine(@"      </td>");
            sbTb.AppendLine(@"      <td style=""width:35%"">");
            sbTb.AppendLine(@"      </td>");

            sbTb.AppendLine(@"  </tr>");
            sbTb.AppendLine(@"  <tr>");
            sbTb.AppendLine(@"      <td>");
            sbTb.AppendLine(@"          ");
            sbTb.AppendLine(@"      </th>");
            sbTb.AppendLine(@"      <td>");
            sbTb.AppendLine(@"      </td>");
            sbTb.AppendLine(@"      <td style=""width:35%"">");
            sbTb.AppendLine(@"      </td>");

            sbTb.AppendLine(@"  </tr>");
            sbTb.AppendLine(@"  <tr><td>&nbsp;</td></tr>");
            sbTb.AppendLine(@"</table>");
            sbTb.AppendLine(@"<table style=""border-collapse: collapse;width:1000px"">");
            sbTb.AppendLine(@"<tbody>");
            sbTb.AppendLine(@"  <tr>");
            sbTb.AppendLine(@"      <th style=" + cssTableTh + ">");
            sbTb.AppendLine(@"          Employee ID");
            sbTb.AppendLine(@"      </th>");
            sbTb.AppendLine(@"      <th style=" + cssTableTh + ">");
            sbTb.AppendLine(@"          Employee Name");
            sbTb.AppendLine(@"      </th>" );
            sbTb.AppendLine(@"      <th style=" + cssTableTh + ">");
            sbTb.AppendLine(@"          Card ID");
            sbTb.AppendLine(@"      </th>");
            sbTb.AppendLine(@"      <th style=" + cssTableTh + ">");
            sbTb.AppendLine(@"          Department ID");
            sbTb.AppendLine(@"      </th>");
            sbTb.AppendLine(@"      <th style=" + cssTableTh + ">");
            sbTb.AppendLine(@"          Department Name");
            sbTb.AppendLine(@"      </th>");            
            sbTb.AppendLine(@"  </tr>");



            sbTb.AppendLine(@"<tr>");
            sbTb.AppendLine(@"      <td colspan=""5"" style=""border-top: 1px solid #ddd; background: #fff;border: 1px solid #bbc2c4;"">");
            sbTb.AppendLine(@"           " + " ");
            sbTb.AppendLine(@"      </td>");
            sbTb.AppendLine(@"</tr>");
            sbTb.AppendLine(@"</tbody>");
            sbTb.AppendLine(@"</table>");
            sbTb.AppendLine(@"</body></html>");


            return sbTb.ToString();
        }















    }
}
