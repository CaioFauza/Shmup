using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text message;
    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
        message = GameObject.Find("UI_EndText").GetComponent<Text>();
        message.text = gm.lifes > 0  ? "You win!" : "You lose!";
    }

    public void RestartGame()
    {
        gm.ChangeState(GameManager.GameState.START);
    }
}
