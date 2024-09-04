using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovementAuthoring : MonoBehaviour {
    public float Speed;
    public GameObject Target;

    private class Baker : Baker<EnemyMovementAuthoring> {
        public override void Bake(EnemyMovementAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyMovementConfig {
                Speed = authoring.Speed
            });
        }
    }
}

public struct EnemyMovementConfig : IComponentData
{
    public float Speed;
    public Entity TargetEntity;
}