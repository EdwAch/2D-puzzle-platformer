using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;
    [SerializeField] private Button nextButton;

    private Action nextButtonClickAction;
    
    private void Awake() {
        nextButton.onClick.AddListener(() => {
            nextButtonClickAction();
        });
    }

    private void Start() {
        Player.Instance.OnLevelEnd += Player_OnLevelEnd;
        
        Hide();
    }

    private void Player_OnLevelEnd(object sender, Player.OnLevelEndEventArgs e) {
        
        if (e.endType == Player.EndType.LevelComplete) {
            titleTextMesh.text = "Level Completed!";
            nextButtonTextMesh.text = "Next Level";
            nextButtonClickAction = GameManager.Instance.GoToNextLevel;
        } else {
            titleTextMesh.text = "Level Failed!";
            nextButtonTextMesh.text = "Retry";
            nextButtonClickAction = GameManager.Instance.RetryLevel;
        }

        statsTextMesh.text =
            GameManager.Instance.GetScore().ToString();
        
        
        Show();
    }


    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    
}
