function ShowPanel(name) {
    var panel = name;
    $('a[data-toggle="ajax-model"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            panel.html(data);
            panel.find('.modal').modal('show');
        })


    })
}
function SetValues(machineName, processName) {
    $.ajax({
        success: function () {
            document.getElementById('processName').value = processName;
            document.getElementById('machineName').value = machineName;
            $('#addBin').modal('show');
        }
    })

}
function BinCheking(binNumber) {
    var attached = document.getElementById('attached');
    var free = document.getElementById('freeToUse');
    var process = document.getElementById('processName');
    var machine = document.getElementById('machineName');
    var button = document.getElementById('submitButton');
    $.ajax({
        type: 'Get',
        url: '@Url.Action("BinStatus", "Waste")',
        dataType: 'json',
        data: { 'binNumber': binNumber },
        success: function (data) {
            if (data['status'] == "Free to use") {
                if (binNumber.length == 12) {

                    free.removeAttribute('hidden');
                    button.removeAttribute('disabled');
                    $('#warning').text("");
                }
                else {
                    attached.hidden = true;
                    free.hidden = true;
                    button.disabled = true;
                    $('#warning').text("");
                }
            }
            else if (data['status'] == "Filling") {
                if (binNumber.length == 12) {
                    $('#addBin').dialog({
                        height: 600
                    })
                    attached.removeAttribute('hidden');
                    process.value = data['process'];
                    machine.value = data['machine'];
                    $('#warning').text("");
                    free.hidden = true;
                }
            }
            else if (data['status'] == null || binNumber.length != 12) {
                $('#warning').text("Please, check bin number!");
                attached.hidden = true;
                free.hidden = true;
                button.disabled = true;
            }
            else {
                attached.hidden = true;
                free.hidden = true;
                button.disabled = true;
                $('#warning').text("");
            }
        }
    })
}
function GetBin(id) {
    $.ajax({
        type: "Get",
        url: '/Waste/GetBin/' + id,
        success: function (data) {
            Swal.fire({
                title: 'Detach bin ' + data + ' ?',
                text: "",
                icon: 'question',
                showCancelButton: true,
                showConfirmButton: true,
                cancelButtonText: 'Back',
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        url: '/Waste/Detach/' + data,
                        data: { 'binNumber': data },
                        success: function (response) {
                            if (response.success) {
                                setTimeout(function () { window.location.reload() }, 1000);
                            }
                        }
                    })
                    Swal.fire(
                        'Done!',
                        '',
                        'success'
                    )
                }
            })
        }
    })
}
