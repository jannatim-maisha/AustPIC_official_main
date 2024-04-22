let dialog;
$(document).ready(function () {
    const executeModel = {
        paymentID : ""
    };
    bKash.init({
        paymentMode: 'checkout', //fixed value ‘checkout’
            //paymentRequest format: {amount: AMOUNT, intent: INTENT}
            //intent options
            //1) ‘sale’ – immediate transaction (2 API calls)
            //2) ‘authorization’ – deferred transaction (3 API calls)
        paymentRequest: {
            amount: '95', //max two decimal points allowed
            intent: 'sale'
        },
        createRequest: function (request) { //request object is basically the paymentRequest object, automatically pushed by the script in createRequest method
            console.log('=> createRequest (request) :: ');
            console.log(request);
            $.ajax({
                url: ClubWebsiteUrls.MERCHANT_BACKEND_CREATE_API_CALLER_URL,
                type: 'POST',
                contentType: 'application/json',
                success: function (obj) {
                    var data = obj;

                    console.log(data);
                    if (obj && data.paymentID != null) {
                        executeModel.paymentID = data.paymentID;
                        paymentID = data.paymentID;
                        dialog.modal('hide');
                        bKash.create().onSuccess(data); //pass the whole response data in bKash.create().onSucess() method as a parameter
                    }
                    else {
                        bKash.create().onError();
                    }
                },
                error: function () {
                    bKash.create().onError();
                }
            });
        },
        executeRequestOnAuthorization: function () {
            $.ajax({
                url: ClubWebsiteUrls.MERCHANT_BACKEND_EXECUTE_API_CALLER_URL,
                type: 'GET',
                data: {
                    paymentID : paymentID
                },
                cache: false,
                success: function (data) {

                    if (data && data.paymentID != null) {
                        window.location.href = ClubWebsiteUrls.SUCCESSFUL_PAYMENT;//Merchant’s success page
                    } else {
                        bKash.execute().onError();
                        bootbox.alert({
                            title: "Payment Unsuccessful",
                            message: data.errorMessage
                        });
                    }
                },
                error: function () {
                    bKash.execute().onError();
                }
            });
        },
        onClose: function () {
            bootbox.alert({
                title: "Closed",
                message: "User has clicked the close button"
            });
        }
    });
    $('#bKash_button').removeAttr('disabled');
});
function clickPayButton() {
    dialog = bootbox.dialog({
        message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Please wait while bkash payment gateway is loading...</p>',
        closeButton: false
    });
    $("#bKash_button").trigger('click');
};
