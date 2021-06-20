using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public abstract class BoardViewState {
    protected Action onTaskCompleted;
    protected TextMeshProUGUI msg;
    protected Button[] selectorRow;
    public BoardViewState(Action onTaskCompleted, TextMeshProUGUI msg, Button[] selectorRow) { 
        this.onTaskCompleted = onTaskCompleted;
        this.msg = msg;
        this.selectorRow = selectorRow;
    }
    public abstract void Init();
    public abstract void OnUpdate(float dt);
    public virtual void DropAChip(Vector2 pos, GameObject go, int row) {}
    public virtual void ShowMsg(bool b, int playerIndex = 0) { }
    protected virtual void ActivateChipButtons(bool b) {
        foreach(Button btn in selectorRow) { btn.interactable = b; }
    }

}
