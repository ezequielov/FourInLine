using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NextTurnState : InGameState{
    int players, current, currentMod;
    Dictionary<int, statesEnum> playersDictionary = new Dictionary<int, statesEnum>();
    Action<int> onNewCurrentPlayer;
    public NextTurnState(Board board, BoardView view, int playersAmount, Dictionary<int, statesEnum> playersDictionary, Action<statesEnum> nextStateAction, Action<int> onNewCurrentPlayer) : base(board, view, nextStateAction) {
        players = playersAmount;
        System.Random random = new System.Random();
        current = random.Next(1, players + 1);
        this.playersDictionary = playersDictionary;
        this.onNewCurrentPlayer = onNewCurrentPlayer;
    }
    public override void Init() {
        if (!board.IsAnEmptySlotInBoardAviable()) {
            SetNewState(statesEnum.draw);
            return;
        }
        current++;
        currentMod = current % players;
        onNewCurrentPlayer.Invoke(currentMod);
        SetNewState(playersDictionary[currentMod]);
    }
    public override void OnRowSelected(int index) { }
    public override void ViewHandler() { 
        
    }
}
