using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class HeroData
{
    [Header("Visual Data")] // what the fella looks like
    [SerializeField] private static List<Sprite> _sHeadArr = new List<Sprite>();
    [SerializeField] private static List<Sprite> _sTorsoArr = new List<Sprite>();
    [SerializeField] private static List<Sprite> _sArmArr = new List<Sprite>();
    [SerializeField] private static List<Sprite> _sHandArr = new List<Sprite>();
    [SerializeField] private static List<Sprite> _sLegArr = new List<Sprite>();
    [SerializeField] private static List<Sprite> _sFootArr = new List<Sprite>();
    [SerializeField] private static List<Sprite> _sNeckArr = new List<Sprite>();

    static HeroData()
    {
        InitilizeArrays();
    }

    private static void InitilizeArrays()
    {
        // Load all sprites from Resources/Sprites folder
        Sprite[] heads = Resources.LoadAll<Sprite>("Heads");
        Sprite[] torsos = Resources.LoadAll<Sprite>("Torsos");
        Sprite[] arms = Resources.LoadAll<Sprite>("Arms");
        Sprite[] hands = Resources.LoadAll<Sprite>("Hands");
        Sprite[] legs = Resources.LoadAll<Sprite>("Legs");
        Sprite[] feet = Resources.LoadAll<Sprite>("Feet");
        Sprite[] necks = Resources.LoadAll<Sprite>("Necks");

        Debug.Log(heads.Length);

        if(heads.Length > 0)
            _sHeadArr.AddRange(heads);
        if(torsos.Length > 0)
            _sTorsoArr.AddRange(torsos);
        if(arms.Length > 0)
            _sArmArr.AddRange(arms);
        if(hands.Length > 0)
            _sHandArr.AddRange(hands);
        if(legs.Length > 0)
            _sLegArr.AddRange(legs);
        if(feet.Length > 0)
            _sFootArr.AddRange(feet);
        if(necks.Length > 0)
            _sNeckArr.AddRange(necks);

        // We don't REALLY need this error checking rn, do we?
        //if (heads.Length > 0)
        //{
        //    _sHeadArr.AddRange(heads);
        //    Debug.Log($"Loaded {_sHeadArr.Count} sprites.");
        //}
        //else
        //    Debug.LogWarning("No sprites found in Resources.");
    }

    public static void RandomizeHero(HeroEntity h)
    {
        h.SetHead(PickRandomSprite(_sHeadArr));
        h.SetTorso(PickRandomSprite(_sTorsoArr));
        h.SetArms(PickRandomSprite(_sArmArr));
        h.SetHands(PickRandomSprite(_sHandArr));
        h.SetLegs(PickRandomSprite(_sLegArr));
        h.SetFeet(PickRandomSprite(_sFootArr));
        h.SetNeck(PickRandomSprite(_sNeckArr));
    }

    private static Sprite PickRandomSprite(List<Sprite> arr)
    {
        int i = UnityEngine.Random.Range(0, arr.Count);
        if (arr[i] != null)
            return arr[i];
        return null;
    }
}
