$(document).ready(function () {
    Init();

    $('#btnAddProgram').click(function () {

        var KP_Kode = $('#ddlKategori').val();
        var BP_Kode = $('#ddlBidang').val();
        var Judul_Program = $('#txtJudul').val();
        var Area_Kode = $('#ddlArea').val();
        var WaktuMulai = $('#dateFrom').val();
        var WaktuSelesai = $('#dateTo').val();
        var Pelaksana = $('#txtPelaksana').val();
        var Penerima = $('#txtPenerima').val();
        var SumberDanaPGE = $('#txtSumberPGE').val();
        var SumberDanaPersero = $('#txtSumberPersero').val();
        var SumberPKBL = $('#txtSumberPKBL').val();        

        var validationMessage = "";

        if (KP_Kode.length < 1) {
            validationMessage += "Kategori Program harus di pilih. \n";
        }
        if (BP_Kode.length < 1) {
            validationMessage += "Bidang Program harus di pilih. \n";
        }
        if (Judul_Program.length < 1) {
            validationMessage += "Judul Program harus di isi. \n";
        }
        if (Area_Kode.length < 1) {
            validationMessage += "Area harus di pilih. \n";
        }
        if (WaktuMulai.length < 1) {
            validationMessage += "Waktu Mulai harus di isi. \n";
        }
        if (WaktuSelesai.length < 1) {
            validationMessage += "Waktu Selesai harus di isi. \n";
        }
        if (Pelaksana.length < 1) {
            validationMessage += "Pelaksana harus di isi. \n";
        }
        if (Penerima.length < 1) {
            validationMessage += "Penerima harus di isi. \n";
        }
        if (SumberDanaPGE.length < 1 && SumberDanaPersero.length < 1 && SumberPKBL.length < 1) {
            validationMessage += "Tidak ada sumber dana, Silahkan di isi minimal satu sumber dana. \n";
        }
        
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }
          
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
            InitializeRealisasi();
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function InitializeRealisasi() {
    var dateFormat = "dd-MMM-yyyy";
    if (($('#hfRealisasiNo').val() == undefined || $('#hfRealisasiNo').val() == null) && ($('#hfTransaksiNo').val() == undefined || $('#hfTransaksiNo').val() == null)) {
        $('#txtTransaksiNo').prop('disabled', true);
        return false;        
    }
    var parameter = new Object();
    parameter.realisasiNo = $('#hfRealisasiNo').val();
    parameter.transaksiNo = $('#hfTransaksiNo').val();
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadRealisasi",
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Input = JSON.parse(response.d);
            if (Input == null) return false;
            $('#txtTransaksiNo').val(Input.TransaksiNo).prop('disabled', true);
            $('#ddlKategori').val(Input.KP_Kode).prop('disabled', true);
            $('#ddlBidang').val(Input.BP_Kode).prop('disabled', true);
            $('#txtJudul').val(Input.Judul_Program).prop('disabled', true);
            $('#ddlArea').val(Input.Area_Kode).prop('disabled', true);
            $('#dateFrom').val(formatDate(dateFromJSON(Input.WaktuMulai), dateFormat));
            $('#dateTo').val(formatDate(dateFromJSON(Input.WaktuSelesai), dateFormat));
            $('#txtPelaksana').val(Input.Pelaksana);
            $('#txtPenerima').val(Input.Penerima);
            $('#txtSumberPGE').val(Input.SumberDanaPGE);
            $('#txtSumberPersero').val(Input.SumberDanaPersero);
            $('#txtSumberPKBL').val(Input.SumberPKBL);
            $('#txtKeterangan').val(Input.Keterangan);
            GetSuccessAddAttachList(Input.AttachmentList, false);
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
        url: window.location.pathname + "/SaveRealisasi",
        data: JSON.stringify(parameter),
        async: true,
        success: function (response) {
            var Input = response.d;
            alert(Input);
            window.location = "/Sharepointfree/SitePages/Home.aspx";
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function getRequestData() {
    var inputRealisasi = new Object();
    inputRealisasi.RealisasiNo = $('#hfRealisasiNo').val();
    inputRealisasi.TransaksiNo = $('#txtTransaksiNo').val();
    inputRealisasi.WaktuMulai = $('#dateFrom').val();
    inputRealisasi.WaktuSelesai = $('#dateTo').val();
    inputRealisasi.Pelaksana = $('#txtPelaksana').val();
    inputRealisasi.Penerima = $('#txtPenerima').val();
    inputRealisasi.SumberDanaPGE = parseFloat($('#txtSumberPGE').val().replace(/[^0-9\.]+/g, ""));
    inputRealisasi.SumberDanaPersero = parseFloat($('#txtSumberPersero').val().replace(/[^0-9\.]+/g, ""));
    inputRealisasi.SumberPKBL = parseFloat($('#txtSumberPKBL').val().replace(/[^0-9\.]+/g, ""));
    inputRealisasi.Keterangan = $('#txtKeterangan').val();
    if (inputRealisasi.TransaksiNo == null || inputRealisasi.TransaksiNo == "") {
        inputRealisasi.KP_Kode = $('#ddlKategori').val();
        inputRealisasi.BP_Kode = $('#ddlBidang').val();
        inputRealisasi.Judul_Program = $('#txtJudul').val();
        inputRealisasi.Area_Kode = $('#ddlArea').val();
    }
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
