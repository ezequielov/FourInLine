using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class AIViewState : PlayerViewState {
    const string ST = "WAIT";
    public AIViewState(TextMeshProUGUI msg, Action onTaskCompleted, Color color, int id, Button[] selectorRow) : base(msg, onTaskCompleted, color, id, selectorRow) {}
    public override void Init() {
        ActivateChipButtons(false);
        ShowMsg(true);
    }
    public override void ShowMsg(bool b, int playerIndex = 0) {
        msg.text = ST;
        msg.gameObject.SetActive(b);
    }
}
