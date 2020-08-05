using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using IMS_IMS_MODEL;
using IMS_ENTITYFRAMEWORK;
using Microsoft.SqlServer.Server;



namespace IMS_ENTITYFRAMEWORK.Repository
{
   public class Repositories
    {
        string UserNameSessionData = "";

        public Repositories()
        {

            HttpContext httpContext = HttpContext.Current;
            if (httpContext.ApplicationInstance.Session.Count > 0)
                UserNameSessionData = httpContext.ApplicationInstance.Session["UserName"].ToString();
        }
        

        public int AdminReg(AdminRegisterModel registrationModel)
        {

            using (var context = new INVENTORYEntities())
            {

                AdminRegister adminRegister = new AdminRegister();
                adminRegister.UserName = registrationModel.UserName;
                adminRegister.Email = registrationModel.Email;
                adminRegister.Password = registrationModel.Password;
                adminRegister.RetypePassword = registrationModel.RetypePassword;

                context.AdminRegister.Add(adminRegister);
                context.SaveChanges();
                return adminRegister.UserId;
            }

        }

        public int CreateUserWBS(CreateUserModel createUserModel)
        {

            using (var context = new INVENTORYEntities())
            {

                AdminRegister adminRegister = new AdminRegister();
                adminRegister.UserName = createUserModel.UserName;
                adminRegister.Email = createUserModel.Email;
                adminRegister.Password = createUserModel.Password;
                adminRegister.RetypePassword = createUserModel.RetypePassword;
                adminRegister.location = createUserModel.Location;
                adminRegister.Role = createUserModel.Role;

                context.AdminRegister.Add(adminRegister);
                context.SaveChanges();
                return adminRegister.UserId;
            }

        }
        //public string UniqueDetailsAssets(AssetDetailsFileIdModel assetDetailsModel)
        //{
        //    using (var context = new INVENTORYEntities())
        //    {
        //        AssetID assetID = new AssetID();
        //        assetID.emp_id = assetDetailsModel._EmpId;
        //        assetID.assetid1 = assetDetailsModel._AssetId;//unique asset id add to ASSETID TABLE
        //      //  byte[] uploadedFile = new byte[assetDetailsModel.UploadAgreement.InputStream.Length];
        //      //  assetDetailsModel.UploadAgreement.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
        //      //  assetID.ContentType = uploadedFile;//FILE UPLOAD AS VARBINARY MAX IN ASSSETID TABLE
        //        //string fileName = Path.GetFileName(assetDetailsModel.UploadAgreement.FileName);
        //        //assetID.filename1 = fileName;//FILE NAME IN ASSETID TABLE
        //        //string fullpath = "~/Agreement_Files/";
        //        //assetDetailsModel.FullPath = fullpath + fileName;
        //        //assetID.filePath = assetDetailsModel.FullPath;//FULL PATH ADD IN ASSETID TABLE
        //        context.AssetID.Add(assetID);//ADD TO TABLE
        //        context.SaveChanges();//SAVE CHANGES IN TABLE
        //        return assetID.emp_id;
        //    }
        //}

        public string AddNewAsset(AssetDetailsModel assetDetailsModel)
        {
            using (var context=new INVENTORYEntities())
            {
               
               
                AssetDetails assetNameKey = new AssetDetails();
                assetNameKey.emp_id = assetDetailsModel.EmpId;
              //assetNameKey.asset_id = assetDetailsModel.AssetId;
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
                
                context.AssetDetails.Add(assetNameKey);
                context.SaveChanges();
                return assetNameKey.emp_id;
            }

        }

