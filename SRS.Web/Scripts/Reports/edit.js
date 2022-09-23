function changeStepPageAndSubmit(index, newIndex) {
    $('[id^="stepIndex"]').each(function () {
        $(this).val(newIndex);
    });
    if (index == 0) {
        $('#updateThemeForm').submit();
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
};

(function () {
    const selectedScientificWorks = [];
    const selectedGrants = [];
    $(function () {
        const scientificWorksSettings = {
            collection: selectedScientificWorks,
            selectedItemsSelector: '.selected-scientific-works',
            fieldName: 'ThemeOfScientificWorkIds',
            searchUrl: '/api/themeOfScientificWorksApi/search?financials=0&financials=1&financials=2&financials=3&financials=4&financials=5&financials=6&financials=8&financials=9'
        };
        const searchComponent = new SearchComponent('#scientific-work-search', scientificWorksSettings.searchUrl, getScientificWorkSearchResultText, (element) => appendScientificWorkSearchResultItem(element, scientificWorksSettings));
        searchComponent.load();
        getSelectedScientificWorks('.initial-scientific-work', scientificWorksSettings);
        $('.selected-scientific-works').on('click', '.bi-file-x-fill', (element) => removeScientificWork(element, scientificWorksSettings));

        const grantsSettings = {
            collection: selectedGrants,
            selectedItemsSelector: '.selected-grants',
            fieldName: 'GrantIds',
            disableCheck: true,
            searchUrl: '/api/themeOfScientificWorksApi/search?financials=7'
        };
        const grantSearchComponent = new SearchComponent('#grant-search', grantsSettings.searchUrl, getScientificWorkSearchResultText, (element) => appendScientificWorkSearchResultItem(element, grantsSettings));
        grantSearchComponent.load();
        getSelectedScientificWorks('.initial-grant', grantsSettings);
        $('.selected-grants').on('click', '.bi-file-x-fill', (element) => removeScientificWork(element, grantsSettings));
        events();
    });

    function events() {
        $('[id^="signButton"]').on('click', function () {
            const confirm = confirm(`Після підписання ви не будете мати право редагувати звіт.
                                         Лише завідувач кафедри може зняти підпис.
                                         Ви впевнені, що бажаєте підписати звіт?`);
            return confirm;
        });
    };

    function getSelectedScientificWorks(selector, settings) {
        const scientificWorks = $(selector);
        for (let i = 0; i < scientificWorks.length; i++) {
            const scientificWork = {
                Id: $(scientificWorks[i])[0].dataset.id,
                ThemeNumber: $(scientificWorks[i])[0].dataset.themenumber,
                Code: $(scientificWorks[i])[0].dataset.code,
                ScientificHead: $(scientificWorks[i])[0].dataset.scientifichead,
                Value: $(scientificWorks[i])[0].dataset.value
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
        if (!settings.disableCheck && settings.collection.length == 1) {
            alert('Звіт повинен мати хоча б одну тему наукової роботи');
            return;
        }

        const container = $(element.currentTarget).closest('.scientific-work');
        const id = container.find('.id').val();
        const index = settings.collection.findIndex(x => x.Id == id);
        if (index != -1) {
            container.remove();
            settings.collection.splice(index, 1);
            renderScientificWorkList(settings);
        }
    };

    function renderScientificWorkList(settings) {
        const selectedScientificWorks = $(settings.selectedItemsSelector);
        selectedScientificWorks.html('');
        for (let i = 0; i < settings.collection.length; i++) {
            selectedScientificWorks.append(getScientificWorkHtml(i, settings.collection[i], settings.fieldName));
        }
    };

    function getScientificWorkHtml(index, scientificWork, fieldName) {
        return `<div class="selected-item scientific-work">
                                <div class="fullname">${getScientificWorkSearchResultText(scientificWork)}<i class="bi bi-file-x-fill text-danger cursor-pointer"></i></div>
                                <input type="hidden" name="${fieldName}[${index}]" class="id" value="${scientificWork.Id}" />
                                 <input type="hidden" class="themenumber" value="${scientificWork.ThemeNumber}" />
                                 <input type="hidden" class="code" value="${scientificWork.Code}" />
                                 <input type="hidden" class="scientifichead" value="${scientificWork.ScientificHead}" />
                                 <input type="hidden" class="value" value="${scientificWork.Value}" />
                            </div>`;
    };
}());