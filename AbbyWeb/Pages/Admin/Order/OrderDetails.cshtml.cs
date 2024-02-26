using Abby.DataAccess.Repository.IRepository;
using Abby.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order
{
  public class OrderDetailsModel : PageModel
  {
    private readonly IUnitOfWork _unitOfWork;
    public OrderDetailVM OrderDetailVM { get; set; }

    public OrderDetailsModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public void OnGet(int id)
    {
      OrderDetailVM = new()
      {
        OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser"),
        OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList()
      };
    }
  }
}
