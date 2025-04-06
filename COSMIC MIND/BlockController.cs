using UnityEngine;

public class BlockController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        SnapToGrid();
    }

    void SnapToGrid()
    {
        
        Vector3 gridPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
        transform.position = gridPosition;
    }
}
