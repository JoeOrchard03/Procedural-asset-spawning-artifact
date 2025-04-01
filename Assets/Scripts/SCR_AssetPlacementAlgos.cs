using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_AssetPlacementAlgos : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] SCR_RoomFirstDungeonGenerator roomFirstGeneratorInstance;
    [SerializeField] SCR_PlayerAgent playerAgentInstance;
    [SerializeField] SCR_GridManager gridManagerInstance;
    [SerializeField] GameObject assetPlaceHolder;

    [SerializeField] float minimumRandomSpawnNumber = 0;
    [SerializeField] float maximumRandomSpawnNumber = 11;
    [SerializeField] float randomSpawnNumber = 3;

    private List<SCR_NodeBase> corridorTiles = new List<SCR_NodeBase>();

    #region "Path first generation/placement"
    public void GenerateDungeonPathFirst()
    {
        Debug.Log("Generating dungeon");
        //Generate dungeon and path
        roomFirstGeneratorInstance.GenerateDungeon(true);
        Invoke(nameof(CheckTilesNotInPath), 3.0f);
    }

    private void CheckTilesNotInPath()
    {
        foreach(GameObject floorTile in gridManagerInstance.floorTileObjs)
        {
            //Checking that the path does not contain the nodes that will be considered for asset placement
            if (!playerAgentInstance.pathNodes.Contains(floorTile.GetComponent<SCR_NodeBase>()))
            {
                RandomSpawnChance(floorTile);
            }
        }
    }

    #endregion

    #region "Corridor considering generation/placement"

    public void GenerateDungeonCorridorsConsidered()
    {
        //Generates dungeon without path
        roomFirstGeneratorInstance.GenerateDungeon(false);
        Invoke(nameof(CheckTilesNotInCorridor), 0.5f);
        Invoke(nameof(GeneratePath), 1.5f);
    }

    private void CheckTilesNotInCorridor()
    {
        //Gets the node bases of the tiles that make up the corridors
        foreach(var tilePosition in roomFirstGeneratorInstance.corridors)
        {
            corridorTiles.Add(gridManagerInstance.GetTileAtPosition(tilePosition + new Vector2(0.5f, 0.5f), true));
            Debug.Log("Adding tile: " + gridManagerInstance.GetTileAtPosition(tilePosition).gameObject.name + " to corridor tiles"); 
        }

        foreach (GameObject floorTile in gridManagerInstance.floorTileObjs)
        {
            //Makes sure tiles that are in the corridorTiles list are not considered for asset placement
            if (!corridorTiles.Contains(floorTile.GetComponent<SCR_NodeBase>()))
            {
                RandomSpawnChance(floorTile, true);
            }
        }
    }

    #endregion

    private void RandomSpawnChance(GameObject floorTile, bool markAssetPlaced = false)
    {
        //Random is used to have odds for whether an asset is placed or not
        var randomNumber = Random.Range(minimumRandomSpawnNumber, maximumRandomSpawnNumber);
        if(randomNumber <= randomSpawnNumber && !CheckTileNotStartOrEndDoor(floorTile))
        {
            SpawnAsset(floorTile);
            if(markAssetPlaced)
            {
                //If path is generated after this the tile is marked as not walkable so it is pathfinded around instead of through
                markTileNotWalkable(floorTile);
            }
        }
    }

    private void markTileNotWalkable(GameObject floorTile)
    {
        SCR_NodeBase tileNodeBase = floorTile.GetComponent<SCR_NodeBase>();
        tileNodeBase.walkable = false;
    }

    private void SpawnAsset(GameObject floorTile)
    {
        Debug.Log("Spawning asset on: " + floorTile.name);
        Instantiate(assetPlaceHolder, floorTile.transform);
    }

    private void GeneratePath()
    {
        playerAgentInstance.FindPath();
    }
    
    //Check to make sure that assets are not placed on the start or end doors as this causes paths to not work
    private bool CheckTileNotStartOrEndDoor(GameObject floorTile)
    {
        if(floorTile.name.Contains("Door"))
        {
            return true;
        }
        return false;
    }
}
