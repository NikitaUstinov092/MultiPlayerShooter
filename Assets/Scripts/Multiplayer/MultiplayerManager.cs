using System.Collections.Generic;
using Colyseus;
using UnityEngine;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
   [SerializeField]
   private Config _startConfig;
   private ColyseusRoom<State> _room;
      
   public ColyseusRoom<State> GetRoom()
   {
      return _room;
   }

   public void SendMessage(string key, Dictionary<string, object> data)
   {
      _room.Send(key, data);
   }
      
   public void SendMessage(string key, string data)
   {
      _room.Send(key, data);
   }
      
   public string GetClientKey()
   {
      return _room.SessionId;
   }
   protected override void Awake()
   {
      base.Awake();
      Instance.InitializeClient();
      Connect();
   }

   private async void Connect()
   {
      _room = await Instance.client.JoinOrCreate<State>("state_handler", _startConfig.GetConfig());
        
      var roomHandlers = GetComponents<IGetColyseusRoom>();
         
      foreach (var handler in roomHandlers)
      {
         handler.SendRoom(_room);
      }
   }
   protected override void OnDestroy()
      {
         base.OnDestroy();
         _room.Leave();
      }
      
   }







