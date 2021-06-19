using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckGameStatusState : InGameState {
    public CheckGameStatusState(Board board, BoardView view, Action<statesEnum> nextStateAction) : base(board, view, nextStateAction) { }
    public override void Init() {
        Debug.Log("CHECK STATE!!!");
        if(board.IsWinConditionInRow(0, 4)) { Debug.Log("PLAYER 0 WINS!!!"); }
        if (board.IsWinConditionInRow(1, 4)) { Debug.Log("PLAYER 1 WINS!!!"); }
        SetNewState();
    }
    public override void SetNewState() { nextStateAction.Invoke(statesEnum.nextTurn); }
    public override void OnRowSelected(int index) {}
    public override void ViewHandler() { }
}
