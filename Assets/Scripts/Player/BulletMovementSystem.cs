using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class BulletMovementSystem : SystemBase {
    
    protected override void OnCreate() {
        RequireForUpdate<BulletConfig>();
    }

    protected override void OnUpdate() {
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        
        foreach ((RefRW<LocalTransform> localTransform, RefRW<BulletConfig> bulletConfig, Entity bulletEntity)
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BulletConfig>>().WithEntityAccess()) {
            
            
            if (bulletConfig.ValueRO.TimeAlive > bulletConfig.ValueRO.Lifetime) {
                entityCommandBuffer.DestroyEntity(bulletEntity);
                continue;
            }
            
            localTransform.ValueRW = localTransform.ValueRW.Translate(bulletConfig.ValueRO.Direction * bulletConfig.ValueRO.Speed * SystemAPI.Time.DeltaTime);
            bulletConfig.ValueRW = new BulletConfig {
                Damage = bulletConfig.ValueRO.Damage,
                Speed = bulletConfig.ValueRO.Speed,
                Lifetime = bulletConfig.ValueRO.Lifetime,
                TimeAlive = bulletConfig.ValueRO.TimeAlive + SystemAPI.Time.DeltaTime,
                Direction = bulletConfig.ValueRO.Direction
            };
        }
        
        entityCommandBuffer.Playback(EntityManager);
    }
}
