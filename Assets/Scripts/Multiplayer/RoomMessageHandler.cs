using Colyseus;
using UnityEngine;

[RequireComponent(typeof(CharacterSpawner))]
public class RoomMessageHandler : MonoBehaviour, IGetColyseusRoom
{
    private ColyseusRoom<State> _room;
    private Storage<GameObject> _storage;

    void IGetColyseusRoom.SendRoom(ColyseusRoom<State> room)
    {
        _room = MultiplayerManager.Instance.GetRoom();
        _storage = GetComponent<CharacterSpawner>().GetStorage(); //TO DO Убрать зависимость, уйти от монобехов
        _room.OnMessage<string>("Shoot", ApplyShoot);
        _room.OnMessage<string>("Death", ApplyDeath);
    }

    private void ApplyDeath(string deadId)
    {
        if (!_storage.HasElement(deadId, out var enemy ))
        {
            return;
        } 
        enemy.GetComponent<DollSpawner>().CreateDoll();
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

public interface IGetColyseusRoom
{
    void SendRoom(ColyseusRoom<State> room);
}
