using Unity.Entities;
using UnityEngine;

public class PlayerShootConfigAuthoring : MonoBehaviour
{
    public GameObject BulletPrefab;
    
    public float ShootCooldown;
    public float BulletSpeed;
    public float BulletLifetime;
    public float BulletDamage;

    private class Baker : Baker<PlayerShootConfigAuthoring>
    {
        public override void Bake(PlayerShootConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new PlayerShootConfig
            {
                BulletPrefabEntity = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                ShootCooldown = authoring.ShootCooldown,
                BulletSpeed = authoring.BulletSpeed,
                BulletLifetime = authoring.BulletLifetime,
                BulletDamage = authoring.BulletDamage
            });
        }
    }
}

public struct PlayerShootConfig : IComponentData
{
    public Entity BulletPrefabEntity;
    
    public float ShootCooldown;
    public float BulletSpeed;
    public float BulletLifetime;
    public float BulletDamage;
    
    public float TimeSinceLastShot;
    
    public bool CanShoot => TimeSinceLastShot >= ShootCooldown;
}