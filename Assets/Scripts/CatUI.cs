using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CatUI : MonoBehaviour
{
    private Text text;
    private Cat player;

    // Use this for initialization
    private void Start()
    {
        text = GetComponent<Text>();
        player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = "Lives: " + player.Lives + "\nHealth: " + player.Health;
    }
}
