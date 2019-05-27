using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCellScaler : MonoBehaviour
{
    RectTransform rect;
    GridLayoutGroup layout;
    const int ROW_COUNT = 2;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        layout = GetComponent<GridLayoutGroup>();
    }
    
    void Update()
    {
        int columnCount = layout.constraintCount;
        Vector2 spacing = layout.spacing;
        float sizeX = (rect.rect.width - (spacing.x * (columnCount + 1))) / columnCount;
        float sizeY = (rect.rect.height - (spacing.y * (ROW_COUNT + 1))) / ROW_COUNT;
        layout.cellSize = new Vector2(sizeX, sizeY);
    }
}
