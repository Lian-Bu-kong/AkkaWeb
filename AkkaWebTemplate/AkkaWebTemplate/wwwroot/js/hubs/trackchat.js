// 建立Connection
var connection = new signalR.HubConnectionBuilder().withUrl("/trackchannel").build();

// Connect BackEnd and FrontEnd Init
connection.start().then(function () {
    // Connect Sucess

}).catch(function (err) {
    return console.error(err.toString());
});

// Register Hub Method (Akka->FrontEnd)
connection.on("UpdateTrackingMap", function (trkMap) {
    var encodedMsg = trkMap;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});