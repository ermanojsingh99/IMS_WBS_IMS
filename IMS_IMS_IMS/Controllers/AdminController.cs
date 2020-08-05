using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IMS_ENTITYFRAMEWORK.Repository;
using IMS_ENTITYFRAMEWORK;
using IMS_IMS_MODEL;
using IMS_IMS_IMS.Filter;
using System.Dynamic;
using ClosedXML.Excel;
using System.Data;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;


namespace IMS_IMS_IMS.Controllers
{
    [IMSAuthentication]
    public class AdminController:Controller
    {

        Repositories repositories = null;

        public AdminController()
        {
            repositories = new Repositories();
            dynamic MyModel = new ExpandoObject();
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                List<AssetNameList> list = context.AssetNameList.ToList();
                ViewBag.AssetList = new SelectList(list, "AssetName", "AssetName");


            }

        }

      
        public ActionResult Index()
        {

            return View();
            
        }
        public ActionResult CreateAdmin()
        {

            return View();

        }
        public ActionResult CreateAdmin(AdminRegisterModel registerModel)
        {

            var result = "";
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(registerModel.Email);
                if (isExist == false)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    result = "Admin Already Registered ! Please choose another user name";


                    return Json(new { result = result }, JsonRequestBehavior.AllowGet);
                }
                else if (isExist == true)
                {

                    int UserId = repositories.AdminReg(registerModel);
                    if (UserId > 0)
                    {
                        ModelState.Clear();
                        //ViewBag.Issuccess = "Registration Successful";
                        result = "Registration Successful";
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
        public JsonResult AssetCountDetails()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
             
                var countCPU = context.AssetDetails.Where(i => i.asset_name == "Desktop").Count();
                return Json(countCPU, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult AssetCountDetailsWS()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countWS = context.AssetDetails.Where(i => i.asset_name == "Workstation").Count();

                return Json(countWS, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AssetCountDetailsLaptop()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countLaptop = context.AssetDetails.Where(i => i.asset_name == "Laptop").Count();

                return Json(countLaptop, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AssetCountDetailsPrinter()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPrinter = context.AssetDetails.Where(i => i.asset_name == "Printer").Count();

                return Json(countPrinter, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AssetCountDetailsTablet()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countTablet = context.AssetDetails.Where(i => i.asset_name == "Tablet").Count();

                return Json(countTablet, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ShowAssetDetails1()
        {

            using (var context = new INVENTORYEntities())
            {
                List<AssetDetailsModel> contentModel = context.AssetDetails.Take(15).Select(item => new AssetDetailsModel()
                {
                    EmpId = item.emp_id,
                   // AssetId = item.asset_id,
                    AssetName = item.asset_name,
                    SerialNo = item.serial_no

                }).ToList();

                return View(contentModel);
            }


        }
        [HttpPost]
        public JsonResult Details(string EmpId)
        {

            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.UserGmda
                                  join e in context.AssetID
                                      on c.emp_id equals e.emp_id
                                  //join t in context.AssetNameKey
                                  //on e.assetid1 equals t.asset_id

                                  select new
                                  {
                                      id = c.emp_id,
                                     // AssetId = e.assetid1,
                                      UserName = c.emp_name,
                                      Designation = c.designation

                                  }).Where(e => e.id == EmpId).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ShowAssetDetail()
        {
            return View();


        }
        [HttpPost]
        public JsonResult ShowAssetDetails()
        {
           
            using (var context = new INVENTORYEntities())
            {
                
                var GetDetails = (from c in context.UserGmda
                                  join e in context.UserGmda
                                      on c.emp_id equals e.emp_id
                                  //join t in context.AssetNameKey
                                  //on e.assetid1 equals t.asset_id

                                  select new
                                  {
                                      id = c.emp_id,
                                    //  AssetId = e.assetid1,
                                      UserName = c.emp_name,
                                     // Designation = c.designation
                                     Email=c.email,
                                     Department=c.department

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }


        }
       [HttpGet]
        public ActionResult _ShowEmployeeAssets1()

        {
            using (var context = new INVENTORYEntities())
            {
                //List<AssetDetailsModel> EmpAssetInfo = context.AssetDetails.ToList();
                List<AssetDetailsModel> List = context.AssetDetails.Select(x => new AssetDetailsModel
                {
                    EmpId = x.emp_id,
                  //  AssetId = x.asset_id,
                    AssetName = x.asset_name,
                    SerialNo = x.serial_no

                }).ToList();
                return View(List);
             }
        }


       
        public JsonResult ShowEmployeeAssets1(string EmployeeId)
        {
           
            using (var context = new INVENTORYEntities())
            {
                var GetDetails = (from user in context.UserGmda
                         join asset in context.AssetDetails on user.emp_id equals asset.emp_id
                         into t
                         from rt in t
                         where rt.emp_id== EmployeeId
                        
                         select new
                         {
                             rt.emp_id,
                             rt.asset_id_sub,
                             rt.asset_name,
                             rt.Dateofissue,
                             rt.hdd,
                             rt.ram,
                             rt.serial_no,
                             rt.warranty_start,
                             rt.warranty_end,
                             rt.ip_address,
                             rt.mac_address,
                             user.email,
                             user.department,
                             user.designation,
                             user.DOB,
                             user.DOJ,
                             user.emp_name
                            
                            
                         }).ToList();
               
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult AddNewAssetDetails()
        {
            return View();
        }


        
        [HttpPost]
        public JsonResult AddNewAssetDetails(AssetDetailsModel assetDetailsModel)
        {
            string message = "";
            string Emp_id = "";

            if (ModelState.IsValid)
            {
               
                var isExist = AssetAlreadyExist(assetDetailsModel.AssetId_unique);
                var SerialExist = AssetAlreadyExist(assetDetailsModel.SerialNo);
                var CheckEmployeeExist = Employee_Exist(assetDetailsModel.EmpId);

                if (isExist == false || SerialExist == false)
                {

                    message = "Asset Already Exist";
                }
                else if(CheckEmployeeExist==true)
                {

                    message = "Employee Not Exist in GMDA !!";
                }

                else if(isExist == true || SerialExist == true)
                 {
                    Emp_id = repositories.AddNewAsset(assetDetailsModel);
                    if (Emp_id != null)
                    {
                        ModelState.Clear();
                         message = "Asset Added Successfully";
                        //return Json(new { message=message},JsonRequestBehavior.AllowGet);
                    }
                   

                }


            }
            return Json(new {  message = message, Emp_id = Emp_id }, JsonRequestBehavior.AllowGet);

          
        }

        public ActionResult AssetDetailsFileIdModel()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddFileDetailsSubmission()
        {
            return View("AddNewAssetDetails");
        }
        
        [HttpPost]
        public JsonResult AddFileDetailsSubmission(AssetDetailsFileIdModel model,HttpPostedFileBase File)
        {
            
            var result = "";

            if (ModelState.IsValid)
            {
              

                   // var AssetIdExist = AssetId_Exist(model._AssetId);
                    var EmployeeExist = Employee_Exist(model._EmpId);
                    var UserEmpExist = CheckEmployeeExist(model._EmpId);
                    if (UserEmpExist == true)
                    {
                        result = "Employee not exist in GMDA";
                        return Json(new { result = result });
                    }
                    else if (EmployeeExist == true)
                    {

                        result = "Please Add New Asset Details First !!";

                        return Json(new { result = result });
                    }
                    else if (EmployeeExist == false)

                    {
                        if (File != null || File.ContentLength > 0)
                        {
                        var FileName = Path.GetFileName(File.FileName);
                        var FileNameWithoutExtension = Path.GetFileNameWithoutExtension(File.FileName);

                        var FilePath = Path.Combine(Server.MapPath("~/App_Data/Agreement_Files/"), FileName);
                        File.SaveAs(FilePath);

                        using (var context = new INVENTORYEntities())
                        {
                            AssetID assetID = new AssetID();
                            assetID.emp_id = model._EmpId;
                            //assetID.assetid1 = model._AssetId;
                            assetID.filename1 = FileName;
                            var path = "~/App_Data/Agreement_Files/";
                            assetID.filePath = path + FileName;

                            context.AssetID.Add(assetID);//ADD TO TABLE
                            context.SaveChanges();

                            result = "Asset Added Successfully";

                        }

                        }
                        else
                    {
                        result = "Upload File First";
                        return Json(new { result = result }, JsonRequestBehavior.AllowGet);

                    }


                    }
              
            }


                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            

        }

        public bool Employee_Exist(string emp_id)
        {

            using (INVENTORYEntities context = new INVENTORYEntities())
            {

                bool status;
               // var AssetExist = context.AssetID.Where(a => a.assetid1 == Asset_id).FirstOrDefault();
                var EmpExist = context.UserGmda.Where(a => a.emp_id == emp_id).FirstOrDefault();

                if (EmpExist != null)
                {

                    status = false;
                }
                else
                {

                    status = true;
                }

                return status;
            }

        }

        // [SubmitButtonSelector(Name = "btnSubmitUnique")]
        public bool AssetAlreadyExist(string Asset_id)
        {

            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                
                bool status;
                var AssetExist = context.AssetDetails.Where(a => a.asset_id_sub == Asset_id).FirstOrDefault();
               // var EmpExist = context.AssetDetails.Where(a => a.serial_no == Asset_id).FirstOrDefault();

                if (AssetExist != null)
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
            
        }
        public bool CheckEmployeeExist(string EmployeeId)
        {

            using (INVENTORYEntities context = new INVENTORYEntities())
            {
             

                bool status;
                
                var EmpExist = context.UserGmda.Where(a => a.emp_id == EmployeeId).FirstOrDefault();

                if (EmpExist != null)
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
            
        }

        public ActionResult _ShowDesktopDetails()
        {
            
            return View();
        }
       
        public JsonResult ShowDesktopDetails()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {


                var result = (from c in context.UserGmda
                                  join e in context.AssetDetails
                                      on c.emp_id equals e.emp_id where e.asset_name=="Desktop"
                                 
                                  select new
                                  {
                                      EmployeeId = c.emp_id,
                                      EmployeeName=c.emp_name,
                                      AssetId = e.asset_id_sub,
                                      Designation = c.designation,
                                      Department = c.department,
                                      AssetName=e.asset_name,
                                      SerialNo = e.serial_no,
                                      IP=e.ip_address

                                  }).ToList();


                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }


        public ActionResult _ShowLaptopDetails()
        {


            return View();
        }

        public JsonResult ShowLaptopDetails()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {


                var result = (from c in context.UserGmda
                              join e in context.AssetDetails
                                  on c.emp_id equals e.emp_id
                              where e.asset_name == "Laptop"

                              select new
                              {
                                  EmployeeId = c.emp_id,
                                  EmployeeName = c.emp_name,
                                  AssetId = e.asset_id_sub,
                                  Designation = c.designation,
                                  Department = c.department,
                                  AssetName = e.asset_name,
                                  SerialNo = e.serial_no,
                                  IP = e.ip_address

                              }).ToList();


                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult _ShowPrinterDetails()
        {
            
            return View();
        }

        public JsonResult ShowPrinterDetails()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                
                var result = (from c in context.UserGmda
                              join e in context.AssetDetails
                                  on c.emp_id equals e.emp_id
                              where e.asset_name == "Printer"

                              select new
                              {
                                  EmployeeId = c.emp_id,
                                  EmployeeName = c.emp_name,
                                  AssetId = e.asset_id_sub,
                                  Designation = c.designation,
                                  Department = c.department,
                                  AssetName = e.asset_name,
                                  SerialNo = e.serial_no,
                                  IP = e.ip_address

                              }).ToList();


                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult _ShowWSDetails()
        {
            return View();
        }

        public JsonResult ShowWSDetails()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                
                var result = (from c in context.UserGmda
                              join e in context.AssetDetails
                                  on c.emp_id equals e.emp_id
                              where e.asset_name == "Workstation"

                              select new
                              {
                                  EmployeeId = c.emp_id,
                                  EmployeeName = c.emp_name,
                                  AssetId = e.asset_id_sub,
                                  Designation = c.designation,
                                  Department = c.department,
                                  AssetName = e.asset_name,
                                  SerialNo = e.serial_no,
                                  IP = e.ip_address

                              }).ToList();


                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult _ShowTabletDetails()
        {
            return View();
        }
        public JsonResult ShowTabletDetails()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                
                var result = (from c in context.UserGmda
                              join e in context.AssetDetails
                                  on c.emp_id equals e.emp_id
                              where e.asset_name == "Tablet"

                              select new
                              {
                                  EmployeeId = c.emp_id,
                                  EmployeeName = c.emp_name,
                                  AssetId = e.asset_id_sub,
                                  Designation = c.designation,
                                  Department = c.department,
                                  AssetName = e.asset_name,
                                  SerialNo = e.serial_no,
                                  IP = e.ip_address,
                                  Mac=e.mac_address

                              }).ToList();


                return Json(result, JsonRequestBehavior.AllowGet);
    }


}

[HttpPost]
        public JsonResult GetDetails(string EmployeeId)
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var result = (from c in context.AssetID
                              join e in context.AssetDetails
                                  on c.emp_id equals e.emp_id
                              where c.emp_id == EmployeeId

                              select new
                              {
                                  EmployeeId = c.emp_id,

                                 

                              }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }

              
        }
        [HttpPost]
        public JsonResult UpdateOwnership(string AssetId,string EmployeeId)
        {
            var result = "";
           // string message = "";

            using (var context = new INVENTORYEntities())
            {


                var EditOwnership = context.AssetDetails.FirstOrDefault(x => x.asset_id_sub == AssetId);
               
                 EditOwnership.emp_id = EmployeeId;
                   
                    context.SaveChanges();
                //result = "Asset ID  " + "<b>" + AssetId + "</b>" + "  Transfered to " + "<b>" + EmployeeId + "</b>";
                result = EditOwnership.emp_id; ;

            }

                return Json(new { result=result, AssetId = AssetId }, JsonRequestBehavior.AllowGet);

        }


        
        public JsonResult EditGetDetails(string AssetId)
        {
         
            using (INVENTORYEntities context = new INVENTORYEntities())
            {

                var result = (from c in context.AssetDetails
                             
                              where c.asset_id_sub == AssetId

                              select new
                              {
                                  EmployeeId = c.emp_id,
                                  AssetId = c.asset_id_sub,
                                  AssetName = c.asset_name,
                                  SerialNo = c.serial_no,
                                  IP = c.ip_address,
                                  HDD = c.hdd,
                                  RAM = c.ram,
                                  processor = c.Processor,
                                  MAC =c.mac_address,
                                  WarrantyStart=c.warranty_start,
                                  WarrantyEnd=c.warranty_end,
                                  DateOfIssue=c.Dateofissue

                              }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult EditAssetDetails( AssetDetailsModel_Edit assetDetailsModel)
        {
            string message = "";
          

            if (ModelState.IsValid)
            {
                using (var context = new INVENTORYEntities())
                {


                    AssetDetails assetNameKey = new AssetDetails();

                        assetNameKey.emp_id = assetDetailsModel.EmpId;
                        assetNameKey.asset_id_sub = assetDetailsModel.AssetId_unique;
                        assetNameKey.asset_name = assetDetailsModel.AssetName;

                        assetNameKey.serial_no = assetDetailsModel.SerialNo;
                        assetNameKey.ip_address = assetDetailsModel.IpAddress;
                        assetNameKey.mac_address = assetDetailsModel.MacAddress;
                        assetNameKey.Processor = assetDetailsModel.Processor;

                        assetNameKey.hdd = assetDetailsModel.HDD;
                        assetNameKey.ram = assetDetailsModel.RAM;
                        assetNameKey.warranty_start = assetDetailsModel.WarrantyStart;
                        assetNameKey.warranty_end = assetDetailsModel.WarrantyEnd;
                        assetNameKey.Dateofissue = assetDetailsModel.DateOfIssue;
                        context.Entry(assetNameKey).State = System.Data.Entity.EntityState.Modified;
                        //context.AssetDetails.Add(assetNameKey);
                        context.SaveChanges();
                        message ="Asset ID  "+"<b>"+ assetDetailsModel.AssetId_unique +"</b>" + "  Updated Successfully";

                    
                }


            }
            return Json(new { message = message}, JsonRequestBehavior.AllowGet);

        }
       
        public ActionResult GetDetailsPrint()
        {
            return View();
        }
        
        public JsonResult GetDetailsPrint1(string ErpID)
        {
            Repositories repository = new Repositories();

            using (var context = new INVENTORYEntities())
            {

                var AssetDetailsList = (from g in context.UserGmda
                                        join c in context.AssetDetails on g.emp_id equals c.emp_id
                                        //join e in context.AssetDetails
                                        //    on c.emp_id equals e.emp_id
                                        select new
                                        {
                                            EmpName = g.emp_name,
                                            Email = g.email,
                                            Department = g.department,
                                            Designation = g.designation,
                                            Mobile = g.mobile,
                                            ErpId = c.emp_id,
                                            // AssetId_unique=c.assetid1,
                                            AssetId = c.asset_id_sub,
                                            AssetName = c.asset_name,
                                            SerialNo = c.serial_no,
                                            // WarrantyStart=e.warranty_start.ToString(),
                                            // WarrantyEnd = e.warranty_end.ToString(),
                                            // Issuedate=e.Dateofissue.ToString(),
                                            MacId = c.mac_address


                                        }).Where(x => x.ErpId == ErpID).ToList();

               
                //var AssetDetailsList = context.AssetNameKey
                //        .SqlQuery("Select * from AssetNameKey where emp_id")
                //        .ToList<AssetNameKey>();
                return Json(AssetDetailsList, JsonRequestBehavior.AllowGet);

            }



        }

        [HttpGet]
        public FileResult ExportSpeardSheetAssetDetails()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("GmdaAssetDetails");
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("ERP Id"),
                                            new DataColumn("Employee Name"),
                                            new DataColumn("Departmet"),
                                            new DataColumn("Designation"),
                                            new DataColumn("Asset ID"),
                                            new DataColumn("Asset Name"),
                                            new DataColumn("Serial No"),
                                            new DataColumn("MAC Address"),
                                            new DataColumn("Warranty Start"),
                                            new DataColumn("Warranty End"),
                                            new DataColumn("Date Of Issue") });


            // var SessionLocationData = Session["location"].ToString();
            var GetDetails = (from g in entities.UserGmda
                              
                              join e in entities.AssetDetails
                                  on g.emp_id equals e.emp_id
                              select new
                              {
                                  ErpId = g.emp_id,
                                  EmpName = g.emp_name,
                                 
                                  Department = g.department,
                                  Designation = g.designation,
                                
                                  AssetId = e.asset_id_sub,
                                  AssetName = e.asset_name,
                                  SerialNo = e.serial_no,
                                  MacId = e.mac_address,
                                  WarrantyStart = e.warranty_start.ToString(),
                                  WarrantyEnd = e.warranty_end.ToString(),
                                  Issuedate = e.Dateofissue.ToString(),
                                  


                              }).ToList();

            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.ErpId, GetColumn.EmpName, GetColumn.Department, GetColumn.Designation, GetColumn.AssetId, GetColumn.AssetName, GetColumn.SerialNo,GetColumn.MacId,GetColumn.WarrantyStart,GetColumn.WarrantyEnd,GetColumn.Issuedate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GMDA_AssetDetails.xlsx");
                }
            }
        }



        public ActionResult ManageUserIMS()
        {
            return View();


        }
        [HttpPost]
        public JsonResult ManageUserIMSDetails()
        {

            using (var context = new INVENTORYEntities())
            {
                List<AdminRegister> GetUserDetails = context.AdminRegister.Where(x => x.location == null && x.Role == null).ToList<AdminRegister>();

                //if (GetUserDetails.Count > 0)
                //{



                // string Location = context.AdminRegister.Where(x => x.location != null).ToList<AdminRegister>().FirstOrDefault().location;
                //  string Role = context.AdminRegister.Where(x => x.Role != null).ToList<AdminRegister>().FirstOrDefault().Role;
                var GetDetails = (from c in GetUserDetails


                                  select new
                                  {
                                      Username = c.UserName,

                                      email = c.Email,

                                      password = c.Password,
                                      ConfirmPassword = c.RetypePassword,
                                     
                                      accountCreateOn = c.AccountCreateOn,
                                     

                                  }).ToList();
                //  }

                return Json(GetDetails, JsonRequestBehavior.AllowGet);

            }


        }

        public JsonResult EditUserGetDetails(string Email)
        {

            using (INVENTORYEntities context = new INVENTORYEntities())
            {

                // int UserId = context.AdminRegister.Where(x => x.Email.Equals(Email)).ToList<AdminRegister>().FirstOrDefault().UserId;
                var result = (from c in context.AdminRegister

                              where c.Email == Email

                              select new
                              {
                                  Userid = c.UserId,
                                  Username = c.UserName,
                                  email = c.Email,
                                  pass = c.Password,
                                  retypePass = c.RetypePassword,
                                 

                              }).ToList();


                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public JsonResult UpdateUserIMS(CreateUserEditIMSModel createUserModel)
        {
            string message = "";


            if (ModelState.IsValid)
            {
                using (var context = new INVENTORYEntities())
                {
                    AdminRegister adminRegisterWBS = new AdminRegister();
                    //int UserID = adminRegisterWBS.UserId;
                    adminRegisterWBS.UserId = createUserModel.UserId;
                    adminRegisterWBS.UserName = createUserModel.UserName;
                    adminRegisterWBS.Email = createUserModel.Email;
                    adminRegisterWBS.Password = createUserModel.Password;

                    adminRegisterWBS.RetypePassword = createUserModel.RetypePassword;
                 
                    context.Entry(adminRegisterWBS).State = System.Data.Entity.EntityState.Modified;
                    //context.AssetDetails.Add(assetNameKey);
                    context.SaveChanges();
                    message = "User Name  " + "<b>" + createUserModel.UserName + "</b>" + "  Updated Successfully";


                }


            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteUserWBS(string Email)
        {
            string message = "";


            if (ModelState.IsValid)
            {
                using (var context = new INVENTORYEntities())
                {
                    var GetDetails = (from c in context.AdminRegister
                                      where c.Email == Email
                                      select c).FirstOrDefault();
                    context.AdminRegister.Remove(GetDetails);
                    context.SaveChanges();
                    message = "User Name  " + "<b>" + GetDetails.UserName + "</b>" + "  Deleted Successfully";


                }


            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);

        }

    }
}