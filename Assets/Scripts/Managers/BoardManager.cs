using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cellPrefab;
    [SerializeField]
    private Transform _boardParent;
    [SerializeField] 
    private Transform _safeAreaTopLeft;
    [SerializeField] 
    private Transform _safeAreaBottomRight;
    [SerializeField] 
    private float wallSpawnChance;
    
    private int _width = 10;
    private int _height = 10;
    
    private Board _board;
    private CellView[,] _cellViews;

    private CellType _currentEditType = CellType.NPC;

    public CellType CurrentEditType
    {
        get => _currentEditType;
        set => _currentEditType = value;
    }

    public int Width
    {
        get => _width;
        set => _width = value;
    }

    public int Height
    {
        get => _height;
        set => _height = value;
    }
    
    public void GenerateBoard()
    {
        if (_cellViews != null)
        {
            foreach (var child in _cellViews)
            {
                Destroy(child.gameObject);
            }
        }
        
        _board = new Board(_width, _height);
        _cellViews = new CellView[_width, _height];
        
        Vector2 _safeTopLeft = _safeAreaTopLeft.position;
        Vector2 _safeBottomRight = _safeAreaBottomRight.position;
        
        float safeWidth = _safeBottomRight.x - _safeTopLeft.x;
        float safeHeight = _safeTopLeft.y - _safeBottomRight.y;
        
        float cellSize = Mathf.Min(safeWidth / _width, safeHeight / _height);

        Vector2 safeCenter = (_safeTopLeft + _safeBottomRight) / 2.0f;
        Vector2 boardOffset = safeCenter - new Vector2(_width, _height) * cellSize / 2.0f;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                CellType cellType = Random.value < wallSpawnChance ? CellType.Wall : CellType.Empty;
                
                var cell = new Cell(new Vector2Int(x, y), cellType);
                _board.Cells[x, y] = cell;

                Vector2 cellPos = new Vector2(x, y) * cellSize + boardOffset;

                GameObject go = Instantiate(_cellPrefab);
                go.transform.position = cellPos;
                go.transform.localScale = Vector3.one * cellSize;
                
                var view = go.GetComponent<CellView>();
                view.SetCoord(cell.CellCoord);
                view.Init(this);
                view.UpdateCellView(cell);
                
                _cellViews[x, y] = view;
            }
        }
        
        ClearEntityAndPath();
    }

    public void SetCellType(int x, int y)
    {
        var cell = _board.Cells[x, y];
        if (cell.CellType == CellType.Wall) 
            return;

        if (_currentEditType == CellType.NPC || _currentEditType == CellType.Goal)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var otherCell = _board.Cells[i, j];
                    if(otherCell.CellType == _currentEditType)
                        otherCell.CellType = CellType.Empty;
                }
            }
        }
        
        cell.CellType = _currentEditType;
        UpdateAllViews();
    }

    public void FindPath()
    {
        ClearPath();
        
        Vector2Int start = Vector2Int.zero;
        Vector2Int end = Vector2Int.zero;
        bool hasStart = false;
        bool hasEnd = false;
        
        for (int x = 0; x < _board.Width; x++) 
        {
            for (int y = 0; y < _board.Height; y++) 
            {
                var cell = _board.Cells[x, y];
                if (cell.CellType == CellType.NPC) 
                {
                    start = new Vector2Int(x, y);
                    hasStart = true;
                }
                if (cell.CellType == CellType.Goal) 
                {
                    end = new Vector2Int(x, y);
                    hasEnd = true;
                }
            }
        }
        
        if (!hasStart || !hasEnd) 
        {
            Debug.LogWarning("Missing NPC or Goal !!!");
            return;
        }
        
        var path = AStar.FindPath(_board, start, end);
        
        if (path != null) 
        {
            foreach (var pos in path) 
            {
                var cell = _board.Cells[pos.x, pos.y];
                if (cell.CellType == CellType.Empty)
                    cell.CellType = CellType.Path;
            }
            string result = string.Join(" -> ", path.Select(p => $"({p.x}, {p.y})"));
            Debug.Log($"Found path: {result}");
        } 
        else 
        {
            Debug.LogWarning("No path found !!!");
        }

        UpdateAllViews();
    }
    
    public void ClearEntityAndPath()
    {
        foreach (var cell in _board.Cells)
        {
            if (cell.CellType == CellType.NPC || cell.CellType == CellType.Goal || cell.CellType == CellType.Path)
            {
                cell.CellType = CellType.Empty;
            }
        }
        UpdateAllViews();
    }
    
    public void ClearPath()
    {
        if (_board.Cells == null) return;
        foreach (var cell in _board.Cells)
        {
            if (cell.CellType == CellType.Path)
            {
                cell.CellType = CellType.Empty;
            }
        }
        UpdateAllViews();
    }

    private void UpdateAllViews()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _cellViews[i, j].UpdateCellView(_board.Cells[i, j]);
            }
        }
    }
}