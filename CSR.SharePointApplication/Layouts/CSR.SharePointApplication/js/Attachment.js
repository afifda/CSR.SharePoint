$(function () {
    $($get("btnUpload")).click(function () {
        if (isUpload() == 0) {
            return false;
        }
        if ($('#fuAttachment').val().length > 0)
        {
            if ($('#fuAttachment').val().length > 0 && $('#fuAttachment1').val().length > 0 && $('#fuAttachment2').val().length > 0) {
                AddtoGridAttch();
            } else if ($('#fuAttachment').val().length > 0 && $('#fuAttachment1').val().length > 0) {
                AddtoGridAttch();
            } else if ($('#fuAttachment').val().length > 0) {
                AddtoGridAttch();
            } else {
                alert('Upload file harus secara urut, dimulai dari isi file pertama');
                return false;
            }
        }
        
        if ($('#fuAttachment').val().length <= 0)
        {
            alert('Upload file harus secara urut, dimulai dari isi file pertama');
            return false;
        }
        return false;
    });
    
    //$("input[id$='fuAttachment1']").prop('disabled', true);
    //$("input[id$='fuAttachment2']").prop('disabled', true);
    $("#ddlAdmin").change(function () {
        loadLookupDataOnChangeAdminSharing();//for company,busines,vendor
    });

    $("#gvAttachment").on("click", ".ClsRemove", deleteRowAttach);
});

function isUpload() {
    //var attach = $("input[id$='fuAttachment']").length==0 && $("input[id$='fuAttachment1']").length==0 || $("input[id$='fuAttachment2']").length==0
    if ($("input[id$='fuAttachment']").length == 0 && $("input[id$='fuAttachment1']").length == 0 || $("input[id$='fuAttachment2']").length == 0) {
        alert('Please insert file to upload');
        return 0;
    }
}

function AddtoGridAttch() {
    var handlerUrl = "/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=uploadFileAttachment";
    if ($('#fuAttachment').val().length > 0) {
        var DocLink = $("#fuAttachment").get(0);
        var DocFile = DocLink.files;
        var files = document.getElementById('fuAttachment').files;

        

        var data = new FormData();
        for (var i = 0; i < DocFile.length; i++) {
            data.append(DocFile[i].name, DocFile[i]);
            //exist file name
            var Cek = existsupload(DocFile[i].name, DocLink.value);
            var CekNameFile = CheckFile(DocFile[i].name, DocLink.value);
        }
        if (Cek == 'true') {
            alert('file already exist');
            return false;
        }
        if (CekNameFile != "") {
            alert(CekNameFile);
            return false;
        }

        $.ajax({
            type: "POST",
            url: handlerUrl,
            data: data,
            contentType: false,
            processData: false,
            dataType: "json",
            success: SuccessAddAttachList,
            error: function (response) {
                alert(response.responseText);
                return false;
            }
        });

    }
    if ($('#fuAttachment1').val().length > 0) {
        var DocLink = $("#fuAttachment1").get(0);
        var DocFile = DocLink.files;
        var files = document.getElementById('fuAttachment1').files;

        var data = new FormData();
        for (var i = 0; i < DocFile.length; i++) {
            data.append(DocFile[i].name, DocFile[i]);
            //exist file name
            var Cek = existsupload(DocFile[i].name, DocLink.value);
            var CekNameFile = CheckFile(DocFile[i].name, DocLink.value);
        }
        if (Cek == 'true') {
            alert('file already exist');
            return false;
        }
        if (CekNameFile != "") {
            alert(CekNameFile);
            return false;
        }

        $.ajax({
            type: "POST",
            url: handlerUrl,
            data: data,
            contentType: false,
            processData: false,
            dataType: "json",
            success: SuccessAddAttachList,
            error: function (response) {
                alert(response.responseText);
                return false;
            }
        });

    }
    if ($('#fuAttachment2').val().length > 0) {
        var DocLink = $("#fuAttachment2").get(0);
        var DocFile = DocLink.files;
        var files = document.getElementById('fuAttachment2').files;

        var data = new FormData();
        for (var i = 0; i < DocFile.length; i++) {
            data.append(DocFile[i].name, DocFile[i]);
            //exist file name
            var Cek = existsupload(DocFile[i].name, DocLink.value);
            var CekNameFile = CheckFile(DocFile[i].name, DocLink.value);
        }
        if (Cek == 'true') {
            alert('file already exist');
            return false;
        }
        if (CekNameFile != "") {
            alert(CekNameFile);
            return false;
        }

        $.ajax({
            type: "POST",
            url: handlerUrl,
            data: data,
            contentType: false,
            processData: false,
            dataType: "json",
            success: SuccessAddAttachList,
            error: function (response) {
                alert(response.responseText);
                return false;
            }
        });

    }


    //if (Cek == 'true') {
    //    alert('file already exist');
    //    return false;
    //}
    //if (CekNameFile != "") {
    //    alert(CekNameFile);
    //    return false;
    //}

    //$.ajax({
    //    type: "POST",
    //    url: handlerUrl,
    //    data: data,
    //    contentType: false,
    //    processData: false,
    //    dataType: "json",
    //    success: SuccessAddAttachList,
    //    error: function (response) {
    //        alert(response.responseText);
    //        return false;
    //    }
    //});
}

