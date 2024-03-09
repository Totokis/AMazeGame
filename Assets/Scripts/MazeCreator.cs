using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class MazeCreator : MonoBehaviour
{
    [SerializeField]
    private GridGenerator gridGenerator;

    [SerializeField]
    private Player player;
    

    [SerializeField]
    private GameObject northSouthWall;

    [SerializeField]
    private GameObject eastWestWall;

   
    private void Start()
    {
        var grid = gridGenerator.CreateGrid();
        var height = grid.GetLength(0);
        var width = grid.GetLength(1);

        var startX = 0;
        var startY = 0;
        var maze = new RecursiveBacktrackingMaze().GetWallsAsCells(startX,startY,width,height);
        

        var generatedMaze = new RecursiveBacktrackingMaze.Cell[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var position = grid[x,y].transform.position+new Vector3(0,0,-1.1f);
                var mask = 0b1111;
                var cell = ~maze[x, y] & (RecursiveBacktrackingMaze.Cell)mask; //wtf i just did here
                generatedMaze[x, y] = cell;
                switch (cell)
                {
                    case RecursiveBacktrackingMaze.Cell.N:
                    {
                        WallN(grid, x, y);
                        DebugExtensions.CreateText( position,"N"+$"\n({x},{y})");
                        break;
                    }
                    case RecursiveBacktrackingMaze.Cell.S:
                    {
                        WallS(grid, x, y);
                        DebugExtensions.CreateText( position,"S"+$"\n({x},{y})");
                        break;
                    }
                    case RecursiveBacktrackingMaze.Cell.E:
                    {
                        WallE(grid, x, y);
                        DebugExtensions.CreateText( position,"E"+$"\n({x},{y})");
                        break;
                    }
                    case RecursiveBacktrackingMaze.Cell.W:
                    {
                        WallW(grid, x, y);
                        DebugExtensions.CreateText( position,"W"+$"\n({x},{y})");
                        break;
                    }
                    case RecursiveBacktrackingMaze.Cell.NS:
                        DebugExtensions.CreateText( position,"NS"+$"\n({x},{y})");
                        WallN(grid,x,y);
                        WallS(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.NE:
                        DebugExtensions.CreateText( position,"NE"+$"\n({x},{y})");
                        WallN(grid,x,y);
                        WallE(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.NW:
                        DebugExtensions.CreateText( position,"NW"+$"\n({x},{y})");
                        WallN(grid,x,y);
                        WallW(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.SE:
                        DebugExtensions.CreateText( position,"SE"+$"\n({x},{y})");
                        WallE(grid,x,y);
                        WallS(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.SW:
                        DebugExtensions.CreateText( position,"SW"+$"\n({x},{y})");
                        WallW(grid,x,y);
                        WallS(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.WE:
                        DebugExtensions.CreateText( position,"WE"+$"\n({x},{y})");
                        WallW(grid,x,y);
                        WallE(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.NEW:
                        DebugExtensions.CreateText( position,"NEW"+$"\n({x},{y})");
                        WallN(grid,x,y);
                        WallE(grid,x,y);
                        WallW(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.NES:
                        DebugExtensions.CreateText( position,"NES"+$"\n({x},{y})");
                        WallN(grid,x,y);
                        WallE(grid,x,y);
                        WallS(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.NWS:
                        DebugExtensions.CreateText( position,"NWS"+$"\n({x},{y})");
                        WallN(grid,x,y);
                        WallW(grid,x,y);
                        WallS(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.SEW:
                        DebugExtensions.CreateText( position,"SEW"+$"\n({x},{y})");
                        WallS(grid,x,y);
                        WallE(grid,x,y);
                        WallW(grid,x,y);
                        break;
                    case RecursiveBacktrackingMaze.Cell.Empty:
                        break;
                        
                    default:
                        Debug.LogError($"Enum throws for this: {cell}");
                        break;
                    
                }
                
            }
        }
        SetStartCellForPlayer(startX, startY, grid,generatedMaze);

    }
    private void SetStartCellForPlayer(int startX, int startY, GameObject[,] grid, RecursiveBacktrackingMaze.Cell[,] generatedMaze)
    {
        player.SetStartCell(startX,startY,grid,generatedMaze);

    }
    private void WallW(GameObject[,] grid, int x, int y)
    {

        var pos = grid[x, y].transform.position;
        Instantiate(eastWestWall, pos + new Vector3(-0.45f, 0), eastWestWall.transform.rotation);
    }
    private void WallE(GameObject[,] grid, int x, int y)
    {

        var pos = grid[x, y].transform.position;
        Instantiate(eastWestWall, pos + new Vector3(0.45f, 0), eastWestWall.transform.rotation);
    }
    private void WallS(GameObject[,] grid, int x, int y)
    {

        var pos = grid[x, y].transform.position;
        Instantiate(northSouthWall, pos + new Vector3(0f, -0.45f), northSouthWall.transform.rotation);
    }
    private void WallN(GameObject[,] grid, int x, int y)
    {

        var pos = grid[x, y].transform.position;
        Instantiate(northSouthWall, pos + new Vector3(0f, 0.45f), northSouthWall.transform.rotation);
    }
}

public class RecursiveBacktrackingMaze
{
    [Flags]
    public enum Cell
    {
        Empty = 0b0000,
        N = 0b0001,
        S = 0b0010,
        E = 0b0100,
        W = 0b1000,
        NS = N|S,
        NE = N|E ,
        NW = N|W,
        SE = S|E,
        SW = S|W,
        WE = W|E,
        NEW = N|E|W,
        NES = N|E|S,
        NWS = N|W|S,
        SEW = S|E|W,
    }

    private Cell GetOppositeWall(Cell cell)
    {
        return cell switch
        {
            Cell.N => Cell.S,
            Cell.S => Cell.N,
            Cell.E => Cell.W,
            Cell.W => Cell.E,
            Cell.Empty => Cell.Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, "Check cell enum")
        };
    }

    private int GetYDistance(Cell cell)
    {
        return cell switch
        {
            Cell.N => 1,
            Cell.S => -1,
            Cell.W => 0,
            Cell.E => 0,
            Cell.Empty => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
        };
    }
    
    private int GetXDistance(Cell cell)
    {
        return cell switch
        {
            Cell.N => 0,
            Cell.S => 0,
            Cell.W => -1,
            Cell.E => 1,
            Cell.Empty => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(cell), cell, null)
        };
    }
    
    public Cell [,] GetWallsAsCells(int startX, int startY, int width, int height)
    {
        var grid =  new Cell[width, height];

        CarvePassagesFrom(startX,startY, grid);
        
        return grid;
    }

    private void CarvePassagesFrom(int startX, int startY, Cell[,] grid)
    {
        var directions = new List<Cell>
        {
            Cell.N,
            Cell.S,
            Cell.E,
            Cell.W,
        };
        directions.Shuffle();

        foreach (var direction in directions)
        {
            var newX = startX + GetXDistance(direction);
            var newY = startY + GetYDistance(direction);

            if ((newY >= 0 && newY <= grid.GetLength(1) - 1) && (newX >= 0 && newX <= grid.GetLength(0) - 1) && grid[newX,newY] == Cell.Empty)
            {
                grid[startX, startY] |= direction;
                grid[newX, newY] |= GetOppositeWall(direction);
                CarvePassagesFrom(newX,newY,grid);
            }

        }


    }
    

}
