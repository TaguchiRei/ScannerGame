using System.Collections.Generic;
using UnityEngine;

public class WallExtensionMazeGenerator : MonoBehaviour
{
    private bool[,] _maze;

    [SerializeField] private int _mazeSize;

    private readonly (int, int)[] _checkPointArray =
    {
        (0, -1),
        (0, 1),
        (-1, 0),
        (1, 0),
    };

    void Start()
    {
        CreateMaze(_mazeSize);
    }

    private void CreateMaze(int size)
    {
        _maze = new bool[size, size];
        Stack<(int,int)> generationWallStack = new();
        
    }

    private List<(int, int)> GetUncheckCell((int, int) roadPoint, int size)
    {
        List<(int, int)> direction = new();

        return direction;
    }
}