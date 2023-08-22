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

    let usersSelectOptions = $("#user-selector");
    if (usersSelectOptions && usersSelectOptions.length) {
        if (!$("#faculty-selector option").length && !$("#cathedra-selector option").length) {
            queryUsers();
        }
        else {
            addCathedraPickerListener();
        }
    }
});

function addFacultyPickerListener(opts) {
    let facultyElem = $("#faculty-selector");
    $('#faculty-selector').change(function (e) {
        let str = "<option value=''>Виберіть кафедру</option>";
        for (let i = 0; i < opts.length; i++) {
            if (opts[i].faculty && (!facultyElem.val() || opts[i].faculty == facultyElem.val())) {
                str += `<option value='${opts[i].value}' data-faculty='${opts[i].faculty}' ${opts[i].selected ? 'selected' : ''}>${opts[i].text}</option>`;
            }
        }
        $("#cathedra-selector").html(str);
        $("#cathedra-selector").trigger("chosen:updated");
        $('#cathedra-selector').change();
    });

    $('#faculty-selector').change();
};

var userQuery;
function addCathedraPickerListener() {
    let cathedraElem = $("#cathedra-selector");
    let facultyElem = $("#faculty-selector");
    $('#cathedra-selector').change(function (e) {
        if (userQuery) {
            userQuery.abort();
        }

        queryUsers(facultyElem.val(), cathedraElem.val());
    });

    $('#cathedra-selector').change();
};

function queryUsers(faculty, cathedra) {
    userQuery = $.ajax(`/api/users/getByFacultyAndCathedra?facultyId=${faculty || ''}&cathedraId=${cathedra || ''}&isActive=true`)
        .done(function (users) {
            let selectedUser = $("#user-selector").val() || $("#user-selector")[0].dataset.selected;
            updateUserList(users, selectedUser);
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