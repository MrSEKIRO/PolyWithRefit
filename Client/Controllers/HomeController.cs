using Client.Refit;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Client.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGetPeople poepleSupplier;

		public HomeController()
        {
			poepleSupplier = RestService.For<IGetPeople>("https://localhost:6000");
		}
        [HttpGet("/api/get")]
		public IActionResult Index()
		{
			var people = poepleSupplier.GetAllStudent().GetAwaiter().GetResult();

			return Json(people);
		}
	}
}
