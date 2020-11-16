var dataTable;
var UserData;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Administration/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "autoWidth": true },
            { "data": "Email", "autoWidth": true },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <td style="width:250px;">
                                        <div class="btn-group" role="group">
                                             <a onclick=Delete("/Administration/DeleteUser/${data}") class='btn btn-danger text-white' style='cursor:pointer;'>
                                                <i class='fa fa-trash'></i>
                                            </a>
                                         </div>
                                 </td>                                
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}
function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}
