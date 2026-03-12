using System;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour {
    
    
    [SerializeField] private TextMeshProUGUI statsTextMesh;


    private void Start() {
        Player.Instance.OnLevelEnd += Player_OnLevelEnd;
        
        Show();
    }

    private void Update() {
        UpdateStatsTextMesh();
    }

    private void UpdateStatsTextMesh() {
        statsTextMesh.text =
            MathF.Round(Time.timeSinceLevelLoad) + "\n" +
            GameManager.Instance.GetScore()
            ;
    }

    private void Player_OnLevelEnd(object sender, Player.OnLevelEndEventArgs e) {
        Hide();
    }


    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
