using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Supermarket.Main.Areas.Management.Controllers
{
    /// <summary>
    /// Used to specify a base controller that requires authorization.
    /// </summary>
    [Authorize]
    public abstract class AbstractManagementAuthorizedController : Controller
    {
        //Nothing to do  here
    }
}
