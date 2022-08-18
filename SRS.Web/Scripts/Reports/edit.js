function changeStepPageAndSubmit(index, newIndex) {
    $('[id^="stepIndex"]').each(function () {
        $(this).val(newIndex);
    });
    if (index == 0) {
        $('#updatePublicationForm').submit();
    }
    if (index == 1) {
        $('#updateThemeForm').submit();
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
    $(function () {
        const searchComponent = new SearchComponent('#scientific-work-search', '/api/themeOfScientificWorksApi/searchAll', getScientificWorkSearchResultText, appendScientificWorkSearchResultItem);
        searchComponent.load();
        getSelectedScientificWorks();
        $('.selected-scientific-works').on('click', '.bi-file-x-fill', removeScientificWork);
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

    function getSelectedScientificWorks() {
        const scientificWorks = $('.initial-scientific-work');
        for (let i = 0; i < scientificWorks.length; i++) {
            const scientificWork = {
                Id: $(scientificWorks[i])[0].dataset.id,
                ThemeNumber: $(scientificWorks[i])[0].dataset.themenumber,
                Code: $(scientificWorks[i])[0].dataset.code,
                ScientificHead: $(scientificWorks[i])[0].dataset.scientifichead,
                Value: $(scientificWorks[i])[0].dataset.value
            }

            selectedScientificWorks.push(scientificWork);
        }

        renderScientificWorkList(selectedScientificWorks);
    }

    function getScientificWorkSearchResultText(scientificWork) {
        return `<div>${scientificWork.ThemeNumber || ''} ${scientificWork.Code || ''} ${scientificWork.ScientificHead || ''}  <span class="theme-name">${scientificWork.Value}</span></div>`;
    };

    function appendScientificWorkSearchResultItem(scientificWork) {
        if (!selectedScientificWorks.find(x => x.Id == scientificWork.Id)) {
            selectedScientificWorks.push(scientificWork);
            renderScientificWorkList(selectedScientificWorks);
        }
    };

    function removeScientificWork(element) {
        if (selectedScientificWorks.length == 1) {
            alert('Звіт повинен мати хоча б одну тему наукової роботи');
            return;
        }

        const container = $(element.currentTarget).closest('.scientific-work');
        const id = container.find('.id').val();
        const index = selectedScientificWorks.findIndex(x => x.Id == id);
        if (index != -1) {
            container.remove();
            selectedScientificWorks.splice(index, 1);
            renderScientificWorkList(selectedScientificWorks);
        }
    };

    function renderScientificWorkList(scientificWorks) {
        const selectedScientificWorks = $('.selected-scientific-works');
        selectedScientificWorks.html('');
        for (let i = 0; i < scientificWorks.length; i++) {
            selectedScientificWorks.append(getScientificWorkHtml(i, scientificWorks[i]));
        }
    };

    function getScientificWorkHtml(index, scientificWork) {
        return `<div class="selected-item scientific-work">
                                <div class="fullname">${getScientificWorkSearchResultText(scientificWork)}<i class="bi bi-file-x-fill text-danger cursor-pointer"></i></div>
                                <input type="hidden" name="ThemeOfScientificWorkIds[${index}]" class="id" value="${scientificWork.Id}" />
                                 <input type="hidden" class="themenumber" value="${scientificWork.ThemeNumber}" />
                                 <input type="hidden" class="code" value="${scientificWork.Code}" />
                                 <input type="hidden" class="scientifichead" value="${scientificWork.ScientificHead}" />
                                 <input type="hidden" class="value" value="${scientificWork.Value}" />
                            </div>`;
    };
}());