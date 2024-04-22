var loadingDialog;
var PaymentID;
function clickPayButton() {
    loadingDialog = bootbox.dialog({
        message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Please wait while bkash payment gateway is loading...</p>',
        closeButton: false
    })
    .on('shown.bs.modal', function () {
        $("#bKash_button").trigger('click');
    });
};
$(document).ready(function () {
    bKash.init({
        paymentMode: 'checkout', //fixed value ‘checkout’

        paymentRequest: {
            amount: PAYMENT_FEE, //max two decimal points allowed
            intent: 'sale'
        },
        createRequest: function (request) {
            $.ajax({
                url: ClubWebsiteUrls.MERCHANT_BACKEND_CREATE_API_CALLER_URL,
                type: 'POST',
                contentType: 'application/json',
                headers: {
                    'XSRF-TOKEN-Join-AUSTPIC-App': $('input[name="XSRF-TOKEN-Join-AUSTPIC-App"]').val()
                },
                success: function (obj) {
                    var data = obj;

                    if (obj && data.paymentID != null) {
                        PaymentID = data.paymentID;
                        loadingDialog.modal('hide');
                        bKash.create().onSuccess(data);
                    }
                    else {
                        loadingDialog.modal('hide');
                        bKash.create().onError();
                        bootbox.alert({
                            title: "Payment Unsuccessful",
                            message: data.errorMessage
                        });
                    }
                },
                error: function () {
                    loadingDialog.modal('hide');
                    bKash.create().onError();
                    bootbox.alert({
                        title: "Payment Unsuccessful",
                        message: data.errorMessage
                    });
                }
            });
        },
        executeRequestOnAuthorization: function () {
            $.ajax({
                url: ClubWebsiteUrls.MERCHANT_BACKEND_EXECUTE_API_CALLER_URL,
                type: "GET",
                cache: false,
                headers: {
                    'XSRF-TOKEN-Join-AUSTPIC-App': $('input[name="XSRF-TOKEN-Join-AUSTPIC-App"]').val()
                },
                data: {
                    PaymentID: PaymentID
                },
                success: function (data) {

                    if (data && data.paymentID != null) {
                        loadingDialog.modal('hide');
                        window.location.href = ClubWebsiteUrls.SUCCESSFUL_PAYMENT;
                    } else {
                        bKash.execute().onError();
                        loadingDialog.modal('hide');
                        bootbox.alert({
                            title: "Payment Unsuccessful",
                            message: data.errorMessage
                        });
                    }
                },
                error: function () {
                    bKash.execute().onError();
                    loadingDialog.modal('hide');
                    bootbox.alert({
                        title: "Payment Unsuccessful",
                        message: data.errorMessage
                    });
                }
            });
        },
        onClose: function () {
            bootbox.alert({
                title: "Failed",
                message: "User cancelled the transaction"
            });
        }
    });
    $('#bKash_button').removeAttr('disabled');
});