using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductStore.Web.Models;
using ProductStore.Web.UI.Infrastructure;
using ProductStore.Web.UI.Models.Home;

namespace ProductStore.Web.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IStoreApiService _storeApiService;

		public HomeController(IStoreApiService storeApiService)
		{
			_storeApiService = storeApiService;
		}

		public async Task<ActionResult> Index(CancellationToken cancellationToken)
		{
			var model = new HomeModel();

			model.ProductCategories = await _storeApiService.GetProductCategories(new PagingModel() {PageSize = 50, PageIndex = 1}, cancellationToken);

			return View(model);
		}
	}
}