(function () {
    const selectedJournalTypes = [];
    let allJournalTypes = [];
    $(function () {
        getJournalTypes();
        getSelectedJournalTypes();
        let journalTypeEntityComponent = new RelatedEntityComponent(getSettings());
        journalTypeEntityComponent.load();
    });

    function getSettings() {
        return {
            relatedEntityContainerId: '#journal-type-related-entity',
            selectedItems: selectedJournalTypes,
            getFormHtml: getJournalTypeFormHtml,
            postLoadForm: updateJournalTypeList,
            getRelatedEntityFormObject: getJournalTypeFormObject,
            getSelectedRelatedEntityHtml: getSelectedJournalTypeHtml
        };
    }

    function getJournalTypes() {
        $.ajax('/api/journalTypes/getAll')
            .done(function (journalTypes) {
                allJournalTypes = journalTypes;
                updateJournalTypeList();
            });
    }

    function getSelectedJournalTypes() {
        const journalTypes = $('#journal-type-related-entity .selected-item');
        for (let i = 0; i < journalTypes.length; i++) {
            const element = $(journalTypes[i]);
            const journalType = {
                Id: element.find('.id').val(),
                Value: element.find('.value').val()
            }

            selectedJournalTypes.push(journalType);
        }
    }

    function updateJournalTypeList() {
        const journalTypeElement = $("#journal-type-selector");
        if (!journalTypeElement.length || journalTypeElement.children().length) {
            return;
        }

        let str = "<option value=''>Виберіть тип журналу</option>";
        for (var i = 0; i < allJournalTypes.length; i++) {
            let journalType = allJournalTypes[i];
            str += `<option value='${journalType.Id}'>${journalType.Value}</option>`;
        }

        journalTypeElement.html(str);
        journalTypeElement.chosen();
    }

    function getJournalTypeFormObject() {
        const journalTypeId = $('#journal-type-related-entity .new-related-entity-form select').val();
        if (!journalTypeId) {
            alert('Виберіть тип журналу');
            return;
        }

        const journalTypeValue = $('#journal-type-related-entity .new-related-entity-form select :selected').text();

        return {
            Id: journalTypeId,
            Value: journalTypeValue
        };
    }

    function getJournalTypeFormHtml() {
        return `<div>
                        <label class="control-label">Тип журналу <span class="text-danger">*</span></label>
                        <div><select id="journal-type-selector" class="form-control chosen-select"></select></div>
                    </div>`;
    }

    function getSelectedJournalTypeHtml(index, journalType) {
        return `<div>
                    <div class="selected-item">
                            <div>
                                ${journalType.Value}
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="JournalTypes[${index}].Id" class="id" value="${journalType.Id}" />
                            <input type="hidden" name="JournalTypes[${index}].Value" class="value" value="${journalType.Value}" />
                    </div>
                </div>`;
    }
}());