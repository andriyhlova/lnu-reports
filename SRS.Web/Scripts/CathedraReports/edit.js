function changeStepPageAndSubmit(index, newIndex) {
    $('[id^="stepIndex"]').each(function () {
        $(this).val(newIndex);
    });
    if (index == 0) {
        $('#updateAchievementSchoolForm').submit();
    }
    if (index == 1) {
        $('#updateThemeForm').submit();
    }
    if (index == 2) {
        $('#updatePublicationForm').submit();
    }
    if (index == 3) {
        $('#updateOtherForm').submit();
    }
    if (index == 4) {
        $('#updateFinishForm').submit();
    }
    if (index == 5) {
        $('#updateEndForm').submit();
    }

    return true;
};

function themeDropdownOnChange() {
    $('.chosen-select-budget-theme').on('change', function () {
        updateThemeDescription('.chosen-select-budget-theme', '#budgetThemeDescription');
    });

    $('.chosen-select-in-work-theme').on('change', function () {
        updateThemeDescription('.chosen-select-in-work-theme', '#inWorkThemeDescription');
    });

    $('.chosen-select-hospdohovir-theme').on('change', function () {
        updateThemeDescription('.chosen-select-hospdohovir-theme', '#hospDohovirThemeDescription');
    });
};

function updateThemeDescription(valueFieldName, descriptionFieldName) {
    const element = $(`${valueFieldName} option:selected`);
    if (!$(descriptionFieldName).val()) {
        $(descriptionFieldName).val(element.val() ? element.text() : '');
    }
};

themeDropdownOnChange();

(function () {
    const selectedDefensesOfGraduates = [];
    const selectedDefensesOfEmployees = [];
    const selectedDefensesOfAcademicCouncils = [];
    $(function () {
        const defensesOfGraduatesSettings = {
            collection: selectedDefensesOfGraduates,
            selectedItemsSelector: '.selected-dissertation-defense-graduates',
            fieldName: 'DissertationDefenseOfGraduates',
            searchUrl: '/api/dissertationDefenseApi/search?'
        };

        const searchGraduatesComponent = new SearchComponent(
            '#dissertation-defense-graduates-search',
            defensesOfGraduatesSettings.searchUrl,
            getDissertationDefenseSearchResultText,
            (element) => appendDefenseSearchResultItem(element, defensesOfGraduatesSettings));

        searchGraduatesComponent.load();
        getSelectedDissertationDefenses('.initial-defense-graduates', defensesOfGraduatesSettings);
        $('.selected-dissertation-defense-graduates').on(
            'click',
            '.bi-file-x-fill',
            (element) => removeDefense(element, defensesOfGraduatesSettings));



        const defensesOfEmployeesSettings = {
            collection: selectedDefensesOfEmployees,
            selectedItemsSelector: '.selected-dissertation-defense-employee',
            fieldName: 'DissertationDefenseOfEmployees',
            searchUrl: '/api/dissertationDefenseApi/search?'
        };

        const searchEmployeesComponent = new SearchComponent(
            '#dissertation-defense-employee-search',
            defensesOfEmployeesSettings.searchUrl,
            getDissertationDefenseSearchResultText,
            (element) => appendDefenseSearchResultItem(element, defensesOfEmployeesSettings));

        searchEmployeesComponent.load();
        getSelectedDissertationDefenses('.initial-defense-employee', defensesOfEmployeesSettings);
        $('.selected-dissertation-defense-employee').on(
            'click',
            '.bi-file-x-fill',
            (element) => removeDefense(element, defensesOfEmployeesSettings));



        const defensesOfAcademicCouncilSettings = {
            collection: selectedDefensesOfAcademicCouncils,
            selectedItemsSelector: '.selected-dissertation-defense-academic-council',
            fieldName: 'DissertationDefenseInAcademicCouncil',
            searchUrl: '/api/dissertationDefenseApi/search?'
        };

        const searchAcademicCouncilComponent = new SearchComponent(
            '#dissertation-defense-academic-council-search',
            defensesOfAcademicCouncilSettings.searchUrl,
            getDissertationDefenseSearchResultText,
            (element) => appendDefenseSearchResultItem(element, defensesOfAcademicCouncilSettings));

        searchAcademicCouncilComponent.load();
        getSelectedDissertationDefenses('.initial-defense-academic-council', defensesOfAcademicCouncilSettings);
        $('.selected-dissertation-defense-academic-council').on(
            'click',
            '.bi-file-x-fill',
            (element) => removeDefense(element, defensesOfAcademicCouncilSettings));
    });

    function getDissertationDefenseSearchResultText(defense) {
        return `<div>${defense.SupervisorDescription || ''}  <span class="theme-name">"${defense.Theme || ''}"  &nbsp;</span></div>   ${defense.SpecializationCode || ''}  ${defense.SpecializationName || ''}  ${defense.YearOfGraduating || ''}p.`;
    };

    function getSelectedDissertationDefenses(selector, settings) {
        const defenses = $(selector);
        for (let i = 0; i < defenses.length; i++) {
            const defense = {
                Id: $(defenses[i])[0].dataset.id,
                Theme: $(defenses[i])[0].dataset.theme,
                SpecializationName: $(defenses[i])[0].dataset.specializationname,
                SpecializationCode: $(defenses[i])[0].dataset.specializationcode,
                SupervisorDescription: $(defenses[i])[0].dataset.supervisordescription,
                YearOfGraduating: $(defenses[i])[0].dataset.yearofgraduating,
            }

            settings.collection.push(defense);
        }

        renderDefenseList(settings);
    }

    function renderDefenseList(settings) {
        const selectedDefenses = $(settings.selectedItemsSelector);
        selectedDefenses.html('');
        for (let i = 0; i < settings.collection.length; i++) {
            selectedDefenses.append(getDefenseHtml(i, settings.collection[i], settings.fieldName));
        }
    };

    function getDefenseHtml(index, defense, fieldName) {
        return `<div class="selected-item defense">
                    <div class="form-group fullname">
                        <lable name="${fieldName}[${index}]" style="max-width:100%" rows="6">
                            ${getDissertationDefenseSearchResultText(defense)}
                        </lable>
                        <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                    </div>
                    <input type="hidden" name="${fieldName}[${index}].Id" class="id" value="${defense.Id || 0}" />
                    <input type="hidden" name="${fieldName}[${index}].Theme" class="theme" value="${defense.Theme}" />
                    <input type="hidden" name="${fieldName}[${index}].SpecializationName" class="specializationname" value="${defense.SpecializationName}" />
                    <input type="hidden" name="${fieldName}[${index}].SupervisorDescription" class="supervisordescription" value="${defense.SupervisorDescription}" />
                    <input type="hidden" name="${fieldName}[${index}].YearOfGraduating" class="yearofgraduating" value="${defense.YearOfGraduating}" />
                </div>`;
    };

    function appendDefenseSearchResultItem(defense, settings) {
        if (!settings.collection.find(x => x.Id == defense.Id)) {
            settings.collection.push(defense);
            renderDefenseList(settings);
        }
    };

    function removeDefense(element, settings) {
        const container = $(element.currentTarget).closest('.defense');
        const id = container.find('.id').val();
        const index = settings.collection.findIndex(x => x.Id == id);
        if (index != -1) {
            container.remove();
            settings.collection.splice(index, 1);
            renderDefenseList(settings);
        }
    };
}());