function SuccessAddAttachList(data, status, xhr) {
    var result = data;
    $("input[id$='fuAttachment']").val("");
    $("input[id$='fuAttachment1']").val("");
    $("input[id$='fuAttachment2']").val("");
    if (result.length != null) {

        for (i = 0; i < result.length; i++) {
            var rowCount = $('#gvAttachment tr').length;
            var strhtml = '<tr>' +
                    '<td class = "ClsNumber">' + rowCount + ' </td>' +
                    '<td class = "ClsFileName">' + result[i].NamaFile + '</td>' +
                    '<td class = "ClsPathFile">' + result[i].NamaPath + '</td>' +
                    '<td class = "ClsTempFile"  style="display:none;">' + result[i].TempPath + '</td>' +
                    '<td class = "ClsAction"><input type="button"  class="button2 ClsRemove" value="Remove" /></td>' +
                    '</tr>'
            $(strhtml).appendTo($("#gvAttachment"));
        }
    }
    else {
        alert('System cannot query your keyword...');
    }
}

function existsupload(filename, LinkDoc) {
    //var result = listattach;
    var pathname = LinkDoc;
    var namefile = filename;
    var message = 'false';

    $('#gvAttachment tr').each(function (idx, elm) {
        var sOrderBy = $("td:eq(0)", elm).html();
        var sFileName = $("td:eq(1)", elm).html();
        var sPathFile = $("td:eq(2)", elm).html();

        if (namefile == sFileName) {
            message = 'true';
        } else if (pathname == sPathFile) {
            message = 'true';
        }
    });
    return message;
}

function CheckFile(filename, LinkDoc) {
    var msg = '';
    var i = filename.lastIndexOf('\\');
    var j = filename.lastIndexOf('.');
    var fName = filename.substring(i + 1, j);
    var regex = /^[A-Za-z0-9-_()+,=\s]*$/;

    if (!regex.test(fName)) {
        msg = 'The file name contains illegal characters\n !,@,#,$,%,^,&,*,",~ \n';
    }
    return msg;


}

function deleteRowAttach() {
    var par = $(this).parent().parent();
    par.remove();

    var rows = $('#gvAttachment tr');
    //setiap each di function memiliki parameter (index, element)
    //Element adalah patokan dari object. Contoh : $('#gvAttachment tr') maka element adalah object dari row of tr
    //Jika $('#gvAttachment td') maka element yang dimaksud adalah object dari cell of td
    $('#gvAttachment tr').each(function (idx, elm) {
        $("td:eq(0)", elm).html(idx);
    });
};

