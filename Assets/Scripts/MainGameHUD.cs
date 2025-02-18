using UnityEngine;
using TMPro;

public class MainGameHUD : MonoBehaviour
{
    [SerializeField, Tooltip("TMP object displaying our current health")] TextMeshProUGUI _healthValueText;
    [SerializeField, Tooltip("TMP object displaying the # of collected coins")] TextMeshProUGUI _coinValueText;
    [SerializeField, Tooltip("TMP object displaying our remaining lives")] TextMeshProUGUI _livesValueText;
    [SerializeField, Tooltip("The health manager we're displaying data for")] HealthManager _healthManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int curHealth = Mathf.RoundToInt(_healthManager.GetHealthCur());
        int maxHealth = Mathf.RoundToInt(_healthManager.GetHealthMax());
        _healthValueText.text = curHealth + "/" + maxHealth;

        _coinValueText.text = GameSessionManager.instance.GetCoins().ToString();
        
        _livesValueText.text = GameSessionManager.instance.GetLives().ToString();
    }
}
