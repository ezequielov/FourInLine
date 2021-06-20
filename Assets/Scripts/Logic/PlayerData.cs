using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData {
    public PlayerData(int id, bool isAI) {
        this.id = id;
        this.isAI = isAI;
    }
    public int id;
    public bool isAI;
}
