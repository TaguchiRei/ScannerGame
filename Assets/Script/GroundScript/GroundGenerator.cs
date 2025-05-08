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
        /*
        StringBuilder st = new StringBuilder();
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                st.Append(_maze[i, j] ? "　" : "壁");
            }

            st.Append("\n");
        }

        string maze = st.ToString();
        Debug.Log(maze);
        */
    }


    /// <summary>
    /// 迷路生成を行う
    /// </summary>
    /// <param name="size">必ず奇数を入力</param>
    void CreateMaze(int size)
    {
        _maze = new bool[size, size];
        Stack<(int, int)> road = new Stack<(int, int)>();
        var startPos = (Random.Range(0, size/2) * 2, Random.Range(0, size/2) * 2);
        Debug.Log(startPos.ToString());
    }
    
}