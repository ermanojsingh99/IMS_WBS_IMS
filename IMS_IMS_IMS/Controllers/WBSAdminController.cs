using IMS_ENTITYFRAMEWORK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using IMS_IMS_IMS.Filter;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using IMS_IMS_MODEL;
using IMS_ENTITYFRAMEWORK.Repository;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;

namespace IMS_IMS_IMS.Controllers
{
    [IMSAuthentication]
    public class WBSAdminController : Controller
    {
        // GET: WBSAdmin
        WBSController WBSController = new WBSController();
        Repositories repositories = null;
        INVENTORYEntities Datacontext;

        public WBSAdminController()
        {
            repositories = new Repositories();
            Datacontext = new INVENTORYEntities();

            List<ItemListTable> ItemList = Datacontext.ItemListTable.ToList();
            ViewBag.ItemList = new SelectList(ItemList, "ItemName", "ItemName");

            List<LocationZone> locationList = Datacontext.LocationZone.ToList();
            ViewBag.LocationList = new SelectList(locationList, "locationZone1", "locationZone1");

            var GetTableNames = typeof(INVENTORYEntities).GetProperties().Select(x => x.Name).ToArray();

            foreach (var EntityName in GetTableNames)
            {
                if (EntityName == "PumpingMachineryMotor")
                {
                    ViewBag.ItemName = EntityName;

                }
                if (EntityName == "PumpingMachineryPumpset")
                {
                    ViewBag.PumpingMachineryPumpsetDetails = EntityName;
                }

                if (EntityName == "Transformer")
                {

                    ViewBag.PowerEquipmentItemName = "Power Equipment";
                }
                if (EntityName == "Land")
                {

                    ViewBag.LandDetails = EntityName;
                }
                if (EntityName == "Valve")
                {

                    ViewBag.ValveDetails = EntityName;
                }

                if (EntityName == "PanelBoard")
                {

                    ViewBag.PanelBoardDetails = "Panel Board";
                }
                if (EntityName == "Building")
                {

                    ViewBag.BuildingDetails = EntityName;
                }
                if (EntityName == "StorageSediTank")
                {

                    ViewBag.TankDetails = "Tank";
                }
                if (EntityName == "Pipe")
                {

                    ViewBag.PipeDetails = EntityName;
                }

                if (EntityName == "WTP")
                {

                    ViewBag.WTPDetails = "Water Treatment Plant";
                }
            }



        }


        public ActionResult Index()
        {
            return View();
        }
       

