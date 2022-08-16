(function () {
    const selectedFinancials = [];
    const minYear = 2010;
    const minAmount = 1;
    $(function () {
        getUsers();
        getSelectedFinancials();
        toggleSubCategories();
        addFinancialEvent();
        cancelFinancialEvent();
        saveFinancialEvent();
        removeFinancialEvent();
    });

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

    function addFinancialEvent() {
        $('.add-financial').click(() => {
            if ($('.new-financial').children().length == 0) {
                $('.new-financial').html(getFinancialContainer());
            }
        });
    }

    function cancelFinancialEvent() {
        $('form').on('click', '.cancel-financial', () => {
            $('.new-financial').html('');
        });
    }

    function saveFinancialEvent() {
        $('form').on('click', '.save-financial', () => {
            const year = $('.new-financial input[name=year]').val();
            if (!validateYear(year)) {
                alert('Введіть коректний рік');
                return;
            }

            const amount = $('.new-financial input[name=amount]').val();
            if (!validateAmount(amount)) {
                alert('Введіть коректну суму');
                return;
            }

            const financial = {
                Id: 0,
                Year: year,
                Amount: amount
            };
            selectedFinancials.push(financial);
            $('.selected-financials').append(getSelectedFinancial(selectedFinancials.length-1, financial));
            $('.new-financial').html('');
        });
    }

    function validateYear(value) {
        return value && +value > minYear;
    }

    function validateAmount(value) {
        return value && +value > minAmount;
    }

    function removeFinancialEvent() {
        $('form').on('click', '.bi-trash', (element) => {
            const container = $(element.currentTarget).closest('.selected-item');
            const id = container.find('.id').val();
            const index = selectedFinancials.findIndex(x => x.Id == id);
            if (index != -1) {
                container.remove();
                selectedFinancials.splice(index, 1);
                renderFinancialList(selectedFinancials);
            }
        });
    }

    function renderFinancialList(financials) {
        const selectedFinancials = $('.selected-financials');
        selectedFinancials.html('');
        for (let i = 0; i < financials.length; i++) {
            selectedFinancials.append(getSelectedFinancial(i, financials[i]));
        }
    };

    function getFinancialContainer() {
        return `<div class="new-financial-form">
                    <div>
                        <label class="control-label">Рік <span class="text-danger">*</span></label>
                        <input class="form-control" type="number" name="year" value="" min="${minYear}"/>
                    </div>
                    <div>
                        <label class="control-label">Сума <span class="text-danger">*</span></label>
                        <input class="form-control" type="number" name="amount" value="" min="${minAmount}" />
                    </div>
                    <div class="financial-buttons">
                        <button class="btn btn-danger cancel-financial" type="button">Відмінити</button>
                        <button class="btn btn-success save-financial" type="button">Зберегти</button>
                    </div>
                </div>`;
    }

    function getSelectedFinancial(index, financial) {
        return `<div>
                    <div class="selected-item">
                        <div>${financial.Year} рік - ${financial.Amount} грн<i class="bi bi-trash text-danger cursor-pointer"></i></div>
                            <input type="hidden" name="ThemeOfScientificWorkFinancials[${index}].Id" class="id" value="${financial.Id}" />
                            <input type="hidden" name="ThemeOfScientificWorkFinancials[${index}].Year" class="year" value="${financial.Year}" />
                            <input type="hidden" name="ThemeOfScientificWorkFinancials[${index}].Amount" class="amount" value="${financial.Amount}" />
                    </div>
                </div>`;
    }
}());