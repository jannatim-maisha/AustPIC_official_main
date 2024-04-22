// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

unlayer.init({
    id: 'editor-container',
    projectId: 1234,
    displayMode: 'email'
});

//var design = {
//    "body": {
//        "values": {
//            "backgroundColor": "#ffffff",
//            "contentAlign": "left",
//            "contentWidth": "850"
//        }
//    }
//};

//unlayer.loadDesign(design);

unlayer.loadBlank({
    backgroundColor: "#ffffff",
    contentAlign: "left",
    contentWidth: "850px",
    fontFamily: {
        label: "Helvetica",
        value: "'Helvetica Neue', Helvetica, Arial, sans-serif"
    }
});

unlayer.addEventListener('design:updated', function (updates) {
    unlayer.exportHtml(function (data) {
        var json = data.design; // design json
        var html = data.html; // design html
        var BlogBody = data.chunks.body;
        $('#unlayer-html').val(BlogBody);

        const tempDiv = document.createElement('div');
        tempDiv.innerHTML = BlogBody;
        let extractedText = tempDiv.textContent.replace(/\n/g, '').replace(/\s+/g, ' ').trim();

        const maxLength = 1000;
        if (extractedText.length > maxLength) {
            extractedText = extractedText.substring(0, maxLength);
        }
        console.log(extractedText);
        query({ "inputs": extractedText }).then((response) => {
            console.log(JSON.stringify(response));

            let negativeScore;
            let positiveScore;
            let neutralScore;
            let labelResult = response[0][0].label;

            //console.log("Sentiment Analysis Result:", result[0]);

            for (let i = 0; i < response[0].length; i++) {
                if (response[0][i].label === "negative") {
                    negativeScore = response[0][i].score;
                } else if (response[0][i].label === "positive") {
                    positiveScore = response[0][i].score;
                } else if (response[0][i].label === "neutral") {
                    neutralScore = response[0][i].score;
                }
            }

            console.log("Result:", labelResult);
            console.log("Neutral Score:", neutralScore);
            console.log("Positive Score:", positiveScore);
            console.log("Negative Score:", negativeScore);

            if (labelResult === "negative" || negativeScore >= 0.5) {
                $('#sclass').val("negative");                       
            } else {
                $('#sclass').val(labelResult);
            }
            console.log("sclass:", $('#sclass').val());
        });
    })
})

function onComplete(response) {
    //alert("Ajax Request Complete");
    console.log(response);
}
//function onsuccess(response) {
//    toastr.options = {
//        positionclass: "toast-bottom-center",
//        preventduplicates: true,
//        progressbar: true,
//        onhidden: function () {
//            window.location.href = '/blog/index';
//        }
//    };
//    toastr.success("Blog Post Successful! Redirecting...");
//}

async function query(data) {
    const response = await fetch(
        "https://api-inference.huggingface.co/models/ProsusAI/finbert",
        {
            headers: { Authorization: "Bearer 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" },
            method: "POST",
            body: JSON.stringify(data),
        }
    );
    const result = await response.json();
    return result;
}

//query({ "inputs": "I like you. I love you" }).then((response) => {
//    console.log(JSON.stringify(response));
//});

//async function classifyBlogSentiment(blogBody) {

//    console.log(blogBody);

//    const tempDiv = document.createElement('div');
//    tempDiv.innerHTML = blogBody;
//    let extractedText = tempDiv.textContent.replace(/\n/g, '').replace(/\s+/g, ' ').trim();

//    console.log(extractedText);

//    const maxLength = 2300;
//    if (extractedText.length > maxLength) {
//        extractedText = extractedText.substring(0, maxLength);
//    }

//    console.log(extractedText);

//    const data = {
//        "inputs": extractedText
//    };

//    console.log(data);

//    try {
//        const result = await query(data);

//        let negativeScore;
//        let positiveScore;
//        let neutralScore;
//        let labelResult = result[0][0].label;

//        console.log("Sentiment Analysis Result:", result[0]);

//        for (let i = 0; i < result[0].length; i++) {
//            if (result[0][i].label === "negative") {
//                negativeScore = result[0][i].score;
//            } else if (result[0][i].label === "positive") {
//                positiveScore = result[0][i].score;
//            } else if (result[0][i].label === "neutral") {
//                neutralScore = result[0][i].score;
//            }
//        }

