﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;

public class RegisterForTest : MonoBehaviour
{
    public class WebSocketForTest
    {
        public string eventName;
        public string studentID;
        

    }

    public class WebSocketSendRequestInfo
    {
        public string eventName;
        public string token;
    }
    public class SendAnswer
    {
        public string eventName;
        public string token;
        public string answer;
    }

    //public string eventIndex[];
    //public List<string> eventIndex = new List<string>();
    public WebSocket ws;
    public string callBackData;
    public Text textCallback;
    public int[] data ;
    public int dataAns  = 0;
    //public string testAns;
    void Start()
    {

        
        ws = new WebSocket("ws://gi455-305013.an.r.appspot.com/");
        ws.OnMessage += OnMessage;
        ws.Connect();

        //Invoke("SendData", 5f);

    }

    public void OnMessage(object sender, MessageEventArgs messageEventArgs)
    {
        callBackData = messageEventArgs.Data;
        textCallback.text = callBackData;

        
        Debug.Log(callBackData);
        Debug.Log(messageEventArgs.Data);
    }

    public void SendData()
    {
        
        SendAnswer newSendAnswer = new SendAnswer();
        newSendAnswer.eventName = "SendAnswer";
        newSendAnswer.token = "";
        newSendAnswer.answer = "";

        string strToJson = JsonUtility.ToJson(newSendAnswer);
        Debug.Log(strToJson);
        ws.Send(strToJson);
    }
    public void AnswerTest()
    {
        //int i ;
        /*data = new int[] {176,844,978,559,136,955,78,345,573,728,188,891,497,383,378,22,788,6,420,847,655,310,880,694,790,607,239,874,223,113,885,111,761,208,566,496,522,446,459,985,513,716,830,863,126,778,879,43,515,666,839,875,643,272,298,823,399,841,164,881,506,55,632,545,7,249,731,924,519,475,946,412,583,37,586,831,848,457,802,994,575,710,585,642,25,130,703,838,656,725,284,636,313,424,588,54,222,177,325,836,366,248,428,837,634,789,95,411,111,603,963,350,7,870,146,279,774,149,722,874,951,45,456,560,57,406,338,130,388,301,185,881,243,696,98,197,738,218,62,471,451,674,432,444,181,671,856,720,22,394,699,935,61,240,981,419,565,552,51,969,504,969,794,422,819,56,548,824,425,934,726,730,592,442,394,515,772,95,618,339,395,997,32,876,670,761,449,979,893,799,602,146,724,119,717,362,906,937,794,166,137,416,392,576,315,877,370,732,962,52,763,260,669,667,922,228,935,209,271,313,768,809,622,128,317,987,714,119,205,904,323,456,472,723,348,611,9,822,231,32,402,509,421,250,748,215,313,463,381,244,247,826,597,165,981,536,217,552,987,379,343,702,966,888,136,195,590,805,863,919,437,880,3,943,359,9,635,240,604,34,927,409,678,930,380,798,653,836,328,566,181,411,833,997,180,857,385,899,747,257,328,733,728,163,140,772,452,185,988,349,168,401,153,514,567,613,427,316,190,10,475,910,97,735,148,193,818,99,612,495,69,598,860,884,953,211,256,344,383,346,561,708,103,355,603,908,620,266,760,762,156,13,610,932,389,160,235,200,395,816,342,690,685,296,920,161,348,614,68,79,197,524,723,254,334,722,513,226,651,531,967,867,517,867,945,147,785,912,694,983,501,900,829,611,349,367,301,858,62,3,977,150,575,71,855,293,336,315,176,154,971,438,261,196,708,424,951,474,226,104,285,696,969,947,482,873,125,923,934,154,147,632,707,433,510,207,910,327,470,44,902,743,737,413,948,159,293,849,712,847,26,300,740,619,72,510,703,935,193,227,397,828,824,487,308,963,622,151,259,63,705,608,575,432,3,603,699,210,52,547,834,998,448,353,133,258,553,309,562,905,701,662,481,902,295,471,768,109,649,394,311,995,682,234,396,312,314,598,111,107,673,35,397,780,269,966,335,635,287,770,872,546,167,106,685,639,641,762,747,70,892,407,804,536,459,621,496,101,804,312,347,67,705,44,784,59,368,446,229,109,934,86,790,209,591,782,845,550,739,246,727,417,655,258,157,750,263,925,602,597,182,844,694,21,275,16,577,544,776,252,294,348,890,918,350,830,249,685,432,747,442,265,347,403,100,237,417,493,57,192,481,551,301,478,844,799,484,503,373,648,818,610,739,809,975,267,26,550,326,109,314,780,261,132,937,831,763,951,249,188,875,751,302,210,558,508,674,860,737,381,116,820,124,329,63,696,364,189,79,822,388,508,625,789,799,861,418,941,508,277,794,444,726,299,530,851,313,578,946,362,535,44,364,154,783,800,59,207,727,184,795,435,955,212,570,248,132,643,342,371,749,605,975,299,171,32,797,791,8,138,481,951,315,574,83,478,19,904,542,966,112,692,25,64,499,648,18,872,386,463,383,237,449,124,899,870,772,872,179,504,697,108,325,342,620,255,928,927,19,465,125,244,46,492,412,626,821,286,888,379,937,227,860,519,686,931,260,740,678,412,150,218,350,18,707,958,986,189,5,612,699,5,297,711,327,309,203,422,579,376,90,684,663,240,443,71,952,56,94,221,860,982,555,964,716,347,832,788,720,145,426,432,166,293,10,252,603,262,889,246,494,368,986,478,499,371,119,8,633,536,529,217,99,77,434,799,991,953,242,406,961,139,765,355,486,530,837,434,905,111,147,893,592,415,506,749,125,152,155,323,786,890,667,437,658,937,801,202,270,582,446,199,500,832,470,427,618,624,984,510,808,680,770,205,61,704,100,108,455,275,699,304,6,798,251,819,773,163,14,261,918,754,572,627,315,134,788,21,469,278,551,970,17,111,612,803,110,984,954,51,69,589,392,211,613,895,291,667,749,970,265,767,432,483,349,667,742,814,837,351,842,76,331,179,876,980,240,479,7,784,320,606,19,437,868,142,58,136,361,672,92,331,115,276,34,337,101,948,499,349,492,111,140,568,918,496,382,623,498,143,165,256,45,776,531,497,549,16,832,526,383,591,402,865,484,513,38,783,334,108,926,130,713,782,536,620,161,760,758,43};
        
        for(i = 0 ; i < data.Length; i++)
        {
            dataAns += data[i];
        }*/
        Debug.Log(dataAns);
        
    }
    
}
