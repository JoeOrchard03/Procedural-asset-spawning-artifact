### Working Notes: ### 

Generating 2D dungeon in unity using random walk and binary space partitioning.

General overview:
Dungeon is a room or set of rooms
All areas should be traversible
Rooms need corridors connecting those areas

Two main methods of generation:

Corridor First:
Create a series of corridors so you know they are connected togehter and traversible
After you select points on the corridor and use those as the origin to create a room using a different method like random walk

Room First:
Create the rooms first
After connect them with corridors

Algorithms:

Random walk:
Place an agent at a position
They select a random direction
Walk 1 step towards this diretion
Define how many steps the agent should take in total
Can do multiple iterations over the same area, selecting a position that has already been filled as the starting location for the next iteration to ensure that they are connected

Binary space partitioning algorithm:
Split a given area into smaller ones until the areas are too small to split but can fit a room size that is pre determined
Add offset to ensure the rooms are seperated by walls

Dungeon tileset from here:
https://pixel-poem.itch.io/dungeon-assetpuck

�2D Pixel Dungeon Asset Pack by Pixel_Poem�. Itch.Io, https://pixel-poem.itch.io/dungeon-assetpuck. Accessed 3 Feb. 2025.


![alt text](image.png)