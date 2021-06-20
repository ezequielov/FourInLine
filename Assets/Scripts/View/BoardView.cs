using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BoardView : MonoBehaviour
{
    [SerializeField] Transform chipHolder;
    [SerializeField] TextMeshProUGUI msg, topMsg;
    [SerializeField] Button resetBtn;
    Action<int> onRowSelected;
    Action onResetSelected;
    Rect myRect;
    Button[] selectorRow;
    Vector2[][] gridView;
    statesEnum currentState;
    GameObject chip;
    Dictionary<statesEnum, BoardViewState> states = new Dictionary<statesEnum, BoardViewState>();
    List<statesEnum> nextStates = new List<statesEnum>();
    statesEnum logicState = statesEnum.none;

    public void Init(Vector2Int grid, Action<int> onRowSelected, Action onResetSelected, PlayerData[] playerData) {
        resetBtn.onClick.AddListener(() => { OnResetButtonPressed(); });
        currentState = statesEnum.none;
        chip = Resources.Load<GameObject>("Prefabs/Chip");
        this.onRowSelected = onRowSelected;
        this.onResetSelected = onResetSelected;
        myRect = GetComponent<RectTransform>().rect;
        Vector2 boardSize = new Vector2(myRect.width, myRect.height);
        SetButtons(grid, boardSize);
        SetGridView(grid, boardSize);
        SetStates(playerData);
    }
    void SetStates(PlayerData[] playerData) {
        List<statesEnum> playerStatesList = new List<statesEnum> { 
            statesEnum.player1Turn,
            statesEnum.player2Turn,
            statesEnum.player3Turn,
            statesEnum.player4Turn
        };
        List<Color> playerColor = new List<Color> {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow
        };
        for (int i = 0; i < playerData.Length; i++) {
            if (playerData[i].isAI) { states.Add(playerStatesList[i],new AIViewState(topMsg, ToNextState, playerColor[playerData[i].id], playerData[i].id, selectorRow)); }
            else { states.Add(playerStatesList[i], new PlayerViewState(topMsg, ToNextState, playerColor[playerData[i].id], playerData[i].id, selectorRow)); }
        }
        states.Add(statesEnum.draw, new DrawViewState(msg, ToNextState, selectorRow));
        states.Add(statesEnum.win, new WinViewState(msg, ToNextState, selectorRow));
    }
    void OnChipButtonPressed(int index) { onRowSelected.Invoke(index); }
    void OnResetButtonPressed() { onResetSelected.Invoke(); }
    void SetButtons(Vector2Int grid, Vector2 boardSize) {               
        Button selector = Resources.Load<Button>("Prefabs/ChipButton");
        selectorRow = new Button[grid.x];
        for (int i = 0; i < grid.x; i++) {
            int index = i;
            selectorRow[i] = Instantiate<Button>(selector);
            selectorRow[i].transform.parent = transform;
            float xPos = boardSize.x / grid.x * i + (Screen.width / 2 + myRect.xMin) + ((boardSize.x / grid.x) / 2f);
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
    void Update() {
        if(currentState == statesEnum.none) { return; }
        states[currentState].OnUpdate(Time.deltaTime);
    }
    public void DropAChip(int row, int line, int player) {
        GameObject go = Instantiate<GameObject>(chip);
        go.transform.parent = chipHolder;
       // go.GetComponent<RectTransform>().position = gridView[row][line];
        states[logicState].DropAChip(gridView[row][line], go, row);
    }
    public void ShowMsg(bool b, int playerIndex = 0) {
        states[logicState].ShowMsg(b, playerIndex);
    }
    public void AddNextState(statesEnum s) {
        if (!nextStates.Contains(s)) { 
            nextStates.Add(s);
            logicState = s;
            if(currentState == statesEnum.none) { ToNextState(); }
        }
    }
    public void ToNextState() {
        if(nextStates.Count > 0 && currentState != nextStates[0]) {
            currentState = nextStates[0];
            nextStates.Remove(currentState);
            states[currentState].Init();
        }
        else if(nextStates.Count == 0) { currentState = statesEnum.none; }
    }
    public void SetCurrentPlayer(int id) {}
}
