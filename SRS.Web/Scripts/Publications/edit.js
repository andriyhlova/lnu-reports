(function () {
    const selectedUsers = [];
    let journalSearchComponent;
    let selectedJournal = null;

    $(function () {
        const searchComponent = new SearchComponent('#user-search', '/api/users/searchAll', getUserSearchResultText, appendUserSearchResultItem);
        searchComponent.load();
        getSelectedUsers();
        $('.selected-users').on('click', '.bi-file-x-fill', removeUser);
        journalSelectorSetup();
        publicationTypeChanged();
        languageChanged();
        const availableFieldsComponent = new AvailableFieldsComponent('select[name=PublicationType]', 'types', separator);
        availableFieldsComponent.load();
    });

    function publicationTypeChanged() {
        $('select[name=PublicationType]').change((e) => {
            onPublicationTypeChanged(e.target.value);
        });
        $('select[name=PublicationType]').change();
    }

    function onPublicationTypeChanged(publicationType) {
        journalSearchComponent.setSearchUrl(getJournalSearchUrl(publicationType));
        refreshSelectedJournalInfo(publicationType);
    }

    function languageChanged() {
        $('select[name=Language]').change((e) => {
            if (e.target.value == otherLaguageType) {
                $('.other-language-field').show();
                $('input[name=OtherLanguage]').prop('disabled', false);
            }
            else {
                $('.other-language-field').hide();
                $('input[name=OtherLanguage]').prop('disabled', true);
            }
        });
        $('select[name=Language]').change();
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

    function getJournalSearchUrl(publicationType) {
        return `/api/journalsapi/getByPublicationType?publicationType=${publicationType || ''}`;
    }

    function getJournalSearchResultText(journal) {
        return journal.Name;
    }

    function journalSelectorSetup() {
        const initialId = $('#journal-selector').val();
        journalSearchComponent = new SearchComponent('#journal-search', getJournalSearchUrl($('select[name=PublicationType]').val()), getJournalSearchResultText, selectJournal);
        journalSearchComponent.load();
        $('.selected-journal').on('click', '.bi-file-x-fill', removeJournal);

        if (initialId) {
            loadJournalById(initialId, $('select[name=PublicationType]').val());
        }
    }

    function selectJournal(journal) {
        selectedJournal = journal;
        $('#journal-selector').val(journal.Id);
        renderSelectedJournal();
        showJournalQuartile();
    }

    function removeJournal() {
        selectedJournal = null;
        $('#journal-selector').val('');
        renderSelectedJournal();
        showJournalQuartile();
    }

    function renderSelectedJournal() {
        const container = $('.selected-journal');
        container.html('');
        if (selectedJournal) {
            container.append(getJournalHtml(selectedJournal));
        }
    }

    function getJournalHtml(journal) {
        return `<div class="selected-item">
                    <div class="name">${getJournalSearchResultText(journal)} <i class="bi bi-file-x-fill text-danger cursor-pointer"></i></div>
                </div>`;
                }

    function loadJournalById(id, publicationType) {
        $.ajax(`/api/journalsapi/getById?id=${id}&publicationType=${publicationType || ''}`)
            .done(function (journal) {
                if (journal) {
                    selectedJournal = journal;
                    renderSelectedJournal();
                showJournalQuartile();
                }
            });
    }

    function refreshSelectedJournalInfo(publicationType) {
        if (selectedJournal) {
            loadJournalById(selectedJournal.Id, publicationType);
        }
    }

    function showJournalQuartile() {
        const info = $('#journal-quartile-info');
        info.removeClass('text-success text-danger');
        if (!selectedJournal) {
            info.text('');
            return;
        }

        let text = `Квартиль: ${selectedJournal.Quartile}`;
        if (selectedJournal.Matches === true) {
            text += ' — відповідає типу статті';
            info.addClass('text-success');
        }
        else if (selectedJournal.Matches === false) {
            text += ' — не відповідає типу статті';
            info.addClass('text-danger');
        }

        info.text(text);
    }
}());