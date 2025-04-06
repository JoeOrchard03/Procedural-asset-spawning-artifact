using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_AssetPlacementAlgos : MonoBehaviour
{
    [SerializeField] SCR_RoomFirstDungeonGenerator roomFirstGeneratorInstance;
    [SerializeField] SCR_PlayerAgent playerAgentInstance;
    [SerializeField] SCR_GridManager gridManagerInstance;
    [SerializeField] GameObject assetPlaceHolder;

    [Header("Variables controlling asset spawn chance")]
    [SerializeField] float minimumRandomSpawnNumber = 0;
    [SerializeField] float maximumRandomSpawnNumber = 11;
    [SerializeField] float randomSpawnNumber = 3;

    private List<SCR_NodeBase> corridorTiles = new List<SCR_NodeBase>();

    [Header("Variables controlling whether tests repeat and how many times")]
    [SerializeField] bool repeatingTest = false;
    [SerializeField] public int numberOfRepetitions = 3;
    [SerializeField] public int numberOfSuccessfulIterations; 
    [SerializeField] public string typeOfAssetPlacement;

    #region "Path first generation/placement"
    public void GenerateDungeonPathFirst()
    {
        Debug.Log("Generating dungeon");
        typeOfAssetPlacement = "Path first asset placement";
        //Generate dungeon and path
        if(repeatingTest)
        {
            StartCoroutine(PathFirstGenerationRepeating());
        }
        else
        {
            roomFirstGeneratorInstance.GenerateDungeon(true);
            Invoke(nameof(CheckTilesNotInPath), 1.5f);
        } 
    }

    private IEnumerator PathFirstGenerationRepeating()
    {
        for (int i = 0; i < numberOfRepetitions; i++)
        {
            //Resets number of succesful iterations
            if (i == 0)
            {
                numberOfSuccessfulIterations = 0;
            }
            //Tracks what iteration it is on
            Debug.Log("Running path first placement, generation number: " + (i + 1));
            //generates dungeon and path
            roomFirstGeneratorInstance.GenerateDungeon(true);
            //waits half a second to make sure it does not overlap with dungeon being generated
            yield return new WaitForSeconds(0.5f);
            CheckTilesNotInPath();
            //waits so there is a chance to see the assets placed before moving on to next iteration
            yield return new WaitForSeconds(0.25f);
        }
        Debug.Log($"Path first placement has suceeded {numberOfSuccessfulIterations} times out of {numberOfRepetitions} iterations!");
    }

    private void CheckTilesNotInPath()
    {
        foreach(GameObject floorTile in gridManagerInstance.floorTileObjs)
        {
            //Checking that the path does not contain the nodes that will be considered for asset placement
            if (!playerAgentInstance.pathNodes.Contains(floorTile.GetComponent<SCR_NodeBase>()))
            {
                //If not decide whether or not to spawn an asset on the given tile
                RandomSpawnChance(floorTile);
            }
        }
    }

    #endregion

    #region "Corridor considering generation/placement"

    public void GenerateDungeonCorridorsConsidered()
    {
        //Generates dungeon without path
        typeOfAssetPlacement = "Corridor considered asset placement";
        if (repeatingTest)
        {
            StartCoroutine(CorridorConsideredGenerationRepeating());
        }
        else
        {
            roomFirstGeneratorInstance.GenerateDungeon(false);
            Invoke(nameof(CheckTilesNotInCorridor), 0.5f);
            Invoke(nameof(GeneratePath), 1.5f);
        }
    }

    private IEnumerator CorridorConsideredGenerationRepeating()
    {
        for (int i = 0; i < numberOfRepetitions; i++)
        {
            if (i == 0)
            {
                numberOfSuccessfulIterations = 0;
            }
            Debug.Log("Running corridor considered placement, generation number: " + (i + 1));
            roomFirstGeneratorInstance.GenerateDungeon(false);
            yield return new WaitForSeconds(0.25f);
            CheckTilesNotInCorridor();
            yield return new WaitForSeconds(0.25f);
            GeneratePath();
            yield return new WaitForSeconds(0.25f);
        }
        Debug.Log($"Corridor considered placement has suceeded {numberOfSuccessfulIterations} times out of {numberOfRepetitions} iterations!");
    }

    private void CheckTilesNotInCorridor()
    {
        //Gets the node bases of the tiles that make up the corridors
        foreach(var tilePosition in roomFirstGeneratorInstance.corridors)
        {
            corridorTiles.Add(gridManagerInstance.GetTileAtPosition(tilePosition + new Vector2(0.5f, 0.5f), true));
            //Debug.Log("Adding tile: " + gridManagerInstance.GetTileAtPosition(tilePosition).gameObject.name + " to corridor tiles"); 
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

    #region "Place assets completely randomly"

    public void GeneratePlaceAssetsRandomly()
    {
        typeOfAssetPlacement = "Random asset placement";
        if (repeatingTest)
        {
            StartCoroutine(RandomPlacementRepeating());
        }
        else
        {
            roomFirstGeneratorInstance.GenerateDungeon(false);
            Invoke(nameof(PlaceAssetsRandomly), 0.5f);
            Invoke(nameof(GeneratePath), 1.5f);
        }
    }

    private IEnumerator RandomPlacementRepeating()
    {
        for (int i = 0; i < numberOfRepetitions; i++)
        {
            if (i == 0)
            {
                numberOfSuccessfulIterations = 0;
            }
            Debug.Log("Running random placement, generation number: " + (i + 1));
            //Generate dungeon without path
            roomFirstGeneratorInstance.GenerateDungeon(false);
            yield return new WaitForSeconds(0.5f);
            PlaceAssetsRandomly();
            yield return new WaitForSeconds(0.25f);
            //generate path after assets have been placed
            GeneratePath();
            yield return new WaitForSeconds(0.25f);
        }
        Debug.Log($"Random placement has suceeded {numberOfSuccessfulIterations} times out of {numberOfRepetitions} iterations!");
    }

    private void PlaceAssetsRandomly()
    {
        //Randomly places assets across all floor tiles
        foreach (GameObject floorTile in gridManagerInstance.floorTileObjs)
        {
            RandomSpawnChance(floorTile, true);
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
                markTileNotWalkable(floorTile);
            }
        }
    }

    private void markTileNotWalkable(GameObject floorTile)
    {
        //If path is generated after this the tile is marked as not walkable so it is pathfinded around instead of through
        SCR_NodeBase tileNodeBase = floorTile.GetComponent<SCR_NodeBase>();
        tileNodeBase.walkable = false;
    }

    private void SpawnAsset(GameObject floorTile)
    {
        //Spawn a place holder to represent an asset on the floor tiles position 
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
