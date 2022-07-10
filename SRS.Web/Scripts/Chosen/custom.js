$(function () {
    updateSelect();
});

function updateSelect() {
    $('.chosen-select').chosen(
        {
            placeholder_text_single: "Виберіть...",
            disable_search_threshold: 10,
            no_results_text: 'Не знайдено'
        }
    );
};