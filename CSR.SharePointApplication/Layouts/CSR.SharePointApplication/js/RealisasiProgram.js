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
        var SumberDanaPGEPusat = $('#txtSumberPGEPusat').val();
        var SumberDanaPGEArea = $('#txtSumberPGEArea').val();
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
        if (SumberDanaPGEPusat.length < 1 && SumberDanaPersero.length < 1 && SumberPKBL.length < 1 && SumberDanaPGEArea.length < 1) {
            validationMessage += "Tidak ada sumber dana, Silahkan di isi minimal satu sumber dana. \n";
        }
        
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }
          
        submit();
    });
    //$('#btnAddProgramAndLock').click(function () {
    //    var KP_Kode = $('#ddlKategori').val();
    //    var BP_Kode = $('#ddlBidang').val();
    //    var Judul_Program = $('#txtJudul').val();
    //    var Area_Kode = $('#ddlArea').val();
    //    var WaktuMulai = $('#dateFrom').val();
    //    var WaktuSelesai = $('#dateTo').val();
    //    var Pelaksana = $('#txtPelaksana').val();
    //    var Penerima = $('#txtPenerima').val();
    //    var SumberDanaPGEPusat = $('#txtSumberPGEPusat').val();
    //    var SumberDanaPGEArea = $('#txtSumberPGEArea').val();
    //    var SumberDanaPersero = $('#txtSumberPersero').val();
    //    var SumberPKBL = $('#txtSumberPKBL').val();

    //    var validationMessage = "";

    //    if (KP_Kode.length < 1) {
    //        validationMessage += "Kategori Program harus di pilih. \n";
    //    }
    //    if (BP_Kode.length < 1) {
    //        validationMessage += "Bidang Program harus di pilih. \n";
    //    }
    //    if (Judul_Program.length < 1) {
    //        validationMessage += "Judul Program harus di isi. \n";
    //    }
    //    if (Area_Kode.length < 1) {
    //        validationMessage += "Area harus di pilih. \n";
    //    }
    //    if (WaktuMulai.length < 1) {
    //        validationMessage += "Waktu Mulai harus di isi. \n";
    //    }
    //    if (WaktuSelesai.length < 1) {
    //        validationMessage += "Waktu Selesai harus di isi. \n";
    //    }
    //    if (Pelaksana.length < 1) {
    //        validationMessage += "Pelaksana harus di isi. \n";
    //    }
    //    if (Penerima.length < 1) {
    //        validationMessage += "Penerima harus di isi. \n";
    //    }
    //    if (SumberDanaPGEPusat.length < 1 && SumberDanaPersero.length < 1 && SumberPKBL.length < 1 && SumberDanaPGEArea.length < 1) {
    //        validationMessage += "Tidak ada sumber dana, Silahkan di isi minimal satu sumber dana. \n";
    //    }

    //    if (validationMessage.length > 0) {
    //        alert(validationMessage);
    //        return false;
    //    }

    //    submitAndLock();
    //});
    $('#btnBatal').click(function () {
        window.location = "/sites/HumasCSR/SitePages/Home.aspx";
    });
        
    $('#txtSumberPGEPusat').blur(function () {
        $('#txtSumberPGEPusat').formatCurrency({
            symbol: ''
        });
    });
    
    $('#txtSumberPGEArea').blur(function () {
        $('#txtSumberPGEArea').formatCurrency({
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
    if (($('#hfRealisasiNo').val() == undefined || $('#hfRealisasiNo').val() == null || $('#hfRealisasiNo').val() == "") && ($('#hfTransaksiNo').val() == undefined || $('#hfTransaksiNo').val() == null || $('#hfTransaksiNo').val() == "")) {
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
            $('#txtSumberPGEPusat').val(Input.SumberDanaPGEPusat);
            $('#txtSumberPGEArea').val(Input.SumberDanaPGEArea);
            $('#txtSumberPersero').val(Input.SumberDanaPersero);
            $('#txtSumberPKBL').val(Input.SumberPKBL);
            $('#txtKeterangan').val(Input.Keterangan);
            $('.currencyFormat').formatCurrency({
                symbol: ''
            });            
            if (Input.Is_Locked_Realisasi == true) {
                $('#btnAddProgram').prop('disabled', true);
                //$('#btnAddProgramAndLock').prop('disabled', true);
                GetSuccessAddAttachList(Input.AttachmentList, false);
            }
            else {
                $('#btnAddProgram').prop('disabled', false);
                //$('#btnAddProgramAndLock').prop('disabled', false);
                GetSuccessAddAttachList(Input.AttachmentList, true);
            }

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
            window.location = "/sites/HumasCSR/SitePages/Home.aspx";
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function submitAndLock() {
    var parameter = new Object();
    parameter.programString = JSON.stringify(getRequestData());
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        url: window.location.pathname + "/SaveAndLockRealisasi",
        data: JSON.stringify(parameter),
        async: true,
        success: function (response) {
            var Input = response.d;
            alert(Input);
            window.location = "/sites/HumasCSR/SitePages/Home.aspx";
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
    
    if ($('#txtSumberPGEPusat').val() == "") {
        inputRealisasi.SumberDanaPGEPusat = 0
    }
    else {
        inputRealisasi.SumberDanaPGEPusat = parseFloat($('#txtSumberPGEPusat').val().replace(/[^0-9\.]+/g, ""));
    }

    if ($('#txtSumberPGEArea').val() == "") {
        inputRealisasi.SumberDanaPGEArea = 0
    }
    else {
        inputRealisasi.SumberDanaPGEArea = parseFloat($('#txtSumberPGEArea').val().replace(/[^0-9\.]+/g, ""));
    }

    if ($('#txtSumberPersero').val() == "") {
        inputRealisasi.SumberDanaPersero = 0
    }
    else {
        inputRealisasi.SumberDanaPersero = parseFloat($('#txtSumberPersero').val().replace(/[^0-9\.]+/g, ""));
    }
    if ($('#txtSumberPKBL').val() == "") {
        inputRealisasi.SumberPKBL = 0
    }
    else {
        inputRealisasi.SumberPKBL = parseFloat($('#txtSumberPKBL').val().replace(/[^0-9\.]+/g, ""));
    }
       
    inputRealisasi.Keterangan = $('#txtKeterangan').val();
    if (inputRealisasi.TransaksiNo == null || inputRealisasi.TransaksiNo == "") {
        inputRealisasi.KP_Kode = $('#ddlKategori').val();
        inputRealisasi.BP_Kode = $('#ddlBidang').val();
        inputRealisasi.Judul_Program = $('#txtJudul').val();
        inputRealisasi.Area_Kode = $('#ddlArea').val();
    }
    inputRealisasi.AttachmentList = saveAttachment("R");

    return inputRealisasi;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
