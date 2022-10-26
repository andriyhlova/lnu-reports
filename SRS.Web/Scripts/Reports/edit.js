function changeStepPageAndSubmit(index, newIndex) {
    $('[id^="stepIndex"]').each(function () {
        $(this).val(newIndex);
    });
    if (index == 0) {
        return submitThemeForm();
    }
    if (index == 1) {
        $('#updatePublicationForm').submit();
    }
    if (index == 2) {
        $('#updateOtherForm').submit();
    }
    if (index == 3) {
        $('#updateFinishForm').submit();
    }
    if (index == 4) {
        $('#finalizeForm').submit();
    }

    return true;
};

function submitThemeForm() {
    let themes = $('textarea[name^=ThemeOfScientificWorks]');
    let otherTheme = $('textarea[name=OtherThemeOfScientificWorkDescription]').val().trim();
    if ((!themes || !themes.length) && !otherTheme) {
        alert('Виберіть тему наукової роботи або введіть опис іншої наукової роботи');
        return false;
    }
    for (let i = 0; i < themes.length; i++) {
        if (!themes[i].value || !themes[i].value.trim()) {
            alert('Введіть опис теми');
            return false;
        }
    }

    let grants = $('textarea[name^=Grants]');
    for (let i = 0; i < grants.length; i++) {
        if (!grants[i].value || !grants[i].value.trim()) {
            alert('Введіть опис гранту');
            return false;
        }
    }

    $('#updateThemeForm').submit();
    return true;
}

