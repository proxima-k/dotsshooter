using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerShootSystem : SystemBase
{
    protected override void OnCreate() {
        RequireForUpdate<Player>();
    }

    protected override void OnUpdate() {
        
        foreach ((RefRW<PlayerShootConfig> playerShootConfig, RefRO<LocalTransform> localTransform)
                 in SystemAPI.Query<RefRW<PlayerShootConfig>, RefRO<LocalTransform>>()) {
            
            playerShootConfig.ValueRW.TimeSinceLastShot += SystemAPI.Time.DeltaTime;
            
            if (!Input.GetMouseButton(0) || !playerShootConfig.ValueRO.CanShoot) {
                return;
            }
            
         
            playerShootConfig.ValueRW.TimeSinceLastShot = 0f;
            Entity bulletEntity = EntityManager.Instantiate(playerShootConfig.ValueRO.BulletPrefabEntity);
            
            SystemAPI.SetComponent(bulletEntity, new LocalTransform {
                Position = localTransform.ValueRO.Position,
                Rotation = localTransform.ValueRO.Rotation,
                Scale = 1f
            });
            
            SystemAPI.SetComponent(bulletEntity, new BulletConfig {
                Speed = playerShootConfig.ValueRO.BulletSpeed,
                Lifetime = playerShootConfig.ValueRO.BulletLifetime,
                Damage = playerShootConfig.ValueRO.BulletDamage,
                Direction = localTransform.ValueRO.Forward()
            });
            
        }
    }
    
}
