using IMS_ENTITYFRAMEWORK;
using IMS_ENTITYFRAMEWORK.Repository;
using IMS_IMS_IMS.Filter;
using IMS_IMS_MODEL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System;

namespace IMS_IMS_IMS.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        Repositories repositories = null;
        WBSController WBSController = new WBSController();
        WBSAdminController adminController = new WBSAdminController();
        INVENTORYEntities context = new INVENTORYEntities();
        public AccountController()
        {
            repositories = new Repositories();
            

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

 

        [HttpGet]

        public ActionResult ForgotPassword()
        {

            return View();

        }

        [HttpPost]
        public  ActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var result = "";
            if (ModelState.IsValid)
            {
                bool isExist = WBSController.IsEmailExist(forgotPasswordViewModel.Email);
                
                if (isExist == false)
                {
                    bool InternetConnection = false;
                    InternetConnection = adminController.IsInternetConnection();
                    if (InternetConnection == true)
                    {

                        string GetPassword = context.AdminRegister.Where(x => x.Email.Equals(forgotPasswordViewModel.Email)).ToList<AdminRegister>().FirstOrDefault().Password;
                        string GetUserName = context.AdminRegister.Where(x => x.Email.Equals(forgotPasswordViewModel.Email)).ToList<AdminRegister>().FirstOrDefault().UserName;

                        string from = "gmdagmda9@gmail.com"; //From address   
                        string to = forgotPasswordViewModel.Email; //To address    

                        MailMessage message = new MailMessage(from, to);

                        string Timestamp = "" + DateTime.Now.ToString();
                        string msg = @"<h3 NOTE :  style='color:green'>Dear " + GetUserName + " Above password is your old password  please use this to login.</h3>";
                        string mailbody = " <h3 style='color:green'>Hello! " + GetUserName + " </h3>" + "<h2 style='color:green'>Your Password is : " + GetPassword + "</h2><br>" + msg;

                        message.Subject = "GMDA Forgot Password";
                        message.Body = mailbody;
                        message.BodyEncoding = System.Text.Encoding.UTF8;
                        message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                        System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("gmdagmda9@gmail.com", "Gmdait@123");
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = basicCredential1;

                        client.Send(message);


                        ModelState.Clear();
                        //ViewBag.Issuccess = "Registration Successful";
                        result = "Password Sent to Your Email Address";
                    }

                    else
                    {
                        result = "Internet Connection not Available";
                        return Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    }
                }

                    else
                    {
                        result = "Email is not registered with us";
                        return Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    }


                

            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);



        }
    }
}