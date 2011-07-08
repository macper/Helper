$(document).ready(function () {
    $("#newUserDialog").dialog({ autoOpen: false, width: 200, height: 200, closeText: 'X', buttons: [
    {
        text: "Ok",
        click: CreateUser
    }
]
    });
});

    function CreateUser() {
        var txtUserName = $('#MainContent_tbUserName').get(0).value;
        var txtPassword = $('#MainContent_tbPassword').get(0).value;
        DnDHelper.Web.Admin.UserManagementService.CreateUser(txtUserName, txtPassword, CreateUserCallback);
    }

    function CreateUserCallback(result) {
        var label = $('#errorMessage').get(0);
        $('#errorMessage').css('display', '');
        if (result.ErrorCode == 0) {
            label.value = "Użytkownik został dodany";
            $('#newUserDialog').dialog("close");
            $('form').submit();
        }
        else {
            label.innerHTML = result.ErrorDescription;
        }
    }
