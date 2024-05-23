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
        this.initLabels();
        this.onTypeChanged(element.val());
    }
    initLabels() {
        const fields = $('.field');
        for (let i = 0; i < fields.length; i++) {
            const label = $(fields[i]).find('.field-label');
            if (label) {
                $(fields[i]).attr('data-name', label.text());
            }
        }
    }
    onTypeChanged(value) {
        const fields = $('.field');
        for (let i = 0; i < fields.length; i++) {
            const types = fields[i].dataset[this._attributeName].split(this._separator);
            const field = $(fields[i]);
            const type = types.map(x => {
                const values = x.split(';');
                return {
                    type: values[0],
                    name: values[1]
                };
            }).find(x => x.type == value);
            if (type) {
                field.show();
                const label = field.find('.field-label');
                label?.text(type.name || field[0].dataset['name']);
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