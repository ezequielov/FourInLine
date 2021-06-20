using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WinState : InGameState{
    int currentPlayer;
    public WinState(Board board, BoardView view, Action<statesEnum> nextStateAction) : base(board, view, nextStateAction) { }
    public override void Init() {
        ViewHandler();
    }
    public override void SetNewState(statesEnum s) { nextStateAction.Invoke(s); }
    public override void OnRowSelected(int index) { }
    public override void ViewHandler() { 
        view.ShowWinner(true, currentPlayer);
    }
    public override void NewCurrentPlayer(int id) { currentPlayer = id; }
}
