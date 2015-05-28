$(document).ready(function () {
    Init();

    $('#btnAddProgram').click(function () {
        submit();
    });
  
    $('#txtSumberPGE').blur(function () {
        $('#txtSumberPGE').formatCurrency({
            symbol: ''
        });
    });
    $('#txtSumberPersero').blur(function () {
        $('#txtSumberPersero').formatCurrency({
            symbol: ''
        });
    });
    $('#txtSumberPKBL').blur(function () {
        $('#txtSumberPKBL').formatCurrency({
            symbol: ''
        });
    });

    $('#dateFrom').datepicker();
    $('#dateTo').datepicker();
});

function Init() {
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadInputPage",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Input = JSON.parse(response.d);
            if (Input == null) return false;
            $.each(Input.Kategori, function (key, value) {
                $("#ddlKategori").append($("<option></option>").val
             (value.KP_Kode).html(value.KP_Nama));
            });
            $.each(Input.Bidang, function (key, value) {
                $("#ddlBidang").append($("<option></option>").val
             (value.BP_Kode).html(value.BP_Nama));
            });
            $.each(Input.Area, function (key, value) {
                $("#ddlArea").append($("<option></option>").val
             (value.AreaCode).html(value.AreaName));
            });
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function submit() {
    var parameter = new Object();
    parameter.programString = JSON.stringify(getRequestData());
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        url: window.location.pathname + "/SaveProgram",
        data: JSON.stringify(parameter),
        async: true,
        success: function (response) {
            var Input = response.d;
            alert(Input);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function getRequestData() {
    var inputRealisasi = new Object();
    inputRealisasi.TransaksiNo = $('#txtTransaksiNo').val();
    inputRealisasi.WaktuMulai = $('#dateFrom').val();
    inputRealisasi.WaktuSelesai = $('#dateTo').val();
    inputRealisasi.Pelaksana = $('#txtPelaksana').val();
    inputRealisasi.Penerima = $('#txtPenerima').val();
    inputRealisasi.SumberDanaPGE = parseFloat($('#txtSumberPGE').val().replace(/[^0-9\.]+/g, ""));
    inputRealisasi.SumberDanaPersero = parseFloat($('#txtSumberPersero').val().replace(/[^0-9\.]+/g, ""));
    inputRealisasi.SumberPKBL = parseFloat($('#txtSumberPKBL').val().replace(/[^0-9\.]+/g, ""));
    inputRealisasi.Keterangan = $('#txtKeterangan').val();
    inputRealisasi.AttachmentList = saveAttachment();
    return inputRealisasi;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
