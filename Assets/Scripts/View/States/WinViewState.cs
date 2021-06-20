using UnityEngine.UI;
using TMPro;
using System;

public class WinViewState : BoardViewState {
    const string ST_1 = "PLAYER ";
    const string ST_2 = " WINS !!";
    public WinViewState(TextMeshProUGUI msg, Action onTaskCompleted, Button[] selectorRow) : base(onTaskCompleted, msg, selectorRow) {}
    public override void OnUpdate(float dt) { }
    public override void Init() {
        ActivateChipButtons(false);
        ShowMsg(true);
    }
    public override void ShowMsg(bool b, int playerIndex = 0) {
        msg.text = ST_1 + playerIndex + ST_2;
        msg.gameObject.SetActive(b);
    }
}
