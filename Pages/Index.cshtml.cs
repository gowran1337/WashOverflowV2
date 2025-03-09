using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WashOverflowV2.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}


        public IActionResult OnPostBookPage()
        {
            return RedirectToPage("BookPage"); // Redirects to Bookpage.cshtml
        }
        public IActionResult OnPostMyBookingsPage()
        {
            return RedirectToPage("MyBookingsPage"); // Redirects to NextPage.cshtml
        }
        public void OnGet()
		{

		}
	}
}
