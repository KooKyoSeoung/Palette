using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectData
{
    public static int SELECT_S = 0;
    public static int SELECT_N = 0;
    public static int SELECT_F = 0;
    public static int SELECT_T = 0;

    public static PlayerName NAME = PlayerName.Hati;

    public static void InitData()
    {
        SELECT_S = 0;
        SELECT_N = 0;
        SELECT_F = 0;
        SELECT_T = 0;

        NAME = PlayerName.Hati;
    }

    public static void SetPlayerName()
    {
        string snResult = (SELECT_S >= SELECT_N) ? "S" : "N";
        string ftResult = (SELECT_F >= SELECT_T) ? "F" : "T";

        switch (snResult + ftResult)
        {
            case "SF":
                NAME = PlayerName.Purum;
                break;
            case "ST":
                NAME = PlayerName.Rubi;
                break;
            case "NF":
                NAME = PlayerName.Clover;
                break;
            case "NT":
                NAME = PlayerName.Thunder;
                break;
            default:
                NAME = PlayerName.Hati;
                break;
        }
    }
}

public enum PlayerName
{
    Hati,
    Rubi,
    Purum,
    Thunder,
    Clover
}