using System.Collections.Generic;
namespace Nc.Erp.WorksSupport.Api.Common
{

    /// <summary>
    /// Created by      : Truong Hoang Nhi
    /// Created date    : 25/05/2017
    /// Definition object response to client.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> where T : class
    {
        /// <summary>
        /// Return status.
        /// </summary>
        public bool? Success { get; set; }

        /// <summary>
        /// Return message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Return Error Code.
        /// </summary>
        public IList<ValidationError> Errors { get; set; }

        /// <summary>
        /// Object data to return.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Return total record.
        /// </summary>
        public int? TotalRecord { get; set; }
    }
}