using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    public Image[] hearts;
    
    public int maxHealth = 10;
    private int health;

    private Player player;
    
    void Start()
    {
        player = Player.Instance;
    }

    void Update()
    {
        health = (int)player.PlayerHP;
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;  // HPがある分フルハート
            }
            else
            {
                hearts[i].sprite = emptyHeart; // HPがない分空のハート
            }

            // maxHealthに達していないハートは非表示
            hearts[i].enabled = (i < maxHealth);
        }
    }
}