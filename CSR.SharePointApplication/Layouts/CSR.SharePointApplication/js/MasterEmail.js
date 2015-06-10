$(document).ready(function () {
    Init();

    $('#btnAddMasterArea').click(function () {
        clearModalMasterArea();
        $('#hfEditMode').val('0');
        $('#modalMasterArea').modal('show');
    });

    $('#btnSaveMasterArea').click(function () {
        var Area = $("#txtArea").val();
        var To = $("#txtTo").val();
        var Subject = $("#txtSubject").val();
        var Message = $("#txtMessage").val();

        var validationMessage = "";

        if (Area.length < 1) {
            validationMessage += "Area harus di pilih. \n";
        }
        if (To.length < 1) {
            validationMessage += "Kepada harus di isi. \n";
        }
        if (Subject.length < 1) {
            validationMessage += "Subject harus di isi. \n";
        }
        if (Message.length < 1) {
            validationMessage += "Pesan harus di pilih. \n";
        }
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }
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
    $('#tblMasterArea tbody').remove();
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
                        '<td >' + Area[i].Bidang + ' </td>' + 
                        '<td >' + Area[i].Kepada + ' </td>' +
                        '<td >' + Area[i].Type + ' </td>' +
                        '<td >' + Area[i].URL + ' </td>' +
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
    $("#txtArea").empty();
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
                    $("#txtArea").append($("<option></option>").val
                 (value.AreaCode).html(value.AreaName));
                });
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
    $("#txtBidang").empty();
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/LoadMasterBidang",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Area = response.d;
            if (Area.length > 0) {
                $.each(Area, function (key, value) {
                    $("#txtBidang").append($("<option></option>").val
                 (value.BP_Kode).html(value.BP_Nama));
                });
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
    $("#txtBidang").val(row.children()[1].innerText).prop("disabled", false);
    $("#txtKepada").val(row.children()[2].innerText);
    if (row.children()[3].innerText = "P") {
        document.getElementById("RadioProgram").checked = true;
    } else {
        document.getElementById("RadioRealisasi").checked = true;
    }

    $("#txtURL").val(row.children()[4].innerText)
    $("#txtTo").val(row.children()[5].innerText)
    $("#txtSubject").val(row.children()[6].innerText)
    $("#txtMessage").val(row.children()[7].innerText)


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
    masterUser.Bidang = $("#txtBidang").val();
    masterUser.Kepada = $("#txtKepada").val();
    var type = $('input[name=Program]:checked').val();
    if (type != 0) {
        var type = $('input[name=Realisasi]:checked').val();
    }
    masterUser.Type = type;
    masterUser.URL = $("#txtURL").val();
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
