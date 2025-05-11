using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch : MazeBase
{
    [SerializeField] private MazeBase mazeBase;
    [SerializeField] private MazePosition _startPos;
    [SerializeField] private MazePosition _endPos;


    private void SearchStart()
    {
        Stack<(int, int)> route = new();
        route.Push((_startPos.x, _startPos.y));
        bool goal = false;
        while (!goal)
        {
            var pos = route.Pop();
            
        }
    }

    [Serializable]
    private struct MazePosition
    {
        public int x;
        public int y;
    }
}
