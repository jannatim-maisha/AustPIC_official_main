// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var keywordsContainer = document.getElementById("keywordsContainer");

if (keywordsContainer) {
    console.log("Element with ID 'keywordsContainer' found.");

    try {
        var blogBodyValue = $('.blog-content').attr('data-blog-body');
        const tempDiv = document.createElement('div');
        tempDiv.innerHTML = blogBodyValue;
        let extractedText = tempDiv.textContent.replace(/\n/g, '').replace(/\s+/g, ' ').trim();
        console.log(extractedText);

        keywordQuery({ "inputs": extractedText }).then((response) => {
            //console.log(JSON.stringify(response));

            const uniqueKeywordsSet = new Set();
            const filteredData = response.filter(item => item.score > 0.97);
            filteredData.forEach(item => uniqueKeywordsSet.add(item.word));
            // Convert set back to an array
            const uniqueKeywordsArray = Array.from(uniqueKeywordsSet);
            console.log(uniqueKeywordsArray);
            var keywordsHTML = uniqueKeywordsArray.map(keyword => `<span class="rounded border border-3 border-secondary bg-white b-1 py-1 px-2 text-dark mx-1">${keyword} </span>`)
                .join(' ');
            keywordsHTML = `<span class="keywords-label">Keywords: </span>${keywordsHTML}`;
            keywordsContainer.innerHTML = keywordsHTML;

        });
    } catch (error) {
        console.error("Error during keyword extraction:", error);

        // Check if the error is due to model loading
        if (error.error === "Model keyword extraction is currently loading") {
            const estimatedTime = error.estimated_time || 20; // Default to 20 seconds if estimated_time is not provided
            console.log(`Model loading, retrying in ${estimatedTime} seconds...`);

            // Retry after the estimated time
            setTimeout(() => {
                extractKeyword(blogBody);
            }, estimatedTime * 1000); // Convert seconds to milliseconds
        }
    }
} else {
    console.error("Element with ID 'keywordsContainer' not found.");
}

async function keywordQuery(data) {
    const response = await fetch(
        "https://api-inference.huggingface.co/models/yanekyuk/bert-uncased-keyword-extractor",
        {
            headers: { Authorization: "Bearer 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" },
            method: "POST",
            body: JSON.stringify(data),
        }
    );
    const result = await response.json();
    return result;
}
