class AvailableFieldsComponent {
    _selectElementSelector;
    _attributeName;
    _separator;

    constructor(selectElementSelector, attributeName, separator) {
        this._selectElementSelector = selectElementSelector;
        this._attributeName = attributeName;
        this._separator = separator;
    }

    load() {
        const element = $(this._selectElementSelector);
        $(element).change((e) => {
            this.onTypeChanged(e.target.value);
        });

        this.onTypeChanged(element.val());
    }

    onTypeChanged(value) {
        const fields = $('.field');
        for (let i = 0; i < fields.length; i++) {
            const types = fields[i].dataset[this._attributeName].split(this._separator);
            const field = $(fields[i]);
            if (types.includes(value)) {
                field.show();
                field.find('[name]').prop("disabled", false);
            }
            else {
                field.hide();
                field.find('[name]').prop("disabled", true);
            }
        }
        $('.chosen-select').trigger('chosen:updated');
    }
}