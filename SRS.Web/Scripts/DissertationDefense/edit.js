(function () {
    $(function () {
        getUsers();
    });

    function getUsers() {
        $.ajax('/api/users/getByFacultyAndCathedra?')
            .done(function (users) {
                let selectedUser = $("#user-selector").val() || $("#user-selector")[0].dataset.selected;
                updateUserList(users, selectedUser, "#user-selector");

                let selectedHead = $("#head-selector").val() || $("#head-selector")[0].dataset.selected;
                updateUserList(users, selectedHead, "#head-selector");
            });
    }

    function updateUserList(users, selectedUser, selectorId) {
        let str = "<option value=''>Виберіть користувача</option>";
        for (var i = 0; i < users.length; i++) {
            let user = users[i];
            str += `<option value='${user.Id}' ${user.Id == selectedUser ? 'selected' : ''}>${[user.LastName, user.FirstName, user.FathersName].join(' ')}</option>`;
        }

        $(selectorId).html(str);
        $(selectorId).trigger("chosen:updated");
    }
}());