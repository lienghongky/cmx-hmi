using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class SocketConnection : MonoBehaviour
{
    WebSocket websocket;
    public SafetyService safetyService;

  // Start is called before the first frame update
  async void Start()
  {
    //  delay invoke the connectToServer method
    Invoke("connectToServer", 10);
  }

  void Update()
  {
    #if !UNITY_WEBGL || UNITY_EDITOR
      websocket.DispatchMessageQueue();
    #endif
  }

  async void connectToServer(){
    // delay 5 seconds to wait for the safety service to start

    
    websocket = new WebSocket("ws://localhost:8765");

    websocket.OnOpen += () =>
    {
      Debug.Log("Connection open!");
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
    };

    websocket.OnClose += (e) =>
    {
      Debug.Log("Connection closed!");
    };

    websocket.OnMessage += (bytes) =>
    {
  

      // getting the message as a string
      var message = System.Text.Encoding.UTF8.GetString(bytes);
    //   replace ' with " to make it a valid json string
      message = message.Replace("'", "\"");
      BlobList blobList = JsonUtility.FromJson<BlobList>(message);
      List<SafetyService.ObjectData> objectDataList = new List<SafetyService.ObjectData>();
      for (int i = 0; i < blobList.list.Count; i++)
      {
        Blob blob = blobList.list[i];
        //  add scalre to x and y to make it visible
        if (blob.x <= -5 || blob.y <= -5 || blob.x >= 5 || blob.y >= 10)
        {
          continue;
        }
        var scale = 2.7f;
        var objectdata = new SafetyService.ObjectData
          {
              Class = "Blob",
              ClassID = 1,
              Id = i,
              Position = new Vector3(blob.x * scale, 0, blob.y * scale),
              Scale = new Vector3(1, 1, 1),
              Orientation = new Vector3(0, blob.angle, 0),
              Time = 0.0f
          };    
        objectDataList.Add(objectdata);
        
      }
      Debug.Log("Received object: " + objectDataList.Count);
      safetyService.SetObjectlist(objectDataList);


    };

    // waiting for messages
    await websocket.Connect();
  }

  async void SendWebSocketMessage()
  {
    if (websocket.State == WebSocketState.Open)
    {
      // Sending bytes
      await websocket.Send(new byte[] { 10, 20, 30 });

      // Sending plain text
      await websocket.SendText("plain text message");
    }
  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }


[Serializable]
public class Blob
{
    public float x;
    public float y;
    public float angle;
}

[Serializable]
public class BlobList
{
    public List<Blob> list;
}
}
