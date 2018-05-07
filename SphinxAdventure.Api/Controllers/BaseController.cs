using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SphinxAdventure.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Guid UserId
        {
            get
            {
                return Guid.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            }
        }
    }
}
