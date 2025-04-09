### Computing artifact for asset placement through procedurally generated levels

The artifact's current state is that it supports level generation using procedural generation, more specifcally utilizing the binary space partitoning algorithm to generate rooms, corridors are then connected by mapping the centre of these rooms and connecting them.

The artifact also currently supports path finding using A* pathfinding, it is able to determine whether there is a valid path from the start of the level to the end or whether it is blocked by assets. If a valid path is found, this will be shown by green triangles showing the path from the start to the end of the level.

The artifact also currently supports three different type of asset placement functions these being random, path first and corridor considered asset placement.

Random asset placement is done by generating the level, getting all floor tiles that make up the level and then using random chance to determine whether an asset will be spawned on each one. After the path finding agent will try and determine whether there is a possible route from the start to the end of the dungeon.

Path first asset placement is done by first finding the path from the start to end of the level, after this every tile has a chance for an asset to spawn on it, apart from if that tile is in the path node list that is generated when the path is found.

Corridor considered asset placement is done by generating the level, determining where the corridor tiles are and adding these to a list, after this every tile apart from tiles in the corridor tile list have a chance for an asset to spawn on it. The pathfinding agent then triesto find a route from the start to the end of the dungeon.

By pressing on the path first asset placement button in the hierarchy of the computing artifact and scrolling down to the "SCR_Asset Placement Algos" script the way the assets are placed can be altered for testing, including altering the variables controlling the random chance for an asset to spawn, as well as determining if the iterations should repeat when the buttons are pressed in the scene as well as how many times the algorithm should repeat.

By pressing on the RoomsFirstGen game object in the hierarchy of the computing artifact there are also variables that can be modified, such as changing the minimum size of rooms as well as whether the corridors should use the random walk algorithm.
