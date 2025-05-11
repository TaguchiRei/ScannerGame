using System.Collections.Generic;
using UnityEngine;

public abstract class MazeBase : MonoBehaviour
{
    public bool[,] Maze { get; protected set; }

    protected readonly (int, int)[] CheckPointArray =
    {
        (0, -1),
        (0, 1),
        (-1, 0),
        (1, 0)
    };

    /// <summary>
    /// 未調査地点を取得する。セルと一つ跳びで調査するのでループを作成できる
    /// </summary>
    /// <param name="roadPoint"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    protected List<(int, int)> GetUncheckCellHoles((int, int) roadPoint, int size)
    {
        List<(int, int)> unexplored = new();
        foreach (var point in CheckPointArray)
        {
            var checkPos = (roadPoint.Item1 + point.Item1 * 2, roadPoint.Item2 + point.Item2 * 2);
            if (checkPos.Item1 <= 0
                || checkPos.Item1 >= size
                || checkPos.Item2 <= 0
                || checkPos.Item2 >= size) continue;
            
            if (!Maze[checkPos.Item1, checkPos.Item2])
            {
                unexplored.Add(point);
            }
        }
        return unexplored;
    }   

    /// <summary>
    /// 未調査地点を取得する。
    /// </summary>
    /// <param name="roadPoint"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    protected List<(int, int)> GetUncheckCell((int, int) roadPoint, int size)
    {
        List<(int, int)> unexplored = new();
        foreach (var point in CheckPointArray)
        {
            var checkPos = (roadPoint.Item1 + point.Item1, roadPoint.Item2 + point.Item2);
            var checkPos2 = (roadPoint.Item1 + point.Item1 * 2, roadPoint.Item2 + point.Item2 * 2);
            if (checkPos.Item1 <= 0
                || checkPos.Item1 >= size
                || checkPos.Item2 <= 0
                || checkPos.Item2 >= size
                || checkPos2.Item1 <= 0
                || checkPos2.Item1 >= size
                || checkPos2.Item2 <= 0
                || checkPos2.Item2 >= size) continue;
            if (!Maze[checkPos.Item1, checkPos.Item2]) unexplored.Add(point);
        }

        return unexplored;
    }
    
    
}