$(document).ready(function () {
    Init();

    $('#btnAddMasterKategori').click(function () {
        clearModalMasterKategori();
        $('#hfEditMode').val('0');
        $('#modalMasterKategori').modal('show');        
    });

    $('#btnSaveMasterKategori').click(function () {
        var KP_Nama = $("#txtNamaKategori").val();
        var validationMessage = "";
        if (KP_Nama.length < 1) {
            validationMessage += "Nama Kategori harus di isi. \n";
        }
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }
        saveKategori();
    });

    $('#tblMasterKategori').on("click", ".btnEdit", editKategori);
    $('#tblMasterKategori').on("click", ".btnDelete", deleteKategori);
});

function clearModalMasterKategori() {    
    $("#txtNamaKategori").val("");
}

function Init() {
    $('#tblMasterKategori tbody').remove();
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadMasterKategori",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Kategori = response.d;
            if (Kategori.length > 0) {
                for (i = 0; i < Kategori.length; i++) {
                    var seq = i + 1;
                    var strhtml = '<tr id="KategoriRow_"' + seq + '>' +
                        '<td style = "display:none">' + Kategori[i].KP_Kode + ' </td>' +
                        '<td >' + Kategori[i].KP_Nama + ' </td>' +                        
                        '<td align="Center"><input type="button"  class="button2 btnEdit" value="Ubah"/><input type="button"  class="button2 btnDelete" value="Hapus"/> </td> ' +
                        '</tr>';
                    $(strhtml).appendTo($("#tblMasterKategori"));

                }
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function editKategori() {
    var $element = this;
    var row = $($element).parents("tr:first");

    clearModalMasterKategori();
    $("#hfEditMode").val("1");
    $("#modalMasterKategori").modal("show");
    $("#hfKodeKategori").val(row.children()[0].innerText)
    $("#txtNamaKategori").val(row.children()[1].innerText)
}

function deleteKategori() {
    var $element = this;
    var row = $($element).parents("tr:first");
    $("#dialog-confirm").html("Apakah anda yakin menghapus kategori ini?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Warning",
        height: 150,
        width: 350,
        buttons: {
            "Ya": function () {
                
                var KodeKategori = row.children()[0].innerText;
                var parameter = {
                    kodeKategori: KodeKategori
                };
                $.ajax({
                    type: "POST",
                    url: window.location.pathname + "/DeleteMasterKategori",
                    async: false,
                    cache: false,
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    async: false,
                    success: function (response) {
                        var Kategori = response.d;
                        if (Kategori == "Success") {
                            $("#modalMasterKategori").modal("hide");
                            alert("Master Kategori telah dihapus.")
                            Init();
                        }
                    },
                    error: function (response) {
                        alert(response.responseText);
                        $("#modalMasterKategori").modal("hide");
                    }
                });
                $(this).dialog('close');
            },
            "Tidak": function () {
                $(this).dialog('close');
            }
        }
    });    
}

function saveKategori() {
    var EditMethod = true;
    var editMode = $("#hfEditMode").val();
    if (editMode == 0) EditMethod = false;
    var masterKategori = new Object();
    masterKategori.KP_Kode = $("#hfKodeKategori").val();
    masterKategori.KP_Nama = $("#txtNamaKategori").val();
    var parameter = new Object();
    parameter.masterKategoriString = JSON.stringify(masterKategori);
    parameter.isEdit = EditMethod;
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/SaveMasterKategori",
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Kategori = response.d;
            $("#modalMasterKategori").modal("hide");
            alert(Kategori)
            Init();
        },
        error: function (response) {
            alert(response.responseText);
            $("#modalMasterKategori").modal("hide");
        }
    });
}

