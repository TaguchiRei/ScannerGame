using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundGenerator : MonoBehaviour
{
    private bool[,] _maze;
    
    private void Start()
    {
        CreateMaze(11);
        
    }

    
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="size">必ず奇数を入力</param>
    void CreateMaze(int size)
    {
        _maze = new bool[size, size];
        Stack<(int x,int y)> road = new();
        
        //初期地点をランダムな偶数の座標に決定する
        var x = Random.Range(1, size / 2 + 1);
        var y = Random.Range(1, size / 2 + 1);

        road.Push((x * 2, y * 2));
        while (road.Count == 0)
        {
            List<(int,int)> checkedPoints = new();
            var roadPos = road.Pop();
            
            //２個先のセルを探索
            (int, int)[] checkPos =
            {
                (roadPos.x , roadPos.y - 2),
                (roadPos.x , roadPos.y + 2),
                (roadPos.x - 2, roadPos.y),
                (roadPos.x + 2, roadPos.y)
            };
            foreach (var pos in checkPos)
            {
                if(AllCheck(pos.Item1, pos.Item2, size)) checkedPoints.Add(pos);
            }
            
            if(checkedPoints.Count == 0) continue;//周囲がすべて探索済みならそのまま戻る
            //未探索セルがあった場合保存する
            road.Push(roadPos);
            var selectPos = checkedPoints[Random.Range(0, checkedPoints.Count)];
            road.Push(selectPos);
            _maze[selectPos.Item1,selectPos.Item1] = true;
            
        }
    }

    /// <summary>
    /// 迷路の範囲内且つ未探索の場合trueを返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private bool AllCheck(int x, int y, int size)
    {
        if (y <= 0 || y >= size || _maze[x, y] || x <= 0 || x >= size) return false;
        _maze[x, y] = true;
        return true;
    }
}
