using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private int levelNumber;

    [SerializeField] private List<GameLevel> gameLevelList;
    
    public static GameManager Instance { get; private set; }
    
    private int score;

    private void Start() {
        Player.Instance.OnCoinPickup += Player_OnCoinPickup;
        
        LoadCurrentLevel();
    }

    private void Awake() {
        Instance = this;
    }

    private void LoadCurrentLevel() {
        foreach (GameLevel gameLevel in gameLevelList) {
            if (gameLevel.GetLevelNumber() == levelNumber) {
                GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Player.Instance.transform.position = spawnedGameLevel.GetPlayerStartPosition();
            }
        }
    }

    private void Player_OnCoinPickup(object sender, EventArgs e) {
        AddScore(500);
    }

    private void AddScore(int addScoreAmount) {
        score += addScoreAmount;
    }

    public int GetScore() {
        return score;
    }
}
