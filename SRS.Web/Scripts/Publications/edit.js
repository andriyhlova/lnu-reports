(function () {
    const selectedUsers = [];
    $(function () {
        const searchComponent = new SearchComponent('#user-search', '/api/users/searchAll', getUserSearchResultText, appendUserSearchResultItem);
        searchComponent.load();
        getSelectedUsers();
        $('.selected-users').on('click', '.bi-file-x-fill', removeUser);
        publicationTypeChanged();

        const availableFieldsComponent = new AvailableFieldsComponent('select[name=PublicationType]', 'types', separator);
        availableFieldsComponent.load();
    });

    function publicationTypeChanged() {
        $('select[name=PublicationType]').change((e) => {
            fillJournals(e.target.value);
        });
        $('select[name=PublicationType]').change();
    }

    function getSelectedUsers() {
        const users = $('.initial-user');
        for (let i = 0; i < users.length; i++) {
            const user = {
                Id: $(users[i])[0].dataset.id,
                FirstName: $(users[i])[0].dataset.firstname,
                LastName: $(users[i])[0].dataset.lastname,
                FathersName: $(users[i])[0].dataset.fathersname,
                FullName: $(users[i])[0].dataset.fullname,
            }

            selectedUsers.push(user);
        }

        renderUserList(selectedUsers);
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
        return `<div class="selected-item user">
                            <div class="fullname">${getUserSearchResultText(user)} <i class="bi bi-file-x-fill text-danger cursor-pointer"></i></div>
                            <input type="hidden" name="Users[${index}].Id" class="id" value="${user.Id}" />
                            <input type="hidden" name="Users[${index}].LastName" class="lastname" value="${user.LastName}" />
                            <input type="hidden" name="Users[${index}].FirstName" class="firstname" value="${user.FirstName}" />
                            <input type="hidden" name="Users[${index}].FathersName" class="fathersname" value="${user.FathersName}" />
                        </div>`;
    };

    function fillJournals(publicationType) {
        let selectedJournal = $('#journal-selector').val() || $('#journal-selector')[0].dataset.selected;
        $.ajax(`/api/journalsapi/getByPublicationType?publicationType=${publicationType}`)
            .done(function (journals) {
                let str = "<option value=''>Виберіть журнал</option>";
                for (var i = 0; i < journals.length; i++) {
                    let journal = journals[i];
                    str += `<option value='${journal.Id}' ${journal.Id == selectedJournal ? 'selected' : ''}>${journal.Name}</option>`;
                }

                $("#journal-selector").html(str);
                $("#journal-selector").trigger("chosen:updated");
            });
    }
}());