       public int AddWaterDetailsModel(WaterBoostingInventory waterBoosting)
        {
            using (var context = new INVENTORYEntities())
            {


                WaterPumpAssetDetails waterAssetDetailsTable = new WaterPumpAssetDetails();
                waterAssetDetailsTable.id = waterBoosting.Id;
                //assetNameKey.asset_id = assetDetailsModel.AssetId;
                waterAssetDetailsTable.location = waterBoosting.Location;

                waterAssetDetailsTable.itemType = waterBoosting.ItemType;

                waterAssetDetailsTable.category = waterBoosting.Category;
                waterAssetDetailsTable.itemName = waterBoosting.ItemName;
                waterAssetDetailsTable.pumphousename = waterBoosting.PumpHouseName;
                waterAssetDetailsTable.pumphouseWTP = waterBoosting.PumpHouseWTP;

                waterAssetDetailsTable.pumpreferencename = waterBoosting.PumpReferenceName;
                waterAssetDetailsTable.reatedhead = waterBoosting.RatedHead;
                waterAssetDetailsTable.retedpower = waterBoosting.RatedPower;
                waterAssetDetailsTable.reatedflow = waterBoosting.RatedFlow;
                waterAssetDetailsTable.typeofpump = waterBoosting.TypeOfPump;
                waterAssetDetailsTable.make = waterBoosting.Make;
                waterAssetDetailsTable.Quantity = waterBoosting.Quantity;
                waterAssetDetailsTable.UOM = waterBoosting.UOM;
                waterAssetDetailsTable.descript = waterBoosting.Description;



                context.WaterPumpAssetDetails.Add(waterAssetDetailsTable);
                context.SaveChanges();
                return waterAssetDetailsTable.id;
            }

        }

        public int CreateItem(CreateItemModel createItem)
        {
            using (var context = new INVENTORYEntities())
            {


                CreateItemTable createItemTable = new CreateItemTable();
                createItemTable.id = createItem.id;
                createItemTable.ItemCode = createItem.ItemCode;
                createItemTable.locationzone = createItem.Location;

                createItemTable.ItemType = createItem.ItemType;

                createItemTable.Category = createItem.Category;
                createItemTable.ItemName = createItem.ItemName;

                
                createItemTable.Make = createItem.Make;
                createItemTable.Quantity = createItem.Quantity;
                createItemTable.UOM = createItem.UOM;
                createItemTable.descript = createItem.Description;
                createItemTable.Status = createItem.Status;



                context.CreateItemTable.Add(createItemTable);
                context.SaveChanges();
                return createItemTable.id;
            }

        }

        public int PumpiingMachineryMotor1(PumpingMachineryMotorModel pumpingMachineryMotor)
        {
            
         
            using (var context = new INVENTORYEntities())
            {


                PumpingMachineryMotor pumpingMachinery = new PumpingMachineryMotor();

              
                pumpingMachinery.location = pumpingMachineryMotor.Location;

                pumpingMachinery.itemname = pumpingMachineryMotor.ItemName;


                pumpingMachinery.BHP = pumpingMachineryMotor.BHP;


                pumpingMachinery.make = pumpingMachineryMotor.Make;
                pumpingMachinery.ItemType = pumpingMachineryMotor.Itemtype;
                pumpingMachinery.qty = pumpingMachineryMotor.Quantity;

                pumpingMachinery.discription = pumpingMachineryMotor.Description;
                pumpingMachinery.status = pumpingMachineryMotor.Status;
                pumpingMachinery.UserName = UserNameSessionData;



                context.PumpingMachineryMotor.Add(pumpingMachinery);
                context.SaveChanges();
                return pumpingMachinery.itemcode;
               
            }

        }


        public int PumpingMachineryPumpSet(PumpingMachineryPumpSetModel pumpingMachineryPumpSet)
        {
            using (var context = new INVENTORYEntities())
            {


                PumpingMachineryPumpset machineryPumpset = new PumpingMachineryPumpset();


                machineryPumpset.location = pumpingMachineryPumpSet.Location;

                machineryPumpset.itemname = pumpingMachineryPumpSet.ItemName;
                machineryPumpset.SubLocation = pumpingMachineryPumpSet.SubLocation;
                

                machineryPumpset.discharge = pumpingMachineryPumpSet.Discharge;


                machineryPumpset.head =pumpingMachineryPumpSet.Head;
                machineryPumpset.make = pumpingMachineryPumpSet.Make;
                machineryPumpset.qty = pumpingMachineryPumpSet.Quantity;

                machineryPumpset.discription = pumpingMachineryPumpSet.Description;
                machineryPumpset.status = pumpingMachineryPumpSet.Status;
                machineryPumpset.UserName = UserNameSessionData;


                context.PumpingMachineryPumpset.Add(machineryPumpset);
                context.SaveChanges();
                return machineryPumpset.itemcode;
                
            }

        }