        [HttpGet]
        public ActionResult ShowWaterPumpMotor()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult ShowPumpingMachineryMotorDetails(string Location)
        {
            
            using (var context = new INVENTORYEntities())
            {


                var GetDetails = (from c in context.PumpingMachineryMotor.Where(x=> x.location==Location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      bhp = c.BHP,
                                      Itemtype = c.ItemType,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username=c.UserName

                                  }).ToList();


                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpGet]
        public ActionResult ShowWaterPumpMotorCB()
        {

            return View();
        }

        [HttpPost]
        public JsonResult ShowPumpingMachineryMotorCB(string Location)
        {

            using (var context = new INVENTORYEntities())
            {


                var GetDetails = (from c in context.PumpingMachineryMotor.Where(x => x.location == Location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      bhp = c.BHP,
                                      Itemtype = c.ItemType,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();


                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult ShowPumpingMachineryMotorSTPBeh(string Location)
        {

            using (var context = new INVENTORYEntities())
            {


                var GetDetails = (from c in context.PumpingMachineryMotor.Where(x => x.location == Location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      bhp = c.BHP,
                                      Itemtype = c.ItemType,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();


                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public FileResult ExportSpeardSheetPumpMachineryMotor()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("PumpMachineryMotor");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Item Name"),
                                            new DataColumn("Item Type"),
                                            new DataColumn("Location"),
                                            new DataColumn("Make"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Status"),
                                            new DataColumn("Remark") });


           // var SessionLocationData = Session["location"].ToString();
            var GetDetails = from pumpmachineMotor in entities.PumpingMachineryMotor select pumpmachineMotor;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.ItemType, GetColumn.location, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PumpMachineryMotor.xlsx");
                }
            }
        }

        public ActionResult ShowPowerEquipment()
        {

            return View();
        }

        [HttpPost]
        public JsonResult ShowPowerEquipmentDetails()
        {
            //var SessionLocationData = Session["location"].ToString();


            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Transformer


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      PowerType = c.powertype,
                                      KiVA = c.KVA,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();


                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public FileResult ExportExcelPowerEquipment()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("PowerEquipment");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Item Name"),
                                            new DataColumn("Power Type"),
                                            new DataColumn("Location"),
                                            new DataColumn("KVA"),
                                            new DataColumn("Make"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Status"),
                                            new DataColumn("Remark") });


           // var SessionLocationData = Session["location"].ToString();
            var GetDetails = from PowerEquipment in entities.Transformer select PowerEquipment;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.powertype, GetColumn.location, GetColumn.KVA, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PowerEquipmentDetails.xlsx");
                }
            }
        }

        public ActionResult ShowPowerEquipmentCB()
        {

            return View();
        }

        [HttpPost]
        public JsonResult ShowPowerEquipmentDetailsCB()
        {
            //var SessionLocationData = Session["location"].ToString();


            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Transformer


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      PowerType = c.powertype,
                                      KiVA = c.KVA,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();


                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult ShowLandDetails()
        {

            return View();
        }
      

        [HttpPost]
        public JsonResult ShowLandDetail(string location)
        {
           // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Land.Where(x=>x.location==location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Length = c.length,
                                      Width = c.breadth,
                                      Area = c.area,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpGet]
        public FileResult ExportSpeardSheetLand()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("Land");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Property Name"),
                                            new DataColumn("Location"),
                                            new DataColumn("Length"),
                                            new DataColumn("Width"),
                                            new DataColumn("Total Covered Area"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Remark") });


           // var SessionLocationData = Session["location"].ToString();
            var GetDetails = from land in entities.Land select land;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.location, GetColumn.length, GetColumn.breadth, GetColumn.area, GetColumn.qty, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LandProperty.xlsx");
                }
            }
        }

        [HttpGet]
        public ActionResult ShowLandDetailsCB()
        {

            return View();
        }


        [HttpPost]
        public JsonResult ShowLandDetailCB(string location)
        {
            // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Land.Where(x => x.location == location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Length = c.length,
                                      Width = c.breadth,
                                      Area = c.area,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }
        public ActionResult ShowPanelBoardDetails()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowPanelBoardDetail(string Location)
        {
           // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PanelBoard.Where(x => x.location == Location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Capacity = c.capacity,

                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public FileResult ExportSpeardSheetPanelBoard()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("PanelBoard");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Item Name"),
                                            new DataColumn("Location"),
                                            new DataColumn("Capacity"),
                                            new DataColumn("Make"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Status"),
                                            new DataColumn("Remark") });


          //  var SessionLocationData = Session["location"].ToString();
            var GetDetails = from panelboard in entities.PanelBoard select panelboard;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.location, GetColumn.capacity, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PanelBoard.xlsx");
                }
            }
        }

        public ActionResult ShowPanelBoardDetailsCB()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowPanelBoardDetailCB(string Location)
        {
            // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PanelBoard.Where(x => x.location == Location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Capacity = c.capacity,

                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ShowValveDetails()
        {

            return View();

        }


        [HttpPost]
        public JsonResult ShowValveDetail(string location)
        {
            //var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Valve.Where(x=>x.location==location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      ValveType = c.valveType,
                                      Size = c.size,
                                      Make = c.make,
                                      Quantity = c.qty,
                                     // Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public FileResult ExportSpeardSheetValve()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("Valve");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Item Type"),
                                            new DataColumn("Valve Name"),
                                            new DataColumn("Location"),
                                            new DataColumn("Size(in mm.)"),
                                            new DataColumn("Make"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Status"),
                                            new DataColumn("Remark") });


           // var SessionLocationData = Session["location"].ToString();
            var GetDetails = from valve in entities.Valve select valve;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.valveType, GetColumn.location, GetColumn.size, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Valve.xlsx");
                }
            }
        }

        public ActionResult ShowValveDetailsCB()
        {

            return View();

        }


        [HttpPost]
        public JsonResult ShowValveDetailCB(string location)
        {
            //var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Valve.Where(x => x.location == location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      ValveType = c.valveType,
                                      Size = c.size,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      // Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpGet]
        public ActionResult ShowPipeDetails()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowPipeDetail(string location)
        {
           // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Pipe.Where(x => x.location == location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      PipeType1 = c.PipeType,
                                      Diameter = c.diameter,
                                      Length = c.lenght,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpGet]
        public FileResult ExportSpeardSheetPipe()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("Pipe");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Item Type"),
                                            new DataColumn("Pipe Name"),
                                            new DataColumn("Location"),
                                            new DataColumn("Diameter"),
                                            new DataColumn("Length"),
                                            new DataColumn("Make"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Status"),
                                            new DataColumn("Remark") });


           // var SessionLocationData = Session["location"].ToString();
            var GetDetails = from pipe in entities.Pipe select pipe;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.PipeType, GetColumn.location, GetColumn.diameter, GetColumn.lenght, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "pipe.xlsx");
                }
            }
        }


        [HttpGet]
        public ActionResult ShowPipeDetailsCB()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowPipeDetailCB(string location)
        {
            // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Pipe.Where(x => x.location == location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      PipeType1 = c.PipeType,
                                      Diameter = c.diameter,
                                      Length = c.lenght,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpGet]
        public ActionResult WTPShowDetails()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ShowWTP_Details(string location)
        {
          //  var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.WTP.Where(x=>x.location==location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Diameter = c.diameter,
                                      Capacity = c.capacity,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpGet]
        public FileResult ExportSpeardSheetWTP()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("WTP");
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Item Name"),

                                            new DataColumn("Location"),
                                            new DataColumn("Diameter"),
                                            new DataColumn("Capacity"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Remark") });


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from wtp in entities.WTP select wtp;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.location, GetColumn.diameter, GetColumn.capacity, GetColumn.qty, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WTP.xlsx");
                }
            }
        }


        [HttpGet]
        public ActionResult WTPShowDetailsCB()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ShowWTP_DetailsCB(string location)
        {
            //  var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.WTP.Where(x => x.location == location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Diameter = c.diameter,
                                      Capacity = c.capacity,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }



        [HttpGet]
        public ActionResult ShowStorageTankDetails()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowStorageSediTankDetails(string location)
        {
           // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.StorageSediTank.Where(x=>x.location==location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      TankType = c.tanktype,
                                      Height = c.height,
                                      Length = c.length,
                                      Width = c.breadth,
                                      Capacity = c.capacity,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public FileResult ExportSpeardSheetTankType()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("Tank");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Item Type"),
                                            new DataColumn("Tank Name"),
                                            new DataColumn("Location"),
                                            new DataColumn("Height"),
                                            new DataColumn("Length"),
                                            new DataColumn("Breadth"),
                                            new DataColumn("Capacity"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Remark") });


          //  var SessionLocationData = Session["location"].ToString();
            var GetDetails = from Tank in entities.StorageSediTank select Tank;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.tanktype, GetColumn.location, GetColumn.height, GetColumn.length, GetColumn.breadth, GetColumn.capacity, GetColumn.qty, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TankDetails.xlsx");
                }
            }
        }

        [HttpGet]
        public ActionResult ShowStorageTankDetailsCB()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowStorageSediTankDetailsCB(string location)
        {
            // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.StorageSediTank.Where(x => x.location == location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      TankType = c.tanktype,
                                      Height = c.height,
                                      Length = c.length,
                                      Width = c.breadth,
                                      Capacity = c.capacity,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpGet]
        public ActionResult ShowBuildingdetails()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowBuildingDetails()
        {
           // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Building


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      ItemName = c.itemname,
                                      BuildingName = c.buildingName,
                                      Length = c.length,
                                      Width = c.breadth,
                                      CoveredArea = c.coveredarea,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }
        [HttpGet]
        public FileResult ExportSpeardSheetBuilding()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("Building");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Building Type"),
                                            new DataColumn("Building Name"),
                                            new DataColumn("Location"),
                                            new DataColumn("Length"),
                                            new DataColumn("Width"),
                                            new DataColumn("Covered Area(in ..)"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Remark") });


           // var SessionLocationData = Session["location"].ToString();
            var GetDetails = from building in entities.Building select building;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.buildingName, GetColumn.location, GetColumn.length, GetColumn.breadth, GetColumn.coveredarea, GetColumn.qty, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BuildingProperty.xlsx");
                }
            }
        }


        [HttpGet]
        public ActionResult ShowBuildingdetailsCB()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ShowBuildingDetailsCB()
        {
            // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Building


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      ItemName = c.itemname,
                                      BuildingName = c.buildingName,
                                      Length = c.length,
                                      Width = c.breadth,
                                      CoveredArea = c.coveredarea,
                                      Quantity = c.qty,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpGet]
        public ActionResult ShowPumpSetDetails()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ShowPumpSetDetail(string Location)
        {
           // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PumpingMachineryPumpset.Where(x=> x.location== Location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Discharge = c.discharge,
                                      Head = c.head,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }


        [HttpGet]
        public FileResult ExportSpeardSheetPumpSet()
        {
            INVENTORYEntities entities = new INVENTORYEntities();
            DataTable dt = new DataTable("PumpMachineryPumpset");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Item Name"),
                                           new DataColumn("Location"),
                                            new DataColumn("Discharge"),
                                            new DataColumn("Head"),

                                            new DataColumn("Make"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Status"),
                                            new DataColumn("Remark") });


           // var SessionLocationData = Session["location"].ToString();
            var GetDetails = from pumpset in entities.PumpingMachineryPumpset select pumpset;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.location, GetColumn.discharge, GetColumn.head, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PumpMachineryPumpset.xlsx");
                }
            }
        }

        [HttpGet]
        public ActionResult ShowPumpSetDetailsCB()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ShowPumpSetDetailCB(string Location)
        {
            // var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PumpingMachineryPumpset.Where(x => x.location == Location)


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      Location = c.location,
                                      //ItemName = c.itemname,
                                      Discharge = c.discharge,
                                      Head = c.head,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription,
                                      username = c.UserName

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }


        [HttpGet]
        public ActionResult ItemDetailsShowLocationWise()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddDetailsAdmin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddLocationWBS(AddLocation addLocation)
        {
            string result = "";
            int Id = 0;

            if (ModelState.IsValid)
            {
                Id = repositories.AddLocation(addLocation);
                if (Id > 0)
                {

                    ModelState.Clear();
                    result = "Location Added Successfully";
                }
                else
                {

                    result = "Something Went wrong";
                    return Json(new { result = result }, JsonRequestBehavior.AllowGet);
                }

            }
            else {
                result = "Location field is required";
                return Json(new { result = result }, JsonRequestBehavior.AllowGet);

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]

        public ActionResult CreateUser()
        {

            return View();
        }

        [HttpPost]
        public JsonResult CreateUser(CreateUserModel createUserModel1)
        {

            var result = "";
            if (ModelState.IsValid)
            {
                var isExist = WBSController.IsEmailExist(createUserModel1.Email);
                if (isExist == false)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    result = "User Already Created ! Please choose another user";


                    return Json(new { result = result }, JsonRequestBehavior.AllowGet);
                }
                else if (isExist == true)
                {
                    bool InternetConnection = false;
                    InternetConnection=IsInternetConnection();
                    if(InternetConnection==true)
                    {
                        int UserId = repositories.CreateUserWBS(createUserModel1);
                        if (UserId > 0)
                        {
                            string from = "er.manojsingh99@gmail.com"; //From address   
                            string to = createUserModel1.Email; //To address    

                            string userName = createUserModel1.UserName;



                            MailMessage message = new MailMessage(from, to);

                            string Timestamp = "" + DateTime.Now.ToString();
                            string msg = @"<h4>NOTE :</h4> <h5 style='color:green'>Dear " + userName + "  Please use this credential(Email and Password) to login </h5>.";
                            string mailbody = "User Created on :" + Timestamp + "<br>" + "Hello! " + userName + "<br> <br>" + "Your Username/Email is :" + createUserModel1.Email + "<br><br>" + "Your Password is :" + createUserModel1.Password + "<br> <br>" + "Your Locaton is : "+createUserModel1.Location+ "<br> <br>" + msg;

                            message.Subject = "WBS User Creation ";
                            message.Body = mailbody;
                            message.BodyEncoding = System.Text.Encoding.UTF8;
                            message.IsBodyHtml = true;
                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                            System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("er.manojsingh99@gmail.com", "quickinfo123");
                            client.EnableSsl = true;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = basicCredential1;

                            client.Send(message);


                            ModelState.Clear();
                            //ViewBag.Issuccess = "Registration Successful";
                            result = "Registration Successful";
                        }
                        else
                        {
                            result = "Something Went Wrong";
                            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        result = "Internet Connection not Available";
                        return Json(new { result = result }, JsonRequestBehavior.AllowGet);
                    }
                   
                   
                }

            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);




        }


        public ActionResult ManageUserWBS()
        {
            return View();


        }
        [HttpPost]
        public JsonResult ManageUserWBSDetails()
        {

            using (var context = new INVENTORYEntities())
            {
                List<AdminRegister> GetUserDetails = context.AdminRegister.Where(x => x.location != null && x.Role != null).ToList<AdminRegister>();

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
                                      Location = c.location,
                                      accountCreateOn = c.AccountCreateOn,
                                      role = c.Role

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
                                  Location = c.location,
                                  role = c.Role,


                              }).ToList();


                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public JsonResult UpdateUserWBS(CreateUserEditModel createUserModel)
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
                    adminRegisterWBS.location = createUserModel.Location;
                    adminRegisterWBS.Role = createUserModel.Role;

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

        [HttpGet]
        public JsonResult DDLroleType()
        {
            List<DDLRoleType> obj = new List<DDLRoleType>()
            {
                new DDLRoleType {Id=1, RoleType="Admin" },
                new DDLRoleType {Id=2, RoleType="User" },
                new DDLRoleType {Id=3, RoleType="Super Admin" },


            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public bool IsInternetConnection()
        {

            if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return true;
            }
          else
            {
                return false;
            }
            
        }

        [HttpPost]
        public ActionResult AddItemName()
        {

            return View();
        }
        [HttpPost]
        public JsonResult AddItemNameWBS(AddItemWBS  addItemWBS)
        {
            string result = "";
            int Id = 0;

            if (ModelState.IsValid)
            {
                Id = repositories.AddItemName(addItemWBS);
                if (Id > 0)
                {

                    ModelState.Clear();
                    result = "Item Name Added Successfully";
                }
                else
                {

                    result = "Something Went wrong";
                }

            }
            else
            {
                result = "Item field is required";
                return Json(new { result = result }, JsonRequestBehavior.AllowGet);

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);

        }

       [HttpPost]
        public JsonResult TestCount(string location)
        {
          
            var  GetItemNames = typeof(INVENTORYEntities).GetProperties().Select(x => x.Name).ToArray();
            int motorCount,PanelCount=0;


            foreach (var ItemName in GetItemNames)
            {
                
                if (ItemName == "PumpingMachineryMotor")
                {
                    using (INVENTORYEntities context = new INVENTORYEntities())
                    {
                        motorCount = context.PumpingMachineryMotor.Where(i => i.itemname == "PumpingMachineryMotor" &&i.location==location).Count();

                        
                    }
                }

                if (ItemName == "PanelBoard")
                {
                    using (INVENTORYEntities context = new INVENTORYEntities())
                    {
                        PanelCount = context.PanelBoard.Where(i => i.itemname == "PanelBoard" && i.location == location).Count();


                    }
                }

            }






            //    int result = 0;
            ////var res1 = typeof(Valve).GetProperties().Select(x => x.Name).ToArray();
            //for (int i = 0; i < GetTableNames.Length; i++)
            //{
            //    var GetTName = GetTableNames[i];
            //    if (GetTName == "Valve")
            //    {

            //        Dictionary<string, Type> tableTypeDict = new Dictionary<string, Type>()
            //       {
            //          { "Table1", typeof(Valve) }

            //       };
            //        var GetColumnName = typeof(Valve).GetProperties().Select(x => x.Name).ToArray();

            //        string ColumnNameLocation = GetColumnName[1];
            //        string ColumnNameItemName = GetColumnName[2];

            //        // var query = entities.Set(tableTypeDict["Table1"]);
            //        result = entities.Valve.Where(x => x.location == location && ColumnNameItemName == "itemname").Count();


            //    }
            //}
          //  var result = new { GetItemNames, ID = "32" };
            return Json(new { GetItemNames,PanelCount}, JsonRequestBehavior.AllowGet);

        }



    }
}