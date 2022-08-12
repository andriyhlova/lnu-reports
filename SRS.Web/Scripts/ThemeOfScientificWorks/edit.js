(function () {
    $(function () {
        getUsers();
        toggleSubCategories();
    });

    function getUsers() {
        $.ajax('/api/users/getByFacultyAndCathedra')
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

    function toggleSubCategories() {
        $('#Financial').change((e) => {
            let financial = $(e.target).val();
            if (financial == 3) {
                $('.subcategory-container').show();
            }
            else {
                $('.subcategory-container').hide();
            }
        });
        $('#Financial').change();
    }
}());