        public int PanelBoard(PanelBoardModel panelBoardModel)
        {
            using (var context = new INVENTORYEntities())
            {


                PanelBoard panelBoard = new PanelBoard();


                panelBoard.location = panelBoardModel.Location;
                panelBoard.itemname = panelBoardModel.ItemName;
                panelBoard.SubLocation = panelBoardModel.SubLocation;
                
                panelBoard.capacity = panelBoardModel.Capacity;
                panelBoard.make = panelBoardModel.Make;
                panelBoard.qty = panelBoardModel.Quantity;
                panelBoard.status = panelBoardModel.Status;
                panelBoard.discription = panelBoardModel.Description;
                panelBoard.UserName = UserNameSessionData;
                context.PanelBoard.Add(panelBoard);
                context.SaveChanges();
                return panelBoard.itemcode;

            }

        }

        public int Transformer(PowerEquipemntModel transformer)
        {
            using (var context = new INVENTORYEntities())
            {


                Transformer transformer1 = new Transformer();


                transformer1.location = transformer.Location;
                transformer1.itemname = transformer.ItemName;
                transformer1.SubLocation = transformer.SubLocation;
                transformer1.powertype = transformer.PowerType;
                transformer1.KVA = transformer.KVA;
                transformer1.make = transformer.Make;
                transformer1.qty = transformer.Quantity;
                transformer1.status = transformer.Status;
                transformer1.discription = transformer.Description;
                transformer1.UserName = UserNameSessionData;

                context.Transformer.Add(transformer1);
                context.SaveChanges();
                return transformer1.itemcode;

            }

        }

        public int Pipe(PipeModel pipeModel)
        {
            using (var context = new INVENTORYEntities())
            {


                Pipe pipe = new Pipe();


                pipe.location = pipeModel.Location;
                pipe.itemname = pipeModel.ItemName;
                pipe.SubLocation = pipeModel.SubLocation;
                pipe.PipeType = pipeModel.TypeOfPipe;
                pipe.diameter = pipeModel.Diameter;
                pipe.lenght = pipeModel.Length;
                pipe.make = pipeModel.Make;
                pipe.qty = pipeModel.Quantity;
                pipe.status = pipeModel.Status;
                pipe.discription = pipeModel.Description;
                pipe.UserName = UserNameSessionData;

                context.Pipe.Add(pipe);
                context.SaveChanges();
                return pipe.itemcode;

            }
        }

            public int Valve(ValveModel valveModel)
            {
                using (var context = new INVENTORYEntities())
                {


                Valve valve = new Valve();


                valve.location = valveModel.Location;
                valve.itemname = valveModel.ItemName;
                valve.SubLocation = valveModel.SubLocation;
                
                valve.valveType = valveModel.ValveType;
                valve.size = valveModel.Size;
              
                valve.make = valveModel.Make;
                valve.qty = valveModel.Quantity;
               // valve.status = valveModel.Status;
                valve.discription = valveModel.Description;
                valve.UserName = UserNameSessionData;

                    context.Valve.Add(valve);
                    context.SaveChanges();
                    return valve.itemcode;

                }

            }

        public int LandofLocation(LandModel landModel)
        {
            using (var context = new INVENTORYEntities())
            {

               Land land = new Land();
                land.location = landModel.Location;
                land.itemname = landModel.ItemName;
                land.SubLocation = landModel.SubLocation;

                land.length = landModel.Length;
                land.breadth = landModel.Breadth;
                land.area = landModel.Area;

                land.qty = landModel.Quantity;
                // valve.status = valveModel.Status;
                land.discription = landModel.Description;
                land.UserName = UserNameSessionData;

                context.Land.Add(land);
                context.SaveChanges();
                return land.itemcode;

            }

        }

