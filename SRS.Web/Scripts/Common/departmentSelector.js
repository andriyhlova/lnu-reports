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
        $("#cathedra-selector").sel
    });

    $('#faculty-selector').change();
};