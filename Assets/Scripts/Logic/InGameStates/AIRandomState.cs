using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIRandomState : InGameState{
    int rowIndex, playerId;
    System.Random random;
    public AIRandomState(Board board, BoardView view, int id, Action<statesEnum> nextStateAction) : base(board, view, nextStateAction) {
        playerId = id;
    }
    public override void Init() {
        int amount = board.GetNumberOfRows();
        List<int> rowIndexList = new List<int>();
        for(int i = 0; i < amount; i++) {
            if (board.IsAnEmptySlotInRowAviable(i)) { rowIndexList.Add(i); }
        }
        random = new System.Random();
        rowIndex = rowIndexList[random.Next(0, rowIndexList.Count)];
        board.SetChipInSlot(playerId, rowIndex);
        ViewHandler();
        SetNewState();
    }
    public override void SetNewState() { nextStateAction.Invoke(statesEnum.nextTurn); }
    public override void OnRowSelected(int index) {}
    public override void ViewHandler() {
        view.DropAChip(rowIndex, board.GetChipPositionOnTopOfRow(rowIndex), playerId);
    }
}
