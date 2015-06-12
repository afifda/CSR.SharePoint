var isDataTableCreated = false;
$(document).ready(function () {
    LoadAvailableYear();
    $('#btnConfirm').click(function () {
        Confirm();
    });
    $('#btnGenerateTable').click(function () {
        LoadAvailableYear();
    });
});

function LoadAvailableYear() {
    var handlerUrl = "/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=getAvailableYear";
    $.ajax({
        type: "POST",
        url: handlerUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "false",
        success: function (response) {
            var result = JSON.parse(response);
            $.each(result, function (key, value) {
                $("#ddlYear").append($("<option></option>").val(result).html(result));
            });
            LoadProgramList();
            },
        error: function (response) {            
            alert(response.responseText);
        }
    });
    return true;
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
    if ($('#hfIsAdmin').val() == "1") {
        $('#btnUnlock').show();
        $('#btnUnlock').click(function () {
            Unlock();
        });
    }
    var yearSelected = $('#ddlYear').val();
    $('.appr').prop('disabled', $.find(':input[class=chk][type=checkbox]:checked').length == 0);
    var handlerUrl = "/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=loadProgramList&Year=" + yearSelected;
    $.ajax({
        type: "POST",
        url: handlerUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var result = JSON.parse(response);
            GetSuccessProgramList(result);           
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function GetSuccessProgramList(data) {
    var dateFormat = "dd-MMM-yyyy";
    if (data.length > 0) {
        $('#tblProgramList tbody').remove();
        var total = 0;
        for (i = 0; i < data.length; i++) {
            var strStatus = data[i].Is_Locked ? "Telah Dikunci" : "Belum Dikunci";
            var strhtml = '<tr>' +
                '<td ><input type="checkbox" class="chk" data-transno="' + data[i].TransaksiNo + '"></input></td>' +
                '<td ><a href="/Sharepointfree/_layouts/15/CSR.SharePointApplication/InputProgramPage.aspx?TransaksiNo=' + data[i].TransaksiNo + '">' + data[i].TransaksiNo + '</a> ' + ' </td>' +
                '<td >' + data[i].BP_Nama + ' </td>' +
                '<td >' + data[i].Judul_Program + ' </td>' +
                '<td >' + data[i].KP_Nama + ' </td>' +
                '<td >' + data[i].Area_Nama + ' </td>' +
                '<td >' + data[i].Keterangan + ' </td>' +
                '<td class="currencyFormat rightAligned">' + data[i].Jumlah_Anggaran + ' </td>' +
                '<td >' + strStatus + ' </td>' +
                '</tr>';
            $(strhtml).appendTo($("#tblProgramList"));
        }
        $('.currencyFormat').formatCurrency({
            symbol: 'IDR '
        });
        $('.rightAligned').css('text-align', 'right');
        if (!isDataTableCreated) {
            CreateDataTable();
        }
        //$('.rightAligned').css('text-align', 'right');
    }

    else {
        alert('System cannot query your keyword...');
    }
}

function CreateDataTable() {
    var table = $('#tblProgramList').DataTable({
        "columnDefs": [
            { "visible": false, "targets": 2 }
        ],
        "order": [[1, 'asc']],
        "displayLength": 25,
        "drawCallback": function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(2, { page: 'current' }).data().each(function (group, i) {
                if (last !== group) {
                    $(rows).eq(i).before(
                        '<tr class="group"><td colspan="9">' + group + '</td></tr>'
                    );

                    last = group;
                }
            });

            $('.chk').change(function () {
                $('#checkAll').prop('checked', $.find(':input[class=chk][type=checkbox]').length == $.find(':input[class=chk][type=checkbox]:checked').length)
                $('.appr').prop('disabled', $.find(':input[class=chk][type=checkbox]:checked').length == 0);
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

    $('#checkAll').change(function () {
        $('.chk').prop('checked', $('#checkAll').prop('checked'));
        $('.appr').prop('disabled', $.find(':input[class=chk][type=checkbox]:checked').length == 0);
    });
    $('#checkAll').prop('checked', false);
    isDataTableCreated = true;
}

function Confirm() {
    var yearSelected = $('#ddlYear').val();
    var transNo = "";
    $('.chk:checked').each(function () {
        transNo += $(this).attr('data-transno') + "|";
    });
    transNo = transNo.substring(0, transNo.lastIndexOf('|'));
    
    var handlerUrl = "/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=ConfirmProgramList&Year=" + yearSelected + "&TransaksiNo=" + transNo;
    $.ajax({
        type: "POST",
        url: handlerUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var itemCheked = $('.chk:checked').length;
            if (itemCheked == 0) {
                alert("Please select any record to Execute Action");
                return false;
            }
            alert("Rencana Program sudah terkunci");
            window.location.reload();
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    
}

function Unlock() {
    var yearSelected = $('#ddlYear').val();
    var transNo = "";
    $('.chk:checked').each(function () {
        transNo += $(this).attr('data-transno') + "|";
    });
    transNo = transNo.substring(0, transNo.lastIndexOf('|'));

    var handlerUrl = "/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=UnlockProgramList&Year=" + yearSelected + "&TransaksiNo=" + transNo;
    $.ajax({
        type: "POST",
        url: handlerUrl,
        data: {},
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var itemCheked = $('.chk:checked').length;
            if (itemCheked == 0) {
                alert("Please select any record to Execute Action");
                return false;
            }
            alert("Rencana Program sudah dibuka");
            window.location.reload();
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}