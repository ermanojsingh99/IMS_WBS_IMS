$(document).ready(function () {
    $.ajax({

        url: "/Admin/AssetCountDetails/",
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        data: {},
        type: "GET",
        success: function (data) {
            console.log(data);
            $("#cpuCount").append(data);

        }


    });
    $.ajax({

        url: "/Admin/AssetCountDetailsLaptop/",
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        data: {},
        type: "GET",
        success: function (data) {
            console.log(data);
            $("#LaptopCount").append(data);

        }


    });
    $.ajax({

        url: "/Admin/AssetCountDetailsPrinter/",
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        data: {},
        type: "GET",
        success: function (data) {
            console.log(data);
            $("#PrinterCount").append(data);

        }


    });
    $.ajax({

        url: "/Admin/AssetCountDetailsWS/",
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        data: {},
        type: "GET",
        success: function (data) {
            console.log(data);
            $("#WSCount").append(data);

        }


    });

    //$.ajax({
        
    //    url: "/Admin/AssetCountDetailsTablet/",
    //    contentType: 'application/json; charset=utf-8',
    //    datatype: 'json',
    //    data: {},
    //    type: "GET",
    //    success: function (data) {
    //        alert(data);
    //        $("#TabletCnt").append(data);

    //    }


    //});


});



//$(document).on("click", "a[name='idPrinter']", function (e) {
//    alert("Printer  clicked");

//    $.ajax(
//        {
//            url: "/Admin/AddNewAssetDetails",
//            contentType: 'application/json; charset=utf-8',
//            datatype: 'json',
//            data: {

//            },
//            type: "GET",
//            success: function (data) {
                
//            }
//        });


//});
//$(document).on("click", "a[name='idWS']", function (e) {
//    alert("WS clicked");

//    $.ajax(
//        {
//            url: "",
//            contentType: 'application/json; charset=utf-8',
//            datatype: 'json',
//            data: {

//            },
//            type: "GET",
//            success: function (data) {
//                // $("#partial").html(result);
//            }
//        });


//});



