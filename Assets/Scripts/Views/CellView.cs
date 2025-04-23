using System;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer _renderer;

    private Vector2Int _cellCoord;
    private BoardManager _boardManager;
    
    public SpriteRenderer Renderer => _renderer;
    
    public void Init(BoardManager boardManager)
    {
        _boardManager = boardManager;
    }

    public void SetCoord(Vector2Int coord)
    {
        _cellCoord = coord;
    }
    
    public void UpdateCellView(Cell cell)
    {
        Renderer.color = GetColorByCellType(cell.CellType);
    }

    public Color GetColorByCellType(CellType cellType)
    {
        return cellType switch
        {
            CellType.Empty => Color.white,
            CellType.Wall => Color.gray,
            CellType.Path => Color.yellow,
            CellType.NPC => Color.green,
            CellType.Goal => Color.red,
            _ => Color.white
        };
    }

    private void OnMouseDown()
    {
        _boardManager?.SetCellType(_cellCoord.x, _cellCoord.y);
    }
}
