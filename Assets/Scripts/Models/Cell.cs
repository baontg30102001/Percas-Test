using UnityEngine;

public class Cell
{
    private Vector2Int _cellCoord;
    private CellType _cellType;
    public Vector2Int CellCoord => _cellCoord;
    public CellType CellType
    {
        get => _cellType;
        set => _cellType = value;
    }

    public Cell(Vector2Int cellCoord, CellType cellType = CellType.Empty)
    {
        _cellCoord = cellCoord;
        _cellType = cellType;
    }
}

public enum CellType
{
    Empty = 0,
    Wall = 1,
    NPC = 2,
    Goal = 3,
    Path = 4
}
