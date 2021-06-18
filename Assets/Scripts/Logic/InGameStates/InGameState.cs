using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InGameState {
    protected Board board;
    protected BoardView view;
    public InGameState(Board board, BoardView view) {
        this.board = board;
        this.view = view;
    }
    public abstract void Init();
    public abstract void OnRowSelected(int index);
    public abstract void ViewHandler();
}
