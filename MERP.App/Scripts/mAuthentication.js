function MUserAuthentication() {
    if (localStorage.getItem("MUserName") == null) {
        MSWAL('Session Out', 'your session already expired, please login again !!', 'warning');
        window.location.href = "/Login/ViewLogin";
    }
    if ("MUserName" in localStorage) {}
    else {
        MSWAL('Session Out', 'your session already expired, please login again !!', 'warning');
        window.location.href = "/Login/ViewLogin";
    }
}
function MUserLogin(obj) {
    ClearUserSession();
    if (obj == null) { alert('Invalid Object !! Wrong Entry !!') };
    $.ajax({
        type: "POST",
        dataType: "json",
        url: '/Login/UserLogin',
        traditional: true,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var retObj = jQuery.parseJSON(data);
            if (retObj.UserID > 0) {
                SetUserIntoSession(retObj);
                GetAccessedMenu(retObj);
            }
            else {
                ClearUserSession();
                MSWAL("Error", retObj.ErrorMessage, "error");
            }
        },
        error: function (xhr, status, error) {
            MSWAL("Error", error, "error");
        }
    });
}
function GetAccessedMenu(retObj) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Menu/GetAccessedMenu",
        traditional: true,
        data: JSON.stringify(retObj),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            var retObjList = jQuery.parseJSON(data);
            if (retObjList != null) {
                localStorage.setItem('MMenus', data);
                window.location.href = "/Home/Dashboard";
            }
            else {
                alert(retObjList[0].ErrorMessage);
            }
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}
function MLogout() {
    swal({
        title: "Are you sure?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                ClearUserSession();
                window.location.href = "/login/ViewLogin";
            }
        })
}
function SetUserIntoSession(obj) {
    localStorage.setItem('MUserName', obj.UserName);
    localStorage.setItem('MUserBranchName', obj.BranchName);
    localStorage.setItem('MUserBranchID', obj.BranchId);
    localStorage.setItem('MUserFullName', obj.FullName);
    localStorage.setItem('MUserGroupName', obj.GroupName);
    var dLoginTime = new Date();
    localStorage.setItem('MUserLoginLime', dLoginTime);
}
function ClearUserSession() {
    localStorage.setItem('MUser', null);
    localStorage.setItem('MUserName', null);
    localStorage.setItem('MUserBranchName', null);
    localStorage.setItem('MUserBranchID', null);
    localStorage.setItem('MUserFullName', null);
    localStorage.setItem('MUserGroupName', null);
    localStorage.setItem('MUserLoginLime', null);
    localStorage.clear();
}