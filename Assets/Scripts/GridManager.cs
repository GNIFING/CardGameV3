using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    [SerializeField] private GameObject _unit;
    
    //public Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }


    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";


                if(x == 2 && y == 0)
                {
                    var unit = Instantiate(_unit, new Vector3(x, y), Quaternion.identity);
                    unit.transform.parent = spawnedTile.transform;
                }

                //_tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    //public Tile GetTileAtPosition(Vector2 pos)
    //{
    //    if(_tiles.TryGetValue(pos, out var tile))
    //    {
    //        return tile;
    //    }
    //    return null;
    //}
}
