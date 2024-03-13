using System.Collections.Generic;
using Colyseus;
using UnityEngine;

   public class MultiplayerManager : ColyseusManager<MultiplayerManager>
   {
      [SerializeField]
      private CharacterMove _player;
      
      [SerializeField]
      private EnemyMoveConfig _enemy;
      
      private ColyseusRoom<State> _room;
      protected override void Awake()
      {
         base.Awake();
         Instance.InitializeClient();
         Connect();
      }

      private async void Connect()
      {
         Dictionary<string, object> data = new Dictionary<string, object>
         {
            { "speed", _player.Speed }
         };
         
         _room = await Instance.client.JoinOrCreate<State>("state_handler", data);
         _room.OnStateChange += OnChanged;
      }

      private void OnChanged(State state, bool isfirststate)
      {
          if(!isfirststate)
             return;
         
          
          state.players.ForEach((key, player) =>
          {
             if (key == _room.SessionId)
             {
                CreatePlayer(player);
                return;
             }
             CreateEnemy(key, player);
          });
          _room.State.players.OnAdd += CreateEnemy;
          _room.State.players.OnRemove += RemoveEnemy;
      }

      private void CreatePlayer(Player player)
      {
         var position = new Vector3(player.pX, player.pY, player.pZ);
         Instantiate(_player, position, Quaternion.identity);
      }
      
      private void CreateEnemy(string key, Player player)
      {
         var position = new Vector3(player.pX, player.pY, player.pZ);
         var enemy = Instantiate(_enemy, position, Quaternion.identity);
         enemy.Init(player);
      }

      private void RemoveEnemy(string key, Player enemy)
      {
         
      }
       
      protected override void OnDestroy()
      {
         base.OnDestroy();
         _room.Leave();
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
   }

