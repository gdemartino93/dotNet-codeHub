$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/course/getall' },
        "columns": [
            { data: 'title', "width": "10%" },
            { data: 'description', "width": "10%" },
            { data: 'price', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            { data: 'createdAt', "width": "15%" },
        
           
        ]
    });
}