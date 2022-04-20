using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderWorld : MonoBehaviour
{
    int[,] map;

    public Tilemap tilemap;
    public Tilemap resourceTilemap;
    public TileBase tile; 
    public TileBase tilenull;
    
    public static int sectorSize;

    // Start is called before the first frame update
    void Start()
    {
        sectorSize = 50;

        map = GenerateArray(50, 50, true);
        /*
        map = new int[8, 8] {{ 0,1,0,0,0,0,0,0},
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
        map[0,map.GetUpperBound(0) ] = 1;
        map[0, 0] = 1;
        map[map.GetUpperBound(0), 0] = 1;
        RenderMap(map, "0, 0");// bottom right
        */
        
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }

    public static (int x, int y) ParseSector(string sector) {
        string[] split = sector.Split(',');
        return (int.Parse(split[0])*sectorSize, int.Parse(split[1])*sectorSize);
    }

    public static (int x, int y) GetCordinates((int i, int j) ind, string sector)
    {
        int x = ind.i; int y = -ind.j;
        (int x, int y) offset = RenderWorld.ParseSector(sector);//parse sector
        return (x + offset.x, y + offset.y);
    }

    // array for testing
    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x <= map.GetUpperBound(0); x++) {
            for (int y = 0; y <= map.GetUpperBound(1); y++) {
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
   

    public void RenderMap(int[,] map, string sector)
    {
        (int x,int y)offset= RenderWorld.ParseSector(sector);
        Debug.Log("offset:"+ offset);
        //Loop through the width of the map
        for (int y = 0; y <= map.GetUpperBound(0); y++) {
            //Loop through the height of the map
            for (int x = 0; x <= map.GetUpperBound(0); x++) {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1) {
                    
                    tilemap.SetTile(new Vector3Int(y+offset.x, -x+offset.y, 0), tilenull);
                    resourceTilemap.SetTile(new Vector3Int(y + offset.x, -x + offset.y, 0), tile);
                }
                else {
                    //Debug.Log(map[x, y]+ " " + x+","+y);
                    tilemap.SetTile(new Vector3Int(y+offset.x, -x+offset.y, 0), tilenull);
                }
            }
        }
    }

    
}
