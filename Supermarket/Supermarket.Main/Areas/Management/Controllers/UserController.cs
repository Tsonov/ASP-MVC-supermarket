using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Supermarket.Core.Repositories;
using Supermarket.Core.Models;
using Supermarket.Main.Areas.Management.Models;
using WebMatrix.WebData;

namespace Supermarket.Main.Areas.Management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : AbstractAuthorizedController
    {
        private readonly IUsersRepository _usersRepository;

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
                UserInfoViewModel userViewModel = new UserInfoViewModel();
                AutoMapper.Mapper.Map(user, userViewModel);
                return View(userViewModel);
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
                    _usersRepository.AddUser(model.UserName, model.Password, model.Email, model.FirstName, model.LastName);
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
                UserInfoViewModel userViewModel = new UserInfoViewModel();
                AutoMapper.Mapper.Map(user, userViewModel);
                return View(userViewModel);
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
                _usersRepository.UpdateUser(model.Id, model.Email, model.FirstName, model.LastName);
                _usersRepository.Save();
                return RedirectToAction("Index");
            }

            //Return the view if any error occurred
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = _usersRepository.GetUser(id);
            if (user != null)
            {
                UserInfoViewModel userViewModel = new UserInfoViewModel();
                AutoMapper.Mapper.Map(user, userViewModel);
                return View(userViewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserInfoViewModel userModel)
        {
            _usersRepository.DeleteUser(userModel.Id);
            _usersRepository.Save();
            return RedirectToAction("Index");
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
