using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;


namespace ProgramChat_Hw
{
    public class Websocket_Chat : MonoBehaviour
    {

        public class MessageData
        {
            public string status;
            public string message;
            public string roomName;

        }


        public enum State
        {
            Lobby,
            CreateRoom,
            JoinRoom,
            InRoom
        }
        private State state;

        public Text inputCreateRoom;
        public Text inputJoinRoom;
        private string roomUserName;
        //private string roomName;

        public GameObject lobby;

        public GameObject programChat;
        public Text roomNameText;

        public Text chatText;
        public Text userText;
        public Text coUserText;
        public InputField ip;
        public InputField port;
        //private bool checkChatUpdate = false;
        //public bool checkIsChatUser = false;
        private string tempMessageString;

        public GameObject bgLogin;
        public string statusEvent;
        public bool isSent = false;

        private WebSocket websocket;

        /*struct ChatManager
        {
            public string status;
            public string messageUser;

            public ChatManager(string status,string messageUser)
            {
                this.status = status;
                this.messageUser = messageUser;
            }
        }*/

        struct SocketEvent
        {
            public string eventName;
            public string dataRoomName;

            public SocketEvent(string eventName, string dataRoomName)
            {
                this.eventName = eventName;
                this.dataRoomName = dataRoomName;
            }

        }

        //private GameObject Room;
        private string testText;

        void Start()
        {
            tempMessageString = "";
            state = State.Lobby;
        }

        public void Update()
        {
            CreateJoinRoom();
            MessageInRoomAndLeaveRoom();
            
        }



        private void OnDestroy()
        {
            if (websocket != null)
            {
                websocket.Close();
            }
        }

        public void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            tempMessageString = messageEventArgs.Data;
            Debug.Log(tempMessageString);

            //SocketEvent receiveEventData = JsonUtility.FromJson<SocketEvent>(tempMessageString);

            /*if(receiveEventData.eventName == "CreateSuccess")
            {
              lobby.SetActive(false);
              programChat.SetActive(true);
              Debug.Log("CreateJoinRoom" + roomUserName);
              roomUserName = receiveEventData.dataRoomName;
              roonNameText.text = roomUserName;
              state = State.InRoom;
              Debug.Log(receiveEventData.eventName);
            }*/



        }
        public void MessageInRoomAndLeaveRoom()
        {
            if (state == State.InRoom && roomUserName == roomNameText.text && tempMessageString != "")
            {
                if (tempMessageString == "CanLeave" && state == State.InRoom)
                {

                    programChat.SetActive(false);
                    state = State.Lobby;
                    lobby.SetActive(true);

                    tempMessageString = "";

                }
                else
                {
                    MessageData receiveMessageData = JsonUtility.FromJson<MessageData>(tempMessageString);
                    Debug.Log(receiveMessageData.message);

                    if (receiveMessageData.status == "CanSentMessage" && receiveMessageData.roomName == roomUserName)
                    {
                        Debug.Log("CanSent");
                        if (receiveMessageData.message == userText.text)
                        {
                            chatText.text += receiveMessageData.message + "\n";
                            isSent = false;
                        }
                        else
                        {
                            coUserText.text += receiveMessageData.message + "\n";
                        }

                        //coUserText.text += tempMessageString + "\n";
                        tempMessageString = "";


                    }
                    
                }



            }

        }
        public void CreateJoinRoom()
        {
            if (tempMessageString != "" && state == State.Lobby)
            {
                SocketEvent receiveEventData = JsonUtility.FromJson<SocketEvent>(tempMessageString);
                //Debug.Log(roomName);

                if (receiveEventData.eventName == "CreateSuccess" || receiveEventData.eventName == "JoinRoomSuccess")
                {
                    lobby.SetActive(false);
                    programChat.SetActive(true);
                    roomUserName = receiveEventData.dataRoomName;
                    Debug.Log("CreateJoinRoom" + roomUserName);
                    roomNameText.text = roomUserName;
                    state = State.InRoom;

                }
                tempMessageString = "";

            }

        }

        public void CreateRoom()
        {
            roomUserName = inputCreateRoom.text;


            if (state == State.Lobby && websocket.ReadyState == WebSocketState.Open)
            {
                SocketEvent socketEvent = new SocketEvent("CreateRoom", roomUserName);

                string jsonStr = JsonUtility.ToJson(socketEvent);
                websocket.Send(jsonStr);

            }
        }
        public void JoinRoom()
        {
            roomUserName = inputJoinRoom.text;


            if (state == State.Lobby && websocket.ReadyState == WebSocketState.Open)
            {
                SocketEvent socketEvent = new SocketEvent("JoinRoom", roomUserName);

                string jsonStr = JsonUtility.ToJson(socketEvent);
                websocket.Send(jsonStr);

            }
        }

        public void LeaveRoomButton()
        {
            if (state == State.InRoom && websocket.ReadyState == WebSocketState.Open)
            {
                SocketEvent socketEvent = new SocketEvent("LeaveRoom", roomUserName);

                string jsonStr = JsonUtility.ToJson(socketEvent);
                websocket.Send(jsonStr);
                chatText.text = "";
                coUserText.text = "";


            }
        }

        public void LoginButton()
        {
            websocket = new WebSocket($"ws://{ip.text}:{port.text}/");
            //websocket = new WebSocket("ws://127.0.0.1:5600/");
            websocket.OnMessage += OnMessage;
            websocket.Connect();
            bgLogin.SetActive(false);
        }
        public void SendButton()
        {
            //chatText.text += userText.text + "\n";
            if (userText.text == "" || websocket.ReadyState != WebSocketState.Open)
                return;

            MessageData NewMessageData = new MessageData();
            NewMessageData.status = "SentMessage";
            NewMessageData.message = userText.text;
            NewMessageData.roomName = roomUserName;

            string toJsonStr = JsonUtility.ToJson(NewMessageData);
            isSent = true;
            websocket.Send(toJsonStr);
            userText.text = "";

            //ChatToSever();
            //websocket.Send(JsonUtility.ToJson(userText.text));
        }





    }
}

