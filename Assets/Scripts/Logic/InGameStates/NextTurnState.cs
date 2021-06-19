using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NextTurnState : InGameState{
    int players, current, currentMod;
    Dictionary<int, statesEnum> playersDictionary = new Dictionary<int, statesEnum>();
    public NextTurnState(Board board, BoardView view, int playersAmount, Dictionary<int, statesEnum> playersDictionary, Action<statesEnum> nextStateAction) : base(board, view, nextStateAction) {
        players = playersAmount;
        System.Random random = new System.Random();
        current = random.Next(1, players + 1);
        this.playersDictionary = playersDictionary;
    }
    public override void Init() {
        current++;
        currentMod = current % players;
        SetNewState();
    }
    public override void OnRowSelected(int index) { }
    public override void SetNewState() { nextStateAction.Invoke(playersDictionary[currentMod]); }
    public override void ViewHandler() { 
        
    }
}
