using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckGameStatusState : InGameState {
    public CheckGameStatusState(Board board, BoardView view, Action<statesEnum> nextStateAction) : base(board, view, nextStateAction) { }
    public override void Init() { 
    }
    public override void SetNewState() {}
    public override void OnRowSelected(int index) {}
    public override void ViewHandler() { }
}
