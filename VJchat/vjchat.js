$(function () {
    // Declare a proxy to reference the hub.
    var chat = $.connection.chatHub;

 
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {
        // Html encode display name and message.
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page.
        $('#discussion').append('<div class="chatbox__messages__user-message chatbox__messages__user-message--ind-message"><p class="name">' + encodedName + '</p><br /><p class="message">' + encodedMsg + '</p></div>');

        // check if msg overflow, if do move upwards
        var cHeight = $("#discussion").height();
        var height = 0;
        $("#discussion").children().each(function () {
            height += $(this).outerHeight(true);
        });
        if (cHeight < height) {
            var trns = "translateY(" + (cHeight - height) + "px)";
            $('.chatbox__messages__user-message').css("transform", trns);
        }
    };

    // function for adding connected users
    chat.client.addOnlineUsers = function (name) {
        $('#users').html("<h1>User list</h1>");
        for (let sname of name) {
            var encodedName = $('<div />').text(sname.userName).html();
            $('#users').append('<div class="chatbox__user--active"><p>' + encodedName + '</p></div>');
        }
    }

    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt('Enter your name:', ''));

    // Set initial focus to message input box.
    $('#message').focus();

  //
    // Start the connection.
    $.connection.hub.start().done(function () {

        chat.server.addUser($('#displayname').val());

        $('#sendmessage').submit(function () {
            // Call the Send method on the hub.
            chat.server.send($('#displayname').val(), $('#message').val());
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();

            // Dont reload page
            return false;
        });
    });
});