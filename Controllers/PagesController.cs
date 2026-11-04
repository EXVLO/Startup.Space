using Microsoft.AspNetCore.Mvc;

namespace StartupLab.Controllers;

/// <summary>
/// Mockup pages that show how the website will look. They use sample data from <see cref="Models.MockData"/>.
/// </summary>
public class PagesController : Controller
{
    [HttpGet("Login")]
    public IActionResult Login() => View();

    [HttpGet("Register")]
    public IActionResult Register() => View();

    [HttpGet("Startups")]
    public IActionResult Startups() => View();

    [HttpGet("Startups/{startupId:int}")]
    public IActionResult StartupDetails(int startupId) => View(startupId);

    [HttpGet("Founders")]
    public IActionResult Founders() => View();

    [HttpGet("Founders/{founderId:int}")]
    public IActionResult FounderDetails(int founderId) => View(founderId);

    [HttpGet("News")]
    public IActionResult News() => View();

    [HttpGet("Booking")]
    public IActionResult Booking() => View();

    [HttpGet("Toolkit")]
    public IActionResult Toolkit() => View();

    [HttpGet("Toolkit/Videos")]
    public IActionResult ToolkitVideos() => View();

    [HttpGet("Toolkit/Pathway")]
    public IActionResult ToolkitPathway() => View();

    [HttpGet("AboutUs")]
    public IActionResult AboutUs() => View();

    [HttpGet("AboutUs/Team")]
    public IActionResult Team() => View();

    [HttpGet("Archive")]
    public IActionResult Archive() => View();

    [HttpGet("Profile")]
    public IActionResult Profile() => View();

    [HttpGet("MyStartups")]
    public IActionResult MyStartups() => View();

    [HttpGet("Settings")]
    public IActionResult Settings() => View();
}
