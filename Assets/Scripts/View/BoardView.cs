using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoardView : MonoBehaviour
{
    Action<int> onRowSelected;
    Rect myRect;
    Button[] selectorRow;

    public void Init(Vector2Int grid, Action<int> onRowSelected) {
        this.onRowSelected = onRowSelected;
        myRect = GetComponent<RectTransform>().rect;
        SetButtons(grid);
    }
    void OnChipButtonPressed(int index) { onRowSelected.Invoke(index); }
    void SetButtons(Vector2Int grid) {        
        Vector2 boardSize = new Vector2(myRect.width, myRect.height);
        Button selector = Resources.Load<Button>("Prefabs/ChipButton");
        selectorRow = new Button[grid.x];
        for (int i = 0; i < grid.x; i++) {
            int index = i;
            selectorRow[i] = Instantiate<Button>(selector);
            selectorRow[i].transform.parent = transform;
            float xPos = boardSize.x / grid.x * i + (Screen.width / 2 + myRect.xMin) + ((boardSize.x / grid.x) / 2);
            selectorRow[i].onClick.AddListener(() => { OnChipButtonPressed(index); });
            selectorRow[i].GetComponent<RectTransform>().position = new Vector2(xPos, Screen.height * 0.9f);
        }
    }
}
