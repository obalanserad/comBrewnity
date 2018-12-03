using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ComBrewnityV2.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ComBrewnityV2.Controllers
{
    public class BaseApiController : ControllerBase
    {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="modelState"></param>
            /// <returns></returns>
            protected ApiDataException CreateApiDataException(ModelStateDictionary modelState)
            {
                return CreateApiDataException(1, modelState);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="errorCode"></param>
            /// <param name="modelState"></param>
            /// <returns></returns>
            protected ApiDataException CreateApiDataException(int errorCode, ModelStateDictionary modelState)
            {

                var result = new List<ApiDataException.FieldError>();
                var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                                .Select(x => new { x.Key, x.Value.Errors });

                foreach (var erroneousField in erroneousFields)
                {
                    var fieldKey = erroneousField.Key;
                    var fieldErrors = erroneousField.Errors
                                       .Select(error => new ApiDataException.FieldError(fieldKey, error.ErrorMessage, error.Exception));
                    result.AddRange(fieldErrors);
                }



                ApiDataException exception = new ApiDataException(errorCode, "Field validation error", HttpStatusCode.BadRequest);
                exception.FieldErrors = result;
                return exception;
            }
        
    }
}