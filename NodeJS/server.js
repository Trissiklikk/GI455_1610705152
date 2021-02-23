const splice = require("sqlite3").verbose();
let DB = new splice.Database("./DB/programChat.db", splice.OPEN_CREATE | splice.OPEN_READWRITE, (err) => {

    if (err) throw err;
    console.log("Connected to database");


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
            var id;
            var password;
            var name;

            //-------------------------------------------------------------------------------------------------------------------------------Register
            if (toJson.eventName == "Register") {
                id = toJson.userID;
                password = toJson.userPassword;
                name = toJson.userName;
                DB.all("INSERT INTO UserDataProgramChat (UserID,Password,UserName) VALUES ('" + id + "','" + password + "','" + name + "')", (err, rows) => {

                    if (err) {

                        var resultData =
                        {
                            eventName: "RegisterFail",
                            userID: toJson.userID
                        }
                        //Json to string
                        jsonToStr = JSON.stringify(resultData)

                        ws.send(jsonToStr);
                        console.log(err);

                    }
                    else {

                        var resultData =
                        {
                            eventName: "RegisterSuccess",
                            userID: toJson.userID,
                            userName: toJson.userName

                        }
                        //Json to string
                        jsonToStr = JSON.stringify(resultData)

                        ws.send(jsonToStr);
                    }
                });
            }

            else if (toJson.eventName == "Login") { //-------------------------------------------------------------------------------Login
                id = toJson.userID;
                password = toJson.userPassword;
                name = toJson.userName;
                
                DB.all("SELECT * FROM UserDataProgramChat WHERE UserID='"+id+"' AND Password= '"+password+"'", (err, rows) => {

                    if (err) {

                        //console.log(err);
                    }

                    if (rows.length > 0) {
                        
                        var userNameFormServer = rows[0].UserName;
                        //DB.all("SELECT UserName FROM UserDataProgramChat WHERE UserID='" + id + "'LIMIT 1", (err, rows))
                        console.log(userNameFormServer);

                        var resultData =
                        {
                            eventName: "LoginSuccess",
                            userID: toJson.userID,
                            userName: userNameFormServer

                        }
                        //Json to string
                        jsonToStr = JSON.stringify(resultData)

                        ws.send(jsonToStr);
                        
                                                                        
                    }

                    else {

                        var resultData =
                        {
                            eventName: "LoginFail",
                            userID: toJson.userID
                        }
                        //Json to string
                        jsonToStr = JSON.stringify(resultData)

                        ws.send(jsonToStr);
                        //console.log(err);

                    }
                    /*else {


                        var userNameFormServer = rows.UserName
                        DB.all("SELECT UserName FROM UserDataProgramChat WHERE UserID='" + id + "'", (err, rows))
                        console.log(userNameFormServer);

                        var resultData =
                        {
                            eventName: "LoginSuccess",
                            userID: toJson.userID,
                            userName: userNameFormServer

                        }
                        //Json to string
                        jsonToStr = JSON.stringify(resultData)

                        ws.send(jsonToStr);


                    }*/
                });
            }

            // ---------------------------------------------------------------------------------------------------------------------CreateRoom


            else if (toJson.eventName == "CreateRoom") {
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
                        eventName: "CreateFail",
                        dataRoomName: toJson.dataRoomName
                    }
                    //Json to string
                    jsonToStr = JSON.stringify(resultData)
                    //console.log(jsonToStr);
                    ws.send(jsonToStr);
                }

            }

            // ---------------------------------------------------------------------------------------------------------------------------JoinRoom
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
                    var newRoom = {
                        roomName: toJson.dataRoomName,
                        wsList: []
                    }
                    newRoom.wsList.push(ws);
                    roomList.push(newRoom);
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

                }
            }

            // ------------------------------------------------------------------------------------------------------------------------LeaveRoom
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

            //--------------------------------------------------------------------SentMeassge
            else if (toJson.status == "SentMessage") {

                /*var resultDataMessage = {
    
                    status: "CanSentMessage",
                    message: toJson.message,
                    roomName: toJson.roomName
                }
                jsonToStr = JSON.stringify(resultDataMessage)
    
                var messageInRoom = -1;
                
                for (var i = 0; i < roomList.length; i++) {
                    for (var j = 0; j < roomList[i].wsList.length; j++) {
                        if (roomList[i].wsList[j] == ws) {
                            messageInRoom = i;
                            break;
                        }
                    }
                }
                for (var i = 0; i < roomList[messageInRoom].wsList.length; i++) {
                    console.log("Ya2");
    
                    roomList[messageInRoom].wsList[i].send(jsonToStr);
    
                }*/
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

            for (var i = 0; i < roomList.length; i++) {
                for (var j = 0; j < roomList[i].wsList.length; j++) {
                    if (roomList[i].wsList[j] == ws) {
                        roomList[i].wsList.splice(j, 1);
                        break;
                    }
                }
            }
            for (var i = 0; i < wsList.length; i++) {
                if (wsList[i] == ws) {
                    wsList.slice(i, 1);
                    break;
                }
            }

        });
    });

    function ArrayRemove(arr, value) {
        return arr.filter((element) => {
            return element != value;
        });
    }
});
