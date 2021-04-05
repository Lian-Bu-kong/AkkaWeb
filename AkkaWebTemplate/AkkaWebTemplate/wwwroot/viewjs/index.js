//  控制項元素
var _btn_reqtrack;

// 建立Connection
var connection = new signalR.HubConnectionBuilder().withUrl("/trackHub").build();


//  取得 dom 上所需元素
function GetElement() {
    _btn_reqtrack = $('#btn_reqtrack');
}


// Connect BackEnd and FrontEnd Init
connection.start().then(function () {
    // Connect Sucess
    GetElement();

    _btn_reqtrack.on('click', (e) => {
        connection.invoke('ReqTrackScan','Req Track Map');
    });

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