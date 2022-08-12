using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

public class LeaveTypesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}