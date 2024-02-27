using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

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
    public IActionResult OnPostHandleStatus(int orderId, string status)
    {
      if (status == "Refunded")
      {
        OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == orderId);
        var options = new RefundCreateOptions
        {
          Reason = RefundReasons.RequestedByCustomer,
          PaymentIntent = orderHeader.PaymentIntentId,
        };
        var service = new RefundService();
        Refund refund = service.Create(options);
      }

      _unitOfWork.OrderHeader.UpdateStatus(orderId, status);
      _unitOfWork.Save();
      return RedirectToPage("OrderList");
    }
  }
}
