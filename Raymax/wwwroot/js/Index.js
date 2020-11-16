var dataTable;
var UserData;

$(document).ready(function () {
    loadDataTable();
    CreateUser();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "order": [[0, "desc"]],
        "ajax": {
            "url": "/Invoice/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "autoWidth": true },
            { "data": "customerName", "autoWidth": true },
            { "data": "contact", "autoWidth": true },
            { "data": "date_Created", "autoWidth": true },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <td style="width:250px;">
                                        <div class="btn-group" role="group">                                             
                                            <a href="/Invoice/Edit/${data}"class='btn  btn-info text-white' style='cursor:pointer;'>
                                              <i class='fa fa-edit'></i>
                                            </a>
                                            &nbsp;
                                            <a href="/Invoice/Print/${data}"class='btn  btn-success text-white' style='cursor:pointer;'>
                                              <i class='fa fa-print'></i>
                                            </a>
                                               &nbsp;
                                            <a onclick=Delete("/Invoice/DeleteInvoice/${data}") class='btn btn-danger text-white' style='cursor:pointer;'>
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

function CreateUser() {
    $("#btnSave").click(function () {
        UserData = $("#UserForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Admin/User/Create",
            data: UserData,
            success: function () {
                window.location.href = ("/Admin/User/Index");
            }
        })
    })
}
