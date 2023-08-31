using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public Canvas gameOverCanvas; // 
    public GameObject gameOverPanel;


    public void ShowGameOverPanel()
    {
        gameOverCanvas.gameObject.SetActive(true); // ativa o canvas (se ainda nao tiver ativo)
        gameOverPanel.SetActive(true); // ativa o paine do gameOver
        Time.timeScale = 0; //Pausa o jogo defenindo o tempo de scala como 0

    }


    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false); //desativa o painel de gameOver
        Time.timeScale = 1;  //retorn o tempo de scala normal e despausa o game
    }
}
