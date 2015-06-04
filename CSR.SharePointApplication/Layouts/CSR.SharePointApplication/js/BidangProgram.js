$(document).ready(function () {
    Init();

    $('#btnAddProgram').click(function () {
        var Judul_Program = $('#txtJudul').val();
        var KP_Kode = $('#ddlKategori').val();
        var BP_Kode = $('#ddlBidang').val();
        var Area_Kode = $('#ddlArea').val();
        var Jumlah_Anggaran = $('#txtJumlahAnggaran').val();
        var Waktu_Mulai = $('#dateFrom').val();
        var Waktu_Sampai = $('#dateTo').val();
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
        if (Jumlah_Anggaran.length < 1) {
            validationMessage += "Jumlah Anggaran harus di isi. \n";
        }
        if (Waktu_Mulai.length < 1) {
            validationMessage += "Waktu Mulai harus di isi. \n";
        }
        if (Waktu_Sampai.length < 1) {
            validationMessage += "Waktu Selesai harus di isi. \n";
        }       
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }

        submit();
    });

    $('#btnAddMasterBidang').click(function () {
        var transaksiNo = $('#hfTransaksiNo').val()
        window.location = "InputRealisasiPage.aspx?TransaksiNo=" + transaksiNo;
        return false;
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
    $('.ToggleDiv').hide();
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
            InitializeProgram();
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function InitializeProgram() {
    var dateFormat = "dd-MMM-yyyy";
    if ($('#hfTransaksiNo').val() != undefined && $('#hfTransaksiNo').val() != null && $('#hfTransaksiNo').val().length > 0) {
        $('.ToggleDiv').show();
        var parameter = {
            transaksiNo: $('#hfTransaksiNo').val()
        };
        $.ajax({
            type: "POST",
            url: window.location.pathname + "/LoadProgram",
            data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: true,
            success: function (response) {
                var Input = JSON.parse(response.d);
                if (Input == null) return false;
                $('#ddlKategori').val(Input.KP_Kode);
                $('#ddlBidang').val(Input.BP_Kode);
                $('#txtJudul').val(Input.Judul_Program);
                $('#ddlArea').val(Input.Area_Kode);
                $('#txtJumlahAnggaran').val(Input.Jumlah_Anggaran);
                $('#txtOutcome').val(Input.Outcome_Diharapkan);
                $('#dateFrom').val(formatDate(dateFromJSON(Input.Waktu_Mulai), dateFormat));
                $('#dateTo').val(formatDate(dateFromJSON(Input.Waktu_Sampai), dateFormat));
                $('#txtKeterangan').val(Input.Keterangan);
                GetSuccessDetailsList(Input.RealisasiList);
                GetSuccessAddAttachList(Input.AttachmentList, false);
                
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}

function GetSuccessDetailsList(RealisasiList) {
    var dateFormat = "dd-MMM-yyyy";
    if (RealisasiList.length > 0) {
        var total = 0;
        for (i = 0; i < RealisasiList.length; i++) {
            if (RealisasiList[i].RealisasiNo != null) {
                var Jumlah = RealisasiList[i].SumberDanaPGE + RealisasiList[i].SumberDanaPersero + RealisasiList[i].SumberPKBL;
                var strhtml = '<tr>' +
                    '<td >' + RealisasiList[i].RealisasiNo + ' </td>' +
                    '<td >' + formatDate(dateFromJSON(RealisasiList[i].WaktuMulai), dateFormat) + ' </td>' +
                    '<td >' + formatDate(dateFromJSON(RealisasiList[i].WaktuSelesai), dateFormat) + ' </td>' +
                    '<td >' + RealisasiList[i].Pelaksana + ' </td>' +
                    '<td >' + RealisasiList[i].Penerima + ' </td>' +
                    '<td class="currencyFormat rightAligned">' + Jumlah + '</td>' +
                    '<td ><a href="InputRealisasiPage.aspx?RealisasiNo=' + RealisasiList[i].RealisasiNo + '">lihat</a>' + '</td>' +
                    '</tr>';
                $(strhtml).appendTo($("#tblRealisasi"));
            }
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
            window.location = "/Sharepointfree/SitePages/Home.aspx";
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
