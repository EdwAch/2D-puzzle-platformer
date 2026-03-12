using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private Button nextButton;

    private void Awake() {
        nextButton.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
    }

    private void Start() {
        Player.Instance.OnLevelEnd += Player_OnLevelEnd;
        
        Hide();
    }

    private void Player_OnLevelEnd(object sender, Player.OnLevelEndEventArgs e) {
        
        if (e.endType == Player.EndType.LevelComplete) {
            titleTextMesh.text = "Level Completed!";
        } else {
            titleTextMesh.text = "Level Failed!";
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
