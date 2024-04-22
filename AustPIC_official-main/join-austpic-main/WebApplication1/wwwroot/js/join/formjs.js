/**
 * Define a function to navigate betweens form steps.
 * It accepts one parameter. That is - step number.
 */

var model;
const navigateToFormStep = (stepNumber) => {
    /**
     * Hide all form steps.
     */
	 //return;
    document.querySelectorAll(".form-step").forEach((formStepElement) => {
        formStepElement.classList.add("d-none");
    });
    /**
     * Mark all form steps as unfinished.
     */
    document.querySelectorAll(".form-stepper-list").forEach((formStepHeader) => {
        formStepHeader.classList.add("form-stepper-unfinished");
        formStepHeader.classList.remove("form-stepper-active", "form-stepper-completed");
    });
    /**
     * Show the current form step (as passed to the function).
     */
    document.querySelector("#step-" + stepNumber).classList.remove("d-none");
    /**
     * Select the form step circle (progress bar).
     */
    const formStepCircle = document.querySelector('li[step="' + stepNumber + '"]');
    /**
     * Mark the current form step as active.
     */
    formStepCircle.classList.remove("form-stepper-unfinished", "form-stepper-completed");
    formStepCircle.classList.add("form-stepper-active");
    /**
     * Loop through each form step circles.
     * This loop will continue up to the current step number.
     * Example: If the current step is 3,
     * then the loop will perform operations for step 1 and 2.
     */
    for (let index = 0; index < stepNumber; index++) {
        /**
         * Select the form step circle (progress bar).
         */
        const formStepCircle = document.querySelector('li[step="' + index + '"]');
        /**
         * Check if the element exist. If yes, then proceed.
         */
        if (formStepCircle) {
            /**
             * Mark the form step as completed.
             */
            formStepCircle.classList.remove("form-stepper-unfinished", "form-stepper-active");
            formStepCircle.classList.add("form-stepper-completed");
        }
    }
};
/**
 * Select all form navigation buttons, and loop through them.
 */
/*document.querySelectorAll(".btn-navigate-form-step").forEach((formNavigationBtn) => {
    /**
     * Add a click event listener to the button.
     
    formNavigationBtn.addEventListener("click", () => {
        /**
         * Get the value of the step.
         
        const stepNumber = parseInt(formNavigationBtn.getAttribute("step_number"));
        /**
         * Call the function to navigate to the target form step.
         
        navigateToFormStep(stepNumber);
    });
});*/

function firstNext()
{
	if(firstStepValidation()==true)
	{
		navigateToFormStep(2);
	}
};
function secondNext()
{
    if (firstStepValidation() == false) {
        navigateToFormStep(1);
    }
	else if(secondStepValidation()==true)
	{
		navigateToFormStep(3);
	}
};
function thirdPrev()
{
	navigateToFormStep(2);
};
function secondPrev()
{
	navigateToFormStep(1);
};
function validateFileType(){
    var fileName = document.getElementById("picture").value;
    if (fileName.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Picture is empty!"
        });
        document.getElementById("picture").value = "";
        return false;
    }
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
        return true;
    }
    else
    {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Only jpg/jpeg and png files are allowed!"
        });
        document.getElementById("picture").value = "";
        return false;
    }   
};

function firstStepValidation() {
    //return true;
	///^([a-zA-Z]+\s)*[a-zA-Z]+$/
    let name = document.getElementById("name").value;
    let email = document.getElementById("email").value;
    let phone = document.getElementById("phone_number").value;
	if(nameValidation(name) && emailValidation(email) && numberValidation(phone))
        return true;
    return false;
	
};
function secondStepValidation() {
    //return true;
    let studentId  = document.getElementById("student_id").value;
    if(studentIdValidation(studentId)) return true;
	return false;
};
function thirdStepValidation() {
    //return true;
    let birthDate = document.getElementById("birth-date").value;
    return birthDateValidation(birthDate) && validateFileType();
};
function nameValidation(name)
{
    let pattern = /^([a-zA-Z]+\s)*[a-zA-Z]+$/;
    if (name.match(pattern)) return true;
    else if (name.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Name is empty"
        });
    }
    else {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Name accepts only words and space between them"
        });
        return false;
    }
}
function emailValidation(email)
{
    const tester = /^[-!#$%&'*+\/0-9=?A-Z^_a-z`{|}~](\.?[-!#$%&'*+\/0-9=?A-Z^_a-z`{|}~])*@[a-zA-Z0-9](-*\.?[a-zA-Z0-9])*\.[a-zA-Z](-?[a-zA-Z0-9])+$/;
    if (!email) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Email is empty"
        });
        return false;
    }
    const emailParts = email.split('@');
    if (emailParts.length !== 2) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    const account = emailParts[0];
    const address = emailParts[1];
    if (account.length > 64) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    else if (address.length > 255) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    const domainParts = address.split('.');
    if (domainParts.some(function (part) {
        return part.length > 63;
    })) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    if (!tester.test(email)) return false;
        return true;
};
function numberValidation(number)
{
    const pattern = /^01[3-9]\d{8}$/;
    if (number.match(pattern))
        return true;
    else if (number.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Mobile Number is Empty"
        });
    }
    else if (number.length != 11) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Number must contain 11 digits"
        });
    }
    else {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Number not valid"
        });
    }
    return false;
};
function studentIdValidation(Id)
{
    const pattern = /^(\d{9}|\d{11})$/;
    if(Id.match(pattern))
        return true;
    else if (Id.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Student Id is Empty"
        });
        return false;
    }
    bootbox.alert({
        title: "<b>Error</b>",
        message: "Not a valid Student Id"
    });
    return false;
};
function birthDateValidation(dateString) {
    if (dateString.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is Empty"
        });
        return false;
    }
    const pattern = /^\d{4}\-\d{1,2}\-\d{1,2}$/;
    if (!dateString.match(pattern)) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is not valid"
        });
        return false;
    }
    var parts = dateString.split("-");
    var day = parseInt(parts[2], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[0], 10);

    if (year < 1985 || year > 2020 || month == 0 || month > 12) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is out of range"
        });
        return false;
    }

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    if ((day > 0 && day <= monthLength[month - 1])==false) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is not valid"
        });
        return false;
    }
    return true;
    
};

function finalSubmit() {

    if (!firstStepValidation() || !secondStepValidation() || !thirdStepValidation())
        return false;
    model = new FormData($('#userAccountSetupForm').get(0));
    let verificationDialog = bootbox.dialog({
        message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Verifying Your Information...</p>',
        closeButton: false
    })
    .on('shown.bs.modal', function () {
        $.ajax({
            url: ClubWebsiteUrls.CHECK_NEW_MEMBER_INFO,
            type: "POST",
            cache: false,
            contentType: false,
            processData: false,
            data: model,

            success: function (response) {
                verificationDialog.modal('hide');
                if (response.status) {
                    window.location.href = ClubWebsiteUrls.PAYMENT_PAGE;
                }
                else if (!response.status) {
                    bootbox.alert({
                        title: "<b>Error</b>",
                        message: response.message
                    });
                }
                else {
                    bootbox.alert({
                        title: "<b>Error</b>",
                        message: "An Unknown Error Occured"
                    });
                }
            },
            error: function (err) {
                verificationDialog.modal('hide');
                bootbox.alert({
                    title: "<b>Error</b>",
                    message: "An Unknown Error Occured"
                });
            }
        });
    });
};


