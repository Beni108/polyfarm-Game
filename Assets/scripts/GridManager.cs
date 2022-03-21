using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _rows, _cols;
    private float tileSize = 1;
    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        GameObject refernceTile =(GameObject)Instantiate(Resources.Load("Prefab/Tile"));
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                GameObject tile =(GameObject) Instantiate(refernceTile,transform);
                float posX = col * tileSize;
                float posY = row * -tileSize;
                bool offset = (row % 2 == 0 && col % 2 != 0) || (col %2 ==0 && row %2 !=0);
               // tile.Init(offset);
                tile.transform.position = new Vector2(posX, posY);
            }
            
        }
        Destroy(refernceTile);
    }
}
