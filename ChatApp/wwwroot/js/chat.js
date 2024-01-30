"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

var users = {};
var sender_username;

connection.on("UserConnected", function (username, connectionId) {
    users[connectionId] = username;
});

connection.on("UserDisconnected", function (connectionId) {
    delete users[connectionId];
});


connection.on("ReceiveMessage", function (sender, message, connectionId) {
    var messageList = document.getElementById("messageList");
    var newMessage = document.createElement("li");

    console.log("Sender: " + sender);
    console.log("Connection ID: " + connectionId);
    console.log("Sender Username: " + users[connectionId]);

    if (sender_username === sender) {
        console.log("Message is sent from: " + sender_username);
        newMessage.classList.add("sender");
        newMessage.textContent = "You: " + message;
    } else {
        console.log("Received message from: " + sender);
        newMessage.classList.add("receiver");
        newMessage.textContent = sender + ": " + message;
    }

    messageList.appendChild(newMessage);
});

connection.on("ReceiveConnectionId", function (connectionid) {
    //store the connection id for the current user.
    users[connectionid] = sender_username;

    //ensure username before connection start.
    if (sender_username) {
        startconnection();
    }
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault();

    var messageInput = document.getElementById("messageInput");
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", sender_username, message).catch(function (err) {
        return console.error(err.toString());
    })

    messageInput.value = '';
})

function startConnection() {
    connection.start().then(function () {
        console.log("SignalR connection started");
        document.getElementById("sendButton").disabled = false;

        connection.invoke("StoreConnectionId", sender_username).catch(function (err) {
            return console.error(err.toString());
        });
    }).catch(function (err) {
        console.error("Error starting sigalr connection: " + err.toString());
    });
}

if (sender_username) {
    startConnection();
}