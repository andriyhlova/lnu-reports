(function () {
    $(function () {
        const availableFieldsComponent = new AvailableFieldsComponent('select[name=PublicationType]', 'types', separator);
        availableFieldsComponent.load();
    });
}());