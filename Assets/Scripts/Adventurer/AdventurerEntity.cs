using UnityEngine;
using UnityEngine.AI;

public class AdventurerEntity : MonoBehaviour, ITradeManager
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

    [Header("References")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshPath _path;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private ItemData _requestedItem;

    void Start()
    {
        // initialize in game manager later
        AdventurerData data = new AdventurerData();

        // keep here
        AdventurerData.RandomizeHero(this);
        _hp = Random.Range(1, 100);
        _walkSpeed = Random.Range(3, 10); // tank vs scout i guess
        _reputation = Random.Range(-5000, 5000);


        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _walkSpeed;

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
        Quaternion q = Quaternion.LookRotation((Camera.main.transform.position - transform.position), transform.up);
        Vector3 eulerAngles = q.eulerAngles;
        transform.localEulerAngles = new Vector3(0, eulerAngles.y, 0);
        if(_agent.hasPath && Vector3.Distance(transform.position, _agent.pathEndPosition) > _stoppingDistance)
        {
            transform.position += (_agent.nextPosition - transform.position).normalized * Time.deltaTime * _walkSpeed;
        }
    }

    public void WalkToTarget(Vector3 location)
    {
        _agent.SetDestination(location);
    }

    public void SetHead(Sprite s) { _sHead.sprite = s; }
    public void SetTorso(Sprite s) { _sTorso.sprite = s; }
    public void SetArms(Sprite s) { _sArms.sprite = s; }
    public void SetHands(Sprite s) {_sHands.sprite = s; }
    public void SetLegs(Sprite s) { _sLegs.sprite = s; }
    public void SetFeet(Sprite s) { _sFeet.sprite = s; }
    public void SetNeck(Sprite s) { _sNeck.sprite = s; }

    public bool HandItem(ItemData item)
    {
        return _requestedItem.Equals(item);
    }

    public bool TradeActive()
    {
        return _requestedItem != null;
    }
}