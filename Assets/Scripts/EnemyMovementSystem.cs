using System.Collections;
using System.Collections.Generic;
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

    protected override void OnUpdate() {
        foreach ((RefRW<LocalTransform> localTransform, RefRO<EnemyMovementConfig> enemyMovementConfig)
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnemyMovementConfig>>()) {
            
            // if squared distance is less than 1, set new target position
            if (math.lengthsq(enemyMovementConfig.ValueRO.TargetPosition - localTransform.ValueRO.Position) < 1) {
                continue;
            }
            
            
            float3 moveDirection = math.normalize(enemyMovementConfig.ValueRO.TargetPosition - localTransform.ValueRO.Position);
            localTransform.ValueRW = localTransform.ValueRW.Translate(moveDirection * enemyMovementConfig.ValueRO.Speed * SystemAPI.Time.DeltaTime);
        }
    }
}
