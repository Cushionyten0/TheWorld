using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        //private IMailService _mailService;

        public AppController (IMailService mailService,
            IConfigurationRoot config,
            IWorldRepository repository,
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index ()
        {
            try
            {
                var data = _repository.GetAllTrips ();
                return View (data);
            }
            catch (Exception e)
            {
                _logger.LogError ($"Failed to get trips in Index Page: {e.Message}");
                return Redirect ("/error");
            }
        }
        public IActionResult Contact ()
        {
            //throw new InvalidOperationException ("Bad things happen to good developers");

            return View ();
        }

        [HttpPost]
        public IActionResult Contact (ContactViewModel model)
        {
            if (model.Email.Contains ("aol.com"))
            {
                ModelState.AddModelError ("Email", "We don't support AOL addresses");
            }
            if (ModelState.IsValid)
            {
                _mailService.SendMail (_config["MailSettings:ToAddress"], model.Email, "From TheWorld", model.Message);
                ModelState.Clear ();
                ViewBag.UserMessage = "Message Sent";
            }

            return View ();

        }

        public IActionResult About ()
        {
            return View ();
        }
    }
}