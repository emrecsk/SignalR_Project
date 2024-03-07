$(function () {

    const connection = new signalR.HubConnectionBuilder().withUrl("/typeSafeHub").configureLogging(signalR.LogLevel.Information).build(); 
    
        connection.start().then(function () {
            console.log('Connected to SignalR Hub');
            $('#connectionId').text("Connection ID is " + connection.connectionId);
        }).catch(function (err) {
            return console.error(err.toString());
        });

    $('#message-send-btn').on('click', function () {
        const message = $('#message-input').val();
        connection.invoke('GettingFromClient', message).catch(function (err) {
            return console.error(err.toString());
        });
    });

    $('#message-send-btn-to').on('click', function () {
        const message = $('#message-input').val();
        connection.invoke('CallingByClient', message).catch(function (err) {
            return console.error(err.toString());
        });

    });

    $('#message-send-btn-other').on('click', function () {
        const message = $('#message-input').val();
        connection.invoke('SendingToOthers', message).catch(function (err) {
            return console.error(err.toString());
        });

    });

    $('#message-send-btn-specific').on('click', function () {
        const message = $('#message-input').val();
        const connectionId = $('#connectionIdInput').val();
        connection.invoke('SendingToSpecific', message, connectionId).catch(function (err) {
            return console.error(err.toString());
        });

    });

    //subscription to the server events

    connection.on('SendtoAllClientAsync', function (message) {
        $('#messages-list').append('<li>' + message + '</li>');        
    });

    connection.on('OnlineClients', function (count) {
        $('#counter_sign').text(count);
        count = count * 10;
        if (count >= 20 && count < 40) {            
            $('#bar_prog').removeClass('bg-warning');
            $('#bar_prog').addClass('bg-info');
        } else if (count >= 50 && count < 60) {
            $('#bar_prog').removeClass('bg-info');
            $('#bar_prog').removeClass('bg-danger');
            $('#bar_prog').addClass('bg-warning');
        } else if (count >= 70 && count < 90) {
            $('#bar_prog').removeClass('bg-warning');
            $('#bar_prog').addClass('bg-danger');
        }
        count = count + '%';
        $('#bar_prog').width(count);
    });

    connection.on('SendtoCallingClient', function (message) {
        $('#messages-list').append('<li>' + message + '</li>');
    });

    connection.on('SendtoOtherClients', function (message) {
        $('#messages-list').append('<li>' + message + '</li>');
    });

    connection.on('SendtoSpecificClient', function (message) {
        $('#messages-list').append('<li>' + message + '</li>');
    });
});