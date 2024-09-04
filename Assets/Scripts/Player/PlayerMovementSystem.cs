using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerMovementSystem : SystemBase
{
    protected override void OnCreate() {
        RequireForUpdate<PlayerMovementConfig>();
    }

    [BurstCompile]
    protected override void OnUpdate() {
        float deltaTime = SystemAPI.Time.DeltaTime;

        // float2 input;
        // input.x = Input.GetAxisRaw("Horizontal");
        // input.y = Input.GetAxisRaw("Vertical");
        float input = Input.GetAxisRaw("Horizontal");

        foreach ((RefRW<LocalTransform> localTransform, RefRO<PlayerMovementConfig> playerMovementConfig)
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerMovementConfig>>()) {

            float3 currentPosition = localTransform.ValueRO.Position;
            localTransform.ValueRW.Position = new float3(currentPosition.x, 0, currentPosition.z) + new float3(input,0,0) * playerMovementConfig.ValueRO.Speed * deltaTime;
            // localTransform.ValueRW.Translate(new float3(input.x, 0, input.y) * playerMovementConfig.ValueRO.Speed * deltaTime);
        }
    }
    
}
