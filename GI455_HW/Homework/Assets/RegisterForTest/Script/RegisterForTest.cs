using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class RegisterForTest : MonoBehaviour
{
    public class WebSocketForTest
    {
        public string eventName;
        public string data;

    }
    private WebSocket ws;
    private string callBackData;
    
    void Start()
    {
        ws = new WebSocket("ws://gi455-305013.an.r.appspot.com/");
        WebSocketForTest newWebSocketForTest = new WebSocketForTest();
        newWebSocketForTest.eventName = "GetStudentData";
        newWebSocketForTest.data = "1610705152";

        string strToJson = JsonUtility.ToJson(newWebSocketForTest);
        ws.Send(strToJson);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMessage(object sender, MessageEventArgs messageEventArgs)
    {
        callBackData = messageEventArgs.Data;
        Debug.Log(callBackData);
    }
}
