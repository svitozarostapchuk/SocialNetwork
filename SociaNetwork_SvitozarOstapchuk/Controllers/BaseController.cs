using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            return Convert.ToInt32(HttpContext.User
                    .FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
