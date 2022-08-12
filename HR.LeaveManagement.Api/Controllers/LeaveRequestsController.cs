using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

public class LeaveRequestsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}