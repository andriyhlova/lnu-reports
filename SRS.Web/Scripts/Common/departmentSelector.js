$(function () {
    let cathedrasSelectOptions = $("#cathedra-selector option");
    let opts = [];
    for (let i = 0; i < cathedrasSelectOptions.length; i++) {
        opts.push({
            value: cathedrasSelectOptions[i].value,
            faculty: cathedrasSelectOptions[i].dataset.faculty,
            text: cathedrasSelectOptions[i].text,
            selected: cathedrasSelectOptions[i].value == cathedrasSelectOptions.parent().val()
        });
    };
    addFacultyPickerListener(opts);

    let usersSelectOptions = $("#user-selector option");
    if (usersSelectOptions && usersSelectOptions.length) {
        var optsUser = [];
        for (var i = 0; i < usersSelectOptions.length; i++) {
            if (usersSelectOptions[i].value) {
                optsUser.push({
                    value: usersSelectOptions[i].value,
                    faculty: usersSelectOptions[i].dataset.faculty,
                    cathedra: usersSelectOptions[i].dataset.cathedra,
                    text: usersSelectOptions[i].text,
                    selected: usersSelectOptions[i].value == usersSelectOptions.parent().val()
                });
            }
        }
        addCathedraPickerListener(optsUser);
    }
});

function addFacultyPickerListener(opts) {
    let facultyElem = $("#faculty-selector");
    $('#faculty-selector').change(function (e) {
        var str = "<option value=''>Виберіть кафедру</option>";
        for (var i = 0; i < opts.length; i++) {
            if (facultyElem.val() == '' || opts[i].faculty == facultyElem.val()) {
                str += `<option value='${opts[i].value}' data-faculty='${opts[i].faculty}' ${opts[i].selected ? 'selected' : ''}>${opts[i].text}</option>`;
            }
        }
        $("#cathedra-selector").html(str);
        $("#cathedra-selector").trigger("chosen:updated");
        $('#cathedra-selector').change();
    });

    $('#faculty-selector').change();
};

function addCathedraPickerListener(optsUser) {
    let cathedraElem = $("#cathedra-selector");
    let facultyElem = $("#faculty-selector");
    $('#cathedra-selector').change(function (e) {
        var str = "<option value=''>Виберіть користувача</option>";
        for (var i = 0; i < optsUser.length; i++) {
            if ((cathedraElem.val() == '' && (facultyElem.val() == '' || optsUser[i].faculty == facultyElem.val())) || optsUser[i].cathedra == cathedraElem.val()) {
                str += `<option value='${optsUser[i].value}' data-faculty='${optsUser[i].faculty}' data-cathedra='${optsUser[i].cathedra}' ${optsUser[i].selected ? 'selected' : ''}>${optsUser[i].text}</option>`;
            }
        }
        $("#user-selector").html(str);
        $("#user-selector").trigger("chosen:updated");
    });

    $('#cathedra-selector').change();
};