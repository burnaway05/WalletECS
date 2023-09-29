using Unity.Entities;
using UnityEngine;

namespace Wallet
{
    public class Game : MonoBehaviour
    {
        void Start()
        {
            var entity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent<Coin>(entity);
        }
    }
}