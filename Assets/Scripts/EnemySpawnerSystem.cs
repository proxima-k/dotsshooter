using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemySpawnerSystem : SystemBase
{
    protected override void OnCreate() {
        RequireForUpdate<EnemySpawnerConfig>();
    }

    protected override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.A)) {
            EnemySpawnerConfig enemySpawnerConfig = SystemAPI.GetSingleton<EnemySpawnerConfig>();

            for (int i = 0; i < 100; i++) {
                Entity spawnedEntity = EntityManager.Instantiate(enemySpawnerConfig.EnemyPrefabEntity);
                SystemAPI.SetComponent(spawnedEntity, new LocalTransform {
                    Position = new float3(UnityEngine.Random.Range(-10f, 10f), 0.6f, UnityEngine.Random.Range(-4f, 7f)),
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
                
                // get speed from EnemyMovementConfig
                EnemyMovementConfig enemyMovementConfig = SystemAPI.GetComponent<EnemyMovementConfig>(spawnedEntity);
                
                SystemAPI.SetComponent(spawnedEntity, new EnemyMovementConfig {
                    Speed = enemyMovementConfig.Speed,
                    TargetPosition = new float3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f))
                });
            }
        }
    }
}
