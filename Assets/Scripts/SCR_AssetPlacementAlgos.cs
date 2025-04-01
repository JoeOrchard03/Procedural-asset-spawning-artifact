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

    public void GenerateDungeonPathFirst()
    {
        Debug.Log("Generating dungeon");
        roomFirstGeneratorInstance.GenerateDungeon(true);
        Invoke(nameof(CheckTilesNotInPath), 3.0f);
    }

    public void GenerateDungeonCorridorsConsidered()
    {
        roomFirstGeneratorInstance.GenerateDungeon(false);
        Invoke(nameof(CheckTilesNotInCorridor), 0.5f);
        Invoke(nameof(GeneratePath), 1.5f);
    }

    private void CheckTilesNotInCorridor()
    {
        foreach(var tilePosition in roomFirstGeneratorInstance.corridors)
        {
            corridorTiles.Add(gridManagerInstance.GetTileAtPosition(tilePosition + new Vector2(0.5f, 0.5f), true));
            Debug.Log("Adding tile: " + gridManagerInstance.GetTileAtPosition(tilePosition).gameObject.name + " to corridor tiles"); 
        }

        foreach (GameObject floorTile in gridManagerInstance.floorTileObjs)
        {
            if (!corridorTiles.Contains(floorTile.GetComponent<SCR_NodeBase>()))
            {
                RandomSpawnChance(floorTile, true);
            }
        }
    }

    private void CheckTilesNotInPath()
    {
        foreach(GameObject floorTile in gridManagerInstance.floorTileObjs)
        {
            if (!playerAgentInstance.pathNodes.Contains(floorTile.GetComponent<SCR_NodeBase>()))
            {
                RandomSpawnChance(floorTile);
            }
        }
    }

    private void RandomSpawnChance(GameObject floorTile, bool markAssetPlaced = false)
    {
        var randomNumber = Random.Range(minimumRandomSpawnNumber, maximumRandomSpawnNumber);
        if(randomNumber <= randomSpawnNumber && !CheckTileNotStartOrEndDoor(floorTile))
        {
            SpawnAsset(floorTile);
            if(markAssetPlaced)
            {
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
    
    private bool CheckTileNotStartOrEndDoor(GameObject floorTile)
    {
        if(floorTile.name.Contains("Door"))
        {
            return true;
        }
        return false;
    }
}
