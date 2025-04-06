using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject cellPrefab;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newCell = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity);
                newCell.transform.parent = this.transform;
            }
        }
    }
}
