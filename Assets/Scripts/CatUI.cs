using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CatUI : MonoBehaviour
{
    public Text text;
   
    // Update is called once per frame
    private void Update()
    {
        text.text = "Lives: " + GameManager.Instance.Player.Lives + "\nHealth: " + GameManager.Instance.Player.Health;
    }
}
