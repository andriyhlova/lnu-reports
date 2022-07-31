function changeStepPageAndSubmit(index, newIndex) {
    $('[id^="stepIndex"]').each(function () {
        $(this).val(newIndex);
    });
    if (index == 0) {
        $('#updateBudgetThemeForm').submit();
    }
    if (index == 1) {
        $('#updateInWorkThemeForm').submit();
    }
    if (index == 2) {
        $('#updateHospDohovirThemeForm').submit();
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