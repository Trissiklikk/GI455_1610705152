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

        public class LoginRegisterData
        {
            public string eventName;
            public string userID;
            public string userPassword;
            public string userName;
        }

        public enum State
        {
            Lobby,
            LoginRegister,
            JoinRoom,
            InRoom
        }
        private State state;

        //private string roomName;
        [Header("CallBackUI")]//---------------------CallBackUI
        public GameObject callBackUi;
        public Text callBackText;

        [Header("LobbyUI")]//---------------------LobbyUI
        public GameObject lobbyUi;
        public Text inputCreateRoom;
        public Text inputJoinRoom;
        private string roomUserName;
        public Text nameThisUser;

        [Header("ChatUI")]//------------------ChatUI
        public GameObject programChat;
        public Text roomNameText;
        public Text chatText;
        public Text userText;
        public Text coUserText;
        private string tempMessageString;

        [Header("LoginUI")]//-------------------------LoginUI

        public GameObject loginUi;
        public InputField loginUserIDInput;
        public InputField loginPasswordInput;

        [Header("RegisterUI")]//-----------------------RegisterUI
        public GameObject registerUi;
        public InputField registerUserIDInput;
        public InputField registerPasswordInput;
        public InputField registerNameInput;


        [Header("ConnectUI")]//------------------------ConnectUI
        public GameObject connectUi;
        public InputField ip;
        public InputField port;
        
        public string statusEvent;
        private WebSocket websocket;


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



        void Start()
        {
            tempMessageString = "";
            state = State.LoginRegister;
        }

        public void Update()
        {
            CallBackCreateJoinRoom();
            CallBackLoginRegister();
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
                    lobbyUi.SetActive(true);

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
                            //isSent = false;
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

        public void CallBackLoginRegister()
        {
            if(tempMessageString != "" && state == State.LoginRegister){

                LoginRegisterData receiveLoginRegisterData = JsonUtility.FromJson<LoginRegisterData>(tempMessageString);
                if(receiveLoginRegisterData.eventName == "LoginSuccess"){
                    loginUi.SetActive(false);
                    lobbyUi.SetActive(true);
                    nameThisUser.text += "\n\n" + receiveLoginRegisterData.userName;
                    state = State.Lobby;


                }
                else if (receiveLoginRegisterData.eventName == "LoginFail"){
                    callBackUi.SetActive(true);
                    callBackText.text = "Login Fail With UserID : "+receiveLoginRegisterData.userID + " Please try again";
                }
                else if(receiveLoginRegisterData.eventName == "RegisterSuccess"){
                    registerUi.SetActive(false);
                    lobbyUi.SetActive(true);
                    nameThisUser.text += "\n\n" +receiveLoginRegisterData.userName;
                    state = State.Lobby;

                }
                else if(receiveLoginRegisterData.eventName == "RegisterFail"){
                    callBackUi.SetActive(true);
                    callBackText.text = "Register Fail With UserID : "+receiveLoginRegisterData.userID + " Please try again";
                }

                tempMessageString = "";
            }
            


        }
        public void CallBackCreateJoinRoom()
        {
            if (tempMessageString != "" && state == State.Lobby)
            {
                SocketEvent receiveEventData = JsonUtility.FromJson<SocketEvent>(tempMessageString);
                //Debug.Log(roomName);

                if (receiveEventData.eventName == "CreateSuccess" || receiveEventData.eventName == "JoinRoomSuccess")
                {
                    lobbyUi.SetActive(false);
                    programChat.SetActive(true);
                    roomUserName = receiveEventData.dataRoomName;
                    Debug.Log("CreateJoinRoom" + roomUserName);
                    roomNameText.text = roomUserName;
                    state = State.InRoom;

                }
                else if(receiveEventData.eventName == "JoinRoomFail")
                {
                    callBackText.text = "Room name : "+receiveEventData.dataRoomName+" is not found";
                    callBackUi.SetActive(true);

                }
                else if(receiveEventData.eventName == "CreateFail")
                {
                    callBackText.text = "Room name : "+receiveEventData.dataRoomName+" it already create";
                    callBackUi.SetActive(true);
                }
                tempMessageString = "";

            }

        }


        //------------------------------------------------------ Button ------------------------------------------------------------------------------
        public void CreateRoom()//------------------------------------------------------CreateRoomButton
        {
            roomUserName = inputCreateRoom.text;


            if (state == State.Lobby && websocket.ReadyState == WebSocketState.Open)
            {
                SocketEvent socketEvent = new SocketEvent("CreateRoom", roomUserName);

                string jsonStr = JsonUtility.ToJson(socketEvent);
                websocket.Send(jsonStr);

            }
        }
        public void JoinRoom()//------------------------------------------------------JoinRoomButton
        {
            roomUserName = inputJoinRoom.text;


            if (state == State.Lobby && websocket.ReadyState == WebSocketState.Open)
            {
                SocketEvent socketEvent = new SocketEvent("JoinRoom", roomUserName);

                string jsonStr = JsonUtility.ToJson(socketEvent);
                websocket.Send(jsonStr);

            }
        }

        

        public void LeaveRoomButton()//------------------------------------------------------------------------------- LeaveRoomButton
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

        public void ConnectButton()// -----------------------------------------------------------------------------------ConnectButton    
        {
            //websocket = new WebSocket($"ws://{ip.text}:{port.text}/");
            websocket = new WebSocket("ws://127.0.0.1:5600/");
            websocket.OnMessage += OnMessage;
            websocket.Connect();
            loginUi.SetActive(true);
            connectUi.SetActive(false);
        }
        public void SendButton()   // -----------------------------------------------------------------------------------SendButton     
        {
            //chatText.text += userText.text + "\n";
            if (userText.text == "" || websocket.ReadyState != WebSocketState.Open)
                return;

            MessageData NewMessageData = new MessageData();
            NewMessageData.status = "SentMessage";
            NewMessageData.message = userText.text;
            NewMessageData.roomName = roomUserName;

            string toJsonStr = JsonUtility.ToJson(NewMessageData);
            //isSent = true;
            websocket.Send(toJsonStr);
            userText.text = "";

        }

        public void LoginButton()//------------------------------------------------------------------------------------ LoginButton
        {

            LoginRegisterData NewLoginData = new LoginRegisterData();
            NewLoginData.eventName = "Login";
            NewLoginData.userID = loginUserIDInput.text;
            NewLoginData.userPassword = loginPasswordInput.text;

            string toJsonStr = JsonUtility.ToJson(NewLoginData);
            websocket.Send(toJsonStr);    
            


        }

        public void RegisterButtonToServer()//------------------------------------------------------------------------------------ RegisterButton
        {           

            LoginRegisterData NewRegisterData = new LoginRegisterData();
            NewRegisterData.eventName = "Register";
            NewRegisterData.userID = registerUserIDInput.text;
            NewRegisterData.userPassword = registerPasswordInput.text;
            NewRegisterData.userName = registerNameInput.text;

            string toJsonStr = JsonUtility.ToJson(NewRegisterData);
            websocket.Send(toJsonStr);
            
            

        }
        public void RegisterButton()
        {
            loginUi.SetActive(false);
            registerUi.SetActive(true);

        }

        public void CloseCallBackButton()//------------------------------------------------------------------------------------ CloseCallBackButton
        {
            callBackUi.SetActive(false);

        }


    }
}

