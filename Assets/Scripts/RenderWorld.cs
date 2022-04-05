using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderWorld : MonoBehaviour
{
    int[,] map;

    public Tilemap tilemap;
    public TileBase tile; 
    public TileBase tilenull;
 /* 
    // Start is called before the first frame update
    void Start()
    {
        //map = GenerateArray(100, 100, false);
        //map = new int[8, 8] {{ 0,1,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0},
                            { 0,0,0,1,1,0,0,0},
                            { 0,0,1,1,1,0,0,0},
                            { 0,0,0,0,0,0,0,0},
                            { 0,0,0,0,1,1,1,1},
                            { 0,0,0,0,1,1,0,0},
                            { 1,0,0,0,0,0,0,0}};
        //RenderMap(map, tilemap, tile, tilenull, (-8, 8)); //top left
        //RenderMap(map, tilemap, tile,tilenull,(0,8));   //top right
        //RenderMap(map, tilemap, tile, tilenull, (-8, 0));// bottom left
        //RenderMap(map, (0, 0));// bottom right
    }
*/
    // Update is called once per frame
    void Update()
    {
        
    }


    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                if (empty) {
                    map[x, y] = 0;
                }
                else {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }
   

    public void RenderMap(int[,] map, (int x,int y)offset)
    {
        //Loop through the width of the map
        for (int y = 0; y <= map.GetUpperBound(0); y++) {
            //Loop through the height of the map
            for (int x = 0; x <= map.GetUpperBound(1); x++) {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1) {
                    
                    tilemap.SetTile(new Vector3Int(y+offset.x, -x+offset.y, 0), tile);
                }
                else {
                    tilemap.SetTile(new Vector3Int(y+offset.x, -x+offset.y, 0), tilenull);
                }
            }
        }
    }
}
