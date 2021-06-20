using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIPlayerState : InGameState{
    int rowIndex, playerId;
    System.Random random;
    public AIPlayerState(Board board, BoardView view, int id, Action<statesEnum> nextStateAction, statesEnum myState) : base(board, view, nextStateAction, myState) {
        playerId = id;
    }
    public override void Init() {
        random = new System.Random();
        int amount = board.GetNumberOfRows();
        List<int> rowIndexList = new List<int>();
        for(int i = 0; i < amount; i++) {
            if (board.IsAnEmptySlotInRowAviable(i)) { rowIndexList.Add(i); }
        }
        if(rowIndexList.Count == 0) { return; }
        List<int> bestChoice = new List<int>();
        foreach(int xPos in rowIndexList) {
            if(board.IsWinConditionInRow(xPos, playerId, 3)) { if (!bestChoice.Contains(xPos)) { bestChoice.Add(xPos); } }
            if (board.GetHowManyBeside(xPos, playerId, true, 3) + board.GetHowManyBeside(xPos, playerId, false, 3) >= 3) { bestChoice.Add(xPos); }
        }
        if(bestChoice.Count > 0) { rowIndex = bestChoice[random.Next(0, bestChoice.Count)]; }
        else { rowIndex = rowIndexList[random.Next(0, rowIndexList.Count)]; }
                
        board.SetChipInSlot(playerId, rowIndex);
        ViewHandler();
        SetNewState(statesEnum.check);
    }
    public override void OnRowSelected(int index) {}
    public override void ViewHandler() {
        view.AddNextState(myState);
        view.DropAChip(rowIndex, board.GetChipPositionOnTopOfRow(rowIndex), playerId);
    }

}
