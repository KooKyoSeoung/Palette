using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image[] healthUI = new Image[5];
    [SerializeField] private Sprite[] healthSprite = new Sprite[4];
    [SerializeField] private Sprite emptySprite;
    
    public void DrawUI(int player, int health)
    {
        for (int i = 0; i < healthUI.Length; i++)
        {
            if (i < health)
                healthUI[i].sprite = healthSprite[player];
            else
                healthUI[i].sprite = emptySprite;
        }
    }
}
