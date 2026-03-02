using UnityEngine;

public class HeroEntity : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _sHead;
    [SerializeField] private SpriteRenderer _sTorso;
    [SerializeField] private SpriteRenderer _sArms;
    [SerializeField] private SpriteRenderer _sHands;
    [SerializeField] private SpriteRenderer _sLegs;
    [SerializeField] private SpriteRenderer _sFeet;
    [SerializeField] private SpriteRenderer _sNeck; // for jewlery and such "amulet of bleebleborp" or smth

    void Start()
    {
        // initialize in game manager later
        HeroData data = new HeroData();

        // keep here
        HeroData.RandomizeHero(this);
    }

    void Update()
    {

    }

    public void SetHead(Sprite s) { _sHead.sprite = s; }
    public void SetTorso(Sprite s) { _sTorso.sprite = s; }
    public void SetArms(Sprite s) { _sArms.sprite = s; }
    public void SetHands(Sprite s) {_sHands.sprite = s; }
    public void SetLegs(Sprite s) { _sLegs.sprite = s; }
    public void SetFeet(Sprite s) { _sFeet.sprite = s; }
    public void SetNeck(Sprite s) { _sNeck.sprite = s; }
}