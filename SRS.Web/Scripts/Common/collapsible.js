$(document).ready(function () {
    var collapsed = true;
    $('.collapsible').click(function (e) {
        let target = $(e.target).data('target');
        if (collapsed) {
            $(target).css('visibility', 'visible');
            $(target).css('height', 'auto');
        }
        else {
            $(target).css('visibility', 'hidden');
            $(target).css('height', '0px');
        }

        collapsed = !collapsed;
        $(e.target).blur();
    });
});