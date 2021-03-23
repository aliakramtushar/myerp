function MExcel(obj_List, file_Name) {
    if (obj_List.length == 0) {
        MSWAL('Empty', 'No Data Found !!', 'warning');
        return;
    }
    $("#MExcel").excelexportjs({
        containerid: "MExcel"
        , datatype: 'json'
        , dataset: obj_List
        , columns: getColumns(obj_List)
        , worksheetName: file_Name
    });
    //dvjson = MExcel
}
function MExcelCustom(obj_List, col_List, file_Name) {
    if (obj_List.length == 0) {
        MSWAL('Empty', 'No Data Found !!', 'warning');
        return;
    }
    $("#divMExcel").excelexportjs({
        containerid: "divMExcel",
        datatype: 'json',
        dataset: obj_List,
        columns: col_List,
        worksheetName: file_Name
    });
    //dvjson = divMExcel
}