using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckGameStatusState : InGameState {
    int currentPlayer;
    public CheckGameStatusState(Board board, BoardView view, Action<statesEnum> nextStateAction) : base(board, view, nextStateAction) { }
    public override void Init() {
        if (board.IsWinConditionAchieved(currentPlayer)) { SetNewState(statesEnum.win); }
        else { SetNewState(statesEnum.nextTurn); }       
    }
    public override void SetNewState(statesEnum s) { nextStateAction.Invoke(s); }
    public override void OnRowSelected(int index) {}
    public override void ViewHandler() { }
    public override void NewCurrentPlayer(int id) { currentPlayer = id; }
}
