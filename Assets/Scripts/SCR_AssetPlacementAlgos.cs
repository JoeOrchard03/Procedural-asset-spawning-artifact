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

    public void GenerateDungeon()
    {
        Debug.Log("Generating dungeon");
        roomFirstGeneratorInstance.GenerateDungeon();
        Invoke(nameof(CheckTiles), 3.0f);
    }

    private void CheckTiles()
    {
        foreach(GameObject floorTile in gridManagerInstance.floorTileObjs)
        {
            if (!playerAgentInstance.pathNodes.Contains(floorTile.GetComponent<SCR_NodeBase>()))
            {
                RandomSpawnChance(floorTile);
            }
        }
    }

    private void RandomSpawnChance(GameObject floorTile)
    {
        var randomNumber = Random.Range(minimumRandomSpawnNumber, maximumRandomSpawnNumber);
        if(randomNumber <= randomSpawnNumber)
        {
            SpawnAsset(floorTile);
        }
    }

    private void SpawnAsset(GameObject floorTile)
    {
        Debug.Log("Spawning asset on: " + floorTile.name);
        Instantiate(assetPlaceHolder, floorTile.transform);
    }
}
