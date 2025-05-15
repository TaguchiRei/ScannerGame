using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SetWallObject : MonoBehaviour
{
    [SerializeField] private MazeBase _mazeBase;
    [SerializeField] private int _mazeSize;
    [SerializeField] private GameObject _mazeObjectPrefab;
    [SerializeField] private float _objectSize;
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
                if (_mazeBase.Maze[i, j])
                {
                    posList.Add(new Vector3(i * _objectSize, 0, j * _objectSize));
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