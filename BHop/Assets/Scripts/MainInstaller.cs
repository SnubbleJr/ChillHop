using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller<MainInstaller>
{
    public BHopBehaviour Player;
    public PhysicsData PhysicsDataObject;
    public RespawnData StartRespawn;
    public RespawnData CheckpointRespawn;
    public override void InstallBindings()
    {
        Container.Bind<BHopBehaviour>().FromInstance(Player);

        Container.Bind<Rigidbody>().WithId("PlayerBody").FromInstance(Player.GetComponent<Rigidbody>());

        Container.Bind<PhysicsData>().FromInstance(PhysicsDataObject);
        Container.Bind<RespawnData>().WithId("StartRespawn").FromInstance(StartRespawn);
        Container.Bind<RespawnData>().WithId("CheckpointRespawn").FromInstance(CheckpointRespawn);
    }
}