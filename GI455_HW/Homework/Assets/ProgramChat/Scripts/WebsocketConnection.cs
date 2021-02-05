using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;


namespace ProgramChat
{
    public class WebsocketConnection : MonoBehaviour
    {

        private WebSocket websocket;
        

        void Start()
        {
            websocket = new WebSocket("ws://127.0.0.1:5500/");
            websocket.OnMessage += OnMessage;
            websocket.Connect();
            //websocket.Send("i am coming here.");
        }

        
        // Update is called once per frame
        void Update()
        {

            RandomNum();

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
            Debug.Log("Receive msg : " + messageEventArgs.Data);  
        }

        public void RandomNum()
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (websocket.ReadyState == WebSocketState.Open)
                {
                    websocket.Send("Random Number :" + Random.Range(0, 99999));
                }

            }
        }
    }
    

}

