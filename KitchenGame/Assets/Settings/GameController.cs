using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Player MainPlayer => _mainPlayer;
    [SerializeField]public ClearCounter testCounter;

    private Player _mainPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMainPlayer(Player mainPlayer)
    {
        _mainPlayer = mainPlayer;
    }
    

    // Add your game logic here
}