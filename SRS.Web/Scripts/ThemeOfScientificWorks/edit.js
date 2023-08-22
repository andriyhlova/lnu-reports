(function () {
    const selectedFinancials = [];
    const selectedCathedras = []
    const minYear = 2010;
    const minAmount = 1;
    let allCathedras = [];
    $(function () {
        getUsers();
        getSelectedFinancials();
        getSelectedCathedras();
        getCathedras();
        financialChange();
        const financialEntityComponent = new RelatedEntityComponent(getFinancialSettings());
        financialEntityComponent.load();
        const cathedraEntityComponent = new RelatedEntityComponent(getCathedraSettings());
        cathedraEntityComponent.load();
    });

    function getUsers() {
        $.ajax('/api/users/getByFacultyAndCathedra?')
            .done(function (users) {
                let selectedUser = $("#user-selector").val() || $("#user-selector")[0].dataset.selected;
                updateUserList(users, selectedUser, "#user-selector");

                let selectedScientificHead = $("#scientific-head-selector").val() || $("#scientific-head-selector")[0].dataset.selected;
                updateUserList(users, selectedScientificHead, "#scientific-head-selector");
            });
    }

    function updateUserList(users, selectedUser) {
        let str = "<option value=''>Виберіть користувача</option>";
        for (var i = 0; i < users.length; i++) {
            let user = users[i];
            str += `<option value='${user.Id}' ${user.Id == selectedUser ? 'selected' : ''}>${[user.LastName, user.FirstName, user.FathersName].join(' ')}</option>`;
        }

        $("#user-selector").html(str);
        $("#user-selector").trigger("chosen:updated");
    }

    function getFinancialSettings() {
        return {
            relatedEntityContainerId: '#financial-related-entity',
            selectedItems: selectedFinancials,
            getFormHtml: getFinancialFormHtml,
            postLoadForm: null,
            getRelatedEntityFormObject: getFinancialFormObject,
            getSelectedRelatedEntityHtml: getSelectedFinancialHtml
        };
    }

    function getSelectedFinancials() {
        const financials = $('#financial-related-entity .selected-item');
        for (let i = 0; i < financials.length; i++) {
            const financial = {
                Id: $(financials[i]).find('.id').val(),
                Year: $(financials[i]).find('.year').val(),
                Amount: $(financials[i]).find('.amount').val()
            }

            selectedFinancials.push(financial);
        }
    }

    function updateUserList(users, selectedUser, userSelector) {
        let str = "<option value=''>Виберіть користувача</option>";
        for (var i = 0; i < users.length; i++) {
            let user = users[i];
            str += `<option value='${user.Id}' ${user.Id == selectedUser ? 'selected' : ''}>${[user.LastName, user.FirstName, user.FathersName].join(' ')}</option>`;
        }

        $(userSelector).html(str);
        $(userSelector).trigger("chosen:updated");
    }

    function financialChange() {
        $('#Financial').change((e) => {
            let financial = $(e.target).val();
            //if (financial == 0) {
            //    $('.subcategory-container').show();
            //    $('.subcategory-container select').prop('disabled', false);
            //}
            //else {
            //    $('.subcategory-container').hide();
            //    $('.subcategory-container select').prop('disabled', true);
            //}

            if (financial == 9) {
                $('.other-project-type-container').show();
                $('.other-project-type-container input').prop('disabled', false);
            }
            else {
                $('.other-project-type-container').hide();
                $('.other-project-type-container input').prop('disabled', true);
            }
        });
        $('#Financial').change();
    }

    function getFinancialFormObject() {
        const year = $('#financial-related-entity .new-related-entity-form input[name=year]').val();
        if (!validateYear(year)) {
            alert('Введіть коректний рік');
            return;
        }

        const amount = $('#financial-related-entity .new-related-entity-form input[name=amount]').val();
        if (!validateAmount(amount)) {
            alert('Введіть коректну суму');
            return;
        }

        return {
            Id: 0,
            Year: year,
            Amount: amount
        };
    }

    function validateYear(value) {
        return value && +value > minYear;
    }

    function validateAmount(value) {
        return value && +value > minAmount;
    }

    function getFinancialFormHtml() {
        return `<div>
                        <label class="control-label">Рік <span class="text-danger">*</span></label>
                        <input class="form-control" type="number" name="year" value="" min="${minYear}"/>
                    </div>
                    <div>
                        <label class="control-label">Сума <span class="text-danger">*</span></label>
                        <input class="form-control" type="number" name="amount" value="" min="${minAmount}" />
                    </div>`;
    }

    function getSelectedFinancialHtml(index, financial) {
        return `<div>
                    <div class="selected-item">
                        <div>${financial.Year} рік - ${financial.Amount} грн<i class="bi bi-file-x-fill text-danger cursor-pointer"></i></div>
                            <input type="hidden" name="ThemeOfScientificWorkFinancials[${index}].Id" class="id" value="${financial.Id}" />
                            <input type="hidden" name="ThemeOfScientificWorkFinancials[${index}].Year" class="year" value="${financial.Year}" />
                            <input type="hidden" name="ThemeOfScientificWorkFinancials[${index}].Amount" class="amount" value="${financial.Amount}" />
                    </div>
                </div>`;
    }

    function getCathedraSettings() {
        return {
            relatedEntityContainerId: '#cathedra-related-entity',
            selectedItems: selectedCathedras,
            getFormHtml: getCathedraFormHtml,
            postLoadForm: updateCathedraList,
            getRelatedEntityFormObject: getCathedraFormObject,
            getSelectedRelatedEntityHtml: getSelectedCathedraHtml,
            identifierClass: '.cathedraId',
            identifierProperty: 'CathedraId'
        };
    }

    function getSelectedCathedras() {
        const cathedras = $('#cathedra-related-entity .selected-item');
        for (let i = 0; i < cathedras.length; i++) {
            const cathedra = {
                Id: $(cathedras[i]).find('.id').val(),
                CathedraId: $(cathedras[i]).find('.cathedraId').val(),
                CathedraName: $(cathedras[i]).find('.cathedraName').val(),
                ThemeOfScientificWorkId: $(cathedras[i]).find('.themeOfScientificWorkId').val()
            }

            selectedCathedras.push(cathedra);
        }
    }

    function getCathedras() {
        $.ajax('/api/cathedrasapi/getAll')
            .done(function (cathedras) {
                allCathedras = cathedras;
                updateCathedraList();
            });
    }

    function getCathedraFormHtml() {
        return `<div>
                        <label class="control-label">Кафедра <span class="text-danger">*</span></label>
                        <div><select id="cathedra-selector" class="form-control chosen-select"></select></div>
                    </div>`;
    }

    function updateCathedraList() {
        const cathedraElement = $("#cathedra-selector");
        if (!cathedraElement.length || cathedraElement.children().length) {
            return;
        }

        let str = '';
        for (var i = 0; i < allCathedras.length; i++) {
            let cathedra = allCathedras[i];
            str += `<option value='${cathedra.Id}'>${cathedra.Name}</option>`;
        }

        cathedraElement.html(str);
        cathedraElement.chosen();
        cathedraElement.trigger("chosen:updated");
    }

    function getCathedraFormObject() {
        const cathedraId = $('#cathedra-related-entity .new-related-entity-form select').val();
        if (!cathedraId) {
            alert('Виберіть кафедру');
            return;
        }

        const cathedraName = $('#cathedra-related-entity .new-related-entity-form select :selected').text();
        const themeOfScientificWorkId = $('input[name=Id]').val();
        return {
            Id: 0,
            CathedraId: cathedraId,
            CathedraName: cathedraName,
            ThemeOfScientificWorkId: themeOfScientificWorkId
        };
    }

    function getSelectedCathedraHtml(index, cathedra) {
        return `<div>
                    <div class="selected-item">
                            <div>
                                <div>${cathedra.CathedraName}</div>
                                <i class="bi bi-file-x-fill text-danger cursor-pointer"></i>
                            </div>
                            <input type="hidden" name="ThemeOfScientificWorkCathedras[${index}].Id" class="id" value="${cathedra.Id}" />
                            <input type="hidden" name="ThemeOfScientificWorkCathedras[${index}].ThemeOfScientificWorkId" class="themeOfScientificWorkId" value="${cathedra.Id}" />
                            <input type="hidden" name="ThemeOfScientificWorkCathedras[${index}].CathedraId" class="cathedraId" value="${cathedra.CathedraId}" />
                            <input type="hidden" name="ThemeOfScientificWorkCathedras[${index}].CathedraName" class="cathedraName" value="${cathedra.CathedraName}" />
                    </div>
                </div>`;
    }
}());