(function () {
    const selectedScientificWorks = [];
    const selectedGrants = [];
    $(function () {
        const scientificWorksSettings = {
            collection: selectedScientificWorks,
            selectedItemsSelector: '.selected-scientific-works',
            fieldName: 'ThemeOfScientificWorks',
            searchUrl: '/api/themeOfScientificWorksApi/search?financials=0&financials=1&financials=2&financials=3&financials=4&financials=5&financials=6&financials=8&financials=9'
        };
        const searchComponent = new SearchComponent('#scientific-work-search', scientificWorksSettings.searchUrl, getScientificWorkSearchResultText, (element) => appendScientificWorkSearchResultItem(element, scientificWorksSettings));
        searchComponent.load();
        getSelectedScientificWorks('.initial-scientific-work', scientificWorksSettings);
        $('.selected-scientific-works').on('click', '.bi-file-x-fill', (element) => removeScientificWork(element, scientificWorksSettings));
        $('.selected-scientific-works').on('blur', 'textarea', (element) => saveScientificWork(element, scientificWorksSettings));

        const grantsSettings = {
            collection: selectedGrants,
            selectedItemsSelector: '.selected-grants',
            fieldName: 'Grants',
            disableCheck: true,
            searchUrl: '/api/themeOfScientificWorksApi/search?financials=7'
        };
        const grantSearchComponent = new SearchComponent('#grant-search', grantsSettings.searchUrl, getScientificWorkSearchResultText, (element) => appendScientificWorkSearchResultItem(element, grantsSettings));
        grantSearchComponent.load();
        getSelectedScientificWorks('.initial-grant', grantsSettings);
        $('.selected-grants').on('click', '.bi-file-x-fill', (element) => removeScientificWork(element, grantsSettings));
        $('.selected-grants').on('blur', 'textarea', (element) => saveScientificWork(element, scientificWorksSettings));
        events();
        publicationCheckboxChanged();
    });

    function events() {
        $('[id^="signButton"]').on('click', function () {
            const confirm = confirm(`Після підписання ви не будете мати право редагувати звіт.
                                         Лише завідувач кафедри може зняти підпис.
                                         Ви впевнені, що бажаєте підписати звіт?`);
            return confirm;
        });
    };

    function publicationCheckboxChanged() {
        const elements = $('#updatePublicationForm div[data-field-id] input[type=checkbox]');
        elements.change((e) => {
            onCheckboxChanged(e.target);
        });

        for (let i = 0; i < elements.length; i++) {
            onCheckboxChanged(elements[i]);
        }
    }

    function onCheckboxChanged(element) {
        if (!$(element).is(":visible")) {
            return;
        }
        const field = $(element).parent().parent();
        const fieldsToProcess = $(`#updatePublicationForm div[data-field-id=${field[0].dataset.fieldId}]`);
        if ($(element).is(":checked")) {
            for (let i = 0; i < fieldsToProcess.length; i++) {
                if (fieldsToProcess[i] != field[0]) {
                    $(fieldsToProcess[i]).hide();
                    $(fieldsToProcess[i]).find('input[type=checkbox]').prop('checked', false);
                }
            }
        } else {
            fieldsToProcess.show();
        }
    }

    function getSelectedScientificWorks(selector, settings) {
        const scientificWorks = $(selector);
        for (let i = 0; i < scientificWorks.length; i++) {
            const scientificWork = {
                Id: $(scientificWorks[i])[0].dataset.id,
                ReportThemeId: $(scientificWorks[i])[0].dataset.themeid,
                ThemeNumber: $(scientificWorks[i])[0].dataset.themenumber,
                Code: $(scientificWorks[i])[0].dataset.code,
                ScientificHead: $(scientificWorks[i])[0].dataset.scientifichead,
                Value: $(scientificWorks[i])[0].dataset.value,
                Description: $(scientificWorks[i])[0].dataset.description
            }

            settings.collection.push(scientificWork);
        }

        renderScientificWorkList(settings);
    }

    function getScientificWorkSearchResultText(scientificWork) {
        return `<div>${scientificWork.ThemeNumber || ''} ${scientificWork.Code || ''} ${scientificWork.ScientificHead || ''}  <span class="theme-name">${scientificWork.Value}</span></div>`;
    };

    function appendScientificWorkSearchResultItem(scientificWork, settings) {
        if (!settings.collection.find(x => x.Id == scientificWork.Id)) {
            settings.collection.push(scientificWork);
            renderScientificWorkList(settings);
        }
    };

    function removeScientificWork(element, settings) {
        const container = $(element.currentTarget).closest('.scientific-work');
        const id = container.find('.themeid').val();
        const index = settings.collection.findIndex(x => x.Id == id);
        if (index != -1) {
            container.remove();
            settings.collection.splice(index, 1);
            renderScientificWorkList(settings);
        }
    };

    function saveScientificWork(element, settings) {
        const container = $(element.target).closest('.scientific-work');
        const id = container.find('.themeid').val();
        const theme = settings.collection.find(x => x.Id == id);
        if (theme) {
            theme.Description = $(element.target).val();
        }
    }

    function renderScientificWorkList(settings) {
        const selectedScientificWorks = $(settings.selectedItemsSelector);
        selectedScientificWorks.html('');
        for (let i = 0; i < settings.collection.length; i++) {
            selectedScientificWorks.append(getScientificWorkHtml(i, settings.collection[i], settings.fieldName));
        }
    };

    function getScientificWorkHtml(index, scientificWork, fieldName) {
        return `<div class="selected-item scientific-work">
                                <div class="form-group fullname">${getScientificWorkSearchResultText(scientificWork)}<i class="bi bi-file-x-fill text-danger cursor-pointer"></i></div>
<div class="form-group">
<label class="control-label">
                    Опис виконаної роботи <span class="text-danger">*</span>
                </label>
<div>
<textarea class="form-control" name="${fieldName}[${index}].Description" style="max-width:100%" rows="6" data-value="${scientificWork.Description}">${scientificWork.Description || ''}</textarea>
</div>
</div>
                                <input type="hidden" name="${fieldName}[${index}].Id" class="id" value="${scientificWork.ReportThemeId || 0}" />
                                <input type="hidden" class="themenumber" value="${scientificWork.ThemeNumber}" />
                                <input type="hidden" name="${fieldName}[${index}].ThemeOfScientificWorkId" class="themeid" value="${scientificWork.Id}" />
                                 <input type="hidden" class="code" value="${scientificWork.Code}" />
                                 <input type="hidden" class="scientifichead" value="${scientificWork.ScientificHead}" />
                                 <input type="hidden" class="value" value="${scientificWork.Value}" />
                            </div>`;
    };
}());