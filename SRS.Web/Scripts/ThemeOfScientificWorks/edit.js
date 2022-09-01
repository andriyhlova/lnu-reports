(function () {
    const selectedFinancials = [];
    const minYear = 2010;
    const minAmount = 1;
    $(function () {
        getUsers();
        getSelectedFinancials();
        toggleSubCategories();
        const financialEntityComponent = new RelatedEntityComponent(getSettings());
        financialEntityComponent.load();
    });

    function getSettings() {
        return {
            relatedEntityContainerId: '#financial-related-entity',
            selectedItems: selectedFinancials,
            getFormHtml: getFinancialFormHtml,
            postLoadForm: null,
            getRelatedEntityFormObject: getFinancialFormObject,
            getSelectedRelatedEntityHtml: getSelectedFinancialHtml
        };
    }

    function getUsers() {
        $.ajax('/api/users/getByFacultyAndCathedra')
            .done(function (users) {
                let selectedUser = $("#user-selector").val() || $("#user-selector")[0].dataset.selected;
                updateUserList(users, selectedUser);
            });
    }

    function getSelectedFinancials() {
        const financials = $('.selected-item');
        for (let i = 0; i < financials.length; i++) {
            const financial = {
                Id: $(financials[i]).find('.id').val(),
                Year: $(financials[i]).find('.year').val(),
                Amount: $(financials[i]).find('.amount').val()
            }

            selectedFinancials.push(financial);
        }
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

    function toggleSubCategories() {
        $('#Financial').change((e) => {
            let financial = $(e.target).val();
            if (financial == 3) {
                $('.subcategory-container').show();
                $('.subcategory-container select').prop('disabled', false);
            }
            else {
                $('.subcategory-container').hide();
                $('.subcategory-container select').prop('disabled', true);
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
}());