//        console.log("Result:", labelResult);
        
//        console.log("Neutral Score:", neutralScore);
//        console.log("Positive Score:", positiveScore);
//        console.log("Negative Score:", negativeScore);

//        const neutralScore = result[0][0].label === "neutral" ? result[0][0].score : null;
//        console.log("Neutral Label Score:", neutralScore);

//        return { labelResult, neutralScore, positiveScore, negativeScore };

//    } catch (error) {
//        console.error("Error during sentiment analysis:", error);

//         Check if the error is due to model loading
//        if (error.error === "Model ProsusAI/finbert is currently loading") {
//            const estimatedTime = error.estimated_time || 20; // Default to 20 seconds if estimated_time is not provided
//            console.log(`Model loading, retrying in ${estimatedTime} seconds...`);

//             Retry after the estimated time
//            setTimeout(() => {
//                classifyBlogSentiment(blogBody);
//            }, estimatedTime * 1000); // Convert seconds to milliseconds
//        }

//        return null;
//    }
//}

//async function onSuccess(response) {

//    let sentimentAnalysisDone = false;

//    toastr.options = {
//        positionClass: "toast-bottom-center",
//        preventDuplicates: true,
//        progressBar: true,
//        onHidden: async function () {
//            try {
//                if (!sentimentAnalysisDone) {
//                    sentimentAnalysisDone = true;

//                    const { labelResult, neutralScore, positiveScore, negativeScore } = await classifyBlogSentiment($('#unlayer-html').val());

//                    console.log(labelResult);

//                    if (labelResult === "negative" || negativeScore >= 0.5) {
//                        $('#sclass').val(labelResult);
//                        console.log($('#sclass').val());
//                        toastr.warning("Blog blocked by the AI moderator! Please re-check for negative sentiments and Try again.");
//                    } else {
//                        $('#sclass').val(labelResult);
//                        console.log($('#sclass').val());
//                        toastr.success("Blog Post Successful! Redirecting...")
//                        window.location.href = '/Blog/Index';
//                    }
//                }
//            } catch (error) {
//                console.error("Error during sentiment analysis:", error);
//            }
//        }
//    };
//    toastr.success("Wait for moderation...");
//}

async function onSuccess(response) {

    let sentimentAnalysisDone = false;

    toastr.options = {
        positionClass: "toast-bottom-center",
        preventDuplicates: true,
        progressBar: true,
        onHidden: async function () {
            try {
                if (!sentimentAnalysisDone) {
                    sentimentAnalysisDone = true;

                    if ($('#sclass').val() === "negative") {
                        toastr.warning("Blog blocked by the AI moderator! Please re-check for negative sentiments and Try again.");
                    } else {
                        toastr.success("Blog Post Successful! Redirecting...")
                        window.location.href = '/Blog/Index';
                    }
                }
            } catch (error) {
                console.error("Error during sentiment analysis:", error);
            }
        }
    };
    toastr.info("Wait for moderation...");
}

function onFailure(xhr, status, error) {
    toastr.options = {
        positionClass: "toast-bottom-center",
        preventDuplicates: true,
        progressBar: true
    };
    toastr.error("An error occurred while posting the blog. Please try again later.");
    console.error(xhr, status, error);
}

function onCompleteNewsletter(data) {
    console.log(data);
}
function onSuccessNewsletter(data, status, xhr) {
    console.log(xhr.status);
    console.log(xhr.responseText);
    toastr.options = {
        positionClass: "toast-bottom-center",
        preventDuplicates: true,
        progressBar: true
    };
    toastr.success("Thank you for subscribing!");
    console.log(data);
}
function onFailureNewsletter(xhr, status, error) {
    console.log(xhr.status);
    console.log(xhr.responseText);
    toastr.options = {
        positionClass: "toast-bottom-center",
        preventDuplicates: true,
        progressBar: true
    };
    if (xhr.status === 409) {
        toastr.warning("You have already subscribed to our newsletter!");
    } else {
        toastr.error("An error occurred while subscribing to our newsletter. Please try again later.");
    }
    console.error(xhr, status, error);
}

