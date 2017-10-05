using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Nc.Erp.WorksSupport.Api.Common;

namespace Nc.Erp.WorksSupport.Api.Providers
{
    public class MultipartFormDataMemoryStreamProvider : MultipartMemoryStreamProvider
    {
        public class FileInfo
        {
            public Byte[] Content { get; set; }
            public string ContentType { get; internal set; }
            public string FileName { get; set; }
            public long FileSize { get; internal set; }
            public FileInfo()
            {
                this.ContentType = FileContentType.Png;
                this.FileSize = 0;
            }
        }

        private readonly Collection<bool> isFormData = new Collection<bool>();
        private readonly NameValueCollection formData = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
        private readonly List<FileInfo> fileData = new List<FileInfo>();

        public NameValueCollection FormData
        {
            get { return this.formData; }
        }

        public List<FileInfo> FileData
        {
            get { return this.fileData; }
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (null == parent)
            {
                throw new ArgumentNullException("parent");
            }

            if (null == headers)
            {
                throw new ArgumentNullException("headers");
            }

            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
            if (null == contentDisposition)
            {
                throw new InvalidOperationException("Did not find required 'Content-Disposition' header field in MIME multipart body part.");
            }

            isFormData.Add(String.IsNullOrEmpty(contentDisposition.FileName));
            return base.GetStream(parent, headers);
        }

        public override async Task ExecutePostProcessingAsync()
        {
            for (var index = 0; index < this.Contents.Count; ++index)
            {
                var formContent = this.Contents[index];
                if (this.isFormData[index])
                {
                    // Field
                    var formFieldName = UnquoteToken(formContent.Headers.ContentDisposition.Name) ?? string.Empty;
                    var formFieldValue = await formContent.ReadAsStringAsync();
                    FormData.Add(formFieldName, formFieldValue);
                }
                else
                {
                    // File
                    var fileInfo = new FileInfo
                                       {
                                           FileName =
                                               UnquoteToken(formContent.Headers.ContentDisposition.FileName),
                                           Content = await formContent.ReadAsByteArrayAsync(),
                                           FileSize = (long)formContent.Headers.ContentLength,
                                           ContentType = formContent.Headers.ContentType.MediaType
                                       };

                    FileData.Add(fileInfo);
                }
            }
        }

        private static string UnquoteToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            if (token.StartsWith("\"", StringComparison.Ordinal)
                && token.EndsWith("\"", StringComparison.Ordinal)
                && (token.Length > 1))
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }
    }
}