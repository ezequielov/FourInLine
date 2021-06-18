using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameStatusState : InGameState {
    public CheckGameStatusState(Board board, BoardView view) : base(board, view) { }
    public override void Init() { 
    }
    public override void OnRowSelected(int index) {}
    public override void ViewHandler() { }
}
