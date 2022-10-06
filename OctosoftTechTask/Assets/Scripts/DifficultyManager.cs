using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviourPun
{
    public static DifficultyManager sharedInstance;
    [SerializeField] Toggle easyBtn, normalbtn, hardbtn;
    public int player1Difficulty = 1;
    public int player2Difficulty = 1;
    public bool isHost = false;
    [SerializeField] GameObject difficultyTiers;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
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
