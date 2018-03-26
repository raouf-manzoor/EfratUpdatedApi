using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EfratWebApi.Controllers
{
    [EnableCorsAttribute("*", "*", "*", SupportsCredentials = true)]
    public class AccountController : ApiController
    {
        // GET: Account
        [HttpPost]
        public dynamic RegisterUser()
        {
             return new List<string>() { "Raouf Manzoor", "Manzoor Hussain Abbasi", "Punjab University" };
        }
    }
}