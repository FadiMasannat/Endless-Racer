using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class environmentTile
{
    public GameObject prefab;
    [Tooltip("How far in front of the previous tile the next one should be placed")]
    public float length;
    [Tooltip("Randomly selects between the two values for the horizontal placement of the tile.")]
    public Vector2 xPositionRange;
    [Tooltip("Randomly selects between the two values for the vertical placement of the tile.")]
    public Vector2 heightRange;
}

//To add a class as a component it has to inherit from monobehaviour, but if I make environmentTile a monobehaviour, then you can't customize it in the editor, so this is a really janky fix.
public class tileData : MonoBehaviour
{
    public GameObject prefab;
    public float length;
    public Vector2 xPositionRange;
    public Vector2 heightRange;
}

public class EnvironmentMovement : MonoBehaviour
{
    [Tooltip("Number of tiles to generate in front of the car")]
    public float worldViewDistance = 5;
    [Tooltip("The speed that the tiles move backwards (The car doesn't move, the ground moves under the car)")]
    public float movementSpeed = 1;
    [Tooltip("The objects to be generated in each 'slice' of the terrain")]
    public environmentTile[] environmentPieces;

    [Tooltip("A parent object to store all of the instantiated tiles")]
    public Transform environmentParent;

    // Start is called before the first frame update
    void Start()
    {
        //how many slices to make
        for(int i = 0; i < worldViewDistance; i++)
        {
            //generate a terrain slice
            for (int t = 0; t < environmentPieces.Length; t++)
            {
                environmentTile tile = environmentPieces[t];
                GameObject prefab = Instantiate(tile.prefab, environmentParent);
                prefab.transform.position = new Vector3(Random.Range(tile.xPositionRange.x, tile.xPositionRange.y), Random.Range(tile.heightRange.x, tile.heightRange.y), tile.length * i);
                tileData data = prefab.AddComponent<tileData>();
                data.prefab = tile.prefab;
                data.length = tile.length;
                data.xPositionRange = tile.xPositionRange;
                data.heightRange = tile.heightRange;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform tile in environmentParent)
        {
            tile.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            tileData data = tile.GetComponent<tileData>();
            if (tile.position.z < -data.length)
            {
                tile.position = new Vector3(Random.Range(data.xPositionRange.x, data.xPositionRange.y), Random.Range(data.heightRange.x, data.heightRange.y), data.length * (worldViewDistance-1));
            }
        }
    }
}
