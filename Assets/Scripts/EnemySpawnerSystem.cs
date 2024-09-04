using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemySpawnerSystem : SystemBase
{
    protected override void OnCreate() {
        RequireForUpdate<EnemySpawnerConfig>();
    }

    [BurstCompile]
    protected override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnWave();
        }
    }

    private void SpawnWave() {
        EnemySpawnerConfig enemySpawnerConfig = SystemAPI.GetSingleton<EnemySpawnerConfig>();

        for (int i = 0; i < 50; i++) {
            Entity spawnedEntity = EntityManager.Instantiate(enemySpawnerConfig.EnemyPrefabEntity);
            SystemAPI.SetComponent(spawnedEntity, new LocalTransform {
                Position = new float3(UnityEngine.Random.Range(-14f, 14f), 0, UnityEngine.Random.Range(25f, 40f)),
                Rotation = quaternion.identity,
                Scale = 1f
            });
                
            // get speed from EnemyMovementConfig
            EnemyMovementConfig enemyMovementConfig = SystemAPI.GetComponent<EnemyMovementConfig>(spawnedEntity);
                
            SystemAPI.SetComponent(spawnedEntity, new EnemyMovementConfig {
                Speed = enemyMovementConfig.Speed,
                TargetEntity = enemySpawnerConfig.PlayerEntity
            });
        }
    }
}
