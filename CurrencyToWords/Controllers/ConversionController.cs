using Microsoft.Web.Http;
using NumberInWords.Core;
using CurrencyToWord.Entities;
using CurrencyToWord.Service;
using System;
using System.Net;
using System.Web.Http;

namespace CurrencyToWordsWeb.Controllers
{
    [ApiVersion("1")]
    [RoutePrefix("api/v{version:apiVersion}/convert")]
    public class ConversionController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IConversionService _conversionService;

        public ConversionController(ILogger logger, IConversionService conversionService)
        {
            _logger = logger;
            _conversionService = conversionService;
        }

        [HttpPost]
        [Route("number")]
        public IHttpActionResult ConvertNumber(InputModel input)
        {
            try
            {
                _logger.WriteDebug("ConversionController - ConvertNumber: Method Started");
                if (ModelState.IsValid && input.Number >= Constants.MinLimit && input.Number <= Constants.MaxLimit)
                {
                    _logger.WriteDebug("ConversionController - ConvertNumber: Convert input in service");
                    var result = _conversionService.ConvertToWord(input);
                    _logger.WriteDebug("ConversionController - ConvertNumber: Conversion Successful");
                    return Ok(new Response<OutputModel>()
                    {
                        Success = result != null,
                        Data = result,
                        ErrorCode = result == null ? (int?)HttpStatusCode.NoContent : null,
                        ErrorMessage = result == null ? Convert.ToString(HttpStatusCode.NoContent) : string.Empty
                    });
                }
                else
                {
                    _logger.WriteError("ConversionController - ConvertNumber: Bad Request. Method Ended");
                    return Ok(new Response<string>()
                    {
                        ErrorCode = (int)HttpStatusCode.BadRequest,
                        ErrorMessage = Constants.GenericErrorMessage
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError("ConversionController - ConvertNumber: Method Failed. Error Details: " + ex.Message, ex);
                return Ok(new Response<string>()
                {
                    ErrorCode = (int)HttpStatusCode.InternalServerError,
                    ErrorMessage = Constants.GenericErrorMessage
                });
            }
        }
    }
}
