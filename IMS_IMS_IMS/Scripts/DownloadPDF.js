$(document).ready(function () {

    //$("#MydataTable").hide();
    //$("#MydataTable1").hide();
    //$("#divtable").hide();
    //$("#divtable1").hide();
    //  $("#btnSearch").click(function () {
    //$("#MydataTable tbody").empty();
    //  $("#MydataTable1 tbody").empty();
    $("#loaderDiv").show();
    // var id = $("#txtSearch").val();
    var ErpID = getUrlVars()["ErpID"];

    $.ajax({

        type: "GET",
        url: "http://localhost:50298/Home/GetDetailsErpId/?ErpID=" + ErpID,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { ErpID: ErpID },
        success: function (data) {
            window.history.pushState("Details", "Title", "<w$5&89%88:$&/Home/; ?>/GetDetailsErpId1");
            $("#MydataTable").show();
            $("#MydataTable1").show();
            $("#divtable").show();
            $("#divtable1").show();


            // $('#blogTable tbody').empty();
            var row = "";
            var row1 = "";

            // var data1 = "";
            $.each(data, function (index, item) {

                row = "<tr><td>" + item.ErpId + "</td><td>" + item.EmpName + "</td><td>" + item.Email + "</td><td>" + item.Department + "</td><td>" + item.Designation + "</td><td>" + item.Mobile + "</td></tr>";

                row1 += "<tr><td>" + item.AssetId + "</td><td>" + item.AssetName + "</td><td>" + item.SerialNo + "</td><td>" + item.WarrantyStart + "</td><td>" + item.WarrantyEnd + "</td><td>" + item.Issuedate + "</td><td>" + item.MacId + "</td></tr>";
                //data1 = item.AssetName;

            });
            $("#MydataTable tbody").append(row);
            $("#MydataTable1 tbody").append(row1);



            $("#loaderDiv").hide();

        },
        error: function (error) {
            alert('error; ' + eval(error));

        }
    });

    //});

})



$(document).on("click", "a[name='lnkViews']", function (e) {
    alert("Calling function");
    var ErpId = $("#txtSearch").val();
    alert(ErpId);
    $.ajax(
        {
            url: "/Home/DownloadAttachment/",
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            data: {
                ImageName: "abc.pdf"
            },
            type: "GET",
            success: function () {
                @*//window.location = '@Url.Action("DownloadAttachment", "PostDetail", new { studentId = erp })';*@
                }
        });


});

$("#btndownload").click(function () {

    // var EmployeeId = $("#txtSearch").val();
    var EmployeeId = getUrlVars()["ErpID"];
    alert(EmployeeId);
    $.ajax(
        {
            url: "/Home/DownloadPDF/",
            contentType: 'application/pdf',
            datatype: 'native',
            data: { EmpId: EmployeeId },
            xhrFields: { responseType: 'blob' },
            type: "GET",
            success: function (response) {

                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(new Blob([response], { type: "application/pdf" }));
                link.download = "SignedAgrrement" + new Date() + ".pdf";
                link.click();


            }
        });
})