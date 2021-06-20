using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InGameState {
    protected Board board;
    protected BoardView view;
    protected Action<statesEnum> nextStateAction;
    protected statesEnum myState;
    public InGameState(Board board, BoardView view, Action<statesEnum> nextStateAction, statesEnum myState) {
        this.myState = myState;
        this.board = board;
        this.view = view;
        this.nextStateAction = nextStateAction;
    }
    public abstract void Init();
    public abstract void OnRowSelected(int index);
    public void SetNewState(statesEnum s) { nextStateAction.Invoke(s); }
    public abstract void ViewHandler();
    public virtual void NewCurrentPlayer(int id) { }

}
