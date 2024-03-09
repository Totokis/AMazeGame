using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject[,] _grid;
    private RecursiveBacktrackingMaze.Cell[,] _maze;
    [SerializeField]private Vector2Int currentPosition;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           
            if ((_maze[currentPosition.x, currentPosition.y] & RecursiveBacktrackingMaze.Cell.N)!=RecursiveBacktrackingMaze.Cell.N)
            {
                currentPosition.x = currentPosition.x;
                currentPosition.y += 1;
            }

            transform.position= _grid[currentPosition.x, currentPosition.y].transform.position;

        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if ((_maze[currentPosition.x, currentPosition.y] & RecursiveBacktrackingMaze.Cell.S)!=RecursiveBacktrackingMaze.Cell.S)
            {
                currentPosition.x = currentPosition.x;
                currentPosition.y -= 1;
            } 
            transform.position= _grid[currentPosition.x, currentPosition.y].transform.position;
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ((_maze[currentPosition.x, currentPosition.y] & RecursiveBacktrackingMaze.Cell.E)!=RecursiveBacktrackingMaze.Cell.E)
            {
                currentPosition.x += 1;
                currentPosition.y = currentPosition.y;
                
            } 
            transform.position= _grid[currentPosition.x, currentPosition.y].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if ((_maze[currentPosition.x, currentPosition.y] & RecursiveBacktrackingMaze.Cell.W)!=RecursiveBacktrackingMaze.Cell.W)
            {
                currentPosition.x -= 1;
                currentPosition.y = currentPosition.y;
            }
            transform.position= _grid[currentPosition.x, currentPosition.y].transform.position;
        }
    }
   
    public void SetStartCell(int startX, int startY, GameObject[,] grid, RecursiveBacktrackingMaze.Cell[,] generatedMaze)
    {
        var startCell = grid[startX, startY];
        currentPosition = new Vector2Int(startX, startY);
        transform.position = startCell.transform.position;
        _grid = grid;
        _maze = generatedMaze;
    }
}
