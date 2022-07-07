$(function () {
    $("#chkShowPassword").bind("click", function () {
        var txtPassword = $("#Password");
        if ($(this).is(":checked")) {
            txtPassword.after('<input class = "form-control" onchange = "PasswordChanged(this);" id = "txt_' + txtPassword.attr("id") + '" type = "text" value = "' + txtPassword.val() + '" />');
            txtPassword.hide();
        } else {
            txtPassword.val(txtPassword.next().val());
            txtPassword.next().remove();
            txtPassword.show();
        }
    });
});

function PasswordChanged(txt) {
    $(txt).prev().val($(txt).val());
};