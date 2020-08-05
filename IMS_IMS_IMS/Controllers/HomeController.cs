using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS_ENTITYFRAMEWORK.Repository;
using IMS_ENTITYFRAMEWORK;
using IMS_IMS_MODEL;
using System.IO;
using System.Net.Mime;
using IMS_IMS_IMS.Filter;


namespace IMS_IMS_IMS.Controllers
{
    public class HomeController : Controller
    {
        INVENTORYEntities db = new INVENTORYEntities();
        Repositories repositories = null;

        public HomeController()
        {
            repositories = new Repositories();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestAdminLte()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactUsDetailsModal contactUsDetails)
        {

            var result = "";
            if (ModelState.IsValid)
            {
               
              
                    int UserId = repositories.ContactUs(contactUsDetails);
                    if (UserId > 0)
                    {
                        ModelState.Clear();
                       
                        result = "Registration Successful";
                    }
                

            }

            return Json(new { result = result }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ShowAssetDetail()
        {
            return View();

            
        }


       
        public JsonResult ShowAssetDetails()
        {
           

            using (var context = new INVENTORYEntities())
            {

                var GetDetails=(from c in context.UserGmda
                                  join e in context.AssetID
                                      on c.emp_id equals e.emp_id
                                //join t in context.AssetNameKey
                                //on e.assetid1 equals t.asset_id

                                select new
                                  {
                                      Id= c.emp_id,
                                      //assetName=t.asset_name,
                                      //assetKey=t.asset_key,
                                    //  AssetId = e.assetid1,
                                      UserName = c.emp_name,
                                     // Email = c.email,
                                     // Department = c.department,
                                      Designation = c.designation

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }
                 
                
        }

        public ActionResult GetDetailsErpId1()

        {
            return View();
        }

        
   
        public JsonResult GetDetailsErpId(string ErpID)
     {
            Repositories repository = new Repositories();
            
            using (var context = new INVENTORYEntities())
            {
               
                var AssetDetailsList = (from g in context.UserGmda join  c in context.AssetID on g.emp_id equals c.emp_id
                                        join e in context.AssetDetails
                                            on c.emp_id equals e.emp_id
                                        select new
                                        {
                                            EmpName=g.emp_name,
                                            Email=g.email,
                                            Department=g.department,
                                            Designation=g.designation,
                                            Mobile=g.mobile,
                                            ErpId = c.emp_id,
                                           // AssetId_unique=c.assetid1,
                                            AssetId = e.asset_id_sub,
                                            AssetName = e.asset_name,
                                            SerialNo = e.serial_no,
                                            WarrantyStart=e.warranty_start.ToString(),
                                            WarrantyEnd=e.warranty_end.ToString(),
                                            Issuedate=e.Dateofissue.ToString(),
                                            MacId =e.mac_address
                                            

                                        }).Where(x => x.ErpId == ErpID).ToList();

                  if(AssetDetailsList!=null)
                 {


                 }
                //var AssetDetailsList = context.AssetNameKey
                //        .SqlQuery("Select * from AssetNameKey where emp_id")
                //        .ToList<AssetNameKey>();
                return Json(AssetDetailsList, JsonRequestBehavior.AllowGet);

            }



        }

     
        public FileResult DownloadPDF(string EmpId)
        {
            

            using (var context=new INVENTORYEntities())
            {
                

                var GetFilePath = (from p in context.AssetID
                                   where p.emp_id == EmpId
                                   select p).FirstOrDefault();
                
                string FilePath = Server.MapPath(GetFilePath.filePath);
                byte[] pdfByte = GetBytesFromFile(FilePath);
               Response.Clear();
                Response.ContentType = "Application/octet-stream";
                Response.ContentType = "application/pdf";
               
                Response.AddHeader("Content-Disposition", string.Format("attachment;FileName={0}", FilePath));
               
                Response.BinaryWrite(pdfByte);
                Response.Flush();
                Response.Clear();
                
                return File(pdfByte, MimeMapping.GetMimeMapping(FilePath), GetFilePath.filename1);

            }
               
        }
        public byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)
            FileStream fs = null;
            try
            {
                fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
               
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
        public ActionResult GetFileFromDirectory()
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/App_Data/Agreement_Files"));
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            ViewBag.Files = files;
            return View();
        }


       


    }
}