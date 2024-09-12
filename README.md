# Controls
A/D - move left and right  
LMB (Left Mouse Button) - Shoot  
Spacebar - Spawn wave  

# Documentation
### code structure
- All scripts have a suffix attached to them. Namely, Authoring, System, Config.
- Config structs are the data structs that are attached to entities.
- Authoring classes are responsible for baking components.
- System scripts operate on the data structs.  

### system
- The enemy holds an Entity reference so that the EnemyMovementSystem script doesn't have to keep finding the player in every update loop.
- An EntityCommandBuffer is utilized within the EnemyMovementSystem to prevent looping through something that is prone to structural change. (Like destroying an enemy entity when it gets close to a player)
- It goes the same with the BulletMovementSystem.