//$('#create-button').on('click', function () {
//    unlayer.exportHtml(function (data) {
//        var BlogBody = data.chunks.body;
//        var BlogTitle = $('#title').val();
//        var BlogAuthor = $('#author').val();
//        var BlogCatergory = $('#catergory').val();
//        var BlogShort = $('#short').val();

//        if (BlogTitle != "" && BlogAuthor != "" && BlogCatergory != "" && BlogShort != "") {
//            var uploadFile = $('#img').get(0);
//            var files = uploadFile.files;
//            var BlogImg = null;

//            if (files.length > 0) {
//                BlogImg = files[0];
//            }

//            var formData = new FormData();
//            formData.append("BlogTitle", BlogTitle);
//            formData.append("BlogAuthor", BlogAuthor);
//            formData.append("BlogCatergory", BlogCatergory);
//            formData.append("BlogShort", BlogShort);
//            formData.append("BlogBody", BlogBody);

//            if (BlogImg != null) {
//                formData.append("img", BlogImg, BlogImg.name);
//            }

//            $.ajax({
//                url: '/Blog/CreateBlog',
//                type: 'POST',
//                data: formData,
//                processData: false,
//                contentType: false,
//                success: function (response) {
//                    alert("Blog Posted");
//                    window.location.href = '/Blog/Index';
//                    console.log(response);
//                },
//                error: function (error) {
//                    alert("Blog Post Failed");
//                    console.log(error);
//                }
//            });
//        } else {
//            alert("One or more input fields are empty.");
//        }
//    });
//});



//$('#create-button').on('click', function () {
//    //unlayer.exportHtml({ includeMetaData: false }, function (data) {
//    unlayer.exportHtml(function (data) {
//        //var BlogBody = data.design; // design json
//        //var BlogBody = data.html; // final html
//        var BlogBody = data.chunks.body;

//        var BlogTitle = $('#title').val();
//        //var BlogDate = $('#date').val();
//        var BlogAuthor = $('#author').val();
//        var BlogCatergory = $('#catergory').val();
//        var BlogShort = $('#short').val();
//        var BlogImg = $('#img').val();

//        console.log(BlogTitle, BlogAuthor, BlogCatergory, BlogShort, BlogImg);

//        //var BlogModel = '@Html.Raw(Json.Encode(AustPIC.Models.BlogModel))';
//        //var obj = new BlogModel(title, date, author, catergory, short, img);

//        //function BlogModel(BlogTitle, BlogAuthor, BlogCatergory, BlogShort, BlogBody, BlogImg, BlogView, BlogId, BlogDate) {
//        //    this.BlogId = BlogId;
//        //    this.BlogTitle = BlogTitle;
//        //    this.BlogDate = BlogDate;
//        //    this.BlogAuthor = BlogAuthor;
//        //    this.BlogCatergory = BlogCatergory;
//        //    this.BlogShort = BlogShort;
//        //    this.BlogImg = BlogImg;
//        //    this.BlogBody = BlogBody;
//        //    this.BlogView = BlogView;
//        //}

//        if (BlogTitle != "" && BlogAuthor != "" && BlogCatergory != "" && BlogShort != "") {
//            const blog = new BlogModel(BlogTitle, BlogAuthor, BlogCatergory, BlogShort, BlogBody, BlogImg);

//                $.ajax({
//                    url: '/Blog/CreateBlog',
//                    type: 'POST',
//                    data: {
//                        BlogTitle: BlogTitle,
//                        BlogAuthor: BlogAuthor,
//                        BlogCatergory: BlogCatergory,
//                        BlogShort: BlogShort,
//                        BlogBody: BlogBody,
//                        BlogImg: BlogImg
//                    },
//                    success: function (response) {
//                        alert("Blog Posted");
//                        window.location.href = '/Blog/Index';
//                        console.log(response);
//                    },
//                    error: function (error) {
//                        alert("Blog Post Failed");
//                        console.log(error);
//                    }
//                });
//            };
//        } else {
//            alert("One or more input fields are empty.");
//        }

//    });
//});



//function loadNavBarView(viewName) {
//    $.ajax({
//        url: viewName,
//        type: 'GET',
//        success: function (data) {
//            $('#NavbarContainer').html(data);
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            console.log('Error loading nav bar view: ' + textStatus + ' ' + errorThrown);
//        }
//    });
//}