$(function () {
    $('#AssetName').change(function () {
       // var value = $(this).find("option:selected").text()
        var value = $("#AssetName option:selected").text();
        //ups
        if (value == "UPS") {
            $("#IpAddress").attr("disabled", true);
            $("#IpAddress").empty();
            $("#MacAddress").attr("disabled", true);
            $("#MacAddress").empty();
            $("#HDD").attr("disabled", true);
            $("#HDD").empty();
            $("#RAM").attr("disabled", true);
            $("#RAM").empty();
            $("#Processor").attr("disabled", true);
            $("#Processor").empty();
        }
        //monitor
        else if (value == "Monitor") {
            $("#IpAddress").attr("disabled", true);
            $("#IpAddress").empty();
            $("#MacAddress").attr("disabled", true);
            $("#MacAddress").empty();
            $("#HDD").attr("disabled", true);
            $("#HDD").empty();
            $("#RAM").attr("disabled", true);
            $("#RAM").empty();
            $("#Processor").attr("disabled", true);
            $("#Processor").empty();
          
        }
            // Printer
        else if (value == "Printer") {

            $("#IpAddress").attr("disabled", true);
            $("#IpAddress").empty();
            $("#MacAddress").attr("disabled", true);
            $("#MacAddress").empty();
            $("#HDD").attr("disabled", true);
            $("#HDD").empty();
            $("#RAM").attr("disabled", true);
            $("#RAM").empty();
            $("#Processor").attr("disabled", true);
            $("#Processor").empty();
           
        }
            //MonitorWS
        else if (value == "MonitorWS") {

            $("#IpAddress").attr("disabled", true);
            $("#IpAddress").empty();
            $("#MacAddress").attr("disabled", true);
            $("#MacAddress").empty();
            $("#HDD").attr("disabled", true);
            $("#HDD").empty();
            $("#RAM").attr("disabled", true);
            $("#RAM").empty();
            $("#Processor").attr("disabled", true);
            $("#Processor").empty();
           
        }
        else {

            $("#IpAddress").attr("disabled", false);
            $("#MacAddress").attr("disabled", false);
            $("#HDD").attr("disabled", false);
            $("#RAM").attr("disabled", false);
            $("#WarrantyStart").attr("disabled", false);
            $("#WarrantyEnd").attr("disabled", false);
            $("#Processor").attr("disabled", false);
          
        }


       
    });


});















