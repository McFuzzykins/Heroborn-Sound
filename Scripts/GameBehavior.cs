using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public string labelText = "Collect all 3 items and win your freedom!";
    public int maxItems = 3;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public Stack<string> lootStack = new Stack<string>();
    public bool GlassCannon = false;
    public bool JumpBoost = false;
    public bool HPBoost = false;
    public float Timer = -1f;


    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
            Debug.LogFormat("Items: {0}", _itemsCollected);
        }
    }
    private int _playerHP = 3;
    public AudioClip deathClip;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if(_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
                Die();
                
            }
            else
            {
                labelText = "Ouch... that's got to hurt.";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }
    void Die()
    {
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        Debug.Log(_state);
        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
    }
    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel();
            }
        }
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2-100, Screen.height / 2-50, 200, 100), "You Lose..."))
            {
                Utilities.RestartLevel(0);
            }
        }
    }
public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You've got a good chance of fiding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count); 
    }
void Update()
    {
        if (Timer >= 0)
            Timer -= Time.deltaTime;
    }
}
