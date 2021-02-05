using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;


namespace ProgramChat_Hw
{
    public class Websocket_Chat: MonoBehaviour
    {
        
        public Text chatText;
        public Text userText;
        public Text coUserText;
        public InputField ip;
        public InputField port;
        private bool checkChatUpdate = false;
        public bool checkIsChatUser = false;
        
        public GameObject bgLogin;

        private WebSocket websocket;


        void Start()
        {
          
        }

        void Update()
        {


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
            Debug.Log("Chat : " + messageEventArgs.Data);
            //coChat = messageEventArgs.Data;

            if (checkIsChatUser == false)
            {
                coUserText.text += messageEventArgs.Data + "\n";
                checkChatUpdate = true;
            }
            else
            {
                checkIsChatUser = false;
            }
        }


        public void SendButton()
        {
            checkIsChatUser = true;
            chatText.text += userText.text + "\n";
            ChatToSever();
        }

        public void ChatToSever()
        {
            if (websocket.ReadyState == WebSocketState.Open)
            {
                websocket.Send(userText.text);
            }

        }

        public void LoginButton()
        {
            websocket = new WebSocket($"ws://{ip.text}:{port.text}/");
            //websocket = new WebSocket("ws://127.0.0.1:5500/");
            websocket.OnMessage += OnMessage;
            websocket.Connect();
            bgLogin.SetActive(false);
        }

    }
}

