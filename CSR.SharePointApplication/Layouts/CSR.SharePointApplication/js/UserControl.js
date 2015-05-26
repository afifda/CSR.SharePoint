$(document).ready(function () {
    Init();

    $('#btnAddMasterUser').click(function () {
        clearModalMasterUser();
        $('#hfEditMode').val('0');
        $('#modalMasterUser').modal('show');
        $("#txtNoPegawai").prop("disabled", false);
    });

    $('#btnSaveMasterUser').click(function () {
        saveUser();
    });

    $('#tblMasterUser').on("click", ".btnEdit", editUser);
    $('#tblMasterUser').on("click", ".btnDelete", deleteUser);
    $("#tblMasterUser").on("click", ".ClsRemove", deleteRowAttach);
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
                        '<td >' + User[i].AreaCode + ' </td>' +
                        '<td align="Center"><input type="button"  class="button2 btnEdit" value="Edit"/><input type="button"  class="button2 btnDelete" value="Hapus"/> </td> ' +
                        '</tr>';
                    $(strhtml).appendTo($("#tblMasterUser"));

                }
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
    $("#txtKodeArea").val(row.children()[3].innerText)

}

function deleteUser() {
    var $element = this;
    var row = $($element).parents("tr:first");

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
}

function saveUser() {
    var EditMethod = true;
    var editMode = $("#hfEditMode").val();
    if (editMode == 0) EditMethod = false;
    var masterUser = new Object();
    masterUser.No_Pegawai = $("#txtNoPegawai").val();
    masterUser.Nama_Pegawai = $("#txtNamaPegawai").val();
    masterUser.UserName = $("#txtUserName").val();
    masterUser.AreaCode = $("#txtKodeArea").val();
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
