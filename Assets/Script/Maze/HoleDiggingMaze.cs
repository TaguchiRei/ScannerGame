using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class HoleDiggingMaze : MazeBase
{
    [SerializeField] private int _mazeSize;
    [SerializeField] private bool _makeLoop;

    [SerializeField] private GameObject _mazeObjectPrefab;
    [SerializeField] private float _objectSize;

    private void Awake()
    {
        if (_mazeSize % 2 == 0) _mazeSize++;
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        CreateMaze(_mazeSize);
        stopWatch.Stop();
        Debug.Log($"生成時間 ：　{stopWatch.ElapsedMilliseconds}ms");

        StringBuilder st = new StringBuilder();
        for (int i = 0; i < _mazeSize; i++)
        {
            for (int j = 0; j < _mazeSize; j++)
            {
                st.Append(Maze[i, j] ? "　" : "壁");
            }

            st.Append("\n");
        }

        string maze = st.ToString();
        Debug.Log(maze);
    }

    /// <summary>
    /// 迷路生成を行う
    /// </summary>
    /// <param name="size">必ず奇数</param>
    private void CreateMaze(int size)
    {
        Maze = new bool[size, size];
        Stack<(int, int)> road = new();
        //スタート位置をランダムな奇数インデックスの位置にする
        //var startPos = (Random.Range(0, size / 2) * 2 + 1, Random.Range(0, size / 2) * 2 + 1);
        var startPos = (1, 1);

        road.Push(startPos);
        Maze[startPos.Item1, startPos.Item2] = true;

        while (road.Count > 0)
        {
            var checkPos = road.Pop();
            var uncheckCell = GetUncheckCellHoles(checkPos, size);
            if (uncheckCell.Count == 0) continue; //未調査セルがなければ戻る

            var direction = uncheckCell[Random.Range(0, uncheckCell.Count)];
            (int, int) newRoadPos = default;
            for (var i = 1; i <= 2; i++)
            {
                newRoadPos = (checkPos.Item1 + direction.Item1 * i, checkPos.Item2 + direction.Item2 * i);
                Maze[newRoadPos.Item1, newRoadPos.Item2] = true;
            }

            road.Push(checkPos);
            road.Push(newRoadPos);
        }

        SetWall();
    }

    private void SetWall()
    {
        var posList = new List<Vector3>();
        for (int i = 0; i < _mazeSize; i++)
        {
            for (int j = 0; j < _mazeSize; j++)
            {
                if (Maze[i, j])
                {
                    posList.Add(new Vector3(i * _objectSize, 0, j * _objectSize));
                }
            }
        }

        Span<Vector3> posSpan = posList.ToArray();
        Span<Quaternion> rotSpan = new Quaternion[posSpan.Length];
        var result = InstantiateAsync(_mazeObjectPrefab, posList.Count, posSpan, rotSpan);
        _ = WallInitialize(result, posList);
    }

    private async UniTask WallInitialize(AsyncInstantiateOperation<GameObject> operation, List<Vector3> posList)
    {
        var result = await operation;
        for (int i = 0; i < posList.Count; i++)
        {
            var wallObj = result[i].GetComponent<WallObjecct>();
            wallObj.mazeBase = this;
            wallObj.pos = posList[i] / _objectSize;
            wallObj.Initialize();
        }
    }
}