using System.IO;

using System.Text;
using System.Text.RegularExpressions;


namespace TMSQPWeb.Services
{
    public class FileManagementService
    {




        public string OpenTextFile(string _fullPathFileName)
        {
            //var fullPathFileName = _webPathFileName;
            using (var sr = new StreamReader(_fullPathFileName))
            {
                // Read the stream as a string, and write the string to the console.
                return sr.ReadToEnd();
            }
        }
        public async Task<string> OpenTextFileAsync(string _fullPathFileName)
        {
            //var fullPathFileName = _webPathFileName;
            using (var sr = new StreamReader(_fullPathFileName))
            {
                // Read the stream as a string, and write the string to the console.
                return await sr.ReadToEndAsync();
            }
        }








        public async Task<BusinessProcessResult> SaveFileAsync(IFormFile _uploadFile
            , string _saveToPath, string _saveToFileName)
        {
            var result = new BusinessProcessResult();

            //var bInValid = false;
            var targetPathFileName = Path.Combine(_saveToPath, _saveToFileName);





            if (!System.IO.File.Exists(targetPathFileName))
            {
                // 檢查目標路徑是否存在
                if (!System.IO.Directory.Exists(_saveToPath))
                {

                    try
                    {
                        // 建立目標路徑
                        System.IO.Directory.CreateDirectory(_saveToPath);
                    }
                    catch (Exception ex)
                    {
                        result.SetErrorMessage(ex.ComposeExceptionMessage());
                        return result;
                    }

                }


                try
                {
                    using (FileStream fs = System.IO.File.Create(targetPathFileName))
                    {
                        await _uploadFile.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    //bInValid = true;
                    //HResult = 0x80070020
                    //Message = The process cannot access the file 'D:\Project\2-研發中\CHT-CAREER\Source\CHT-CareerApi\upload\5\IMGP1897.jpg' because it is being used by another process.
                    result.SetErrorMessage(ex.ComposeExceptionMessage());
                    return result;
                }

            }
            else
            {
                try
                {
                    using (FileStream fs = System.IO.File.Open(targetPathFileName, FileMode.Append))
                    {
                        await _uploadFile.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    //bInValid = true;
                    //HResult = 0x80070020
                    //Message = The process cannot access the file 'CHT-CareerApi\upload\5\IMGP1897.jpg' because it is being used by another process.
                    result.SetErrorMessage(ex.ComposeExceptionMessage());
                    return result;
                }
            }

            result.ResultId = targetPathFileName;
            return result;
        }








        public string AddNewSaveFileName(string _uploadFileName)
        {
            Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");
           

            // 排除空白字元
            var saveFileName = _uploadFileName
                    .Trim()
                    .Replace(" ", "")
                    .Replace("'", "-")
                    ;

            var result = illegalInFileName.Replace(saveFileName, "");


            // ------------------------------------------------
            var saveFileNameWithoutExt = result;
            var extName = string.Empty;
            // zero-base
            var positionPoint = result.LastIndexOf(".");
            if (positionPoint > -1)
            {
                extName = result.Substring(positionPoint);
                saveFileNameWithoutExt = result.Substring(0, positionPoint);
            }


            if (result.Length > 48)
                result = saveFileName.Substring(0, 16);

            return DateTime.Now.ToString("MMddHHmm")
                + '_' + result
                    ;
        }











    }
}
