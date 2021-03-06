using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTurn : InGameState {
    int rowIndex, playerId;
    public PlayerTurn(Board board, BoardView view, int id, Action<statesEnum> nextStateAction, statesEnum myState) : base(board, view, nextStateAction, myState) { 
        playerId = id;
    }
    public override void Init() {
        view.AddNextState(myState);
    }
    public override void OnRowSelected(int index) {
        if (board.IsAnEmptySlotInRowAviable(index)) { 
            board.SetChipInSlot(playerId, index);
            rowIndex = index;
            ViewHandler();
            SetNewState(statesEnum.check);
        }        
    }
    public override void ViewHandler() {
        view.DropAChip(rowIndex, board.GetChipPositionOnTopOfRow(rowIndex), playerId);
    }
}
