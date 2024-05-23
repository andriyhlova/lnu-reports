(function () {
    $(function () {
        const availableFieldsComponent = new AvailableFieldsComponent('input[name=PublicationType]', 'types', separator);
        availableFieldsComponent.load();
    });
}());