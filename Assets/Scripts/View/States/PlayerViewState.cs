using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PlayerViewState : BoardViewState {
    protected Color color;
    protected int id;
    protected GameObject chip;
    Vector2  toPos;
    float posLerp, speed, fromPosY;
    const float SPEED = 0.001f;
    const string ST = "SELECT A CHIP TO DROP";
    RectTransform rectTr;
    public PlayerViewState(TextMeshProUGUI msg, Action onTaskCompleted, Color color, int id, Button[] selectorRow) : base(onTaskCompleted, msg, selectorRow) {
        this.color = color;
        this.id = id;
        chip = null;
    }
    public override void Init() {
        ActivateChipButtons(true);
        ShowMsg(true); 
    }
    public override void OnUpdate(float dt) {
        if (chip == null) { return; }
        posLerp += dt / speed;
        rectTr.position = new Vector3(toPos.x, Mathf.Lerp(fromPosY, toPos.y, posLerp), 0);
        if(posLerp >= 1) {
            posLerp = 0;
            chip = null;
            ShowMsg(false);
            onTaskCompleted.Invoke();
        }
    }
    public override void DropAChip(Vector2 pos, GameObject go, int row) {
        rectTr = go.GetComponent<RectTransform>();
        fromPosY = selectorRow[row].transform.gameObject.GetComponent<RectTransform>().position.y;
        toPos = pos;
        chip = go;
        chip.GetComponent<Image>().color = color;
        chip.SetActive(true);
        speed = (fromPosY - toPos.y) * SPEED;
    }
    public override void ShowMsg(bool b, int playerIndex = 0) {
        msg.text = ST;
        msg.gameObject.SetActive(b);
    }
}
