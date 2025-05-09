using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundGenerator : MonoBehaviour
{
    private bool[,] _maze;

    private readonly (int, int)[] _checkPointArray =
    {
        (0, -1),
        (0, 1),
        (-1, 0),
        (1, 0),
    };

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            CreateMaze(11);
            StringBuilder st = new StringBuilder();
            for (int j = 0; j < 11; j++)
            {
                for (int k = 0; k < 11; k++)
                {
                    st.Append(_maze[j, k] ? "　" : "壁");
                }

                st.Append("\n");
            }

            string maze = st.ToString();
            Debug.Log(maze);
        }
    }


    /// <summary>
    /// 迷路生成を行う
    /// </summary>
    /// <param name="size">必ず奇数を入力</param>
    void CreateMaze(int size)
    {
        _maze = new bool[size, size];
        Stack<(int, int)> road = new(); //探索中の道を保存するスタック
        //スタート位置をランダムな奇数インデックスの位置にする
        var startPos = (Random.Range(0, size / 2) * 2 + 1, Random.Range(0, size / 2) * 2 + 1);
        road.Push(startPos);
        while (road.Count > 0)
        {
            var checkPos = road.Pop();
            var uncheckCell = GetUncheckCell(checkPos, size);
            if (uncheckCell.Count == 0)
            {
                continue; //未調査セルが無ければ戻る
            }

            //ランダムな調査方向に道を作る
            var direction = uncheckCell[Random.Range(0, uncheckCell.Count)];
            (int, int) newRoadPos = default;
            for (int i = 1; i <= 2; i++)
            {
                newRoadPos = (checkPos.Item1 + direction.Item1 * i, checkPos.Item2 + direction.Item2 * i);
                _maze[newRoadPos.Item1, newRoadPos.Item2] = true;
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
        var unexplored = new List<(int, int)>();
        foreach (var point in _checkPointArray)
        {
            var checkPos = (roadPoint.Item1 + point.Item1 * 2, roadPoint.Item2 + point.Item2 * 2);
            if (checkPos.Item1 <= 0 || checkPos.Item1 >= size || checkPos.Item2 <= 0 ||
                checkPos.Item2 >= size) continue;

            if (!_maze[checkPos.Item1, checkPos.Item2])
            {
                unexplored.Add(point);
            }
        }

        return unexplored;
    }
}