var websocket = require('ws');
var callbackInitServer = () => {
    console.log("ThymeServer is running.");

}

var wss = new websocket.Server({ port: 5600 }, callbackInitServer);

var wsList = [];
var roomList = [];

wss.on("connection", (ws) => {

    console.log("client connected.");
    wsList.push(ws);



    // Lobby
    ws.on("message", (data) => {

        var toJson = JSON.parse(data)

        // CreateRoom


        if (toJson.eventName == "CreateRoom") {
            var roomIsFound = false;
            //roomNumber = toJson.dataRoomName;
            for (var i = 0; i < roomList.length; i++) {
                if (roomList[i].roomName == toJson.dataRoomName) {
                    roomIsFound = true;
                    break;
                }
            }

            if (roomIsFound == false) {
                var newRoom = {
                    roomName: toJson.dataRoomName,
                    wsList: []
                }
                newRoom.wsList.push(ws);
                roomList.push(newRoom);
                console.log("CreateSuccess");

                //Data To User
                var resultData =
                {
                    eventName: "CreateSuccess",
                    dataRoomName: toJson.dataRoomName
                }
                //Json to string
                jsonToStr = JSON.stringify(resultData)

                ws.send(jsonToStr);
                //ws.send("CanCreate");

            }
            else {
                var resultData =
                {
                    eventName: "Createfail",
                    dataRoomName: toJson.dataRoomName
                }
                //Json to string
                jsonToStr = JSON.stringify(resultData)
                //console.log(jsonToStr);
                ws.send(jsonToStr);
            }

        }

        // JoinRoom
        else if (toJson.eventName == "JoinRoom") {
            var roomIsFound = false;
            //roomNumber = toJson.dataRoomName;
            for (var i = 0; i < roomList.length; i++) {
                if (roomList[i].roomName == toJson.dataRoomName) {
                    roomIsFound = true;
                    console.log("Here");
                    break;
                }
            }

            if (roomIsFound == true) {
                var joinRoom = {
                    roomName: toJson.dataRoomName,
                    wsList: []
                }
                joinRoom.wsList.push(ws);
                roomList.push(joinRoom);
                console.log("JoinRoomSuccess");


                //Data To User
                var resultData =
                {
                    eventName: "JoinRoomSuccess",
                    dataRoomName: toJson.dataRoomName
                }
                //Json to string
                jsonToStr = JSON.stringify(resultData)
                ws.send(jsonToStr);

                //console.log(jsonToStr);


            }
            else {
                console.log("JoinRoomFail");
                var resultData =
                {
                    eventName: "JoinRoomFail",
                    dataRoomName: toJson.dataRoomName
                }
                //Json to string
                jsonToStr = JSON.stringify(resultData)
                ws.send(jsonToStr);
                //console.log(jsonToStr);
                //ws.send(jsonToStr);
            }
        }

        // LeaveRoom
        else if (toJson.eventName == "LeaveRoom") {
            var isFound = false;
            //roomNumber = null;
            //ws.send("CanLeave");
            for (var i = 0; i < roomList.length; i++) {
                for (var j = 0; j < roomList[i].wsList.length; j++) {
                    if (roomList[i].wsList[j] == ws) {
                        roomList[i].wsList.splice(j, 1);

                        break;
                    }
                }
            }
            ws.send("CanLeave");
        }

        //SentMeassge
        else if (toJson.status == "SentMessage") {

            console.log("Ya2");
            var resultDataMessage = {

                status: "CanSentMessage",
                message: toJson.message,
                roomName: toJson.roomName

            }
            jsonToStr = JSON.stringify(resultDataMessage)

            for (var i = 0; i < roomList.length; i++) {
                for (var j = 0; j < roomList[i].wsList.length; j++) {
                        roomList[i].wsList[j].send(jsonToStr);
                        
                }

            }

        }
    });


    ws.on("close", () => {
        console.log("client disconnected.");
        //wsList = ArrayRemove(wsList,ws);
        for (var i = 0; i < wsList.length; i++) {
            if (wsList[i] == ws) {
                wsList.slice(i, 1);
                break;
            }
        }


        for (var i = 0; i < roomList.length; i++) {
            for (var j = 0; j < roomList[i].wsList.length; j++) {
                if (roomList[i].wsList[j] == ws) {
                    roomList[i].wsList.splice(j, 1);
                    break;
                }
            }
        }

    });
});

function ArrayRemove(arr, value) {
    return arr.filter((element) => {
        return element != value;
    });
}

function Boardcast(data) {


}

