using NumberInWords.Core;
using CurrencyToWord.Entities;
using CurrencyToWord.Service;
using System.Web.Mvc;

namespace CurrencyToWordsWeb.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger _logger;
        readonly IConversionService _conversionService;

        public HomeController()
        {
            _logger = WebApiConfig.Resolve<ILogger>();
            _conversionService = WebApiConfig.Resolve<IConversionService>();
        }

        public HomeController(ILogger logger, IConversionService conversionService)
        {
            _logger = logger;
            _conversionService = conversionService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InputModel input)
        {
            _logger.WriteDebug("HomeController - Index: Method Started");
            if (ModelState.IsValid && input.Number >= (decimal)0.01 && input.Number <= (decimal)999999999999999.99)
            {
                _logger.WriteDebug("HomeController - Index: Convert input in service");
                ViewBag.Result = _conversionService.ConvertToWord(input);
                _logger.WriteDebug("HomeController - Index: Conversion Successful");
                
            }
            return View(input);
        }
    }
}