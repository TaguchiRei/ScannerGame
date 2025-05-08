using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundGenerator : MonoBehaviour
{
    private bool[,] _maze;

    private void Start()
    { 
        CreateMaze(11);
        StringBuilder st = new StringBuilder();
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                st.Append(_maze[i, j] ? " " : "壁");
            }
            st.Append("\n");
        }
        string maze = st.ToString();
        Debug.Log(maze);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="size">必ず奇数を入力</param>
    void CreateMaze(int size)
    {
        Debug.Log("Generating");
        _maze = new bool[size, size];
        Stack<(int x, int y)> road = new();

        //初期地点をランダムな偶数の座標に決定する
        var x = Random.Range(1, size / 2 + 1);
        var y = Random.Range(1, size / 2 + 1);

        road.Push((x * 2, y * 2));
        while (road.Count != 0)
        {
            List<(int, int)> checkedPoints = new();
            var roadPos = road.Pop();

            //２個先のセルを探索
            (int, int)[] checkPos =
            {
                (roadPos.x, roadPos.y - 2),
                (roadPos.x, roadPos.y + 2),
                (roadPos.x - 2, roadPos.y),
                (roadPos.x + 2, roadPos.y)
            };
            for (int i = 0; i < 4; i++)
            {
                if (AllCheck(checkPos[i].Item1, checkPos[i].Item2, size, i)) ;
            }
            

            if (checkedPoints.Count == 0) continue; //周囲がすべて探索済みならそのまま戻る

            //未探索セルがあった場合最初にpopした値を戻してからそのセルを保存
            var selectPos = checkedPoints[Random.Range(0, checkedPoints.Count)];
            road.Push(roadPos);
            road.Push(selectPos);
            _maze[selectPos.Item1, selectPos.Item1] = true;
        }
        Debug.Log($"Generated {_maze.Length}");
    }

    /// <summary>
    /// 迷路の範囲内且つ未探索の場合trueを返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="size"></param>
    /// <param name="numberA"></param>
    /// <returns></returns>
    private bool AllCheck(int x, int y, int size, int numberA)
    {
        Debug.Log($"{x},{y}");
        if (y <= 0 || y >= size || x <= 0 || x >= size || _maze[x, y]) return false;
        _maze[x, y] = true;
        switch (numberA)
        {
            case 0:
                _maze[x, y + 1] = true;
                break;
            case 1:
                _maze[x, y - 1] = true;
                break;
            case 2:
                _maze[x + 1, y] = true;
                break;
            default:
                _maze[x - 1, y] = true;
                break;
        }

        return true;
    }
}