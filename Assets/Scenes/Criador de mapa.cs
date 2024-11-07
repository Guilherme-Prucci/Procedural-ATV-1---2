using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Criadordemapa : MonoBehaviour
{
    public int atualizaMap= 5;

    public int width = 30;
    public int height = 30;

    public string seed;
    public bool useRandomSeed;

    [Range(0f,100f)]
    public int randomFillPercent;

    int[,] map;

     void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GenerateMap();
            SmoothMap();
        }
    }

    void GenerateMap()
    {
        map = new int[width,height];
        RandomFillMap();
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        System.Random rand = new System.Random(seed.GetHashCode());

        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = (rand.Next(0, 100) < randomFillPercent) ? 1 : 0;
                Debug.Log(map[i, j]);
            }
        }
    }

    void SmoothMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int tilesParedesVizinhos = GetSurroundingWallCount(i, j);

                if (tilesParedesVizinhos> 4)
                {
                    map[i, j] = 1;
                }else if (tilesParedesVizinhos < 4)
                {
                    map[i, j] = 0;
                }

            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int count = 0;
        for(int vizinhoX = gridX -1;vizinhoX<=gridX +1;vizinhoX++)
        {
            for (int vizinhoY = gridY - 1; vizinhoY <= gridY + 1; vizinhoY++)
            {
                if (vizinhoX>=0 && vizinhoX <width &&vizinhoY>=0&&vizinhoY<height) 
                {
                    if (vizinhoX != gridX || vizinhoY != gridY)
                    {
                        count += map[vizinhoX, vizinhoY];
                    }
                }
                else
                {
                    count = 0;
                }

            }
        }
        return count;
    }

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j] == 1)
                    {
                        Gizmos.color = Color.white;
                    }
                    else Gizmos.color = Color.black;  
                    Vector3 pos = new Vector3(-width/2 + i+0.5f, 0, -height/2 + j+0.5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
                    
            }
        }
    }
}
