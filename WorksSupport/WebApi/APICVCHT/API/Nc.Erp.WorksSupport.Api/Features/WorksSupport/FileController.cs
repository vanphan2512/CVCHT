using Nc.Erp.WorksSupport.Api.Common;
using Nc.Erp.WorksSupport.Api.Models;
using Nc.Erp.WorksSupport.Api.Providers;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Common;

namespace Nc.Erp.WorksSupport.Api.Features.WorksSupport
{
    using System.Configuration;

    using MyUtils;

    [RoutePrefix("api/v2/files")]
    public class FileController : BaseController
    {
        [HttpPost]
        [Route("upload")]
        public async Task<Response<FileUpload>> UploadFile()
        {
            var ret = new Response<FileUpload>();
            try
            {
                var provider = new MultipartFormDataMemoryStreamProvider();
                await this.Request.Content.ReadAsMultipartAsync(provider);
                var strFileId = "";
                var strFilePath = "";
                var encodeKey = ConfigurationManager.AppSettings["DecryptionKey"];

                var userName = provider.FormData[0];
                var password = provider.FormData[1];
                string user;
                string pass;
                if (WebApiApplication.UserName == null)
                {
                    user = CryptoUtil.DecryptString(userName, encodeKey, true, 256);
                    pass = CryptoUtil.DecryptString(password, encodeKey, true, 256);
                }
                else
                {
                    user = WebApiApplication.UserName;
                    pass = WebApiApplication.Password;
                }

                var objUploadFile =
                    new Library.Common.ERPFMSServices_WSFileManager.UploadFile
                        {
                            BufferFile =
                                provider.FileData[0].Content,
                            FileName =
                                provider.FileData[0].FileName,
                            FMSApplicationID =
                                System.Configuration
                                    .ConfigurationManager
                                    .AppSettings[
                                        "FMSAplication_WorksSupport_Document"]
                        };

                var objR = new PlcFileManager().UploadFile(objUploadFile, ref strFileId, ref strFilePath, user, pass);

                var fileInfo = new FileUpload
                                   {
                                       FileId = strFileId,
                                       FilePath = strFilePath,
                                       FileName = provider.FileData[0].FileName
                                   };
                if (!objR.IsError)
                {
                    ret.Success = true;
                    ret.Data = fileInfo;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objR.Message;
                }
            }
            catch (ValidationException ex)
            {
                ret.Success = false;
                ret.Message = ex.Value.ToString();
            }
            return ret;
        }
    }
}