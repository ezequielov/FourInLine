using TMPro;
using System;
using UnityEngine.UI;

public class DrawViewState : BoardViewState {
    const string ST = "---- DRAW ----";
    public DrawViewState(TextMeshProUGUI msg, Action onTaskCompleted, Button[] selectorRow) : base(onTaskCompleted, msg, selectorRow) {}
    public override void Init() {
        ActivateChipButtons(false);
        ShowMsg(true);
    }
    public override void OnUpdate(float dt) { }
    public override void ShowMsg(bool b, int playerIndex = 0) {
        msg.text = ST;
        msg.gameObject.SetActive(b);
    }
}
