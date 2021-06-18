using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersTurn : InGameState {
    int playerId = 1;
    int rowIndex;
    public PlayersTurn(Board board, BoardView view) : base(board, view) { }
    public override void Init() { }
    public override void OnRowSelected(int index) {
        if (board.IsAnEmptySlotInRowAviable(index)) { 
            board.SetChipInSlot(playerId, index);
            rowIndex = index;
            ViewHandler();
        }        
    }
    public override void ViewHandler() {
        view.DropAChip(rowIndex, board.GetChipPositionOnTopOfRow(rowIndex), playerId);
    }
}
