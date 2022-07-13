$(function () {
    $('.existing-roles').on('click', '.action-btn.remove', function (e) {
        let container = $(e.target).parent();
        if (container.parent().children().length == 1) {
            alert('Користувач повинен мати хоча б одну роль');
            return;
        }
        container = transformElement(container, false);
        $('.available-roles').append(container);
        e.stopPropagation();
    });
    $('.available-roles').on('click', '.action-btn.add', function (e) {
        let container = $(e.target).parent();
        container = transformElement(container, true);
        $('.existing-roles').append(container);
        e.stopPropagation();
    });
});

function transformElement(el, addNewRole) {
    if (addNewRole) {
        let id = el.attr('data-id');
        el.append(`<input type="hidden" name="RoleIds[]" value="${id}"/>`);
        let actionButton = el.find('.action-btn');
        actionButton.addClass('remove text-danger');
        actionButton.removeClass('add');
        actionButton.removeClass('text-success');
        actionButton.text('Видалити');
    }
    else {
        el.find('input').remove();
        let actionButton = el.find('.action-btn');
        actionButton.addClass('add text-success');
        actionButton.removeClass('remove');
        actionButton.removeClass('text-danger');
        actionButton.text('Додати');
    }

    return el;
};