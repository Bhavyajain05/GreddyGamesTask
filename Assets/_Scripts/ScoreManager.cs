using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int Score;
    public int ScoreMultipliar = 1;


    [SerializeField] TextMeshProUGUI ScoreText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        ScoreText.text = ("Score: " + Score);
    }
}