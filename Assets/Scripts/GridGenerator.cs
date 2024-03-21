using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gridObject;

    [SerializeField] private int width = 10;

    [SerializeField] private int height = 10;

    [SerializeField] private Vector2 origin = Vector2.zero;

    [SerializeField] private float offset;


    private GameObject[,] _grid;


    public GameObject[,] CreateGrid()
    {
        _grid = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell =
                    Instantiate(gridObject, origin + new Vector2(x + x * (offset * .5f), y + y * (offset * .5f)),
                        Quaternion.identity);
                _grid[x, y] = cell;
            }
        }

        return _grid;
    }
}