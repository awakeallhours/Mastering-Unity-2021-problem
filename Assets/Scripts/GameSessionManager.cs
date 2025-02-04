using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField, Tooltip("Remianing player lives")] private int _playerLives = 3;
    [SerializeField, Tooltip("Where the player will respawn")] private Transform _respawnLocation;
    [SerializeField, Tooltip("Object to display when game is over")] private GameObject _gameOverObj;
    [SerializeField, Tooltip("Title menu countdown after game is over")] private float _returnToMenuCountdown = 0;
    static public GameSessionManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_returnToMenuCountdown > 0)
        {
            _returnToMenuCountdown -= Time.deltaTime;
            
            if (_returnToMenuCountdown < 0)
                SceneManager.LoadScene("Title Menu");
        }
    }

    public void onPlayerDeath(GameObject player)
    {
        if (_playerLives <= 0)
        {
            //player is out of lives
            GameObject.Destroy(player.gameObject);
            _gameOverObj.SetActive(true);
            _returnToMenuCountdown = 4;
            Debug.Log("Game Over");
        }
        else
        {
            //use a life to respawn the player
            _playerLives--;

            //reset health 
            HealthManager playerHealth = player.GetComponent<HealthManager>();
            if (playerHealth) 
                playerHealth.Reset();

            if(_respawnLocation)
                player.transform.position = _respawnLocation.position;

            Debug.Log("Player lives reaming: " + _playerLives);
        }

    }

    public int GetCoins()
    {
        return PickUpItem.s_objectsCollected; 
    }

    public int GetLives()
    {
        return _playerLives;
    }
}
