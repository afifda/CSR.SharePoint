$(document).ready(function () {
    Init();

    $('#btnAddMasterArea').click(function () {
        clearModalMasterArea();
        $('#hfEditMode').val('0');
        $('#modalMasterArea').modal('show');
    });

    $('#btnSaveMasterArea').click(function () {
        var Area = $("#txtArea").val();
        var To = $("#txtKepada").val();
        var emailTo = $("#txtTo").val();


        var validationMessage = "";

        if (Area == "" || Area == null) {
            validationMessage += "Area harus di pilih. \n";
        }
        if (To == "" || To == null) {
            validationMessage += "Kepada harus di isi. \n";
        }
        if (emailTo == "" || emailTo == null) {
            validationMessage += "Email harus di isi. \n";
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
    $("#txtKepada").val("");
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
                        '<td >' + Area[i].Kepada + ' </td>' +
                        '<td >' + Area[i].To + ' </td>' +
                        '<td align="Center"><input type="button"  class="button2 btnEdit" value="Ubah"/><input type="button"  class="button2 btnDelete" value="Hapus"/> </td> ' +
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
    $("#txtKepada").val(row.children()[1].innerText);
    $("#txtTo").val(row.children()[2].innerText);
    //$("#txtBidang").val(row.children()[1].innerText).prop("disabled", false);
    //if (row.children()[3].innerText = "P") {
    //    document.getElementById("RadioProgram").checked = true;
    //} else {
    //    document.getElementById("RadioRealisasi").checked = true;
    //}
    //$("#txtURL").val(row.children()[4].innerText)
 }

function deleteArea() {
    var $element = this;
    var row = $($element).parents("tr:first");
    $("#dialog-confirm").html("Apakah anda yakin menghapus email ini?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Warning",
        height: 150,
        width: 350,
        buttons: {
            "Ya": function () {
                
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
                $(this).dialog('close');
            },
            "Tidak": function () {
                $(this).dialog('close');
            }
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
    masterUser.Kepada = $("#txtKepada").val();
    //masterUser.Bidang = $("#txtBidang").val();
    //var type = $('input[name=Program]:checked').val();
    //if (type != 0) {
    //    var type = $('input[name=Realisasi]:checked').val();
    //}
    //masterUser.Type = type;
    //masterUser.URL = $("#txtURL").val();
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
