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

function events() {
    $('[id^="signButton"]').on('click', function (ev) {
        const confirm = confirm(`Після підписання ви не будете мати право редагувати звіт.
                                         Лише завідувач кафедри може зняти підпис.
                                         Ви впевнені, що бажаєте підписати звіт?`);
        return confirm;
    });
};

events();