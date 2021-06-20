using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DrawState : InGameState {
    public DrawState(Board board, BoardView view, Action<statesEnum> nextStateAction) : base(board, view, nextStateAction) { }
    public override void Init() {
        ViewHandler();
    }
public override void OnRowSelected(int index) { }
public override void ViewHandler() {
        view.Draw();
}

}