        public int buildingLocation(BuildingModel buildingModel)
        {
            using (var context = new INVENTORYEntities())
            {

                Building building = new Building();
                building.location = buildingModel.Location;
                building.itemname = buildingModel.ItemName;
                building.SubLocation = buildingModel.SubLocation;
                building.buildingName = buildingModel.BuildingName;
                building.length = buildingModel.Length;
                building.breadth = buildingModel.Breadth;
                building.coveredarea = buildingModel.CoveredArea;

                building.qty = buildingModel.Quantity;
                // valve.status = valveModel.Status;
                building.discription = buildingModel.Description;
                building.UserName = UserNameSessionData;

                context.Building.Add(building);
                context.SaveChanges();
                return building.itemcode;

            }

        }
        public int storageSedimentationTank(StorageSedimentationTankModel sedimentationTankModel)
        {
            using (var context = new INVENTORYEntities())
            {

                StorageSediTank storageSediTank  = new StorageSediTank();
                storageSediTank.location = sedimentationTankModel.Location;
                storageSediTank.itemname = sedimentationTankModel.ItemName;
                storageSediTank.SubLocation = sedimentationTankModel.SubLocation;
                storageSediTank.tanktype = sedimentationTankModel.TankType;
                storageSediTank.height = sedimentationTankModel.Height;
                storageSediTank.length = sedimentationTankModel.Length;
                storageSediTank.breadth = sedimentationTankModel.Breadth;
                storageSediTank.capacity = sedimentationTankModel.Capacity;

                storageSediTank.qty = sedimentationTankModel.Quantity;
                // valve.status = valveModel.Status;
                storageSediTank.discription = sedimentationTankModel.Description;
                storageSediTank.UserName = UserNameSessionData;

                context.StorageSediTank.Add(storageSediTank);
                context.SaveChanges();
                return storageSediTank.itemcode;

            }

        }

        public int WTP(WTPModel wTPModel)
        {
            using (var context = new INVENTORYEntities())
            {


                WTP wtp = new WTP();


                wtp.location = wTPModel.Location;
                wtp.itemname = wTPModel.ItemName;
                wtp.SubLocation = wTPModel.SubLocation;

                wtp.diameter = wTPModel.Diameter;
                wtp.capacity = wTPModel.Capacity;

                wtp.qty = wTPModel.Quantity;

                wtp.discription = wTPModel.Description;
                wtp.UserName = UserNameSessionData;

                context.WTP.Add(wtp);
                context.SaveChanges();
                return wtp.itemcode;

            }
        }

        public int AddLocation(AddLocation addLocation)
        {
            using (var context = new INVENTORYEntities())
            {


                LocationZone locationZone = new LocationZone();


                locationZone.locationZone1 = addLocation.LocationName;
                

                context.LocationZone.Add(locationZone);
                context.SaveChanges();
                return locationZone.id;

            }
        }

        public int AddItemName(AddItemWBS addItemWBS)
        {
            using (var context = new INVENTORYEntities())
            {


                ItemListTable itemListTable = new ItemListTable();


                itemListTable.ItemName = addItemWBS.ItemName;


                context.ItemListTable.Add(itemListTable);
                context.SaveChanges();
                return itemListTable.Id;

            }
        }


        public int ContactUs(ContactUsDetailsModal contactUsDetailsModal)
        {
            using (var context = new INVENTORYEntities())
            {

                CONTACTUS ContactUs = new CONTACTUS();
                ContactUs.FNAME = contactUsDetailsModal.FirstName;
                ContactUs.LNAME = contactUsDetailsModal.LastName;
                ContactUs.EMAIL = contactUsDetailsModal.Email;
                ContactUs.MOBILE = contactUsDetailsModal.Mobile;
                ContactUs.REMARK = contactUsDetailsModal.Description;
                context.CONTACTUS.Add(ContactUs);
                context.SaveChanges();
                return ContactUs.ID;

            }

        }
    }

}
