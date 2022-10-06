using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviourPun
{
    public static DifficultyManager sharedInstance;

    [Header("Toogle Buttons")]
    public Toggle easyBtn, normalbtn, hardbtn;
    [Space]
    [Header("Player Difficulties")]
    public int player1Difficulty = 1;
    public int player2Difficulty = 1;
    [Space]
    [Header("Is the player the host?")]
    public bool isHost = false;
    [Space]
    [Header("GameObjects")]
    [SerializeField] GameObject difficultyTiers;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    void ChangeDifficulty(int difficulty, bool host)
    {
        if (host)
        {
            player1Difficulty = difficulty;
        }
        else
        {
            player2Difficulty = difficulty;
        }
    }
    public void ShowDifficulties()
    {
        difficultyTiers.SetActive(true);
    }
}
