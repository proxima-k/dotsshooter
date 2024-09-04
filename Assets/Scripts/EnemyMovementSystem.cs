using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemyMovementSystem : SystemBase
{
    protected override void OnCreate() {
        RequireForUpdate<EnemyMovementConfig>();
    }

    [BurstCompile]
    protected override void OnUpdate() {
        
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        
        foreach ((RefRW<LocalTransform> localTransform, RefRO<EnemyMovementConfig> enemyMovementConfig, Entity enemyEntity)
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnemyMovementConfig>>().WithEntityAccess()) {
            
            float3 targetPosition = SystemAPI.GetComponent<LocalTransform>(enemyMovementConfig.ValueRO.TargetEntity).Position;
            float3 selfPosition = localTransform.ValueRO.Position;
            
            if (math.lengthsq(targetPosition - selfPosition) < 0.01f) {
                entityCommandBuffer.DestroyEntity(enemyEntity);
                continue;
            }
            
            float3 moveDirection = math.normalize(targetPosition - selfPosition);
            localTransform.ValueRW = localTransform.ValueRW.Translate(moveDirection * enemyMovementConfig.ValueRO.Speed * SystemAPI.Time.DeltaTime);
        }
        
        entityCommandBuffer.Playback(EntityManager);
    }
}
