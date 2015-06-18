$(document).ready(function () {
    Init();

    $('#btnAddMasterBidang').click(function () {
        clearModalMasterBidang();
        $('#hfEditMode').val('0');
        $('#modalMasterBidang').modal('show');        
    });

    $('#btnSaveMasterBidang').click(function () {
        var BP_Nama = $("#txtNamaBidang").val();
        var validationMessage = "";
        if (BP_Nama.length < 1) {
            validationMessage += "Nama Bidang harus di isi. \n";
        }       
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }
        saveMasterBidang();
    });

    $('#tblMasterBidang').on("click", ".btnEdit", editBidang);
    $('#tblMasterBidang').on("click", ".btnDelete", deleteBidang);
});

function clearModalMasterBidang() {    
    $("#txtNamaBidang").val("");
}

function Init() {    
    $('#tblMasterBidang tbody').remove();
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadMasterBidang",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Bidang = response.d;
            if (Bidang.length > 0) {
                for (i = 0; i < Bidang.length; i++) {
                    var seq = i + 1;
                    var strhtml = '<tr id="BidangRow"' + seq + '>' +
                        '<td style = "display:none">' + Bidang[i].BP_Kode + ' </td>' +
                        '<td >' + Bidang[i].KP_Nama + ' </td>' +
                        '<td >' + Bidang[i].BP_Nama + ' </td>' +
                         '<td style = "display:none">' + Bidang[i].KP_Kode + ' </td>' +
                        '<td align="Center"><input type="button"  class="button2 btnEdit" value="Ubah"/><input type="button"  class="button2 btnDelete" value="Hapus"/> </td> ' +
                        '</tr>';
                    $(strhtml).appendTo($("#tblMasterBidang"));

                }
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

    $("#ddlKategori").empty();
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadMasterKategori",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var kategori = response.d;
            if (kategori.length > 0) {
                $.each(kategori, function (key, value) {
                    $("#ddlKategori").append($("<option></option>").val
                 (value.KP_Kode).html(value.KP_Nama.trim()));
                });
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function editBidang() {
    var $element = this;
    var row = $($element).parents("tr:first");

    clearModalMasterBidang();
    $("#hfEditMode").val("1");
    $("#modalMasterBidang").modal("show");
    $("#hfKodeBidang").val(row.children()[0].innerText);
    $("#ddlKategori").val(row.children()[3].innerText.trim());
    //$("#ddlKategori").val("CSR");
    $("#txtNamaBidang").val(row.children()[2].innerText);
    
}

function deleteBidang() {
    var $element = this;
    var row = $($element).parents("tr:first");

    var KodeBidang = row.children()[0].innerText;
    var parameter = {
        kodeBidang: KodeBidang
    };
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/DeleteMasterBidang",
        async: false,
        cache: false,
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var Bidang = response.d;
            if (Bidang == "Success") {
                $("#modalMasterBidang").modal("hide");
                alert("Master Bidang telah dihapus.")
                Init();
            }
        },
        error: function (response) {
            alert(response.responseText);
            $("#modalMasterBidang").modal("hide");
        }
    });
}

function saveMasterBidang() {
    var EditMethod = true;
    var editMode = $("#hfEditMode").val();
    if (editMode == 0) EditMethod = false;
    var masterBidang = new Object();
    masterBidang.BP_Kode = $("#hfKodeBidang").val();
    masterBidang.BP_Nama = $("#txtNamaBidang").val();
    masterBidang.KP_Kode = $("#ddlKategori").val();
    var parameter = new Object();
    parameter.masterBidangString = JSON.stringify(masterBidang);
    parameter.isEdit = EditMethod;
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/SaveMasterBidang",
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Bidang = response.d;
            $("#modalMasterBidang").modal("hide");
            alert(Bidang)
            Init();
        },
        error: function (response) {
            alert(response.responseText);
            $("#modalMasterBidang").modal("hide");
        }
    });
}
