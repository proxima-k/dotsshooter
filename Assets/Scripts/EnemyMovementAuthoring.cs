using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovementAuthoring : MonoBehaviour {
    public float Speed;
    public float3 TargetPosition;

    private class Baker : Baker<EnemyMovementAuthoring> {
        public override void Bake(EnemyMovementAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyMovementConfig {
                Speed = authoring.Speed,
                TargetPosition = new float3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f))
            });
        }
    }
}

public struct EnemyMovementConfig : IComponentData
{
    public float Speed;
    public float3 TargetPosition;
}