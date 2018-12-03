using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ComBrewnityV2.Exceptions
{
    /// <summary>
    /// Api Data Exception
    /// </summary>
    [Serializable]
    [DataContract]
    public class ApiDataException : Exception, IApiExceptions
    {
        #region Public Serializable properties.
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        string reasonPhrase = "ApiDataException";

        [DataMember]
        public string ReasonPhrase
        {
            get { return this.reasonPhrase; }

            set { this.reasonPhrase = value; }
        }

        [DataMember]
        public ICollection<FieldError> FieldErrors { get; set; }

        #endregion

        #region Public Constructor.
        /// <summary>
        /// Public constructor for Api Data Exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorDescription"></param>
        /// <param name="httpStatus"></param>
        public ApiDataException(int errorCode, string errorDescription, HttpStatusCode httpStatus) : base()
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
            HttpStatus = httpStatus;

        }

        public ApiDataException()
        {
            FieldErrors = new List<FieldError>();
        }

        [Serializable]
        [DataContract]
        public class FieldError
        {
            public FieldError(string field, string error, Exception exception)
            {
                Field = field;
                Error = error;
                if (exception != null)//Both values not used at the same time
                    Error = exception.Message;
            }

            [DataMember]
            public String Field { get; set; }

            [DataMember]
            public string Error { get; set; }

        }
        #endregion
    }
}
