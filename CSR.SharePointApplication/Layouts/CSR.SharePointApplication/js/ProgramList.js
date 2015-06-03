$(document).ready(function () {
    LoadAvailableYear();
});

function LoadAvailableYear() {
    var handlerUrl = "/SharePointFree/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=getAvailableYear";
    $.ajax({
        type: "POST",
        url: handlerUrl,
        data: {},
        contentType: "application/json",
        datatype: "json",
        async: true,
        success: SuccessAddYear,
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function SuccessAddYear(data, status, xhr) {
    var result = data;
    $.each(result, function (key, value) {
        $("#ddlYear").append($("<option></option>").val
     (result).html(result));
    });
    LoadProgramList();
}


function LoadProgramList() {
    var yearSelected = $('#ddlYear').val();
    var handlerUrl = "/SharePointFree/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=loadProgramList&Year=" + yearSelected;
    $.ajax({
        type: "POST",
        url: handlerUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: GetSuccessProgramList,
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function GetSuccessProgramList(data, status, xhr) {
    var dateFormat = "dd-MMM-yyyy";
    if (data.length > 0) {
        var total = 0;
        for (i = 0; i < data.length; i++) {
            var strhtml = '<tr>' +
                '<td >' + data[i].TransaksiNo + ' </td>' +
                '<td >' + data[i].BP_Name + ' </td>' +
                '<td >' + data[i].Judul_Program + ' </td>' +
                '<td >' + data[i].KP_Name + ' </td>' +
                '<td >' + data[i].AreaName + ' </td>' +
                '<td >' + data[i].Keterangan + ' </td>' +
                '<td >' + data[i].Jumlah_Anggaran + ' </td>' +
                '</tr>';
            $(strhtml).appendTo($("#tblProgramList"));
        }
        $('.currencyFormat').formatCurrency({
            symbol: ''
        });
        $('.rightAligned').css('text-align', 'right');
    }

    else {
        alert('System cannot query your keyword...');
    }
}

function CreateDataTable() {
    var table = $('#tblProgramList').DataTable({
        "columnDefs": [
            { "visible": false, "targets": 0 }
        ],
        "order": [[1, 'asc']],
        "displayLength": 25,
        "drawCallback": function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(0, { page: 'current' }).data().each(function (group, i) {
                if (last !== group) {
                    $(rows).eq(i).before(
                        '<tr class="group"><td colspan="5">' + group + '</td></tr>'
                    );

                    last = group;
                }
            });
        }
    });

    // Order by the grouping
    $('#tblProgramList tbody').on('click', 'tr.group', function () {
        var currentOrder = table.order()[0];
        if (currentOrder[0] === 1 && currentOrder[1] === 'asc') {
            table.order([1, 'desc']).draw();
        }
        else {
            table.order([1, 'asc']).draw();
        }
    });
}