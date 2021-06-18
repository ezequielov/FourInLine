using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statesEnum { none, playersTurn };
public class InGame : MonoBehaviour
{
    Board board;
    [SerializeField] BoardView view;
    MatchData data;
    statesEnum currentState = statesEnum.none;
    Dictionary<statesEnum, InGameState> states = new Dictionary<statesEnum, InGameState>();
    void Start(){
        data = GetMatchData("MatchData_1");
        Vector2Int grid = new Vector2Int(data.rows, data.lines);
        board = new Board(grid);
        view.Init(grid, OnChipRowSelected);
        InitStates();
    }
    void Update(){
        
    }
    
    void InitStates() {
        states.Add(statesEnum.playersTurn, new PlayersTurn(board, view));
        SetCurrentState(statesEnum.playersTurn);
    }
    MatchData GetMatchData(string st) {
        string jsonObj = Resources.Load<TextAsset>("Data/MatchData/" + st).ToString();
        return JsonUtility.FromJson <MatchData> (jsonObj);
    }
    void OnChipRowSelected(int index) {
        states[currentState].OnRowSelected(index);
    }
    void SetCurrentState(statesEnum s) {
        if(currentState == s) { return; }
        currentState = s;
        states[currentState].Init();
    }

}
