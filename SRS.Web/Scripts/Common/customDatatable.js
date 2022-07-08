$(document).ready(function () {
    $('table.data-table').DataTable(
        {
            "scrollX": true,
            dom: 'lBfrtip',
            buttons: [
                'csv', 'excel'
            ],
            language: {
                url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/uk.json'
            }
        }
    );
});