function GetSuccessAddAttachList(attachmentCRUDList, Aktif) {

    var result = attachmentCRUDList;
    if (attachmentCRUDList.length != null) {

        //var isEdit=Boolean(Aktif);

        if (!Aktif) {
            $('#fuAttachment').hide();
            $('#fuAttachment1').hide();
            $('#fuAttachment2').hide();
            $('#btnUpload').hide();
            for (i = 0; i < result.length; i++) {
                var rowCount = $('#gvAttachment tr').length;
                var strhtml = '<tr>' +
                    '<td class = "ClsNumber">' + rowCount + ' </td>' +
                    '<td class = "ClsFileName">' + result[i].NamaFile + '</td>' +
                    '<td class = "ClsPathFile aDocLink"><a href="javascript:void(0)" onclick="downloadDocFunc(\'' + result[i].NamaPath.replace(/'/g, "####&&&&") + '\');">' + result[i].NamaPath + '</a></td>' +
					'<td class = "ClsTempFile" td style = "display:none">' + result[i].NamaPath + '</td>' +
                    //'<td class = "ClsAction" ><input type="button" class="button2 ClsRemove" value="Remove" disabled="true"/></td>' +
                    '</tr>'
                document.getElementById("Action_ID").style.display = 'none';
                $(strhtml).appendTo($("#gvAttachment"));
            }
        } else if ($('#btnSubmit').prop('disabled', true) == $('#btnSubmit').prop('disabled', true)) {
            for (i = 0; i < result.length; i++) {
                var rowCount = $('#gvAttachment tr').length;
                var strhtml = '<tr>' +
                    '<td class = "ClsNumber">' + rowCount + ' </td>' +
                    '<td class = "ClsFileName">' + result[i].NamaFile + '</td>' +
                    '<td class = "ClsPathFile aDocLink"><a href="javascript:void(0)" onclick="downloadDocFunc(\'' + result[i].NamaPath.replace(/'/g, "####&&&&") + '\');">' + result[i].NamaPath + '</a></td>' +
					'<td class = "ClsTempFile" td style = "display:none">' + result[i].NamaPath + '</td>' +
                    '<td class = "ClsAction"><input type="button"  class="button2 ClsRemove" value="Remove" /></td>' +
                    '</tr>'

                $(strhtml).appendTo($("#gvAttachment"));
            }
        } else {
            for (i = 0; i < result.length; i++) {
                var rowCount = $('#gvAttachment tr').length;
                var strhtml = '<tr>' +
                    '<td class = "ClsNumber">' + rowCount + ' </td>' +
                    '<td class = "ClsFileName">' + result[i].NamaFile + '</td>' +
                    '<td class = "ClsPathFile aDocLink"><a href="javascript:void(0)" onclick="downloadDocFunc(\'' + result[i].NamaPath.replace(/'/g, "####&&&&") + '\');">' + result[i].NamaPath + '</a></td>' +
					'<td class = "ClsTempFile" td style = "display:none">' + result[i].NamaPath + '</td>' +
                    '<td class = "ClsAction"><input type="button"  class="button2 ClsRemove" value="Remove"/></td>' +
                    '</tr>'

                $(strhtml).appendTo($("#gvAttachment"));
            }
        }

    }

    else {
        alert('System cannot query your keyword...');
    }
}

function downloadDocFunc(docLink) {
    docLink = docLink.replace("####&&&&", "'");    
    var requestNo = getUrlVars()["TransaksiNo"];
    var RealisasiNo = getUrlVars()["RealisasiNo"];
    if (requestNo != null) {
        requestNo = $('#hfTransaksiNo').val();
        window.location.href = "/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=downloadFileAttachment&DocPath=" + docLink + "&ReqNo=" + requestNo;
    } else {
        RealisasiNo = $('#hfRealisasiNo').val();
        window.location.href = "/_layouts/15/CSR.SharePointApplication/generichandler.ashx?Method=downloadFileAttachment&DocPath=" + docLink + "&ReqNo=" + RealisasiNo;
    }

    return false;
}

function saveAttachment(flag) {

    var attachmentCRUDList = [];
    $("#gvAttachment tbody tr").each(function (idx, elm) {

        var sOrderBy = $("td:eq(0)", elm).html();
        var sFileName = $("td:eq(1)", elm).html();
        var sPathFile = $("td:eq(2)", elm)[0].lastChild.text == undefined ? $("td:eq(2)", elm).html() : $("td:eq(2)", elm)[0].lastChild.text;
        var Link = $("td:eq(3)", elm).html();

        var item = {};
        item.NoUrut = sOrderBy;
        item.NamaFile = sFileName;
        item.NamaPath = sPathFile;
        item.Flag = flag;
        item.TempPath = Link;
        attachmentCRUDList.push(item);

        if (sFileName.length <= 0) {
            alert('Please select any conditions at Row ' + idx);
            tblparam = false;
        }

    });
    return attachmentCRUDList;
}

function reLayoutAttachment() {
    $('#fuAttachment').hide();
    $('#btnUpload').hide();
    $('#gvAttachment').find('th:eq(4)').hide();
    $('#gvAttachment').find('.ClsAction').hide();
}

function getUrlVars() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
        vars[key] = value;
    });
    return vars;
}