document.addEventListener("DOMContentLoaded", function () {
    var iframe1 = document.getElementById('facebookVideo');
    var iframe2 = document.getElementById('facebookVideo2');
    var iframe3 = document.getElementById('facebookVideo3');

    function reloadIframe(iframe) {
        var tempSrc = iframe.src;
        iframe.src = '';
        iframe.src = tempSrc;
    }

    iframe1.onload = function () {
        setTimeout(function () {
            reloadIframe(iframe1);
        }, 27000); // Предполагаемото време за завършване на видеото в милисекунди
    };

    iframe2.onload = function () {
        setTimeout(function () {
            reloadIframe(iframe2);
        }, 13000); // Предполагаемото време за завършване на видеото в милисекунди
    };
    iframe3.onload = function () {
        setTimeout(function () {
            reloadIframe(iframe2);
        }, 29000); // Предполагаемото време за завършване на видеото в милисекунди
    };
});
