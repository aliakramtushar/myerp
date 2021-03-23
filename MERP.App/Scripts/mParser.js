function MLoadComboBox(cbo_Id, oList) {
    MClearComboBox(cbo_Id);
    for (var i = 0; i < oList.length; i++) {
        $('#' + cbo_Id).append('<option value="' + oList[i].MModelPK + '">' + oList[i].MModelString + '</option>');
    }
}
function MClearComboBox(cbo_Id) {
    //document.getElementById('#'+cbo_Id).innerText = null;
    $('#' + cbo_Id).empty();
}

function MLoadComboBoxWithSelect(cbo_Id, oList) {
    MClearComboBox(cbo_Id);
    $('#' + cbo_Id).append('<option value="0">--select one--</option>');
    for (var i = 0; i < oList.length; i++) {
        $('#' + cbo_Id).append('<option value="' + oList[i].MModelPK + '">' + oList[i].MModelString + '</option>');
    }
}
function MDateFormatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    //return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
    return (d < 10 ? ('0' + d) : d) + '-' + (m < 10 ? ('0' + m) : m) + '-' + y;
}
function MParser(s) {
    if (!s) return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[2], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[0], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}
function MGetDate() {
    //var str = "";
    //str = new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate();
    //return str;
    var date = new Date();
    return (date.getDate() < 10 ? ('0' + date.getDate()) : date.getDate()) + '-' + (date.getMonth() + 1) + '-' + date.getFullYear();
}
function MGetDateSt(sDate) {
    var dateParts = sDate.split("-");
    var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]); 
    return dateObject;
}
function MSetDate(sDate) {
    var dateParts = sDate.split("-");
    var dateObject = (dateParts[0]) + '-' + dateParts[1] + '-' + dateParts[2];
    return dateObject;
}
//---------------------------------easyui datebox-------------------------------------
function merpformatter(date) {
    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return (date.getDate() < 10 ? ('0' + date.getDate()) : date.getDate()) + " " + monthNames[date.getMonth()] + " " + date.getFullYear();
}
function merpparser(s) {
    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    if (!s) return new Date();
    var ss = (s.split(' '));
    var y = parseInt(ss[2], 10);
    var m = parseInt(monthNames.indexOf(ss[1]));
    var d = parseInt(ss[0], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m, d);
    } else {
        return new Date();
    }
}


//------------------------------EasyUI Notification--------------------------//
//showType = show, slide, fade
// c1=green, c2=gray, c5=red, c6=blue, c7=yellow
function MEUAL(title, text, timeout, showType, cls) {
    $.messager.show({
        title: title,
        msg: text,
        timeout: timeout * 1000,
        showType: showType,
        cls: cls
    });
}
//------------------------------EasyUI ProgressBar--------------------------//
function MProgressStart() {
    $.messager.progress({
        //title: 'Please Wait',
        msg: 'Loading Data...'
    });
}
function MProgressEnd() {
    $.messager.progress('close');
}
//----------------------------------SWEET ALERT------------------------------//
//warning error success info
function MSWAL(title, text, icon){
    swal({
        title: title,
        text: text,
        icon: icon,
        button: "OK",
    });
}
function MSWAL_WithFocus(title, text, icon, focus_id) {
    swal({
        title: title,
        text: text,
        icon: icon,
        button: true
    })
    .then((willDelete) => {
        if (willDelete) {
            $('#' + focus_id).focus();
        } 
    });
}
function MSWAL_WithRedirect(title, text, icon, redirect_link) {
    swal({
        title: title,
        text: text,
        icon: icon,
        button: true
    }).then((willDelete) => {window.location.href = redirect_link;});
}
function MSWAL_WithReturnMSWAL(title, text, icon, isButton, isDangerMode, fn_return) {
    swal({
        title: title,
        text: text,
        icon: icon,
        button: isButton,
        dangerMode: isDangerMode,
    })
        .then((willDelete) => {
            if (willDelete) {
                debugger;
                if (fn_return!='')window[fn_return]();
            } else {
                // if needed, code here
            }
        });
}
function MSWAL_WithReturnMSWAL2(title, text, icon, isButtons, isDangerMode, fn_return) {
    swal({
        title: title,
        text: text,
        icon: icon,
        buttons: isButtons,
        dangerMode: isDangerMode,
    }).then((willDelete) => {
        if (willDelete) {
            if (fn_return != '') window[fn_return]();}
        });
}