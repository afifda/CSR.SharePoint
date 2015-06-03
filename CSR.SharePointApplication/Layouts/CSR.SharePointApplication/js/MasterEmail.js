$(document).ready(function () {
    Init();

    $('#btnAddMasterArea').click(function () {
        clearModalMasterArea();
        $('#hfEditMode').val('0');
        $('#modalMasterArea').modal('show');
    });

    $('#btnSaveMasterArea').click(function () {
        saveEmail();
    });

    $('#tblMasterArea').on("click", ".btnEdit", editArea);
    $('#tblMasterArea').on("click", ".btnDelete", deleteArea);
});

function clearModalMasterArea() {
    $("#txtArea").val("");
    $("#txtTo").val("");
    $("#txtSubject").val("");
    $("#txtMessage").val("");
}

function Init() {
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadMasterEmail",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Area = response.d;
            if (Area.length > 0) {
                for (i = 0; i < Area.length; i++) {
                    var seq = i + 1;
                    var strhtml = '<tr id="AreaRow_"' + seq + '>' +
                        '<td >' + Area[i].Area + ' </td>' +
                        '<td >' + Area[i].To + ' </td>' +
                        '<td >' + Area[i].Subject + ' </td>' +
                        '<td >' + Area[i].Message + ' </td>' +
                        '<td align="Center"><input type="button"  class="button2 btnEdit" value="Edit"/><input type="button"  class="button2 btnDelete" value="Hapus"/> </td> ' +
                        '</tr>';
                    $(strhtml).appendTo($("#tblMasterArea"));

                }
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function editArea() {
    var $element = this;
    var row = $($element).parents("tr:first");

    clearModalMasterArea();
    $("#hfEditMode").val("1");
    $("#modalMasterArea").modal("show");
    $("#txtArea").val(row.children()[0].innerText).prop("disabled", false);
    $("#txtTo").val(row.children()[1].innerText)
    $("#txtSubject").val(row.children()[2].innerText)
    $("#txtMessage").val(row.children()[3].innerText)

}

function deleteArea() {
    var $element = this;
    var row = $($element).parents("tr:first");

    var Area = row.children()[0].innerText;
    var parameter = {
        Area: Area
    };
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/DeleteMasterEmail",
        async: false,
        cache: false,
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var Area = response.d;
            if (Area == "Success") {
                $("#modalMasterArea").modal("hide");
                alert("Master Email telah dihapus.")
                Init();
            }
        },
        error: function (response) {
            alert(response.responseText);
            $("#modalMasterArea").modal("hide");
        }
    });
}

function saveEmail() {
    var EditMethod = true;
    var editMode = $("#hfEditMode").val();
    if (editMode == 0) EditMethod = false;
    var masterUser = new Object();
    masterUser.Area = $("#txtArea").val();
    masterUser.To = $("#txtTo").val();
    masterUser.Subject = $("#txtSubject").val();
    masterUser.Message = $("#txtMessage").val();
    var parameter = new Object();
    parameter.masterEmailString = JSON.stringify(masterUser);
    parameter.isEdit = EditMethod;
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/SaveMasterEmail",
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Area = response.d;
            if (Area == "Success") {
                $("#modalMasterArea").modal("hide");
                alert("Master Email telah disimpan.")
                Init();
            }
        },
        error: function (response) {
            alert(response.responseText);
            $("#modalMasterArea").modal("hide");
        }
    });
}
