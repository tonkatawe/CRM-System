// Cloned elements count
var phoneCounter = 0;
var phoneLimit = 4;
$(".add_Phone").click(function AddPhone() {
    //    Increment the cloned element count
    phoneCounter++;
    if (phoneCounter <= phoneLimit) {
        //     Clone the element and assign it to a variable
        var clone = $("#phonesDiv").clone(true)
            .append($('<a class="deletePhone" href="#"><i class="fas fa-window-close fa-lg" style="color: red"></i></a>'))
            .appendTo("#PhoneContainer");
        //     Modify cloned element, using the counter variable
        clone.find('input').attr('name', "Phones[" + phoneCounter + "].Phone");
        clone.find('input').attr('value', "");
        clone.find('input').attr('placeholder', "Add other phone");
        clone.find('input').attr('id', "Phones_" + phoneCounter + "__.Phone");
        clone.find('span').attr('data-valmsg-for', "Phones[" + phoneCounter + "].Phone");
        clone.find('select').attr('name', "Phones[" + phoneCounter + "].PhoneType");
    }
});
$("body").on('click',
    ".deletePhone",
    function () {
        $(this).closest(".phone_input").remove();
        phoneCounter--; // Modify the counter
    });

var emailCounter = 0;
var emailLimit = 1;
$(".add_Email").click(function AddEmail() {

    emailCounter++;
    if (emailCounter <= emailLimit) {

        var clone = $("#emailsDiv").clone(true)
            .append($('<a class="deleteEmail" href="#"><i class="fas fa-window-close fa-lg" style="color: red"></i></a>'))
            .appendTo("#EmailContainer");

        clone.find('input').attr('name', "Emails[" + emailCounter + "].Email");
        clone.find('input').attr('value', "");
        clone.find('input').attr('placeholder', "Add other email");
        clone.find('input').attr('id', "Emails_" + emailCounter + "__.Email");
        clone.find('span').attr('data-valmsg-for', "Emails[" + emailCounter + "].Email");
        clone.find('select').attr('name', "Emails[" + emailCounter + "].EmailType");
    }
});
$("body").on('click',
    ".deleteEmail",
    function () {
        $(this).closest(".email_input").remove();
        emailCounter--;
    });