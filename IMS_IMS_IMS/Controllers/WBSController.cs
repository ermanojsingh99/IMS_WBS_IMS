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
using static System.Net.WebRequestMethods;
using System.Data;
using ClosedXML.Excel;
using System.Net.Mail;
using System.Net;


namespace IMS_IMS_IMS.Controllers
{
    [IMSAuthentication]
    public class WBSController : Controller
    {
       
        Repositories repositories = null;
        INVENTORYEntities Datacontext;
        public WBSController()
        {
            
          
            
            repositories = new Repositories();
          
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                List<ItemListTable> ItemList = context.ItemListTable.ToList();
                ViewBag.ItemList = new SelectList(ItemList, "ItemName", "ItemName");

                List<LocationZone> locationList = context.LocationZone.ToList();
                ViewBag.LocationList = new SelectList(locationList, "locationZone1", "locationZone1");

                Datacontext = new INVENTORYEntities();
                //Loop through the Table name from Entity Framework
                var GetTableNames = typeof(INVENTORYEntities).GetProperties().Select(x => x.Name).ToArray();

                foreach(var EntityName in GetTableNames)
                {
                    if(EntityName== "PumpingMachineryMotor")
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

             

               // ViewBag.ItemName = Datacontext.PumpingMachineryMotor.Where(x => x.itemname.Equals(ItemName1)).ToList<PumpingMachineryMotor>().FirstOrDefault().itemname;
             
            }


        }
        public ActionResult Index()
        {
            return View();
        }

       
        [HttpGet]
        public ActionResult WaterAssetDetailsList()
        {

            return View();

        }
        [HttpPost]
        public JsonResult _WaterAssetDetailsList()
        {
            List<WaterBoostingInventory> GetDetails;
            using (var context = new INVENTORYEntities())
            {
                //List<AssetDetailsModel> EmpAssetInfo = context.AssetDetails.ToList();
                GetDetails = context.WaterPumpAssetDetails.Select(x => new WaterBoostingInventory
                {
                    Location = x.location,
                    ItemType = x.itemType,
                    Category = x.category,
                    ItemName = x.itemName,
                    PumpHouseName = x.pumphousename,
                    PumpHouseWTP = x.pumphouseWTP,
                    PumpReferenceName = x.pumpreferencename,
                    RatedPower = x.retedpower,
                    RatedHead = x.reatedhead,
                    RatedFlow = x.reatedflow,
                    TypeOfPump = x.typeofpump,
                    Make = x.make,
                    Quantity = x.Quantity,
                    UOM = x.UOM,
                    Description = x.descript

                }).ToList();



                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpGet]
        public ActionResult AddWaterDetailsModal()
        {

            return View("WaterAssetDetailsList");

        }

        [HttpPost]
        public JsonResult AddWaterDetailsModal(WaterBoostingInventory boostingInventory)
        {
            string result = "";
            int Id = 0;

            if (ModelState.IsValid)
            {

                //var isExist = AssetAlreadyExist(assetDetailsModel.AssetId_unique);
                //var SerialExist = AssetAlreadyExist(assetDetailsModel.SerialNo);
                //if (isExist == false || SerialExist == false)
                //{

                //    message = "Asset Already Exist";
                //}

                //else if (isExist == true || SerialExist == true)
                //{
                Id = repositories.AddWaterDetailsModel(boostingInventory);
                if (Id > 0)
                {

                    result = "Asset Added Successfully";
                    ModelState.Clear();

                }
                else
                {
                    result = "Something Went wrong";
                }


            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult CreateItem()
        {

            return View();

        }

        [HttpPost]
        public JsonResult CreateItem(CreateItemModel createItemModel)
        {
            string result = "";
            int Id = 0;

            if (ModelState.IsValid)
            {

                //var isExist = AssetAlreadyExist(assetDetailsModel.AssetId_unique);
                //var SerialExist = AssetAlreadyExist(assetDetailsModel.SerialNo);
                //if (isExist == false || SerialExist == false)
                //{

                //    message = "Asset Already Exist";
                //}

                //else if (isExist == true || SerialExist == true)
                //{
                Id = repositories.CreateItem(createItemModel);
                if (Id > 0)
                {
                    ModelState.Clear();
                    result = "Asset Added Successfully";


                }
                else
                {
                    result = "Something Went wrong";
                }


            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult WBSAssetDetails()
        {


            return View();

        }
        [HttpPost]
        public JsonResult WBSAssetDetails(string SelectedItemName)
        {

            if(SelectedItemName!=null)
            {

                RedirectToAction(SelectedItemName);
            }


            return Json("Something went wrong", JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult PumpingMachineryMotor()
        {
            return View();

        }

        [HttpPost]

        public JsonResult PumpingMachineryMotor(PumpingMachineryMotorModel machineryMotorModel)
        {
            
            string message = "";
            int ItemCode = 0;

            if (ModelState.IsValid)
            {
                 ItemCode = repositories.PumpiingMachineryMotor1(machineryMotorModel);
                if(ItemCode>0)
                {

                    message = "Asset Added Successfully";
                }
                else
                {

                    message = "Something Went wrong";
                }

            }


           // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { message = message, ItemCode= ItemCode }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult PumpingMachineryPumpSet()
        {
            return View();

        }

        [HttpPost]

        public JsonResult PumpingMachineryPumpSet(PumpingMachineryPumpSetModel machineryPumpSetModel)
        {
            string message = "";
            int ItemCode = 0;

            if (ModelState.IsValid)
            {
                 ItemCode = repositories.PumpingMachineryPumpSet(machineryPumpSetModel);
                if (ItemCode > 0)
                {

                    message = "Asset Added Successfully";
                }
                else
                {

                    message = "Something Went wrong";
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { message = message, ItemCode = ItemCode }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult PanelBoard()
        {
            return View();

        }

        [HttpPost]

        public JsonResult PanelBoard(PanelBoardModel panelBoardModel)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.PanelBoard(panelBoardModel);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Transformer()
        {
            return View();

        }

        [HttpPost]

        public JsonResult Transformer(PowerEquipemntModel transformers)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.Transformer(transformers);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        public ActionResult Pipe()
        {
            return View();

        }

        [HttpPost]

        public JsonResult Pipe(PipeModel pipeModel)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.Pipe(pipeModel);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        public ActionResult Valve()
        {
            return View();

        }

        [HttpPost]

        public JsonResult Valve(ValveModel valveModel)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.Valve(valveModel);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Land()
        {
            return View();

        }
        [HttpPost]
        public JsonResult Land(LandModel landModel)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.LandofLocation(landModel);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Building()
        {
            return View();

        }

        [HttpPost]
        public JsonResult Building(BuildingModel buildingModel)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.buildingLocation(buildingModel);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult StorageAndSedimentationTank()
        {
            return View();

        }

        [HttpPost]
        public JsonResult StorageAndSedimentationTank(StorageSedimentationTankModel storageSedimentationTank)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.storageSedimentationTank(storageSedimentationTank);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult WaterTreatmentPlant()
        {
            return View();

        }
        [HttpPost]
        public JsonResult WaterTreatmentPlant(WTPModel tPModel)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                int ItemCode = repositories.WTP(tPModel);
                if (ItemCode > 0)
                {

                    status = true;
                }
                else
                {

                    status = false;
                }

            }


            // string strDDLValue = Request.Form["location"].ToString();
            return Json(new { result = status }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult ClearWaterTank()
        {
            return View();

        }
        [HttpGet]
        public ActionResult GeneratingSet()
        {
            return View();

        }


        [HttpPost]
        public JsonResult ShowTransformerDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Transformer.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      PowerType = c.powertype,
                                      KiVA = c.KVA,
                                      Make = c.make,
                                      Quantity=c.qty,
                                      Status=c.status,
                                      Description=c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }


        [HttpPost]
        public JsonResult ShowValveDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Valve.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      ValveType = c.valveType,
                                      Size = c.size,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpPost]
        public JsonResult ShowPumpMotorDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PumpingMachineryMotor.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      BHP = c.BHP,
                                      ItemType = c.ItemType,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        public JsonResult ShowPumpingMachineryMotorSTPBeh()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PumpingMachineryMotor.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      BHP = c.BHP,
                                      ItemType = c.ItemType,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }
        
        [HttpPost]
        public JsonResult ShowPumpSetDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PumpingMachineryPumpset.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      Discharge = c.discharge,
                                      Head = c.head,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        //[HttpPost]
        //public JsonResult ShowPowerEquipmentDetails()
        //{
        //    var SessionLocationData = Session["location"].ToString();
        //    using (var context = new INVENTORYEntities())
        //    {

        //        var GetDetails = (from c in context.Transformer.Where(x => x.location.Equals(SessionLocationData))


        //                          select new
        //                          {
        //                              ItemCode = c.itemcode,
        //                              Location = c.location,
        //                              ItemName = c.itemname,
        //                              Discharge = c.discharge,
        //                              Head = c.head,
        //                              Make = c.make,
        //                              Quantity = c.qty,
        //                              Status = c.status,
        //                              Description = c.discription

        //                          }).ToList();
        //        return Json(GetDetails, JsonRequestBehavior.AllowGet);
        //    }




        //}
        [HttpPost]
        public JsonResult ShowPipeDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Pipe.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      PipeType1 = c.PipeType,
                                      Diameter = c.diameter,
                                      Length = c.lenght,
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }
        [HttpPost]
        public JsonResult ShowPanelBoardDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.PanelBoard.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      Capacity = c.capacity,
                                     
                                      Make = c.make,
                                      Quantity = c.qty,
                                      Status = c.status,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpPost]
        public JsonResult ShowLandDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Land.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      Length = c.length,
                                      Width = c.breadth,
                                      Area = c.area,
                                      Quantity = c.qty,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpPost]
        public JsonResult ShowBuildingDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.Building.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                     // Location = c.location,
                                      //ItemName = c.itemname,
                                      BuildingName = c.buildingName,
                                      Length = c.length,
                                      Width = c.breadth,
                                      CoveredArea = c.coveredarea,
                                      Quantity = c.qty,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpPost]
        public JsonResult ShowStorageSediTankDetails()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.StorageSediTank.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      TankType = c.tanktype,
                                      Height = c.height,
                                      Length = c.length,
                                      Width = c.breadth,
                                      Capacity = c.capacity,
                                      Quantity = c.qty,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpPost]
        public JsonResult ShowWTP_Details()
        {
            var SessionLocationData = Session["location"].ToString();
            using (var context = new INVENTORYEntities())
            {

                var GetDetails = (from c in context.WTP.Where(x => x.location.Equals(SessionLocationData))


                                  select new
                                  {
                                      ItemCode = c.itemcode,
                                      //Location = c.location,
                                      //ItemName = c.itemname,
                                      Diameter = c.diameter,
                                      Capacity = c.capacity,
                                      Quantity = c.qty,
                                      Description = c.discription

                                  }).ToList();
                return Json(GetDetails, JsonRequestBehavior.AllowGet);
            }




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
        public JsonResult DDLlocation()
        {
            List<location> obj = new List<location>()
            {
                new location {Id=1, locationName="BASAI" },
                new location {Id=2, locationName="Chandu Budhera" },
                new location {Id=4, locationName="Sector-51"},
                new location {Id=5, locationName="Sector-16" },
                new location {Id=6, locationName="STPBehrampur" },
                new location {Id=7, locationName="STPDhanwapur" },
                new location {Id=8, locationName="NCR Channel" },
                new location {Id=9, locationName="Sector-16(Part-I)" },
                new location {Id=10, locationName="Sector-16(Part-II)" },
                new location {Id=11, locationName="Sector-72" }
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult DDLItemType()
        {
            List<DDLItemType> obj = new List<DDLItemType>()
            {
                new DDLItemType {Id=1, ItemType="Consumable" },
                new DDLItemType {Id=2, ItemType="Non-Consumable" },
                new DDLItemType {Id=4, ItemType="Scrap"},
               // new DDLItemType {Id=5, ItemType="Sector-" },
               
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult DDLrole()
        {
            List<DDLRoleType> obj = new List<DDLRoleType>()
            {
                new DDLRoleType {Id=1, RoleType="Admin" },
                new DDLRoleType {Id=2, RoleType="User" },
              
               
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult DDLCategory()
        {
            List<DDLCategory> obj = new List<DDLCategory>()
            {
                new DDLCategory {Id=1, Category="Pump" },

                new DDLCategory {Id=4, Category="Other"},
               // new DDLItemType {Id=5, ItemType="Sector-" },
               
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult DDLTypeOfValve()
        {
            List<DDLValveType> obj = new List<DDLValveType>()
            {
                new DDLValveType {Id=1, ValveType="Air Valve" },
                new DDLValveType {Id=2, ValveType="Sluice Valve" },
                new DDLValveType {Id=3, ValveType="NRV Valve" },
                //new DDLValveType {Id=3, ValveType="valve-Type-3" },
                //new DDLValveType {Id=4, ValveType="valve-Type-4"},
               // new DDLItemType {Id=5, ItemType="Sector-" },
               
            }.ToList();
            
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult DDLTypeOfPipe()
        {
            List<DDLPipeType> obj = new List<DDLPipeType>()
            {
                new DDLPipeType {Id=1, PumpType="pump-Type-1" },
                new DDLPipeType {Id=2, PumpType="pump-Type-2" },
                new DDLPipeType {Id=3, PumpType="pump-Type-3" },
                new DDLPipeType {Id=4, PumpType="pump-Type-4"},
               // new DDLItemType {Id=5, ItemType="Sector-" },
               
            }.ToList();
            
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        
        [HttpGet]
        public JsonResult DDLTankType()
        {
            List<DDLTankType> obj = new List<DDLTankType>()
            {
                new DDLTankType {Id=1, TankType="Clear Water Tank" },
                new DDLTankType {Id=2, TankType="Sedimentation Tank" },
               
               
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult DDLItemName()
        {
            List<DDLItemName> obj = new List<DDLItemName>()
            {
                new DDLItemName {Id=1, ItemName="PumpingMachineryMotor" },
                new DDLItemName {Id=2, ItemName="PumpingMachineryPumpSet" },
                new DDLItemName {Id=3, ItemName="PanelBoard" },
                new DDLItemName {Id=4, ItemName="PowerEquipment" },
                new DDLItemName {Id=5, ItemName="Pipe" },
                new DDLItemName {Id=6, ItemName="Valve" },
                new DDLItemName {Id=7, ItemName="Land" },
                new DDLItemName {Id=8, ItemName="Building" },
                new DDLItemName {Id=9, ItemName="Tank" },
                new DDLItemName {Id=10, ItemName="WaterTreatmentPlant" },
                
               
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult DDLPowerType()
        {
            List<DDLPowerType> obj = new List<DDLPowerType>()
            {
                new DDLPowerType {Id=1, PowerType="Generating Set" },
                new DDLPowerType {Id=2, PowerType="Transformer" },
                new DDLPowerType {Id=3, PowerType="VCB" },
                //new DDLPowerType {Id=4, PowerType="Other"},
               // new DDLItemType {Id=5, ItemType="Sector-" },
               
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult DDLSubLocation()
        {
            List<DDLSubLocation> obj = new List<DDLSubLocation>()
            {
                new DDLSubLocation {Id=1, SubLocation="MPS-50MLD" },
                new DDLSubLocation {Id=2, SubLocation="MPS-120MLD" },
                new DDLSubLocation {Id=3, SubLocation="MPS-100MLD" },
                new DDLSubLocation {Id=4, SubLocation="MPS-68MLD" },
                new DDLSubLocation {Id=5, SubLocation="WTP Chandu Budhera" },
                new DDLSubLocation {Id=6, SubLocation="Boosting Station Sector-16" },
                new DDLSubLocation {Id=7, SubLocation="Boosting Station Gwal Pahari" },
                new DDLSubLocation {Id=8, SubLocation="Boosting Station Sector-72" },
                new DDLSubLocation {Id=9, SubLocation="Boosting Station Sector-51" },
                new DDLSubLocation {Id=10, SubLocation="WTP BASAI" },
                new DDLSubLocation {Id=11, SubLocation="Recycle" },
              
              
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult DDLStatus()
        {
            List<Status> obj = new List<Status>()
            {
                new Status {Id=1, status="Fixed" },
                new Status {Id=2, status="Repairable" },
                new Status {Id=3, status="Abandoned(Junk)" },
                new Status {Id=4, status="Material at Site(Stock)" },


            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);

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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from pumpmachineMotor in entities.PumpingMachineryMotor.Where(x => x.location.Equals(SessionLocationData))  select pumpmachineMotor;
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from pumpset in entities.PumpingMachineryPumpset.Where(x => x.location.Equals(SessionLocationData)) select pumpset;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname,GetColumn.location, GetColumn.discharge,GetColumn.head, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from PowerEquipment in entities.Transformer.Where(x => x.location.Equals(SessionLocationData)) select PowerEquipment;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.powertype, GetColumn.location,GetColumn.KVA, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from Tank in entities.StorageSediTank.Where(x => x.location.Equals(SessionLocationData)) select Tank;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.tanktype, GetColumn.location, GetColumn.height,GetColumn.length,GetColumn.breadth,GetColumn.capacity, GetColumn.qty, GetColumn.discription);
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from valve in entities.Valve.Where(x => x.location.Equals(SessionLocationData)) select valve;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.valveType, GetColumn.location,GetColumn.size, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from building in entities.Building.Where(x => x.location.Equals(SessionLocationData)) select building;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.buildingName, GetColumn.location, GetColumn.length, GetColumn.breadth,GetColumn.coveredarea, GetColumn.qty,  GetColumn.discription);
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
            var GetDetails = from wtp in entities.WTP.Where(x => x.location.Equals(SessionLocationData)) select wtp;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.location,GetColumn.diameter, GetColumn.capacity, GetColumn.qty, GetColumn.discription);
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from land in entities.Land.Where(x => x.location.Equals(SessionLocationData)) select land;
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from panelboard in entities.PanelBoard.Where(x => x.location.Equals(SessionLocationData)) select panelboard;
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


            var SessionLocationData = Session["location"].ToString();
            var GetDetails = from pipe in entities.Pipe.Where(x => x.location.Equals(SessionLocationData)) select pipe;
            foreach (var GetColumn in GetDetails)
            {
                dt.Rows.Add(GetColumn.itemname, GetColumn.PipeType, GetColumn.location,GetColumn.diameter,GetColumn.lenght, GetColumn.make, GetColumn.qty, GetColumn.status, GetColumn.discription);
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

        //public ActionResult ShowWaterPumpMotor()
        //{

        //    return View();
        //}

        //[HttpPost]
        //public JsonResult ShowPumpMotorDetails1()
        //{
        //    var SessionLocationData = Session["location"].ToString();
        //    using (var context = new INVENTORYEntities())
        //    {

        //        var GetDetails = (from c in context.PumpingMachineryMotor.Where(x => x.location.Equals(SessionLocationData))


        //                          select new
        //                          {
        //                              ItemCode = c.itemcode,
        //                              Location = c.location,
        //                              ItemName = c.itemname,
        //                              BHP = c.BHP,
        //                              ItemType = c.ItemType,
        //                              Make = c.make,
        //                              Quantity = c.qty,
        //                              Status = c.status,
        //                              Description = c.discription

        //                          }).ToList();
        //        return Json(GetDetails, JsonRequestBehavior.AllowGet);
        //    }




        // }
        [HttpGet]
        public JsonResult AssetCountDetailsPumpSet()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.PumpingMachineryPumpset.Where(i => i.location=="BASAI").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsPumpSetChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.PumpingMachineryPumpset.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        

         [HttpGet]
        public JsonResult AssetCountPumpMotorSTPBeh()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.PumpingMachineryPumpset.Where(i => i.location == "STPBehrampur").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsPumpMotor()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.PumpingMachineryMotor.Where(i => i.location=="BASAI").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsPumpMotorChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.PumpingMachineryMotor.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsValve()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.Valve.Where(i =>i.location=="BASAI").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult AssetCountDetailsValveChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.Valve.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public JsonResult AssetCountDetailsPipe()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.Pipe.Where(i => i.location=="BASAI").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult AssetCountDetailsPipeChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.Pipe.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsPE()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPE = context.Transformer.Where(i => i.location=="BASAI").Count();

                return Json(countPE, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsPEChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPE = context.Transformer.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPE, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsTank()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.StorageSediTank.Where(i => i.location=="BASAI").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsTankChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.StorageSediTank.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult AssetCountDetailsWTP()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.WTP.Where(i =>i.location=="BASAI").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult AssetCountDetailsWTPChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.WTP.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult AssetCountDetailsLand()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countLand = context.Land.Where(i => i.location=="BASAI").Count();

                return Json(countLand, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsLandChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countLand = context.Land.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countLand, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsBuilding()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.Building.Where(i => i.location=="BASAI").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult AssetCountDetailsBuildingChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPumpSet = context.Building.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPumpSet, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsPanelBoard()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPanelBoard = context.PanelBoard.Where(i =>i.location=="BASAI").Count();

                return Json(countPanelBoard, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult AssetCountDetailsPanelBoardChanduBudhera()
        {
            using (INVENTORYEntities context = new INVENTORYEntities())
            {
                var countPanelBoard = context.PanelBoard.Where(i => i.location == "ChanduBudhera").Count();

                return Json(countPanelBoard, JsonRequestBehavior.AllowGet);
            }
        }


    }
}