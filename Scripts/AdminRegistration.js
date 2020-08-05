
$("#containerdiv").width('800px').height('600px');
    $(document).ready(function () {

        $("#btnSubmit").click(function (e) {
          // e.preventDefault();
    //var serializedForm = $(this).serialize(); 
            $("#loaderDiv").show();

           //  var data = $("#myForm").serialize();
            var dataObject = {

                UserName: $("#UserName").val(),
                EmailAddress: $("#EmailAddress").val(),
                Password: $("#Password").val(),
                ConfirmPassword: $("#ConfirmPassword").val(),

            }
            console.log(dataObject);
            $.ajax({

                type: "POST",
                url: "/Account/AdminRegistration",
               // dataType: "json",
               // contentType: "application/json; charset=utf-8",
                data: JSON.stringify(dataObject),
                success: function (response) {
                    console.log(response)
                    alert(response.result);
                }


            });
           
        });


    });