using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SetWallObject : MonoBehaviour
{
    [SerializeField] private MazeBase _mazeBase;
    [SerializeField] private int _mazeSize;
    [SerializeField] private GameObject _mazeObjectPrefab;

    public void GenerateMaze()
    {
        _ = SetWall();
    }

    private async UniTask SetWall()
    {
        var posList = new List<Vector3>();
        for (int i = 0; i < _mazeSize; i++)
        {
            for (int j = 0; j < _mazeSize; j++)
            {
                if (_mazeBase.Maze[i, j] == false)
                {
                    posList.Add(new Vector3(i * 2, 0, j * 2));
                }
            }
        }
        var result = await InstantiateAsync(_mazeObjectPrefab, posList.Count);
        
        for (int i = 0; i < posList.Count; i++)
        {
            result[i].transform.position = posList[i];
        }
    }
    
    
}