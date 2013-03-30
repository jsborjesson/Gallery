$(window).load(function () {

    // Fade out the message-panel after 10s.
    var $messagePanel = $("#UploadMessagePanel");
    if ($messagePanel.length) {
        setTimeout(function () {
            $messagePanel.fadeOut();
        }, 10000);
    }


});