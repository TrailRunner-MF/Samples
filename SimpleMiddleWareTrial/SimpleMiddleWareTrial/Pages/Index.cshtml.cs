using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace SimpleMiddleWareTrial.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string MusicData { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                MusicData = HttpContext.Session.GetString("MusicData");
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, exp.Message);
                MusicData = "(Data is not available.)";
            }

        }
    }
}
