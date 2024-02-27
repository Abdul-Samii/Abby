using Abby.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult Get(string? status=null)
		{
      Console.WriteLine($"Received status: {status}");
      var orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
			if (status != null)
			{
				orderHeaderList = orderHeaderList.Where(u => u.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
			}
			return Json(new { data = orderHeaderList });
		}
	}
}
