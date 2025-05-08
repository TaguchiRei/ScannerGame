using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundGenerator : MonoBehaviour
{
    private bool[,] _maze;
    
    private void Start()
    {
        CreateMaze(5);
    }

    
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="size">必ず奇数を入力</param>
    void CreateMaze(int size)
    {
        _maze = new bool[size, size];
        Stack<(int x,int y)> road = new();
        List<(int x,int y)> checkedPoints = new();
        
        //初期地点をランダムな偶数の座標に決定する
        var x = Random.Range(1, size / 2 + 1);
        var y = Random.Range(1, size / 2 + 1);
        var startPos = new Vector2(x * 2, y * 2);

        road.Push((x * 2, y * 2));
        while (road.Count == 0)
        {
            var roadPos = road.Pop();
            
            //２個先のセルを探索
            (int x,int y) up = (roadPos.x , roadPos.y - 2);
            (int x,int y) down = (roadPos.x , roadPos.y + 2);
            (int x,int y) left = (roadPos.x - 2, roadPos.y);
            (int x,int y) right = (roadPos.x + 2, roadPos.y);

            //セルが範囲外でないか、探索済みかどうかを判定
            if (up.y > 0 && up.y < size)
            {
                
            }
        }
    }
}
