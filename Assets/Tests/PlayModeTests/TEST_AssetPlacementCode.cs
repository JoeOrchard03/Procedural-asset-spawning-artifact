using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class TEST_AssetPlacementCode
{
    private GameObject testOBJ;
    public SCR_AssetPlacementAlgos placementScriptInstance;

    [SetUp]
    public void SetUp()
    {
        //Assigning necessary valuables to placementScriptInstance
        GameObject testOBJ = new GameObject("AssetPlacementTestOBJ");
        placementScriptInstance = testOBJ.AddComponent<SCR_AssetPlacementAlgos>();

        placementScriptInstance.gridManagerInst = new GameObject("GridManager").AddComponent<SCR_GridManager>();
        placementScriptInstance.playerAgent = new GameObject("PlayerAgent").AddComponent<SCR_PlayerAgent>();
        placementScriptInstance.roomFirstGen = new GameObject("RoomFirstGenerator").AddComponent<SCR_RoomFirstDungeonGenerator>();
        placementScriptInstance.assetPlaceHolder = Resources.Load<GameObject>("Asset place holder");
    }

    [TestFixture]
    public class SpawnFunctionality : TEST_AssetPlacementCode
    {
        [UnityTest]
        public IEnumerator RandomSpawnSpawningWhenGuranteed_Test()
        {
            var tile = new GameObject("Tile");
            tile.AddComponent<SCR_PossiblePathNode>();

            //These values gurantee that assets will spawn if the method is working correctly
            placementScriptInstance.minimumRandomSpawnNumber = 0;
            placementScriptInstance.maximumRandomSpawnNumber = 1;
            placementScriptInstance.randomSpawnNumber = 1;

            placementScriptInstance.assetPlaceHolder = new GameObject("Asset");

            placementScriptInstance.RandomSpawnChance(tile);

            //As assets are spawned on the tiles transform this makes it a child, and it will only have 1 child childcount therefore can be used to check if the asset has spawned
            Assert.AreEqual(1, tile.transform.childCount, "Asset not spawned, is supposed to");

            yield return null;
        }

        [UnityTest]
        public IEnumerator RandomSpawnNotSpawningWhenGuranteedToFail_Test()
        {
            var tile = new GameObject("Tile");
            tile.AddComponent<SCR_PossiblePathNode>();

            //These values gurantee that assets will not spawn if the method is working correctly
            placementScriptInstance.minimumRandomSpawnNumber = 5;
            placementScriptInstance.maximumRandomSpawnNumber = 10;
            placementScriptInstance.randomSpawnNumber = 2;

            placementScriptInstance.assetPlaceHolder = new GameObject("Asset");

            placementScriptInstance.RandomSpawnChance(tile);

            Assert.AreEqual(0, tile.transform.childCount, "Asset has spawned, not supposed to");

            yield return null;
        }

        [UnityTest]
        public IEnumerator AssetNotSpawnOnDoor_Test()
        {
            //Tile has "Door" in it's name therefore should not spawn if method working correctly
            var tile = new GameObject("DoorTile");
            tile.AddComponent<SCR_PossiblePathNode>();

            //These values gurantee that assets will pass the random chance check
            placementScriptInstance.minimumRandomSpawnNumber = 0;
            placementScriptInstance.maximumRandomSpawnNumber = 1;
            placementScriptInstance.randomSpawnNumber = 1;

            placementScriptInstance.assetPlaceHolder = new GameObject("Asset");

            placementScriptInstance.RandomSpawnChance(tile);

            Assert.AreEqual(0, tile.transform.childCount, "Asset has spawned on tile called 'door', not supposed to");

            yield return null;

        }

        [UnityTest]
        public IEnumerator AssetSpawns_Test()
        {
            var tile = new GameObject("Tile");
            tile.AddComponent<SCR_PossiblePathNode>();

            placementScriptInstance.SpawnAsset(tile);

            Assert.AreEqual(1, tile.transform.childCount, "Asset has mot spawned, supposed to");

            yield return null;
        }
    }
}