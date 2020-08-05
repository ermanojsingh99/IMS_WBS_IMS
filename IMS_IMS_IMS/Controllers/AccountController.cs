using IMS_ENTITYFRAMEWORK;
using IMS_ENTITYFRAMEWORK.Repository;
using IMS_IMS_IMS.Filter;
using IMS_IMS_MODEL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


namespace IMS_IMS_IMS.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        Repositories repositories = null;
        public AccountController()
        {
            repositories = new Repositories();

           // var custStr
           //custStr = ls.getItem("CustomerApplication");
           // if (custStr == null)
           // {
               
           // }

          

        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
       [IMSAuthentication]
       [HttpGet]
       public ActionResult AdminRegistration()
        {

            return View();


        }

        [HttpPost]
        public JsonResult AdminRegistration(AdminRegisterModel registerModel)
        {

            var result = "";
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(registerModel.Email);
                if (isExist == false)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    result = "Admin Already Registered ! Please choose another user name";


                    return Json(new { result=result},JsonRequestBehavior.AllowGet);
                }
                else if(isExist==true)
                {

                    int UserId = repositories.AdminReg(registerModel);
                    if (UserId > 0)
                    {
                        ModelState.Clear();
                        //ViewBag.Issuccess = "Registration Successful";
                        result= "Registration Successful";
                    }
                }

            }

            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]

        public bool IsEmailExist(string emailID)
        {

            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                // List<RegistrationModel> registrationModels = new List<RegistrationModel>();

                bool status;
                var exist = context.AdminRegister.Where(a => a.Email == emailID).FirstOrDefault();
                if (exist != null)
                {
                    // ALREDY REGISTERED
                    status = false;
                }
                else
                {
                    //Available to use  
                    status = true;
                }

                return status;
            }
            // throw new Exception("Something went wrong");
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Login(LoginModel model)
        {

            using (var context = new INVENTORYEntities())
            {

               
             List<AdminRegister> GetUserDetails = context.AdminRegister.Where(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password)).ToList<AdminRegister>();
                
              
              if (GetUserDetails.Count>0)
                {
                    string Location = context.AdminRegister.Where(x => x.Email.Equals(model.Email)).ToList<AdminRegister>().FirstOrDefault().location;
                    string UserName = context.AdminRegister.Where(x => x.Email.Equals(model.Email)).ToList<AdminRegister>().FirstOrDefault().UserName;
                    string UserType = context.AdminRegister.Where(x => x.Email.Equals(model.Email)).ToList<AdminRegister>().FirstOrDefault().Role;
                    Session["UserName"] = UserName;
                    if (Location!=null && UserType!=null)
                    {
                        if(UserType=="User")
                        {
                            Session["Role"] = UserType;
                            Session["location"] = Location;
                            FormsAuthentication.SetAuthCookie(UserName, false);
                            return RedirectToAction("WBSAssetDetails", "WBS");
                        }

                        else if (UserType == "Super Admin" && Location == "GMDA")
                        {
                            FormsAuthentication.SetAuthCookie(UserName, false);
                            return RedirectToAction("AddDetailsAdmin", "WBSAdmin");
                        }

                        else 
                        {
                            Session["Role"] = UserType;
                            Session["location"] = Location;
                            FormsAuthentication.SetAuthCookie(UserName, false);
                            return RedirectToAction("Index", "WBSAdmin");
                        }
                       
                    }

                    else if (UserType == "Super Admin" && Location == "GMDA")
                    {
                        FormsAuthentication.SetAuthCookie("Super Admin", false);
                        return RedirectToAction("ManageUserIMS", "Admin");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(UserName, false);
                        return RedirectToAction("Index", "Admin");
                    }


                }

                else
                {
                    ViewBag.Message = "User name Or password is incorrect";
                    ModelState.AddModelError("InvalidLogin", "User name and password is incorrect");
                    return View();

                }
                   
            
            }

            
        }

        private bool ValidateUser(string Email, string Password,string UserName)
        {

            bool isValid = false;

            using (var db = new INVENTORYEntities())
            {
               
                var User = db.AdminRegister.FirstOrDefault(u => u.Email == Email);
                var ut = db.AdminRegister.FirstOrDefault(t => t.Password == User.Password);
                var UN = db.AdminRegister.FirstOrDefault(user => user.UserName == User.UserName);


                if (User != null)
                {
                    if (User.Password == Password)
                    {
                        
                        Session["Name"] = User.UserName;
                        Session["Email"] = User.Email;
                        Session["UserName"] = User.UserName;
                        

                        isValid = true;
                    }
                }

            }

            return isValid;
        }

        public ActionResult Logout()
        {
        FormsAuthentication.SignOut();
        return RedirectToAction("Login","Account");
        }
     

    }
}