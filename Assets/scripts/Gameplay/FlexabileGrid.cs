using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexabileGrid : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 CellSize;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrtRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrtRt);
        columns = Mathf.CeilToInt(sqrtRt);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)columns;
        float cellHeight = parentHeight / (float)rows;

        CellSize.x = cellWidth;
        CellSize.y = cellHeight;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var XPos = (CellSize.x * columnCount);
            var yPos = (CellSize.y * rowCount);
         
            SetChildAlongAxis(item, 0, XPos, CellSize.x);

            SetChildAlongAxis(item, 1,yPos, CellSize.y);
        }
    }
    public override void CalculateLayoutInputVertical()
    {
      
    }

    public override void SetLayoutHorizontal()
    {
       
    }

    public override void SetLayoutVertical()
    {
       
    }



}
