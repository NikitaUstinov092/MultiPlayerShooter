using System.Collections.Generic;
using Colyseus;
using UnityEngine;
using Object = UnityEngine.Object;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
   {
      [SerializeField]
      private GameObject _player;
      
      [SerializeField]
      private GameObject _enemy;
      
      private ColyseusRoom<State> _room;
      
      private Storage<GameObject> _storage = new Storage<GameObject>();
      
      private CharacterFactory _characterFactory = new ();

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
         var data = new Dictionary<string, object>
         {
            {"speed", _player.GetComponent<CharacterMove>().Speed}, 
            {"hp", _player.GetComponent<HeroHealth>().GetMaxHP}
         };
         
         _room = await Instance.client.JoinOrCreate<State>("state_handler", data);
         _room.OnStateChange += OnChanged;

         _room.OnMessage<string>("Shoot", ApplyShoot);
      }
      
      private void OnChanged(State state, bool isfirststate)
      {
          if(!isfirststate)
             return;
          
          state.players.ForEach((key, player) =>
          {
             if (key == _room.SessionId)
             {
                _characterFactory.CreateCharacter(player, _player); //Cоздаём персонажа управляемого текущим клиентом (Hero)
                return;
             }
             CreateEnemy(key, player); //Cоздаём оппонента (Enemy)
          });
          
          _room.State.players.OnAdd += CreateEnemy;
          _room.State.players.OnRemove += DestroyEnemy;
      }
      private void CreateEnemy(string key, Player player)
      {
         var enemy = _characterFactory.CreateCharacter(player, _enemy);
         enemy.GetComponent<EnemyMoveDataReciever>().Init(player); 
         _storage.Add(key,enemy);
      }
      
      private void DestroyEnemy(string key, Player player)
      {
         if(!_storage.HasElement(key, out var enemy))
            return;
         
         _storage.Remove(key);
         enemy.GetComponent<EnemyMoveDataReciever>().Destroy();
      }
       
      protected override void OnDestroy()
      {
         _room.OnStateChange -= OnChanged;
         base.OnDestroy();
         _room.Leave();
      }

      private void ApplyShoot(string jsonShootInfo)
      {
         var shootInfo = JsonUtility.FromJson<ShootInfo>(jsonShootInfo);

         if (!_storage.HasElement(shootInfo.Key, out var enemy ))
         {
            return;
         }
         enemy.GetComponent<EnemyShoot>().Shoot(shootInfo);
      }
   }


public class CharacterFactory
{
   public GameObject CreateCharacter(Player player, GameObject prefab)
   {
      var position = new Vector3(player.pX, player.pY, player.pZ);
      var character = Object.Instantiate(prefab, position, Quaternion.identity);
      return character;
   }
}




