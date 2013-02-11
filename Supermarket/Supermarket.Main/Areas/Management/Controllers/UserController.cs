using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Supermarket.Core;
using Supermarket.Core.Models;
using Supermarket.Main.Areas.Management.Models;
using WebMatrix.WebData;

namespace Supermarket.Main.Areas.Management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        IUsersRepository _usersRepository;

        public UserController(IUsersRepository userRepository)
        {
            _usersRepository = userRepository;
        }

        //
        // GET: /Management/User/

        public ActionResult Index()
        {
            var users = _usersRepository
                        .GetUsers()
                        .Select(u => new UserInfoViewModel() { UserName = u.UserName, Id = u.UserId });
            return View(users);
        }

        //
        // GET: /Management/User/Details/5

        [HttpGet]
        public ActionResult Details(int id)
        {
            var user = _usersRepository.GetUser(id);
            if (user != null)
            {
                UserInfoViewModel userModel = new UserInfoViewModel();
                Mapper.Map(user, userModel);
                return View(userModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //
        // GET: /Management/User/Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Management/User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserFullViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserProfile newUser = new UserProfile();
                    Mapper.Map(model, newUser);
                    _usersRepository.AddUser(newUser, model.Password);
                    return RedirectToAction("Index");
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
                }
            }

            //Return the form because some error occurred
            return View();
        }

        //
        // GET: /Management/User/Edit/5

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = _usersRepository.GetUser(id);
            if (user != null)
            {
                UserInfoViewModel userView = new UserInfoViewModel();
                Mapper.Map(user, userView);
                return View(userView);
            }

            return HttpNotFound();
        }

        //
        // POST: /Management/User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _usersRepository.GetUser(model.Id);
                if (user == null)
                {
                    //Something happened in the meantime or an invalid id was given, redirect for now
                    return RedirectToAction("Index");
                }

                //TODO
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                _usersRepository.Save();
                return RedirectToAction("Index");
            }

            //Return the view if any error occurred
            return View();
        }

        public JsonResult DeleteAjax(int id)
        {
            var user = _usersRepository.GetUser(id);
            if (user != null)
            {
                ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.UserName);
                Membership.DeleteUser(user.UserName);
                return Json("User deleted successfully");
            }

            return Json("Indalid user selected");
        }

        protected override void Dispose(bool disposing)
        {
            _usersRepository.Dispose();
            base.Dispose(disposing);
        }

        #region Helpers
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
