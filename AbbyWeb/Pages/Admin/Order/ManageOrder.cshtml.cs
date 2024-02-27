using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Models.ViewModel;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order
{
  [Authorize(Roles = $"{SD.ManagerRole}, {SD.KitchenRole}")]
  public class ManageOrderModel : PageModel
  {
    private readonly IUnitOfWork _unitOfWork;
    public List<OrderDetailVM> OrderDetailVM { get; set; }
    public ManageOrderModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public void OnGet()
    {
      OrderDetailVM = new();
      List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == SD.StatusSubmitted
      || u.Status == SD.StatusInProcess).ToList();

      foreach(OrderHeader item in orderHeaders)
      {
        OrderDetailVM individual = new OrderDetailVM()
        {
          OrderHeader = item,
          OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == item.Id).ToList()
        };
        OrderDetailVM.Add(individual);
      }
    }

    public IActionResult OnPostHandleStatus(int orderId, string status)
    {
      _unitOfWork.OrderHeader.UpdateStatus(orderId, status);
      _unitOfWork.Save();
      return RedirectToPage("ManageOrder");
    }
  }
}
