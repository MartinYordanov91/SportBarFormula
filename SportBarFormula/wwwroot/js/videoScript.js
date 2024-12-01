document.addEventListener("DOMContentLoaded", function () {
    var video1 = document.getElementById('localVideo1');
    var video2 = document.getElementById('localVideo2');
    var video3 = document.getElementById('localVideo3');

    function reloadVideo(video) {
        video.currentTime = 0;
        video.play();
    }

    video1.onended = function () {
        setTimeout(function () {
            reloadVideo(video1);
        }, 1000); // Време за пауза преди да презареди видеото
    };

    video2.onended = function () {
        setTimeout(function () {
            reloadVideo(video2);
        }, 1000); // Време за пауза преди да презареди видеото
    };

    video3.onended = function () {
        setTimeout(function () {
            reloadVideo(video3);
        }, 1000); // Време за пауза преди да презареди видеото
    };
});
