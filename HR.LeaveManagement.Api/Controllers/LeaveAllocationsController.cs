using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

public class LeaveAllocationsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}