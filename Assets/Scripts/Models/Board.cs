using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private int _width;
    private int _height;
    private Cell[,] _cells;
    public Cell[,] Cells => _cells;
    
    public int Width => _width;
    public int Height => _height;
    
    public Board(int width, int height)
    {
        _width = width;
        _height = height;
        _cells = new Cell[_width, _height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _cells[x, y] = new Cell(new Vector2Int(x, y));
            }
        }
    }
}
