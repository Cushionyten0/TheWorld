using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheWorld.Controllers.Web {
    public class AppController : Controller {
        public IActionResult Index () {
            return View ();
        }
        public IActionResult Contact () {
            //throw new InvalidOperationException ("Bad things happen to good developers");
            return View ();
        }
        public IActionResult About () {
            return View ();
        }
    }
}