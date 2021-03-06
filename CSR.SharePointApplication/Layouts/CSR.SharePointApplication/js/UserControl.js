﻿$(document).ready(function () {
    Init();

    $('#btnAddMasterUser').click(function () {
        clearModalMasterUser();
        $('#hfEditMode').val('0');
        $('#modalMasterUser').modal('show');
        $("#txtNoPegawai").prop("disabled", false);
    });

    $('#btnSaveMasterUser').click(function () {
        var No_Pegawai = $("#txtNoPegawai").val();
        var Nama_Pegawai = $("#txtNamaPegawai").val();
        var UserName = $("#txtUserName").val();
        var AreaCode = $("#ddlArea").val();
        var validationMessage = "";
        if (No_Pegawai.length < 1) {
            validationMessage += "No Kategori harus di isi. \n";
        }
        if (Nama_Pegawai.length < 1) {
            validationMessage += "Nama Pegawai harus di isi. \n";
        }
        if (UserName.length < 1) {
            validationMessage += "User Name harus di isi. \n";
        }
        if (AreaCode.length < 1) {
            validationMessage += "Kode Area harus di pilih. \n";
        }
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }
        saveUser();
    });

    $('#tblMasterUser').on("click", ".btnEdit", editUser);
    $('#tblMasterUser').on("click", ".btnDelete", deleteUser);
});

function clearModalMasterUser() {
    $("#txtNoPegawai").val("");
    $("#txtNamaPegawai").val("");
    $("#txtUserName").val("");
    $("#txtKodeArea").val("");
}

function Init() {
    $('#tblMasterUser tbody').remove();
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadMasterUser",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var User = response.d;
            if (User.length > 0) {
                for (i = 0; i < User.length; i++) {
                    var seq = i + 1;
                    var strhtml = '<tr id="UserRow_"' + seq + '>' +
                        '<td >' + User[i].No_Pegawai + ' </td>' +
                        '<td >' + User[i].Nama_Pegawai + ' </td>' +
                        '<td >' + User[i].UserName + ' </td>' +
                        '<td >' + User[i].AreaName + ' </td>' +
                        '<td style = "display:none">' + User[i].AreaCode + ' </td>' +
                        '<td align="Center"><input type="button"  class="button2 btnEdit" value="Ubah"/><input type="button"  class="button2 btnDelete" value="Hapus"/> </td> ' +
                        '</tr>';
                    $(strhtml).appendTo($("#tblMasterUser"));

                }
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    $("#ddlArea").empty();
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadMasterArea",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Area = response.d;
            if (Area.length > 0) {
                $.each(Area, function (key, value) {
                    $("#ddlArea").append($("<option></option>").val
                 (value.AreaCode).html(value.AreaName));
                });
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function editUser() {
    var $element = this;
    var row = $($element).parents("tr:first");

    clearModalMasterUser();
    $("#hfEditMode").val("1");
    $("#modalMasterUser").modal("show");
    $("#txtNoPegawai").val(row.children()[0].innerText).prop("disabled", true);
    $("#txtNamaPegawai").val(row.children()[1].innerText)
    $("#txtUserName").val(row.children()[2].innerText)
    $("#ddlArea").val(row.children()[4].innerText.trim())

}

function deleteUser() {
    var $element = this;
    var row = $($element).parents("tr:first");
    $("#dialog-confirm").html("Apakah anda yakin menghapus user ini?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Warning",
        height: 150,
        width: 350,
        buttons: {
            "Ya": function () {
                
                var No_Pegawai = row.children()[0].innerText;
                var parameter = {
                    NoPegawai: No_Pegawai
                };
                $.ajax({
                    type: "POST",
                    url: window.location.pathname + "/DeleteMasterUser",
                    async: false,
                    cache: false,
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    async: false,
                    success: function (response) {
                        var User = response.d;
                        if (User == "Success") {
                            $("#modalMasterUser").modal("hide");
                            alert("Master User telah dihapus.")
                            Init();
                        }
                    },
                    error: function (response) {
                        alert(response.responseText);
                        $("#modalMasterUser").modal("hide");
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

function saveUser() {
    var EditMethod = true;
    var editMode = $("#hfEditMode").val();
    if (editMode == 0) EditMethod = false;
    var masterUser = new Object();
    masterUser.No_Pegawai = $("#txtNoPegawai").val();
    masterUser.Nama_Pegawai = $("#txtNamaPegawai").val();
    masterUser.UserName = $("#txtUserName").val();
    masterUser.AreaCode = $("#ddlArea").val();
    var parameter = new Object();
    parameter.masterUserString = JSON.stringify(masterUser);
    parameter.isEdit = EditMethod;
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/SaveMasterUser",
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var User = response.d;
            $("#modalMasterUser").modal("hide");
            alert(User)
            Init();
        },
        error: function (response) {
            alert(response.responseText);
            $("#modalMasterUser").modal("hide");
        }
    });
}
