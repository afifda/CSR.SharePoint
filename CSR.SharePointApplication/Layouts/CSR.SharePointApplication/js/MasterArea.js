$(document).ready(function () {
    Init();
    
    $('#btnAddMasterArea').click(function () {
        clearModalMasterArea();
        $('#hfEditMode').val('0');
        $('#modalMasterArea').modal('show');
        $("#txtKodeArea").prop("disabled", false);
    });

    $('#btnSaveMasterArea').click(function () {
        var kodearea =  $("#txtKodeArea").val();
        var namaarea = $("#txtNamaArea").val();
        var validationMessage = "";

        if (kodearea.length < 1) {
            validationMessage += "Kode Area harus di isi. \n";
        }
        if (namaarea.length < 1) {
            validationMessage += "Nama Area harus di isi. \n";
        }
        if (validationMessage.length > 0) {
            alert(validationMessage);
            return false;
        }
        saveArea();
    });

    $('#tblMasterArea').on("click", ".btnEdit", editArea);
    $('#tblMasterArea').on("click", ".btnDelete", deleteArea);
});

function clearModalMasterArea() {
    $("#txtKodeArea").val("");
    $("#txtNamaArea").val("");
}

function Init()
{
    $('#tblMasterArea tbody').remove();
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
                for (i = 0; i < Area.length; i++) {
                    var seq = i + 1;
                    var strhtml = '<tr id="AreaRow_"' + seq + '>' +
                        '<td >' + Area[i].AreaCode + ' </td>' +
                        '<td >' + Area[i].AreaName + ' </td>' +                        
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
}

function editArea()
{
    var $element = this;
    var row = $($element).parents("tr:first");

    clearModalMasterArea();
    $("#hfEditMode").val("1");
    $("#modalMasterArea").modal("show");
    $("#txtKodeArea").val(row.children()[0].innerText).prop("disabled", true);
    $("#txtNamaArea").val(row.children()[1].innerText)
}

function deleteArea()
{
    $("#dialog-confirm").html("Apakah anda yakin menghapus area ini?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Warning",
        height: 150,
        width: 350,
        buttons: {
            "Ya": function () {
                var $element = this;
                var row = $($element).parents("tr:first");

                var KodeArea = row.children()[0].innerText;
                var parameter = {
                    kodeArea: KodeArea
                };
                $.ajax({
                    type: "POST",
                    url: window.location.pathname + "/DeleteMasterArea",
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
                            alert("Master Area telah dihapus.")
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

function saveArea()
{
    var EditMethod = true;
    var editMode = $("#hfEditMode").val();
    if (editMode == 0) EditMethod = false;
    var masterArea = new Object();
    masterArea.AreaCode = $("#txtKodeArea").val();
    masterArea.AreaName = $("#txtNamaArea").val();
    var parameter = new Object();
    parameter.masterAreaString = JSON.stringify(masterArea);
    parameter.isEdit = EditMethod;
    $.ajax({
        type: "POST",
        url: window.location.pathname + "/SaveMasterArea",
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: true,
        success: function (response) {
            var Area = response.d;
            $("#modalMasterArea").modal("hide");
            alert(Area);
            Init();
        },
        error: function (response) {
            alert(response.responseText);
            $("#modalMasterArea").modal("hide");
        }
    });
}
