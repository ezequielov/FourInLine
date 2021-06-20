using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WinState : InGameState{
    int currentPlayer;
    public WinState(Board board, BoardView view, Action<statesEnum> nextStateAction, statesEnum myState) : base(board, view, nextStateAction, myState) { }
    public override void Init() {
        ViewHandler();
    }
    public override void OnRowSelected(int index) { }
    public override void ViewHandler() {
        view.AddNextState(myState);
        view.ShowMsg(true, currentPlayer);
    }
    public override void NewCurrentPlayer(int id) { currentPlayer = id; }
}
