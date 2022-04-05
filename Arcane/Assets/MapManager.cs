using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private List<Tilemap> tilemaps;
    [SerializeField]
    private Tilemap currentTilemap;
    public GameObject player;
    public int gridSize = 100;
    private Vector2 currentGridPos;
    public Grid mapGrid;

    Dictionary<Vector2, Tilemap> mapDic = new Dictionary<Vector2, Tilemap>();

    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;


    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in tileDatas)
        {
            foreach(var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }

        currentGridPos = new Vector2(0, 0);
    }

    private void Start()
    {
        for(int x = -1; x < 2; x++)
        {
            for(int y = -1; y < 2; y++)
            {
                Tilemap newGrid = Instantiate(tilemaps[0], mapGrid.transform);
                newGrid.transform.position = new Vector2(x * gridSize, y * gridSize);
                mapDic.Add(new Vector2(x, y), newGrid);

                if(x == 0 && y == 0)
                {
                    currentTilemap = newGrid;
                }
            }
        }

        player.SetActive(true);
    }

    private void Update()
    {
        if(player.transform.position.x < (currentGridPos.x - 1) * gridSize + gridSize/2)
        {
            currentGridPos.x--;
            NewCurrentGrid();
            MapCleanUp();
        }
        else if(player.transform.position.x > (currentGridPos.x + 1) * gridSize - gridSize/2)
        {
            currentGridPos.x++;
            NewCurrentGrid();
            MapCleanUp();
        }
        else if(player.transform.position.y < (currentGridPos.y - 1) * gridSize + gridSize/2)
        {
            currentGridPos.y--;
            NewCurrentGrid();
            MapCleanUp();
        }
        else if(player.transform.position.y > (currentGridPos.y + 1) * gridSize - gridSize/2)
        {
            currentGridPos.y++;
            NewCurrentGrid();
            MapCleanUp();
        }


        if(player && player.GetComponent<PlayerMovement>().isWalking)
        {
            Vector3Int gridPosition = currentTilemap.WorldToCell(player.GetComponent<Rigidbody2D>().position);
            TileBase currentTile = currentTilemap.GetTile(gridPosition);

            float speedModifier = dataFromTiles[currentTile].speedModifier;
            player.GetComponent<PlayerMovement>().speedMultiplier = speedModifier;
        }
    }

    private void NewCurrentGrid()
    {
        currentTilemap = mapDic[currentGridPos];

        for(int x = -1; x < 2; x++)
        {
            for(int y = -1; y < 2; y++)
            {
                Vector2 tileCoor = new Vector2(currentGridPos.x + x, currentGridPos.y + y);

                if(mapDic.ContainsKey(tileCoor))
                {
                    mapDic[tileCoor].gameObject.SetActive(true);
                }
                else
                {
                    Tilemap newGrid = Instantiate(tilemaps[0], mapGrid.transform);
                    newGrid.transform.position = new Vector2(tileCoor.x * gridSize, tileCoor.y * gridSize);
                    mapDic.Add(tileCoor, newGrid);
                }
            }
        }
    }

    private void MapCleanUp()
    {
        foreach(KeyValuePair<Vector2, Tilemap> mapChunk in mapDic)
        {
            if(Mathf.Abs(mapChunk.Key.x - currentGridPos.x) > 2 || Mathf.Abs(mapChunk.Key.y - currentGridPos.y) > 2)
            {
                mapChunk.Value.gameObject.SetActive(false);
            }
        }
    }
}
