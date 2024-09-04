using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public float Lifetime;
    public float3 Direction;

    private class Baker : Baker<BulletAuthoring> {
        public override void Bake(BulletAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BulletConfig
            {
                Damage = authoring.Damage,
                Speed = authoring.Speed,
                Lifetime = authoring.Lifetime,
                Direction = authoring.Direction
            });
        }
    }
}

public struct BulletConfig : IComponentData
{
    public float Damage;
    public float Speed;
    public float Lifetime;
    public float TimeAlive;
    public float3 Direction;
}