var users = $('#user_code').val();
if (users == "ADMIN") {
    $("#organization_name,#org_code,#edit_org_code,#org_name,#edit_org_name").prop('disabled', false);
}
else {
    $("#organization_name,#org_code,#edit_org_code,#org_name,#edit_org_name").prop('disabled', true);
}