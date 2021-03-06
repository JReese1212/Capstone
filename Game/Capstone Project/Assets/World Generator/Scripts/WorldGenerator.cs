using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WorldGenerator : MonoBehaviour
{
    // Tile Prefabs
    [SerializeField] private TileDataSO TileList;

    // Grid
    private struct Tile
    {
        public string id;
        public int rot;
        public int dist;
    }
    private Tile[,] GenArray;
    private GameObject[,] SpawnArray;
    [SerializeField] private int WorldSize = 50;

    // tile Scale
    [SerializeField] private float TS = 1;
    [SerializeField] private float OS = 64;

    // road settings
    [SerializeField] private int RandomRoadCount = 150;

    //public GameObject player;
    //public GameObject cop;
    //private Vector3 loc = new Vector3(60, 1, 0);
    //private GameObject insCop;
    //private Transform playerTrans;

    public void BuildWorld()
    {
        // Generate Grid World
        GenerateWorld();
        // Place Grid into Scene
        SpawnWorld();

        
    }

    public void DestroyWorld()
    {
        for (int x = 0; x < WorldSize; x++)
        {
            for (int z = 0; z < WorldSize; z++)
            {
                Destroy(SpawnArray[x, z]);
            }
        }
    }

    private void GenerateWorld()
    {
        GenArray = new Tile[WorldSize, WorldSize];

        ////////////////////////////////////////////////////////
        // Generate Rivers and parallel roads
        ////////////////////////////////////////////////////////
        {
            int RiverCount = Random.Range(1, 4);
            bool Horizonal = Random.Range(0, 2) == 0;
            for (int i = 0; i < RiverCount; i++)
            {
                int xz = Random.Range(4, WorldSize - 3);
                if (Horizonal)
                {
                    for (int z = 0; z < WorldSize; z++)
                    {
                        GenArray[xz, z].id = "W";
                        GenArray[xz, z].rot = 0;

                        GenArray[xz - 1, z].id = "RI";
                        GenArray[xz - 1, z].rot = 0;

                        GenArray[xz + 1, z].id = "RI";
                        GenArray[xz + 1, z].rot = 0;
                    }
                }
                else
                {
                    for (int x = 0; x < WorldSize; x++)
                    {
                        GenArray[x, xz].id = "W";
                        GenArray[x, xz].rot = 1;

                        GenArray[x, xz - 1].id = "RI";
                        GenArray[x, xz - 1].rot = 1;

                        GenArray[x, xz + 1].id = "RI";
                        GenArray[x, xz + 1].rot = 1;
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////
        // Generate Border
        ////////////////////////////////////////////////////////
        {
            // bottom left
            GenArray[0, 0].id = "RL";
            GenArray[0, 0].rot = 2;
            // bottom right
            GenArray[WorldSize - 1, 0].id = "RL";
            GenArray[WorldSize - 1, 0].rot = 1;
            // top left
            GenArray[0, WorldSize - 1].id = "RL";
            GenArray[0, WorldSize - 1].rot = 3;
            // top right
            GenArray[WorldSize - 1, WorldSize - 1].id = "RL";
            GenArray[WorldSize - 1, WorldSize - 1].rot = 0;
            // bottom left -> bottom right
            for (int x = 1; x < WorldSize - 1; x++)
            {
                if (GenArray[x, 0].id == "W")
                {
                    GenArray[x, 0].id = "RW";
                    GenArray[x, 0].rot = 1;
                }
                else if (GenArray[x, 0].id == "RI")
                {
                    GenArray[x, 0].id = "RT";
                    GenArray[x, 0].rot = 1;
                }
                else
                {
                    GenArray[x, 0].id = "RI";
                    GenArray[x, 0].rot = 1;
                }

            }
            // bottom right -> top right
            for (int z = 1; z < WorldSize - 1; z++)
            {
                if (GenArray[WorldSize - 1, z].id == "W")
                {
                    GenArray[WorldSize - 1, z].id = "RW";
                    GenArray[WorldSize - 1, z].rot = 0;
                }
                else if (GenArray[WorldSize - 1, z].id == "RI")
                {
                    GenArray[WorldSize - 1, z].id = "RT";
                    GenArray[WorldSize - 1, z].rot = 0;
                }
                else
                {
                    GenArray[WorldSize - 1, z].id = "RI";
                    GenArray[WorldSize - 1, z].rot = 0;
                }
            }
            // top left -> top right
            for (int x = 1; x < WorldSize - 1; x++)
            {
                if (GenArray[x, WorldSize - 1].id == "W")
                {
                    GenArray[x, WorldSize - 1].id = "RW";
                    GenArray[x, WorldSize - 1].rot = 1;
                }
                else if (GenArray[x, WorldSize - 1].id == "RI")
                {
                    GenArray[x, WorldSize - 1].id = "RT";
                    GenArray[x, WorldSize - 1].rot = 3;
                }
                else
                {
                    GenArray[x, WorldSize - 1].id = "RI";
                    GenArray[x, WorldSize - 1].rot = 1;
                }
            }
            // bottom left -> top left
            for (int z = 1; z < WorldSize - 1; z++)
            {
                if (GenArray[0, z].id == "W")
                {
                    GenArray[0, z].id = "RW";
                    GenArray[0, z].rot = 0;
                }
                else if (GenArray[0, z].id == "RI")
                {
                    GenArray[0, z].id = "RT";
                    GenArray[0, z].rot = 2;
                }
                else
                {
                    GenArray[0, z].id = "RI";
                    GenArray[0, z].rot = 0;
                }
            }
        }

        ////////////////////////////////////////////////////////
        // Generate straight horizontal roads 
        ////////////////////////////////////////////////////////
        {
            int SCount = Random.Range(WorldSize / 8 + 2, WorldSize / 5 + 2);
            for (int i = 0; i < SCount; i++)
            {
                // dont build over rivers
                int z = Random.Range(1, WorldSize);
                while (GenArray[0, z].id != "RI")
                {
                    z = Random.Range(1, WorldSize);
                }
                // T intersections
                GenArray[0, z].id = "RT";
                GenArray[0, z].rot = 2;
                GenArray[WorldSize - 1, z].id = "RT";
                GenArray[WorldSize - 1, z].rot = 0;
                // straight away
                for (int x = 1; x < WorldSize - 1; x++)
                {
                    if (GenArray[x, z].id == "W")
                    {
                        GenArray[x, z].id = "RW";
                        GenArray[x, z].rot = 1;
                    }
                    else if (GenArray[x, z].id == "RI")
                    {
                        GenArray[x, z].id = "RX";
                        GenArray[x, z].rot = 0;
                    }
                    else
                    {
                        GenArray[x, z].id = "RI";
                        GenArray[x, z].rot = 1;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////
        // Generate straight vertical roads 
        ////////////////////////////////////////////////////////
        {
            int SCount = Random.Range(WorldSize / 8 + 2, WorldSize / 5 + 2);
            for (int i = 0; i < SCount; i++)
            {
                // dont build over rivers
                int x = Random.Range(1, WorldSize - 1);
                while (GenArray[x, 0].id != "RI")
                {
                    x = Random.Range(1, WorldSize);
                }
                // T intersections
                GenArray[x, 0].id = "RT";
                GenArray[x, 0].rot = 1;
                GenArray[x, WorldSize - 1].id = "RT";
                GenArray[x, WorldSize - 1].rot = 3;
                // straight away
                for (int z = 1; z < WorldSize - 1; z++)
                {
                    if (GenArray[x, z].id == "W")
                    {
                        GenArray[x, z].id = "RW";
                        GenArray[x, z].rot = 0;
                    } 
                    else if (GenArray[x, z].id == "RI")
                    {
                        GenArray[x, z].id = "RX";
                        GenArray[x, z].rot = 0;
                    }
                    else
                    {
                        GenArray[x, z].id = "RI";
                        GenArray[x, z].rot = 0;
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////
        // Generate random roads attempt 2
        ////////////////////////////////////////////////////////
        
        {
            int count = Random.Range(WorldSize / 6 + 5, WorldSize / 4 + 5);

            for (int i = 0; i < RandomRoadCount; i++)
            {
                // find existing road
                int x = Random.Range(1, WorldSize - 1), z = Random.Range(1, WorldSize - 1);
                while (GenArray[x, z].id != "RI")
                {
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            if (z + 1 < WorldSize)
                                z++;
                            break;
                        case 1:
                            if (x + 1 < WorldSize)
                                x++;
                            break;
                        case 2:
                            if (z - 1 > 0)
                                z--;
                            break;
                        case 3:
                            if (x - 1 > 0)
                                x--;
                            break;
                    }
                }
                // Start, make T intersection
                int dir = 0;
                int pdir = 0;
                if (GenArray[x,z].rot == 1)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        if (z + 1 < WorldSize)
                        {
                            if (GenArray[x, z + 1].id != "W")
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 1;
                                z++;
                                dir = 0;
                            }
                            else if (GenArray[x, z - 1].id == "W")
                            {
                                continue;
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 3;
                                z--;
                                dir = 2;
                            }
                        }
                        else
                        {
                            GenArray[x, z].id = "RT";
                            GenArray[x, z].rot = 3;
                            z--;
                            dir = 2;
                        }
                    }
                    else
                    {
                        if (z - 1 > 0)
                        {
                            if (GenArray[x, z - 1].id != "W")
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 3;
                                z--;
                                dir = 2;
                            }
                            else if (GenArray[x, z + 1].id == "W")
                            {
                                continue;
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 1;
                                z++;
                                dir = 0;
                            }
                        }
                        else
                        {
                            GenArray[x, z].id = "RT";
                            GenArray[x, z].rot = 1;
                            z++;
                            dir = 0;
                        }

                    } 
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        if (x + 1 < WorldSize)
                        {
                            if (GenArray[x + 1, z].id != "W")
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 2;
                                x++;
                                dir = 1;
                            }
                            else if (GenArray[x - 1, z].id == "W")
                            {
                                continue;
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 0;
                                x--;
                                dir = 3;
                            }
                        }
                        else
                        {
                            GenArray[x, z].id = "RT";
                            GenArray[x, z].rot = 0;
                            x--;
                            dir = 3;
                        }
                    }
                    else
                    {
                        if (x - 1 > 0)
                        {
                            if (GenArray[x - 1, z].id != "W")
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 0;
                                x--;
                                dir = 3;
                            }
                            else if (GenArray[x + 1, z].id == "W")
                            {
                                continue;
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 2;
                                x++;
                                dir = 1;
                            }
                        }
                        else
                        {
                            GenArray[x, z].id = "RT";
                            GenArray[x, z].rot = 2;
                            x++;
                            dir = 1;
                        }
                    }
                }

                // randomly travel till hitting road
                while (GenArray[x, z].id == null)
                {
                    if (Random.Range(0, 10) < 2)
                    {
                        pdir = dir;
                        if (Random.Range(0, 2) == 0)
                        {
                            if (dir == 3)
                                dir = 0;
                            else
                                dir++;
                        }
                        else
                        {
                            if (dir == 0)
                                dir = 3;
                            else
                                dir--;
                        }
                        // 8 possible turns 
                        switch (pdir, dir)
                        {
                            case (0, 1):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 3;
                                x++;
                                break;
                            case (0, 3):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 0;
                                x--;
                                break;
                            case (1, 0):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 1;
                                z++;
                                break;
                            case (1, 2):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 0;
                                z--;
                                break;
                            case (2, 1):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 2;
                                x++;
                                break;
                            case (2, 3):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 1;
                                x--;
                                break;
                            case (3, 0):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 2;
                                z++;
                                break;
                            case (3, 2):
                                GenArray[x, z].id = "RL";
                                GenArray[x, z].rot = 3;
                                z--;
                                break;
                        }
                    }
                    else
                    {
                        switch (dir)
                        {
                            case 0:
                                GenArray[x, z].id = "RI";
                                GenArray[x, z].rot = 0;
                                z++;
                                break;
                            case 1:
                                GenArray[x, z].id = "RI";
                                GenArray[x, z].rot = 1;
                                x++;
                                break;
                            case 2:
                                GenArray[x, z].id = "RI";
                                GenArray[x, z].rot = 0;
                                z--;
                                break;
                            case 3:
                                GenArray[x, z].id = "RI";
                                GenArray[x, z].rot = 1;
                                x--;
                                break;
                        }
                    }
                }

                // End, make T/X intersection
                if (GenArray[x, z].id == "RT")
                {
                    GenArray[x, z].id = "RX";
                }
                else if (GenArray[x, z].id != "W")
                {
                    switch (dir)
                    {
                        case 0:
                            if (GenArray[x, z].id == "RL")
                            {
                                switch (GenArray[x, z].rot)
                                {
                                    case 1:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 0;
                                        break;
                                    case 2:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 2;
                                        break;
                                }
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 3;
                            }
                            break;
                        case 1:
                            if (GenArray[x, z].id == "RL")
                            {
                                switch (GenArray[x, z].rot)
                                {
                                    case 2:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 1;
                                        break;
                                    case 3:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 3;
                                        break;
                                }
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 0;
                            }

                            break;
                        case 2:
                            if (GenArray[x, z].id == "RL")
                            {
                                switch (GenArray[x, z].rot)
                                {
                                    case 0:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 0;
                                        break;
                                    case 3:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 2;
                                        break;
                                }
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 1;
                            }

                            break;
                        case 3:
                            if (GenArray[x, z].id == "RL")
                            {
                                switch (GenArray[x, z].rot)
                                {
                                    case 0:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 3;
                                        break;
                                    case 1:
                                        GenArray[x, z].id = "RT";
                                        GenArray[x, z].rot = 1;
                                        break;
                                }
                            }
                            else
                            {
                                GenArray[x, z].id = "RT";
                                GenArray[x, z].rot = 2;
                            }
                            break;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////
        // Generate Districts 
        ////////////////////////////////////////////////////////

        // make square
        // outside of square is residential
        // inside is Commercial
        // make new square
        // inside is Downtown
        {
            //bottom left point
            int sqrBotX = Random.Range(WorldSize / 5, WorldSize / 4 + 1);
            int sqrBotZ = Random.Range(WorldSize / 5, WorldSize / 4 + 1);
            //top right point
            int sqrTopX = Random.Range(WorldSize - WorldSize / 5, WorldSize - WorldSize / 4 + 1);
            int sqrTopZ = Random.Range(WorldSize - WorldSize / 5, WorldSize - WorldSize / 4 + 1);

            // residential / Commercial
            for (int x = 0; x < WorldSize; x++)
            {
                for (int z = 0; z < WorldSize; z++)
                {
                    if (x > sqrBotX && x < sqrTopX && z > sqrBotZ && z < sqrTopZ)
                    {
                        // inside square
                        GenArray[x, z].dist = 1;
                    }
                    else
                    {
                        // outside square
                        GenArray[x, z].dist = 0;
                    }
                }
            }
            //bottom left point
            sqrBotX = Random.Range(sqrBotX + 1, sqrTopX / 2);
            sqrBotZ = Random.Range(sqrBotZ + 1, sqrTopZ / 2);
            //top right point
            sqrTopX = Random.Range(sqrBotX + WorldSize / 10, sqrTopX);
            sqrTopZ = Random.Range(sqrBotZ + WorldSize / 10, sqrTopZ);

            // Downtown
            for (int x = sqrBotX; x < sqrTopX; x++)
            {
                for (int z = sqrBotZ; z < sqrTopZ; z++)
                {
                    // inside square
                    GenArray[x, z].dist = 2;
                }
            }
        }
    }

    private void SpawnWorld()
    {
        SpawnArray = new GameObject[WorldSize, WorldSize];

        for (int x = 0; x < WorldSize; x++)
        {
            for (int z = 0; z < WorldSize; z++)
            {
                switch (GenArray[x, z].id)
                {
                    case "W":
                        switch(GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.W0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.W1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.W2, ref x, ref z);
                                break;
                        }
                        break;
                    case "RE":
                        switch (GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.RE0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.RE1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.RE2, ref x, ref z);
                                break;
                        }
                        break;
                    case "RI":
                        switch (GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.RI0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.RI1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.RI2, ref x, ref z);
                                break;
                        }
                        break;
                    case "RW":
                        switch (GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.RW0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.RW1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.RW2, ref x, ref z);
                                break;
                        }
                        break;
                    case "RL":
                        switch (GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.RL0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.RL1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.RL2, ref x, ref z);
                                break;
                        }
                        break;
                    case "RT":
                        switch (GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.RT0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.RT1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.RT2, ref x, ref z);
                                break;
                        }
                        break;
                    case "RX":
                        switch (GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.RX0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.RX1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.RX2, ref x, ref z);
                                break;
                        }
                        break;
                    // handle null aka fill spots
                    default:
                        switch (GenArray[x, z].dist)
                        {
                            case 0:
                                SpawnTile(ref TileList.FILL0, ref x, ref z);
                                break;
                            case 1:
                                SpawnTile(ref TileList.FILL1, ref x, ref z);
                                break;
                            case 2:
                                SpawnTile(ref TileList.FILL2, ref x, ref z);
                                break;
                        }
                        break;
                }
            }
        }

        SpawnArray[0, 0].GetComponent<NavMeshSurface>().BuildNavMesh();


    }

    private void SpawnTile(ref List<GameObject> tile,  ref int x, ref int z)
    {
        SpawnArray[x, z] = Instantiate(tile[Random.Range(0, tile.Count)], new Vector3(x * OS + OS/2, 0, z * OS + OS/2), Quaternion.Euler(0, GenArray[x, z].rot * 90, 0));
        SpawnArray[x, z].transform.localScale = new Vector3(TS, TS, TS);
    }

}

