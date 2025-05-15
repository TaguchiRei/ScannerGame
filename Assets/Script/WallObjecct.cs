using System;
using UnityEngine;

public class WallObjecct : MonoBehaviour
{
    public MazeBase mazeBase;
    public Vector3 pos;
    [SerializeField] private GameObject[] walls;

    private readonly (int, int)[] _checkPointArray =
    {
        (0, -1),//一つ上
        (0, 1),//一つ下
        (-1, 0),//一つ左
        (1, 0)//一つ右
    };

    public void Initialize()
    {
        var size = mazeBase.Maze.GetLength(0);
        for (int i = 0; i < _checkPointArray.Length; i++)
        {
            var point = ((int)pos.x + _checkPointArray[i].Item1, (int)pos.z + _checkPointArray[i].Item2);
            if (point.Item1 >= 0 && point.Item1 <= size - 1 && point.Item2 >= 0 && point.Item2 <= size - 1)
            {
                if(mazeBase.Maze[point.Item1, point.Item2]) walls[i].SetActive(false);
            }
        }
    }
}