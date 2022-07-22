(function () {
    const selectedUsers = [];
    $(function () {
        const searchComponent = new SearchComponent('#user-search', '/api/users/searchAll', getUserSearchResultText, appendUserSearchResultItem);
        searchComponent.load();
        getSelectedUsers();
        $('.selected-users').on('click', '.bi-trash', removeUser)
    });

    function getSelectedUsers() {
        const users = $('.user');
        for (let i = 0; i < users.length; i++) {
            const user = {
                Id: $(users[i]).find('.id').val(),
                FirstName: $(users[i]).find('.firstname').val(),
                LastName: $(users[i]).find('.lastname').val(),
                FathersName: $(users[i]).find('.fathersname').val(),
                FullName: $(users[i]).find('.fullname').text(),
            }

            selectedUsers.push(user);
        }
    }

    function getUserSearchResultText(user) {
        return user.FullName;
    };

    function appendUserSearchResultItem(user) {
        if (!selectedUsers.find(x => x.Id == user.Id)) {
            selectedUsers.push(user);
            renderUserList(selectedUsers);
        }
    };

    function removeUser(element) {
        if (selectedUsers.length == 1) {
            alert('Публікація повинна бути доступна хоча б одному користувачу');
            return;
        }
        
        const container = $(element.currentTarget).closest('.user');
        const id = container.find('.id').val();
        const index = selectedUsers.findIndex(x => x.Id == id);
        if (index != -1) {
            container.remove();
            selectedUsers.splice(index, 1);
            renderUserList(selectedUsers);
        }
    };

    function renderUserList(users) {
        const selectedUsers = $('.selected-users');
        selectedUsers.html('');
        for (let i = 0; i < users.length; i++) {
            selectedUsers.append(getUserHtml(i, users[i]));
        }
    };

    function getUserHtml(index, user) {
        return `<div class="user">
                            <div class="fullname">${user.FullName} <i class="bi bi-trash text-danger cursor-pointer"></i></div>
                            <input type="hidden" name="Users[${index}].Id" class="id" value="${user.Id}" />
                            <input type="hidden" name="Users[${index}].LastName" class="lastname" value="${user.LastName}" />
                            <input type="hidden" name="Users[${index}].FirstName" class="firstname" value="${user.FirstName}" />
                            <input type="hidden" name="Users[${index}].FathersName" class="fathersname" value="${user.FathersName}" />
                        </div>`;
    };
}());