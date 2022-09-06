using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        //Class usada só como base para autoriza 
        //toda vez que for obrigar há token em apis.
    }
}
