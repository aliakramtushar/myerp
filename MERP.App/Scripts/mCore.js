function SetPageHeader(headerStr) {
    $('#lblPageHeader').text(headerStr);
}
function SetPanelHeader(panel_Id, panel_Titel, background_Color) {
    $('#' + panel_Id).panel('setTitle', panel_Titel);
    var panel = $('#' + panel_Id).panel('header');
    panel.css('background', background_Color);
}
function MLoadDataGrid(table_Id, obj_List, table_Title) {
    var sessiontable_Id = localStorage.getItem('TableID');
    var nIndex = localStorage.getItem('SelectedRowIndex');
    if (sessiontable_Id != null && sessiontable_Id == table_Id && nIndex != null && parseInt(nIndex) >= 0) {
        $('#' + table_Id).datagrid('loadData', obj_List);
        $('#' + sessiontable_Id).datagrid('selectRow', parseInt(nIndex));
    }
    else {
        $('#' + table_Id).datagrid('loadData', obj_List);
    }
    //var dgPanel = $('#' + table_Id).datagrid('getPanel');  // get the panel object
    //dgPanel.panel('setTitle', table_Title);
}
function MLoadDataGrid(table_Id, obj_List, table_Title, interval, cbo_Id) {
    $('#' + cbo_Id).html('');
    var nPageCount = Math.ceil(obj_List.length / interval);
    for (var i = 1; i <= nPageCount; i++) {
        $('#' + cbo_Id).append('<option value="' + i + '">' + 'Page ' + i + '</option>');
    }
    obj_List = obj_List.slice(0, interval);
    var sessiontable_Id = localStorage.getItem('TableID');
    var nIndex = localStorage.getItem('SelectedRowIndex');
    if (sessiontable_Id != null && sessiontable_Id == table_Id && nIndex != null && parseInt(nIndex) >= 0) {
        $('#' + table_Id).datagrid('loadData', obj_List);
        $('#' + sessiontable_Id).datagrid('selectRow', parseInt(nIndex));
    }
    else {
        $('#' + table_Id).datagrid('loadData', obj_List);
    }
}
function MUpdateDataGrid(table_Id, obj, nIndex) {
    if (nIndex == -1) {
        nIndex = $('#' + table_Id).datagrid('getRowIndex', obj);
    }
    $('#' + table_Id).datagrid('updateRow', { index: nIndex, row: obj });
    $('#' + table_Id).datagrid('refreshRow', nIndex);
}
function MAddOrUpdateDataGrid(table_Id, obj, is_WinUpdate) {
    if (is_WinUpdate == 0) {
        $('#' + table_Id).datagrid('appendRow', obj);
        $('#' + table_Id).datagrid('selectRow', $('#' + table_Id).datagrid('getRows').length-1);
    }
    if (is_WinUpdate == 1) {
        var nIndex = $('#' + table_Id).datagrid('getRowIndex', $('#' + table_Id).datagrid("getSelected"));
        $('#' + table_Id).datagrid('updateRow', { index: nIndex, row: obj });
        $('#' + table_Id).datagrid('refreshRow', nIndex);
    }
}
function MPaginationPageLoad(table_Id, obj_List, interval, cbo_Id) {
    $('#' + table_Id).datagrid('loadData', []);
    var nPageNum = parseInt($('#' + cbo_Id).val()) - 1;
    obj_List = obj_List.slice(nPageNum * interval, (nPageNum * interval) + interval);
    $('#' + table_Id).datagrid('loadData', obj_List);
}
function MBackLink() {
    window.location.href = localStorage.getItem('BackLink');
}
function MAdd(table_Id, controller_Name, action_Name) {
    var obj_List = $('#' + table_Id).datagrid('getRows');
    localStorage.setItem('MObjectList', obj_List);
    localStorage.setItem('TableID', table_Id);
    localStorage.setItem('SelectedRowIndex', obj_List.length);
    localStorage.setItem('BackLink', window.location.href);
    window.location.href = '/' + controller_Name + '/' + action_Name + '?nID=0';
}
function MSelect(table_Id) {
    var obj = $('#' + table_Id).datagrid("getSelected");
    if (obj == null || obj.MModelPK <= 0) {
        swal({ title: "", text: "Please select an item from list!", icon: "warning", button: true, dangerMode: true, })
        return false;
    }
    return obj;
}
function MEdit(table_Id, controller_Name, action_Name) {
    var obj_List = $('#' + table_Id).datagrid('getRows');
    localStorage.setItem('MObjectList', obj_List);
    localStorage.setItem('TableID', table_Id);
    var obj = $('#' + table_Id).datagrid("getSelected");
    if (obj == null || obj.MModelPK <= 0) {
        swal({title: "",text: "Please select an item from list!",icon: "warning",button: true,dangerMode: true,})
        return false;
    }
    localStorage.setItem('SelectedRowIndex', $('#' + table_Id).datagrid('getRowIndex', obj));
    localStorage.setItem('BackLink', window.location.href);
    window.location.href = '/' + controller_Name + '/' + action_Name + '?nID=' + obj.MModelPK;
}
function MDelete(table_Id, controller_Name, action_Name) {
    var obj = $('#' + table_Id).datagrid("getSelected");
    if (obj == null || obj.MModelPK <= 0) {
        swal({ title: "", text: "Please select an item from list!", icon: "warning", button: true, dangerMode: true, })
        return false;
    }
    swal({title: 'Warning',text: 'are you want to delete?',icon: 'warning',buttons: true,dangerMode: true,})
        .then((willDelete) => {
            if (willDelete) {
                var nIndex = $('#' + table_Id).datagrid('getRowIndex', obj);
                if (obj.MModelPK > 0) {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "/" + controller_Name + "/" + action_Name,
                        traditional: true,
                        data: JSON.stringify(obj),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            var retObj = jQuery.parseJSON(data);
                            if (retObj.MModelPK == 0 && retObj.ErrorMessage == "") {
                                swal({ title: "", text: "Data Deleted Successfully !!", icon: "success", button: true, dangerMode: false, })
                                $('#' + table_Id).datagrid('deleteRow', nIndex);
                            }
                            else if(retObj.ErrorMessage == "Data Deleted Successfully !!" || retObj.ErrorMessage == "") {
                                swal({ title: "", text: retObj.ErrorMessage, icon: "success", button: true, dangerMode: false, })
                                $('#' + table_Id).datagrid('deleteRow', nIndex);
                            }
                            else {
                                swal({ title: "", text: retObj.ErrorMessage, icon: "warning", button: true, dangerMode: true, })
                            }
                        },
                        error: function (xhr, status, error) {
                            swal({ title: "", text: error, icon: "warning", button: true, dangerMode: true, })
                        }
                    });
                }
            }
        });
}
function MUpdate(table_Id, controller_Name, action_Name) {
    var obj = $('#' + table_Id).datagrid("getSelected");
    if (obj == null || obj.MModelPK <= 0) {
        swal({ title: "", text: "Please select an item from list!", icon: "warning", button: true, dangerMode: true, })
        return false;
    }
    swal({title: "Are you sure?",text: "once you update, it will change the database!",icon: "warning",buttons: true,dangerMode: true,})
        .then((willDelete) => {
            if (willDelete) {
                var nIndex = $('#' + table_Id).datagrid('getRowIndex', obj);
                if (obj.MModelPK > 0) {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "/" + controller_Name + "/" + action_Name,
                        traditional: true,
                        data: JSON.stringify(obj),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            var retObj = jQuery.parseJSON(data);
                            if (retObj.MModelPK > 0) {
                                swal({ title: "", text: "Dat Updated Successfully !!", icon: "success", button: true, dangerMode: true, })
                                $('#' + table_Id).datagrid('updateRow', { index: nIndex, row: retObj });
                                $('#' + table_Id).datagrid('refreshRow', nIndex);
                            }
                            else {
                                swal({ title: "", text: retObj.ErrorMessage, icon: "warning", button: true, dangerMode: true, })
                            }
                        },
                        error: function (xhr, status, error) {
                            swal({ title: "", text: error, icon: "warning", button: true, dangerMode: true, })
                        }
                    });
                }
            }
        });
}
function MDeleteOnlyFront(table_Id) {
    var obj = $('#' + table_Id).datagrid("getSelected");
    if (obj == null) {
        swal({ title: "", text: "Please select an item from list!", icon: "warning", button: true, dangerMode: true, })
        return false;
    }
    swal({ title: "Warning", text: "Are You Want To Delete?!", icon: "warning", buttons: true, dangerMode: true, })
        .then((willDelete) => {
            if (willDelete) {
                var nIndex = $('#' + table_Id).datagrid('getRowIndex', obj);
                $('#' + table_Id).datagrid('deleteRow', nIndex);
                MSWAL('Successful', 'Successfully Deleted. Data Not Update in Database !!', 'success');
            }
        });
}
function MSave(obj, controller_Name, action_Name) {
    if (obj == null) {
        swal({ title: "", text: "Invalid Object !! Wrong Entry !!", icon: "warning", button: true, dangerMode: true, })
    };
    swal({ title: "Are you sure?", text: "once you save, it will change the database!", icon: "warning", buttons: true, dangerMode: true, })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: '/' + controller_Name + '/' + action_Name,
                    traditional: true,
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var retObj = jQuery.parseJSON(data);
                        if (retObj.MModelPK > 0) {
                            MSWAL_WithRedirect("", 'Data Saved sucessfully !!', 'success', localStorage.getItem('BackLink'));
                        }
                        else {
                            swal({ title: "", text: retObj.ErrorMessage, icon: "error", button: true, dangerMode: true, })
                        }
                    },
                    error: function (xhr, status, error) {
                        swal({ title: "", text: error, icon: "info", button: true, dangerMode: true, })
                    }
                });
            }
        });
}
function MSearch(table_Id, textBox_Id, controller_Name, action_Name) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/" + controller_Name + "/" + action_Name,
        traditional: true,
        data: JSON.stringify({ ErrorMessage: $('#' + textBox_Id).val() }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var retObjList = jQuery.parseJSON(data);
            if (retObjList != null) {
                if (retObjList.length > 0) {
                    if (retObjList[0].ErrorMessage == '') {
                        MObjList = retObjList;
                        $('#' + table_Id).datagrid('loadData', retObjList);
                    }
                    else {
                        MSWAL('Error', retObjList[0].ErrorMessage, 'error');
                    }
                }
                else {
                    MSWAL('Empty List', 'No Data Found !!', 'info');
                    $('#' + table_Id).datagrid('loadData', []);
                }
            }
            else {
                MSWAL('Error', retObjList[0].ErrorMessage, 'error');
            }
        },
        error: function (xhr, status, error) {
            MSWAL('', error, 'error');
        }
    });
}
function MGeneralAjaxWithReturnFn(obj, controller_Name, action_Name, return_fn) {
    MProgressStart();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/" + controller_Name + "/" + action_Name,
        traditional: true,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            MProgressEnd();
            var retObj = jQuery.parseJSON(data);
            if (retObj != null) {
                if (return_fn != '') {
                    window[return_fn](retObj);
                }
            }
            else {
                MSWAL('Error', 'Something Went Wrong !!', 'error');
            }
        },
        error: function (xhr, status, error) {
            MSWAL('', error, 'error');
        }
    });
}
function MGeneralAjaxWithReturnFn_MaxJson(obj, controller_Name, action_Name, return_fn) {
    MProgressStart();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/" + controller_Name + "/" + action_Name,
        traditional: true,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            MProgressEnd();
            var retObj = data;
            if (retObj != null) {
                if (return_fn != '') {
                    window[return_fn](retObj);
                }
            }
            else {
                MSWAL('Error', 'Something Went Wrong !!', 'error');
            }
        },
        error: function (xhr, status, error) {
            MSWAL('', error, 'error');
        }
    });
}
function MGetsTable(obj, controller_Name, action_Name, table_Id, table_Name, interval, cbo_Id) {      // JSON MAX
    MProgressStart();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/" + controller_Name + "/" + action_Name,
        traditional: true,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            MProgressEnd();
            var objList = data;
            if (objList.length < 1) {
                MSWAL('', 'No Data Found !!', 'info');
            }
            else {
                MObjList = objList;
                if (interval == 0) {
                    MLoadDataGrid(table_Id, objList, table_Name);
                }
                else {
                    MObjList = objList;
                    MLoadDataGrid(table_Id, objList, table_Name, interval, cbo_Id);
                }
            }
        },
        error: function (xhr, status, error) {
            window[return_fn](error)
        }
    });
}
function MLoadDataGridByParentID(table_Id, parent_ID, controller_Name, action_Name) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/" + controller_Name + "/" + action_Name,
        traditional: true,
        data: JSON.stringify({ ErrorMessage: parent_ID}),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //var retObjList = jQuery.parseJSON(data);
            var retObjList = data;
            MObjList = data;
            if (retObjList != null) {
                if (retObjList.length > 0) {
                    if (retObjList[0].ErrorMessage == '') {
                        $('#' + table_Id).datagrid('loadData', []);
                        $('#' + table_Id).datagrid('loadData', retObjList);
                    }
                    else {
                        MSWAL('Error', retObjList[0].ErrorMessage, 'error');
                    }
                }
                else {
                    MSWAL('Empty List', 'No Data Found !!', 'info');
                    $('#' + table_Id).datagrid('loadData', []);
                }
            }
            else {
                MSWAL('Error', retObjList[0].ErrorMessage, 'error');
            }
        },
        error: function (xhr, status, error) {
            MSWAL('', error, 'error');
        }
    });
}
function MLoadComboByParentID(combobox_ID, parent_ID, controller_Name, action_Name) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/" + controller_Name + "/" + action_Name,
        traditional: true,
        data: JSON.stringify({ ErrorMessage: parent_ID }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var retObjList = jQuery.parseJSON(data);
            if (retObjList != null) {
                if (retObjList.length > 0) {
                    if (retObjList[0].ErrorMessage == '') {
                        MLoadComboBoxWithSelect(combobox_ID, retObjList);
                    }
                    else {
                        MSWAL('Error', retObjList[0].ErrorMessage, 'error');
                    }
                }
                else {
                    MSWAL('Empty List', 'No Data Found !!', 'info');
                    $('#' + table_Id).datagrid('loadData', []);
                }
            }
            else {
                MSWAL('Error', retObjList[0].ErrorMessage, 'error');
            }
        },
        error: function (xhr, status, error) {
            MSWAL('', error, 'error');
        }
    });
}
function MLoadTreeGrid(table_Id, obj_List) {
    //data = { "total": "" + data.length + "", "rows": data };
    $("#" + table_Id).treegrid('loadData', [obj_List]);
}
//function MAddModal(modal_Id) {
//    $('#' + modal_Id).modal('show');
//}
//---------------------------------------Win Tree (Single Page)----------------------------------------------------
function MWinTreeAddOrEdit(table_Id, picker_Id, return_fn) {
    var obj = $('#' + table_Id).treegrid('getSelected');
    if (obj == null || obj.id <= 0) {
        swal({ title: "", text: "Please select an item from list!", icon: "warning", button: true, dangerMode: true, })
        return false;
    }
    return_fn(obj);
    $('#' + picker_Id).window('open');
}
function MWinTreeDelete(table_Id, controller_Name, action_Name, return_fn) {
    var treeObj = $('#' + table_Id).treegrid('getSelected');
    var obj = { MModelPK: treeObj.id, id: treeObj.id, parentid: treeObj.parentid}
    if (obj == null || obj.id <= 0) {
        swal({ title: "", text: "Please select an item from list!", icon: "warning", button: true, dangerMode: true, })
        return false;
    }
    swal({ title: 'Warning', text: 'are you want to delete?', icon: 'warning', buttons: true, dangerMode: true, })
        .then((willDelete) => {
            if (willDelete) {
                if (obj.MModelPK > 0) {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "/" + controller_Name + "/" + action_Name,
                        traditional: true,
                        data: JSON.stringify(obj),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            var retObj = jQuery.parseJSON(data);
                            if (retObj.ErrorMessage == "Data Deleted Successfully !!") {
                                swal({ title: "", text: retObj.ErrorMessage, icon: "success", button: true, dangerMode: false, })
                                $('#' + table_Id).treegrid('deleteRow', obj.MModelPK);
                            }
                            else {
                                swal({ title: "", text: retObj.ErrorMessage, icon: "warning", button: true, dangerMode: true, })
                            }
                        },
                        error: function (xhr, status, error) {
                            swal({ title: "", text: error, icon: "warning", button: true, dangerMode: true, })
                        }
                    });
                }
            }
        });
}
//---------------------------------------Win Div (Single Page)----------------------------------------------------
//operation_Id = 1 for add, operation_Id = 2 for edit
function MWinPickerOpen(operation_Id, table_Id, picker_Id, return_fn) {
    if (operation_Id == 1) {
        $('#' + picker_Id).window('open');
        if (return_fn != '') {
            window[return_fn](null);
        }
    }
    else if (operation_Id == 2) {
        var obj = $('#' + table_Id).datagrid("getSelected");
        if (obj == null || obj.MModelPK <= 0) {
            swal({ title: "", text: "Please select an item from list!", icon: "warning", button: true, dangerMode: true, })
            return false;
        }
        if (return_fn != '') {
            window[return_fn](obj);
        }
    }
}
function MWinPickerSave(obj_pkid, obj, controller_Name, action_Name, table_Id, picker_Id, return_fn) {
    if (obj == null) {
        swal({ title: "", text: "Invalid Object !! Wrong Entry !!", icon: "warning", button: true, dangerMode: true, })
    };
    debugger;
    swal({ title: "Are you sure?", text: "once you save, it will change the database!", icon: "warning", buttons: true, dangerMode: true, })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: '/' + controller_Name + '/' + action_Name,
                    traditional: true,
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var retObj = jQuery.parseJSON(data);
                        if (retObj.MModelPK > 0) {
                            MSWAL("", 'Data Saved sucessfully !!', 'success');
                            debugger
                            if (obj_pkid == 0 || obj_pkid == '') {
                                $('#' + table_Id).datagrid('appendRow', retObj);
                                $('#' + table_Id).datagrid('selectRow', $('#' + table_Id).datagrid('getRows').length - 1);
                            }
                            else {
                                $('#' + table_Id).datagrid('updateRow', { index: $('#' + table_Id).datagrid('getRowIndex', $('#' + table_Id).datagrid("getSelected")), row: retObj });
                                $('#' + table_Id).datagrid('refreshRow', $('#' + table_Id).datagrid('getRowIndex', $('#' + table_Id).datagrid("getSelected")));
                            }
                            if (picker_Id != '') { $('#' + picker_Id).window('close'); }
                            if (return_fn != '') { window[return_fn](obj); }
                        }
                        else {
                            MSWAL("", retObj.ErrorMessage, 'error');
                        }
                    },
                    error: function (xhr, status, error) {
                        MSWAL("", error, 'info');
                    }
                });
            }
        });
}