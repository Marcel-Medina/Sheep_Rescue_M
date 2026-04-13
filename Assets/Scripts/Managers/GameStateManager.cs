using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [HideInInspector]
    public int sheepSaved; // Use this for both UI and Power-ups

    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver;
    public SheepSpawner sheepSpawner;
    public PowerUpSpawner powerUpSpawner;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void SavedSheep()
    {
        // 1. Increment the correct variable
        sheepSaved++;

        // 2. Trigger UI update
        UIManager.Instance.UpdateSheepSaved();

        // 3. Check for Power-Up (Every 30 sheep)
        if (sheepSaved > 0 && sheepSaved % 30 == 0)
        {
            if (powerUpSpawner != null)
            {
                powerUpSpawner.SpawnPowerUp();
            }
        }
    }

    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped >= sheepDroppedBeforeGameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();
    }
}