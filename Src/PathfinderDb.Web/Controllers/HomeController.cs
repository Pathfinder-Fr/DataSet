namespace PathfinderDb.Web
{
	using Microsoft.AspNet.Mvc;
	
	public class HomeController : Controller
	{
        // GET: /Home/
        public IActionResult Index()
        {
            return View();
        }
		
	}
}