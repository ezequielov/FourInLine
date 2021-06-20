using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statesEnum { none, nextTurn, player1Turn, player2Turn, AIPlayer1, AIPlayer2, check, win, draw };
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
        List<statesEnum> playerStatesEnum = new List<statesEnum>();
        playerStatesEnum.Add(statesEnum.player1Turn);
        playerStatesEnum.Add(statesEnum.player2Turn);
        playerStatesEnum.Add(statesEnum.AIPlayer1);
        playerStatesEnum.Add(statesEnum.AIPlayer2);
        Dictionary<int, statesEnum> playersDictionary = new Dictionary<int, statesEnum>();
        for(int i = 0; i < data.players; i++) { playersDictionary.Add(i, playerStatesEnum[i]); }
        if (data.players < data.maxPlayers) {           
            for (int i = data.players; i < data.maxPlayers; i++) { playersDictionary.Add(i, playerStatesEnum[i]); }
        }
        for (int i = 0; i < data.players; i++) { states.Add(playersDictionary[i], new PlayerTurn(board, view, i, SetCurrentState));}
        for (int i = data.players; i < data.maxPlayers; i++) { states.Add(playersDictionary[i], new AIPlayerState(board, view, i, SetCurrentState)); }
        states.Add(statesEnum.nextTurn, new NextTurnState(board, view, data.maxPlayers, playersDictionary, SetCurrentState, UpdateCurrentPlayer));
        states.Add(statesEnum.check, new CheckGameStatusState(board, view, SetCurrentState));
        states.Add(statesEnum.win, new WinState(board, view, SetCurrentState));
        states.Add(statesEnum.draw, new DrawState(board, view, SetCurrentState));
        SetCurrentState(statesEnum.nextTurn);
    }

    void UpdateCurrentPlayer(int id) {
        foreach(InGameState igs in states.Values) { igs.NewCurrentPlayer(id); }
        view.SetCurrentPlayer(id);
    }
    MatchData GetMatchData(string st) {
        string jsonObj = Resources.Load<TextAsset>("Data/MatchData/" + st).ToString();
        return JsonUtility.FromJson <MatchData> (jsonObj);
    }
    void OnChipRowSelected(int index) { states[currentState].OnRowSelected(index); }
    void SetCurrentState(statesEnum s) {
        if(currentState == s) { return; }
        currentState = s;
        states[currentState].Init();
    }

}
