using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DepthFirstSearch : MazeBase
{
    [SerializeField] private int _size;
    [SerializeField] private MazeBase _mazeBase;
    [SerializeField] private MazePosition _startPos;
    [SerializeField] private MazePosition _endPos;

    private bool[,] _visited;

    private void Start()
    {
        var route = SearchStart(_size);
        var st = new StringBuilder();
        st.Append("start");
        foreach (var pos in route)
        {
            st.Append($" → {pos}");
        }

        st.Append("[goal]");
        Debug.Log(st.ToString());
    }

    private List<(int, int)> SearchStart(int size)
    {
        Maze = _mazeBase.Maze;
        _visited = new bool[Maze.GetLength(0), Maze.GetLength(1)];

        if (!Maze[_endPos.x, _endPos.y])
        {
            Debug.Log("Goal is Wall");
            return new List<(int, int)>();
        }

        Stack<(int, int)> route = new();
        route.Push((_startPos.x, _startPos.y));
        _visited[_startPos.x, _startPos.y] = true;
        bool goal = false;
        int i = 0;
        while (!goal)
        {
            var pos = route.Pop();
            if (pos == (_endPos.x, _endPos.y)) goal = true;
            var nextDirectionOption = GetUncheckCellNext(pos, size);
            if (nextDirectionOption.Count == 0) continue; //道が行きどまりまたはすべて探索済みなら戻る

            var nextRoute = (pos.Item1 + nextDirectionOption[0].Item1, pos.Item2 + nextDirectionOption[0].Item2);
            route.Push(pos);
            route.Push(nextRoute);
            _visited[nextRoute.Item1, nextRoute.Item2] = true;
            i++;
            if (i > 200)
            {
                Debug.Log("無限ループの可能性があります");
                break;
            }
        }

        return route.ToList();
    }

    [Serializable]
    private struct MazePosition
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// 隣接する未調査地点を取得する。
    /// </summary>
    /// <param name="roadPoint"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private List<(int, int)> GetUncheckCellNext((int, int) roadPoint, int size)
    {
        List<(int, int)> unexplored = new();
        foreach (var point in CheckPointArray)
        {
            var checkPos = (roadPoint.Item1 + point.Item1, roadPoint.Item2 + point.Item2);
            if (checkPos.Item1 <= 0
                || checkPos.Item1 >= size
                || checkPos.Item2 <= 0
                || checkPos.Item2 >= size) continue;

            if (Maze[checkPos.Item1, checkPos.Item2] && !_visited[checkPos.Item1, checkPos.Item2])
            {
                unexplored.Add(point);
            }
        }

        return unexplored;
    }
}