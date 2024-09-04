using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawnerConfigAuthoring : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject player;

    private class Baker : Baker<EnemySpawnerConfigAuthoring> {
        public override void Bake(EnemySpawnerConfigAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity, new EnemySpawnerConfig {
                EnemyPrefabEntity = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                PlayerEntity = GetEntity(authoring.player, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct EnemySpawnerConfig : IComponentData {
    public Entity EnemyPrefabEntity;
    public Entity PlayerEntity;
}