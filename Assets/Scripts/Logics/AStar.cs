using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AStar
{
    private static readonly Vector2Int[] Directions =
    {
        new Vector2Int(1, 0), new Vector2Int(-1, 0),
        new Vector2Int(0, 1), new Vector2Int(0, -1),
        // new Vector2Int(1, 1), new Vector2Int(1, -1),
        // new Vector2Int(-1, 1), new Vector2Int(-1, -1),
    };

    public static List<Vector2Int> FindPath(Board board, Vector2Int start, Vector2Int end)
    {
        var openSet = new PriorityQueue<Vector2Int>();
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, float>();
        var fScore = new Dictionary<Vector2Int, float>();
        
        openSet.Enqueue(start, 0);
        gScore[start] = 0;
        fScore[start] = Heuristic(start, end);
        
        while (openSet.Count > 0) 
        {
            var current = openSet.Dequeue();

            if (current == end)
                return ReconstructPath(cameFrom, current);

            foreach (var dir in Directions) 
            {
                var neighbor = current + dir;
                if (!IsInBounds(neighbor, board)) continue;

                var cell = board.Cells[neighbor.x, neighbor.y];
                if (cell.CellType == CellType.Wall) continue;

                float tentativeG = gScore[current] + Vector2Int.Distance(current, neighbor);
                if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor]) 
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, end);
                    if (!openSet.Contains(neighbor)) openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }
        return null;
    }
    
    private static float Heuristic(Vector2Int a, Vector2Int b) {
        
        return Vector2Int.Distance(a, b);
    }

    private static bool IsInBounds(Vector2Int pos, Board board) 
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < board.Width && pos.y < board.Height;
    }

    private static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current) 
    {
        List<Vector2Int> path = new();
        while (cameFrom.ContainsKey(current)) 
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }
}