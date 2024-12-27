using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    public Transform humanPlayer;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void GameStart() {
        Debug.Log("Game Start");
    }

    public void GameOver() {
        Debug.Log("Game Over");
    }

}