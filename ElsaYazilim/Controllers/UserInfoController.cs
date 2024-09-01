using Microsoft.AspNetCore.Mvc;

namespace ElsaYazilim.Controllers
{
	
	public class UserInfoController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
    
	//[HttpPost]
 //   public IActionResult MailUser(string email)
	//{

	//}

}
