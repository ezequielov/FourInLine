using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BoardView : MonoBehaviour
{
    [SerializeField] Transform chipHolder;
    [SerializeField] TextMeshProUGUI winner;
    Action<int> onRowSelected;
    Rect myRect;
    Button[] selectorRow;
    Vector2[][] gridView;

    public void Init(Vector2Int grid, Action<int> onRowSelected) {
        this.onRowSelected = onRowSelected;
        myRect = GetComponent<RectTransform>().rect;
        Vector2 boardSize = new Vector2(myRect.width, myRect.height);
        SetButtons(grid, boardSize);
        SetGridView(grid, boardSize);
    }
    void OnChipButtonPressed(int index) { onRowSelected.Invoke(index); }
    void SetButtons(Vector2Int grid, Vector2 boardSize) {               
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
    void SetGridView(Vector2Int grid, Vector2 boardSize) {
        Vector2 pos;
        gridView = new Vector2[grid.x][];
        for (int x = 0; x < grid.x; x++) {
            gridView[x] = new Vector2[grid.y];
            pos.x = boardSize.x / grid.x * x + (Screen.width / 2 + myRect.xMin) + ((boardSize.x / grid.x) / 2);
            for (int y = 0; y < grid.y; y++) {
                pos.y = boardSize.y / grid.y * y + (Screen.height / 2 + myRect.yMin) + ((boardSize.y / grid.y) / 2);
                gridView[x][y] = pos;
            }
        }
    }
    public void DropAChip(int row, int line, int player) {
        GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Chip_" + player));
        go.transform.parent = chipHolder;
        go.GetComponent<RectTransform>().position = gridView[row][line];
    }
    public void ShowWinner(bool b, int playerIndex) {
        winner.text = "PLAYER " + playerIndex + " WINS !!";
        winner.gameObject.SetActive(b);
    }
    public void SetCurrentPlayer(int id) {

    }
}
