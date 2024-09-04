using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerMovementConfigAuthoring : MonoBehaviour
{
    public GameObject Player;
    public float Speed;

    private class Baker : Baker<PlayerMovementConfigAuthoring>
    {
        public override void Bake(PlayerMovementConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerMovementConfig
            {
                TargetEntity = GetEntity(authoring.Player, TransformUsageFlags.Dynamic),
                Speed = authoring.Speed
            });
        }
    }
}

public struct PlayerMovementConfig : IComponentData
{
    public Entity TargetEntity;
    public float Speed;
}
