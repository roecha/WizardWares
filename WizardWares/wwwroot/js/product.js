﻿
var dataTable;
$(document).ready(function () {
    dataTable = loadDataTable();
});

function loadDataTable() {
    return $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'price', "width": "10%" },
            { data: 'tradeItem', "width": "10%" },
            { data: 'rarity.name', "width": "10%" },
            { data: 'category.name', "width": "20%" },
            {
                data: 'imageUrl',
                "render": function (data) {
                    if (data) {
                        return `<img src="${data}" width="50%" />`
                    } else {
                        return `<p>NULL</p>`
                    }
                },

                "width": "5%"
            },


            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}