$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/course/getall' },
        "columns": [
            { data: 'title', "width": "30%" },
            { data: 'description', "width": "30%" },
            { data: 'price', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-25 btn-group btnTable d-flex justify-content-center" role="group">
                     <a href="/admin/course/upsert?id=${data}" class="btn btn-primary"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a href="/admin/course/delete/${data}") class="btn btn-danger"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                }
            }
        
           
        ]
    });
}