jQuery.validator.addMethod("identitynovalidation", function (value, element, params) {
    var userIdPropertyName = $(element).attr("data-val-useridproperty");
    var dateOfBirth = $("#" + userIdPropertyName).val(); // Get the DateofBirth value

    // Implement your client-side validation logic here
    // You can use 'value' (the IdentityNo) and 'dateOfBirth' (the DateofBirth) to perform validation

    // Return true if validation passes, false otherwise
    // Example validation:
    // if (valid) {
    //     return true;
    // } else {
    //     return false;
    // }
}, "");

jQuery.validator.unobtrusive.adapters.add("identitynovalidation", [], function (options) {
    options.rules["identitynovalidation"] = true;
    options.messages["identitynovalidation"] = options.message;
});
