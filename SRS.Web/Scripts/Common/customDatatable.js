$(document).ready(function () {
    $('table.data-table').DataTable(
        {
            "scrollX": true,
            dom: 'lBfrtip',
            buttons: [
                'csv', 'excel'
            ]
        }
    );
});