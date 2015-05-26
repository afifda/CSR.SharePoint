﻿$(document).ready(function () {
    Init();

    $('#btnAddProgram').click(function () {
        submit();
    });

    $('#txtJumlahAnggaran').blur(function () {
        $('#txtJumlahAnggaran').formatCurrency({
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
    var inputProgram = new Object();
    inputProgram.TransaksiNo = $('#hfTransaksiNo').val() == null ? "" : $('#hfTransaksiNo').val();
    inputProgram.Judul_Program = $('#txtJudul').val();
    inputProgram.KP_Kode = $('#ddlKategori').val();
    inputProgram.BP_Kode = $('#ddlBidang').val();
    inputProgram.Area_Kode = $('#ddlArea').val();
    inputProgram.Jumlah_Anggaran = parseFloat($('#txtJumlahAnggaran').val().replace(/[^0-9\.]+/g, ""));
    inputProgram.Outcome_Diharapkan = $('#txtOutcome').val();
    inputProgram.Waktu_Mulai = $('#dateFrom').val();
    inputProgram.Waktu_Sampai = $('#dateTo').val();
    inputProgram.Keterangan = $('#txtKeterangan').val();
    inputProgram.AttachmentList = saveAttachment();
    return inputProgram;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
