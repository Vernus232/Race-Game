using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Text playerMoney;
    void Start()
    {
        playerMoney.text = GlobalPlayerData.money.ToString("00000$");
    }

    public void UpdatePlayerUI()
    {
        playerMoney.text = GlobalPlayerData.money.ToString("00000$");
    }
}
