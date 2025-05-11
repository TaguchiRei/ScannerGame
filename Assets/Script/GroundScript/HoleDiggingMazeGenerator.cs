using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class HoleDiggingMazeGenerator : MonoBehaviour
{
    public bool[,] Maze
    {
        get;
        private set;
    }

    [SerializeField] private int _mazeSize;

    private readonly (int, int)[] _checkPointArray =
    {
        (0, -1),
        (0, 1),
        (-1, 0),
        (1, 0),
    };

    private void Start()
    {
        if (_mazeSize % 2 == 0) _mazeSize++;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        CreateMaze(_mazeSize);
        stopwatch.Stop();
        Debug.Log($"処理時間　:　{stopwatch.ElapsedMilliseconds}ms");

        StringBuilder st = new StringBuilder();
        for (int j = 0; j < _mazeSize; j++)
        {
            for (int k = 0; k < _mazeSize; k++)
            {
                st.Append(Maze[j, k] ? "　" : "壁");
            }

            st.Append("\n");
        }

        string maze = st.ToString();
        Debug.Log(maze);
    }


    /// <summary>
    /// 迷路生成を行う
    /// </summary>
    /// <param name="size">必ず奇数を入力</param>
    private void CreateMaze(int size)
    {
        Maze = new bool[size, size];
        Stack<(int, int)> road = new(); //探索中の道を保存するスタック
        //スタート位置をランダムな奇数インデックスの位置にする
        var startPos = (Random.Range(0, size / 2) * 2 + 1, Random.Range(0, size / 2) * 2 + 1);
        road.Push(startPos);
        //_maze[startPos.Item1, startPos.Item2] = true;
        Debug.Log(startPos);
        while (road.Count > 0)
        {
            var checkPos = road.Pop();
            var uncheckCell = GetUncheckCell(checkPos, size);
            if (uncheckCell.Count == 0) continue; //未調査セルが無ければ戻る

            //ランダムな調査方向に道を作る
            var direction = uncheckCell[Random.Range(0, uncheckCell.Count)];
            (int, int) newRoadPos = default;
            for (var i = 1; i <= 2; i++)
            {
                newRoadPos = (checkPos.Item1 + direction.Item1 * i, checkPos.Item2 + direction.Item2 * i);
                Maze[newRoadPos.Item1, newRoadPos.Item2] = true;
            }

            //調査したセルと新たに道にしたセルをスタックに加える
            road.Push(checkPos);
            road.Push(newRoadPos);
        }
    }

    /// <summary>
    /// 未調査地点を取得する。位置マス跳びで調査しているので壁に穴をあけてループを発生させることができる
    /// </summary>
    /// <param name="roadPoint"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    List<(int, int)> GetUncheckCell((int, int) roadPoint, int size)
    {
        List<(int,int)> unexplored = new();
        foreach (var point in _checkPointArray)
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
}