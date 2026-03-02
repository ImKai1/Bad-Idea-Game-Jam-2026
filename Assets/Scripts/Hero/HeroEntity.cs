using UnityEngine;

public class HeroEntity : MonoBehaviour
{
    [Header("Decision Variables")] // random values here will let the hero decide what they want to buy
    [SerializeField] private int _hp;
    [SerializeField] private int _walkSpeed;
    [SerializeField] private int _reputation;

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
        _hp = Random.Range(1, 100);
        _walkSpeed = Random.Range(3, 10); // tank vs scout i guess
        _reputation = Random.Range(-5000, 5000);

        // REPUTATION RANKINGS //
        /*
         * 5000     -   Super Awesome Dude
         * 4000     -   Honorable
         * 3000     -   Middle Hero
         * 2000     -   Liked
         * 1000     -   Nice
         * 0        -   Neutral
         * -1000    -   Unkind
         * -2000    -   Disliked
         * -3000    -   Scoundrel
         * -4000    -   Dishonorable
         * -5000    -   Super Evil
        */
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