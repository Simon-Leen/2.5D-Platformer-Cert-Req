using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _coinsText;

    public void UpdateCoinsDisplay(int coins)
    {
        _coinsText.text = "Coins: " + coins;
    }
}
