$(function () {
    let cathedrasSelectOptions = document.getElementById("cathedra-selector").options;
    let opts = [];
    for (let i = 0; i < cathedrasSelectOptions.length; i++) {
        opts.push({ value: cathedrasSelectOptions[i].value, faculty: cathedrasSelectOptions[i].dataset.faculty, text: cathedrasSelectOptions[i].text });
    };
    addFacultyPickerListener(opts);
});

function addFacultyPickerListener(opts) {
    let facultyElem = document.getElementById("faculty-selector");
    $('#faculty-selector').change(function (e) {
        var str = "<option value=''>Виберіть кафедру</option>";
        for (var i = 0; i < opts.length; i++) {
            if (facultyElem.value == '' || opts[i].faculty == facultyElem.value) {
                str += `<option value='${opts[i].value}' data-faculty='${opts[i].faculty}'>${opts[i].text}</option>`;
            }
        }
        $("#cathedra-selector").html(str);
        $("#cathedra-selector").trigger("chosen:updated");
    });
};