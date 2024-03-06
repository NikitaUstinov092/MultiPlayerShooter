using System.Collections.Generic;
using Colyseus;
using UnityEngine;

   public class MultiplayerManager : ColyseusManager<MultiplayerManager>
   {
      [SerializeField]
      private GameObject _player;
      
      [SerializeField]
      private EnemyController _enemy;
      
      private ColyseusRoom<State> _room;
      protected override void Awake()
      {
         base.Awake();
         Instance.InitializeClient();
         Connect();
      }

      private async void Connect()
      {
         _room = await Instance.client.JoinOrCreate<State>("state_handler");
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
         var position = new Vector3(player.x, 0, player.y);
         Instantiate(_player, position, Quaternion.identity);
      }
      
      private void CreateEnemy(string key, Player enemy)
      {
         var position = new Vector3(enemy.x, 0, enemy.y);
         var enemy1 = Instantiate(_enemy, position, Quaternion.identity);
         enemy.OnChange += enemy1.OnChange;
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
   }

