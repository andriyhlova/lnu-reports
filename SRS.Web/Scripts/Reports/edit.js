jQuery.extend(jQuery.validator.messages, {
    max: jQuery.validator.format("Введіть значенння менше ніж або рівне {0}.")
});

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
        return submitDateForm();
    }
    if (index == 4) {
        $('#finalizeForm').submit();
    }

    return true;
};

function submitThemeForm() {
    let themes = $('textarea[name^=ThemeOfScientificWorks]');
    for (let i = 0; i < themes.length; i++) {
        if (themes[i].name.includes('Description') && (!themes[i].value || !themes[i].value.trim())) {
            alert('Введіть опис теми');
            return false;
        }
    }

    let grants = $('textarea[name^=Grants]');
    for (let i = 0; i < grants.length; i++) {
        if (grants[i].name.includes('Description') && (!grants[i].value || !grants[i].value.trim())) {
            alert('Введіть опис ґранту');
            return false;
        }
    }

    $('#updateThemeForm').submit();
    return true;
}

function submitDateForm() {
    if (!$('#updateFinishForm').valid()) {
        return false;
    }

    $('#updateFinishForm').submit();
    return true;
}

(function () {
    const selectedScientificWorks = [];
    const selectedGrants = [];
    let allUsers = [];
    let selectedPerformersFullTime = []
    let selectedPerformersExternalPartTime = []
    let selectedPerformersLawContract = []
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
        const grantSearchComponent = new SearchComponent('#grant-search', grantsSettings.searchUrl, getScientificWorkSearchResultText, (element) => appendGrantSearchResultItem(element, grantsSettings));
        grantSearchComponent.load();
        getSelectedGrants('.initial-grant', grantsSettings);
        $('.selected-grants').on('click', '.bi-file-x-fill', (element) => removeGrant(element, grantsSettings));
        $('.selected-grants').on('blur', 'textarea', (element) => saveScientificWork(element, scientificWorksSettings));
        events();
        publicationCheckboxChanged();
        getPerformers();
    });

    function events() {
        $('[id^="signButton"]').on('click', function () {
            const confirm = confirm(`Після підписання ви не будете мати право редагувати звіт.
                                         Лише завідувач кафедри може відмінити цю дію.
                                         Ви впевнені, що бажаєте завершити звіт?`);
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
                ReportThemeId: $(scientificWorks[i])[0].dataset.reportthemeid,
                ThemeNumber: $(scientificWorks[i])[0].dataset.themenumber,
                Code: $(scientificWorks[i])[0].dataset.code,
                ScientificHead: $(scientificWorks[i])[0].dataset.supervisorsdescription,
                Value: $(scientificWorks[i])[0].dataset.value,
                Description: $(scientificWorks[i])[0].dataset.description,
                Resume: $(scientificWorks[i])[0].dataset.resume,
                Publications: $(scientificWorks[i])[0].dataset.publications,
                DefendedDissertation: $(scientificWorks[i])[0].dataset.defendeddissertation,
                SupervisorId: $(scientificWorks[i])[0].dataset.supervisorsid,
                ApplicationsUserFullTime: $(scientificWorks[i])[0].dataset.applicationsuserfulltime,
                ApplicationsUserFullTimeId: $(scientificWorks[i])[0].dataset.applicationsuserfulltimeid,
                ApplicationsUserExternalPartTime: $(scientificWorks[i])[0].dataset.applicationsuserexternalparttime,
                ApplicationsUserExternalPartTimeId: $(scientificWorks[i])[0].dataset.applicationsuserexternalparttimeid,
                ApplicationsUserLawContract: $(scientificWorks[i])[0].dataset.applicationsuserlawcontract,
                ApplicationsUserLawContractId: $(scientificWorks[i])[0].dataset.applicationsuserlawcontractid
            }

            settings.collection.push(scientificWork);
        }

        renderScientificWorkList(settings);

        for (let i = 0; i < scientificWorks.length; i++) {
            pushSelectedPerformers(`#performer-full-time-related-entity`, i, selectedPerformersFullTime);
            const performerFullTimeEntityComponent = new RelatedEntityComponent(getPerformerFullTimeSettings(i));
            performerFullTimeEntityComponent.load();

            pushSelectedPerformers(`#performer-external-part-time-related-entity`, i, selectedPerformersExternalPartTime);
            const performerExternalPartTimeEntityComponent = new RelatedEntityComponent(getPerformerExternalPartTimeSettings(i));
            performerExternalPartTimeEntityComponent.load();

            pushSelectedPerformers(`#performer-law-contract-related-entity`, i, selectedPerformersLawContract);
            const performerLawContractEntityComponent = new RelatedEntityComponent(getPerformerLawContractSettings(i));
            performerLawContractEntityComponent.load();
        }
    }

    function getScientificWorkSearchResultText(scientificWork) {
        return `<div>${scientificWork.ThemeNumber || ''} ${scientificWork.Code || ''} ${scientificWork.ScientificHead || ''}  <span class="theme-name">${scientificWork.Value}</span></div>`;
    };

    function appendScientificWorkSearchResultItem(scientificWork, settings) {
        if (!settings.collection.find(x => x.Id == scientificWork.Id)) {
            settings.collection.push(scientificWork);
            renderScientificWorkList(settings);

            index = settings.collection.indexOf(scientificWork);

            pushSelectedPerformers(`#performer-full-time-related-entity`, index, selectedPerformersFullTime);
            const performerFullTimeEntityComponent = new RelatedEntityComponent(getPerformerFullTimeSettings(index));
            performerFullTimeEntityComponent.load();

            pushSelectedPerformers(`#performer-external-part-time-related-entity`, index, selectedPerformersExternalPartTime);
            const performerExternalPartTimeEntityComponent = new RelatedEntityComponent(getPerformerExternalPartTimeSettings(index));
            performerExternalPartTimeEntityComponent.load();

            pushSelectedPerformers(`#performer-law-contract-related-entity`, index, selectedPerformersLawContract);
            const performerLawContractEntityComponent = new RelatedEntityComponent(getPerformerLawContractSettings(index));
            performerLawContractEntityComponent.load();
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
            theme.Resume = $(element.target).val();
            theme.Publications = $(element.target).val();
            theme.DefendedDissertation = $(element.target).val();
        }
    }

    function renderScientificWorkList(settings) {
        const selectedScientificWorks = $(settings.selectedItemsSelector);
        selectedScientificWorks.html('');
        for (let i = 0; i < settings.collection.length; i++) {
            selectedScientificWorks.append(getScientificWorkHtml(i, settings.collection[i], settings.fieldName));
        }
    };

    function getSelectedGrants(selector, settings) {
        const grants = $(selector);
        for (let i = 0; i < grants.length; i++) {
            const grant = {
                Id: $(grants[i])[0].dataset.id,
                ThemeId: $(grants[i])[0].dataset.themeid,
                ThemeNumber: $(grants[i])[0].dataset.themenumber,
                Code: $(grants[i])[0].dataset.code,
                ScientificHead: $(grants[i])[0].dataset.supervisorsdescription,
                Value: $(grants[i])[0].dataset.value,
                Description: $(grants[i])[0].dataset.description
            }

            settings.collection.push(grant);
        }

        renderGrantList(settings);
    }

    function renderGrantList(settings) {
        const selectedGrants = $(settings.selectedItemsSelector);
        selectedGrants.html('');
        for (let i = 0; i < settings.collection.length; i++) {
            selectedGrants.append(getGrantHtml(i, settings.collection[i], settings.fieldName));
        }
    };

    function removeGrant(element, settings) {
        const container = $(element.currentTarget).closest('.scientific-work');
        const id = container.find('.themeid').val();
        const index = settings.collection.findIndex(x => x.Id == id);
        if (index != -1) {
            container.remove();
            settings.collection.splice(index, 1);
            renderGrantList(settings);
        }
    };

    function appendGrantSearchResultItem(scientificWork, settings) {
        if (!settings.collection.find(x => x.Id == scientificWork.Id)) {
            settings.collection.push(scientificWork);
            renderGrantList(settings);


        }
    };

    function getGrantHtml(index, grant, fieldName) {
        return `
                <div class="selected-item grant">
                    <div class="form-group fullname">
                        ${getScientificWorkSearchResultText(grant)}<i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <label class="control-label">
                                Опис виконаної роботи <span class="text-danger">*</span>
                            </label>

                            <div>
                                <textarea class="form-control" name="${fieldName}[${index}].Description" style="max-width:100%" rows="6" data-value="${grant.Description}">${grant.Description || ''}</textarea>
                            </div>
                        </div>
                    </div>

                    <input type="hidden" name="${fieldName}[${index}].Id" class="id" value="${grant.ReportThemeId || 0}" />
                    <input type="hidden" class="themenumber" value="${grant.ThemeNumber}" />
                    <input type="hidden" name="${fieldName}[${index}].ThemeOfScientificWorkId" class="themeid" value="${grant.Id}" />
                    <input type="hidden" class="code" value="${grant.Code}" />
                    <input type="hidden" class="supervisordescription" value="${grant.SupervisorDescription}" />
                    <input type="hidden" class="value" value="${grant.Value}" />
                </div>`;
    }

    function getScientificWorkHtml(index, scientificWork, fieldName) {
        let aditionalFileds = ``;
        let additionalPerformerComponents = ``;

        if (scientificWork.SupervisorId && scientificWork.SupervisorId != undefined) {
            if (scientificWork.SupervisorId.split(",").some(x => x == CurrentUserId)) {
                aditionalFileds = `
                <div class="form-group">
                    <label class="control-label">
                        Резюме
                    </label>

                    <div>
                        <textarea class="form-control" name="${fieldName}[${index}].Resume" style="max-width:100%" rows="6" data-value="${scientificWork.Resume}">${scientificWork.Resume || ''}</textarea>
                    </div>
                </div>
                    
                <div class="form-group">
                    <label class="control-label">
                        Публікації
                    </label >

                    <div>
                        <textarea class="form-control" name="${fieldName}[${index}].Publications" style="max-width:100%" rows="6" data-value="${scientificWork.Publications}">${scientificWork.Publications || ''}</textarea>
                    </div>
                </div>
                    
                <div class="form-group">
                    <label class="control-label">
                        Захищено дисертацій за тематикою роботи
                    </label>

                    <div>
                        <textarea class="form-control" name="${fieldName}[${index}].DefendedDissertation" style="max-width:100%" rows="6" data-value="${scientificWork.DefendedDissertation}">${scientificWork.DefendedDissertation || ''}</textarea>
                    </div>
                </div>`;

                let selectedPerformersFullTimeItems = getFullTimePerformers(
                    fieldName,
                    index,
                    'ApplicationUserFullTime',
                    scientificWork.ReportThemeId,
                    scientificWork.ApplicationsUserFullTime,
                    scientificWork.ApplicationsUserFullTimeId);
                let selectedPerformersExternalTimeItems = getFullTimePerformers(
                    fieldName,
                    index,
                    'ApplicationUserExternalPartTime',
                    scientificWork.ReportThemeId,
                    scientificWork.ApplicationsUserExternalPartTime,
                    scientificWork.ApplicationsUserExternalPartTimeId);
                let selectedPerformersLawContractItems = getFullTimePerformers(
                    fieldName,
                    index,
                    'ApplicationUserLawContract',
                    scientificWork.ReportThemeId,
                    scientificWork.ApplicationsUserLawContract,
                    scientificWork.ApplicationsUserLawContractId);


                additionalPerformerComponents = `
                <div class="form-group">
                    <label class="control-label">Виконавці</label>
                    <div class="form-group">
                        <label class="control-label col-md-3">Штатні</label>
                        <div class="col-md-9" id="performer-full-time-related-entity-${index}">
                            <button class="btn btn-info add-related-entity" type="button">Додати</button>
                            <div class="selected-items">
                                ${selectedPerformersFullTimeItems}
                            </div>
                            <div class="new-related-entity">
                            </div>
                            <hr>
                        </div>

                        <label class="control-label col-md-3">Сумісники</label>
                        <div class="col-md-9" id="performer-external-part-time-related-entity-${index}">
                            <button class="btn btn-info add-related-entity" type="button">Додати</button>
                            <div class="selected-items">
                                ${selectedPerformersExternalTimeItems}
                            </div>
                            <div class="new-related-entity">
                            </div>
                            <hr>
                        </div>

                        <label class="control-label col-md-3">За цивільно-правовим договором</label>
                        <div class="col-md-9" id="performer-law-contract-related-entity-${index}">
                            <button class="btn btn-info add-related-entity" type="button">Додати</button>
                            <div class="selected-items">
                                ${selectedPerformersLawContractItems}
                            </div>
                            <div class="new-related-entity">
                            </div>
                        </div>
                    </div>
                </div>`;
            }
        }

        return `
        <div class="selected-item scientific-work">
            <div class="form-group fullname">
                ${getScientificWorkSearchResultText(scientificWork)}<i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
            </div>
            <div class="form-group">
                <div class="form-group">
                    <label class="control-label">
                        Опис виконаної роботи <span class="text-danger">*</span>
                    </label>

                    <div>
                        <textarea class="form-control" name="${fieldName}[${index}].Description" style="max-width:100%" rows="6" data-value="${scientificWork.Description}">${scientificWork.Description || ''}</textarea>
                    </div>
                </div>


                ${aditionalFileds}

                ${additionalPerformerComponents}
            </div>

            <input type="hidden" name="${fieldName}[${index}].Id" class="id" value="${scientificWork.ReportThemeId || 0}" />
            <input type="hidden" class="themenumber" value="${scientificWork.ThemeNumber}" />
            <input type="hidden" name="${fieldName}[${index}].ThemeOfScientificWorkId" class="themeid" value="${scientificWork.Id}" />
            <input type="hidden" class="code" value="${scientificWork.Code}" />
            <input type="hidden" class="supervisordescription" value="${scientificWork.SupervisorDescription}" />
            <input type="hidden" class="value" value="${scientificWork.Value}" />
        </div>`;
    }

    function getFullTimePerformers(
        fieldName,
        index,
        performerType,
        reportThemeId,
        performersList,
        performersListId) {
        let selectedItemsToHtml = ``;

        if (performersList != undefined && performersList != "") {

            let fullTimePerformers = performersList.split(",")
            let fullTimePerformersId = performersListId.split(",")

            for (let i = 0; i < fullTimePerformers.length; i++) {
                selectedItemsToHtml += `
                <div>
                    <div class="selected-item">
                        <div>
                            <div>${fullTimePerformers[i]}</div>
                            <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                        </div>
                        <input type="hidden" name="${fieldName}[${index}].${performerType}[${i}].Id" class="id" value="${fullTimePerformersId[i]}" />
                        <input type="hidden" name="${fieldName}[${index}].${performerType}[${i}].Name" class="name" value="${fullTimePerformers[i]}" />
                        <input type="hidden" name="${fieldName}[${index}].Id" class="themeId" value="${reportThemeId}" />
                    </div>
                </div>`
            }
        }

        return selectedItemsToHtml;
    }

    function pushSelectedPerformers(performerId, themeIndex, selectedPerformers) {
        const performersFullTime = $(`${performerId}-${themeIndex} .selected-item`);
        let tmpPerformersList = [];
        for (let i = 0; i < performersFullTime.length; i++) {
            const performer = {
                Id: $(performersFullTime[i]).find('.id').val(),
                Name: $(performersFullTime[i]).find('.name').val(),
                ThemeId: $(performersFullTime[i]).find('.themeId').val(),
                ThemeIndex: themeIndex
            }

            tmpPerformersList.push(performer);
        }
        selectedPerformers.push(tmpPerformersList);
    }

    function getPerformers() {
        $.ajax('/api/users/getByFacultyAndCathedra?')
            .done(function (users) {
                allUsers = users;
                updatePerformerFullTimeList();
                updatePerformerExternalPartTimeList();
                updatePerformerLawContractList();
            });
    }

    function getPerformerFullTimeSettings(themeIndex) {
        return {
            relatedEntityContainerId: `#performer-full-time-related-entity-${themeIndex}`,
            selectedItems: selectedPerformersFullTime[themeIndex],
            getFormHtml: getPerformerFullTimeFormHtml,
            postLoadForm: updatePerformerFullTimeList,
            getRelatedEntityFormObject: getPerformerFullTimeFormObject,
            getSelectedRelatedEntityHtml: getSelectedPerformerFullTimeHtml,
            identifierClass: '.id',
            identifierProperty: 'Id',
            idOfIterator: themeIndex
        };
    }

    function getPerformerFullTimeFormHtml() {
        return `
        <div>
            <label class="control-label">Виконавець <span class="text-danger">*</span></label>
            <div><select id="performer-full-time-selector" class="form-control chosen-select"></select></div>
        </div>`;
    }

    function updatePerformerFullTimeList() {
        const performerFullTimeElement = $(`#performer-full-time-selector`);
        if (!performerFullTimeElement.length || performerFullTimeElement.children().length) {
            return;
        }

        let str = "<option value=''>Виберіть виконавця</option>";
        for (var i = 0; i < allUsers.length; i++) {
            let user = allUsers[i];
            str += `<option value='${user.Id}'>${user.FullName}</option>`;
        }

        performerFullTimeElement.html(str);
        performerFullTimeElement.chosen();
        performerFullTimeElement.trigger("chosen:updated");
    }

    function getPerformerFullTimeFormObject(themeIndex) {
        const performerFullTimeId = $(`#performer-full-time-related-entity-${themeIndex} .new-related-entity-form select`).val();
        if (!performerFullTimeId) {
            alert('Виберіть виконавця');
            return;
        }

        const performerFullTimeName = $(`#performer-full-time-related-entity-${themeIndex} .new-related-entity-form select :selected`).text();

        var themeId = document.querySelector(`input[name="ThemeOfScientificWorks[${themeIndex}].Id"]`).value;

        return {
            Id: performerFullTimeId,
            Name: performerFullTimeName,
            ThemeId: themeId,
            ThemeIndex: themeIndex
        };
    }

    function getSelectedPerformerFullTimeHtml(index, performerFullTime) {
        return `<div>
                    <div class="selected-item">
                            <div>
                                ${performerFullTime.Name}
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="ThemeOfScientificWorks[${performerFullTime.ThemeIndex}].ApplicationUserFullTime[${index}].Id" class="id" value="${performerFullTime.Id}" />
                            <input type="hidden" name="ThemeOfScientificWorks[${performerFullTime.ThemeIndex}].ApplicationUserFullTime[${index}].Name" class="name" value="${performerFullTime.Name}" />
                            <input type="hidden" name="ThemeOfScientificWorks[${performerFullTime.ThemeIndex}].Id" class="themeId" value="${performerFullTime.ThemeId}" />
                    </div>
                </div>`;
    }

    function getPerformerExternalPartTimeSettings(themeIndex) {
        return {
            relatedEntityContainerId: `#performer-external-part-time-related-entity-${themeIndex}`,
            selectedItems: selectedPerformersExternalPartTime[themeIndex],
            getFormHtml: getPerformerExternalPartTimeFormHtml,
            postLoadForm: updatePerformerExternalPartTimeList,
            getRelatedEntityFormObject: getPerformerExternalPartTimeFormObject,
            getSelectedRelatedEntityHtml: getSelectedPerformerExternalPartTimeHtml,
            identifierClass: '.id',
            identifierProperty: 'Id',
            idOfIterator: themeIndex
        };
    }

    function getPerformerExternalPartTimeFormHtml() {
        return `
        <div>
            <label class="control-label">Виконавець <span class="text-danger">*</span></label>
            <div><select id="performer-external-part-time-selector" class="form-control chosen-select"></select></div>
        </div>`;
    }

    function updatePerformerExternalPartTimeList() {
        const performerExternalPartTimeElement = $(`#performer-external-part-time-selector`);
        if (!performerExternalPartTimeElement.length || performerExternalPartTimeElement.children().length) {
            return;
        }

        let str = "<option value=''>Виберіть виконавця</option>";
        for (var i = 0; i < allUsers.length; i++) {
            let user = allUsers[i];
            str += `<option value='${user.Id}'>${user.FullName}</option>`;
        }

        performerExternalPartTimeElement.html(str);
        performerExternalPartTimeElement.chosen();
        performerExternalPartTimeElement.trigger("chosen:updated");
    }

    function getPerformerExternalPartTimeFormObject(themeIndex) {
        const performerExternalPartTimeId = $(`#performer-external-part-time-related-entity-${themeIndex} .new-related-entity-form select`).val();
        if (!performerExternalPartTimeId) {
            alert('Виберіть виконавця');
            return;
        }

        const performerExternalPartTimeName = $(`#performer-external-part-time-related-entity-${themeIndex} .new-related-entity-form select :selected`).text();

        var themeId = document.querySelector(`input[name="ThemeOfScientificWorks[${themeIndex}].Id"]`).value;

        return {
            Id: performerExternalPartTimeId,
            Name: performerExternalPartTimeName,
            ThemeId: themeId,
            ThemeIndex: themeIndex
        };
    }

    function getSelectedPerformerExternalPartTimeHtml(index, performerExternalPartTime) {
        return `<div>
                    <div class="selected-item">
                            <div>
                                ${performerExternalPartTime.Name}
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="ThemeOfScientificWorks[${performerExternalPartTime.ThemeIndex}].ApplicationUserExternalPartTime[${index}].Id" class="id" value="${performerExternalPartTime.Id}" />
                            <input type="hidden" name="ThemeOfScientificWorks[${performerExternalPartTime.ThemeIndex}].ApplicationUserExternalPartTime[${index}].Name" class="name" value="${performerExternalPartTime.Name}" />
                            <input type="hidden" name="ThemeOfScientificWorks[${performerExternalPartTime.ThemeIndex}].Id" class="themeId" value="${performerExternalPartTime.ThemeId}" />
                    </div>
                </div>`;
    }

    function getPerformerLawContractSettings(themeIndex) {
        return {
            relatedEntityContainerId: `#performer-law-contract-related-entity-${themeIndex}`,
            selectedItems: selectedPerformersLawContract[themeIndex],
            getFormHtml: getPerformerLawContractFormHtml,
            postLoadForm: updatePerformerLawContractList,
            getRelatedEntityFormObject: getPerformerLawContractFormObject,
            getSelectedRelatedEntityHtml: getSelectedPerformerLawContractHtml,
            identifierClass: '.id',
            identifierProperty: 'Id',
            idOfIterator: themeIndex
        };
    }

    function getPerformerLawContractFormHtml() {
        return `
        <div>
            <label class="control-label">Виконавець <span class="text-danger">*</span></label>
            <div><select id="performer-law-contract-selector" class="form-control chosen-select"></select></div>
        </div>`;
    }

    function updatePerformerLawContractList() {
        const performerLawContractElement = $(`#performer-law-contract-selector`);
        if (!performerLawContractElement.length || performerLawContractElement.children().length) {
            return;
        }

        let str = "<option value=''>Виберіть виконавця</option>";
        for (var i = 0; i < allUsers.length; i++) {
            let user = allUsers[i];
            str += `<option value='${user.Id}'>${user.FullName}</option>`;
        }

        performerLawContractElement.html(str);
        performerLawContractElement.chosen();
        performerLawContractElement.trigger("chosen:updated");
    }

    function getPerformerLawContractFormObject(themeIndex) {
        const performerLawContractId = $(`#performer-law-contract-related-entity-${themeIndex} .new-related-entity-form select`).val();
        if (!performerLawContractId) {
            alert('Виберіть виконавця');
            return;
        }

        const performerLawContractName = $(`#performer-law-contract-related-entity-${themeIndex} .new-related-entity-form select :selected`).text();

        var themeId = document.querySelector(`input[name="ThemeOfScientificWorks[${themeIndex}].Id"]`).value;

        return {
            Id: performerLawContractId,
            Name: performerLawContractName,
            ThemeId: themeId,
            ThemeIndex: themeIndex
        };
    }

    function getSelectedPerformerLawContractHtml(index, performerLawContract) {
        return `<div>
                    <div class="selected-item">
                            <div>
                                ${performerLawContract.Name}
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="ThemeOfScientificWorks[${performerLawContract.ThemeIndex}].ApplicationUserLawContract[${index}].Id" class="id" value="${performerLawContract.Id}" />
                            <input type="hidden" name="ThemeOfScientificWorks[${performerLawContract.ThemeIndex}].ApplicationUserLawContract[${index}].Name" class="name" value="${performerLawContract.Name}" />
                            <input type="hidden" name="ThemeOfScientificWorks[${performerLawContract.ThemeIndex}].Id" class="themeId" value="${performerLawContract.ThemeId}" />
                    </div>
                </div>`;
    }
}());