$(document).ready(function () {
    Init();

    $('#btnExecute').click(function () {
        isValid();
        var WaktuFrom = $('#dateFrom').val();
        var WaktuTo = $('#dateTo').val();
        var Kategori = $('#ddlKategori').val();
        var Bidang = $('#ddlBidang').val();
        var Area = $('#ddlArea').val();
        window.open("/_layouts/15/CSR.SharePointApplication/Report.aspx?ReportType=DetailReport&WaktuFrom=" + WaktuFrom + "&WaktuTo=" + WaktuTo + "&Area=" + Area + "&Kategori=" + Kategori + "&Bidang=" + Bidang,
                        "_blank", "width=800,height=600,resizable=yes,scrollbars=yes,directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0");
        return false;
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

function proses() {
    //var parameter = new Object();
    //parameter.programString = JSON.stringify(getRequestData());
    //$.ajax({
    //    type: "POST",
    //    contentType: "application/json; charset=utf-8",
    //    datatype: "json",
    //    url: window.location.pathname + "/SaveProgram",
    //    data: JSON.stringify(parameter),
    //    async: true,
    //    success: function (response) {
    //        var Input = response.d;
    //        alert(Input);
    //    },
    //    error: function (response) {
    //        alert(response.responseText);
    //    }
    //});
    alert("OK");
}

function view() {
    //var parameter = new Object();
    //parameter.programString = JSON.stringify(getRequestData());
    //$.ajax({
    //    type: "POST",
    //    contentType: "application/json; charset=utf-8",
    //    datatype: "json",
    //    url: window.location.pathname + "/SaveProgram",
    //    data: JSON.stringify(parameter),
    //    async: true,
    //    success: function (response) {
    //        var Input = response.d;
    //        alert(Input);
    //    },
    //    error: function (response) {
    //        alert(response.responseText);
    //    }
    //});
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

function isValid() {
    var validationMessage = "";
    var startdate = $('#dateFrom').val();
    var enddate = $('#dateTo').val();

    if (startdate.length < 1) {
        validationMessage += "Silahakan isi waktu mulai. \n";
    }

    if (enddate.length < 1) {
        validationMessage += "Silahakan isi waktu Selesai. \n";
    }


    if (validationMessage.length > 0) {
        alert(validationMessage);
        return false;
    } else {
        if (startdate > enddate) {
            alert("Tanggal Mulai tidak boleh lebih besar dari Tanggal Selesai");
            return false;
        }
        else {
            proses();
        }

    }
}