using System;
using Colyseus;
using UnityEngine;
using Object = UnityEngine.Object;

public class CharacterSpawner : MonoBehaviour, IGetColyseusRoom
{
   [SerializeField]
   private GameObject _player;
      
   [SerializeField]
   private GameObject _enemy;
   
   private ColyseusRoom<State> _room;
   
   private CharacterFactory _characterFactory = new ();
   private Storage<GameObject> _storage = new Storage<GameObject>();

   public Storage<GameObject> GetStorage() //TO DO Убрать костыль
   {
      return _storage;
   }

   void IGetColyseusRoom.SendRoom(ColyseusRoom<State> colyseusRoom)
   {
      _room = colyseusRoom;
      _room.OnStateChange += OnChanged;
   }
   private void OnDestroy()
   { 
      if(_room!=null) 
         _room.OnStateChange -= OnChanged;
   }
   private void OnChanged(State state, bool isfirststate)
   {
      if(!isfirststate)
         return;
          
      state.players.ForEach((key, player) =>
      {
         if (key == _room.SessionId)
         {
            CreateHero(player);
            return;
         }
         CreateEnemy(key, player); 
      });
          
      _room.State.players.OnAdd += CreateEnemy;
      _room.State.players.OnRemove += DestroyEnemy;
   }

   //Cоздаём персонажа управляемого текущим клиентом (Hero)
   private void CreateHero( Player player)
   {
      var hero = _characterFactory.CreateCharacter(player, _player); 
      hero.GetComponent<HeroDataReciever>().Init(player);
   }
    
   //Cоздаём оппонента (Enemy)
   private void CreateEnemy(string key, Player player)
   {
      var enemy = _characterFactory.CreateCharacter(player, _enemy);
      enemy.GetComponent<EnemyDataReciever>().Init(player); 
      enemy.GetComponent<EnemyDamageSender>().SetPlayerId(key);
      _storage.Add(key,enemy);
   }
   private void DestroyEnemy(string key, Player player)
   {
      if(!_storage.HasElement(key, out var enemy))
         return;
         
      _storage.Remove(key);
      enemy.GetComponent<EnemyDataReciever>().Destroy();
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