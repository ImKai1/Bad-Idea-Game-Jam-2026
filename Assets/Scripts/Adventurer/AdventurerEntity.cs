using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AdventurerEntity : MonoBehaviour, ITradeManager, IInteractable
{
    [Header("Decision Variables")] // random values here will let the hero decide what they want to buy
    [SerializeField] private int _hp;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private int _reputation;

    //[Header("Sprites")]
    //[SerializeField] private SpriteRenderer _sHead;
    //[SerializeField] private SpriteRenderer _sTorso;
    //[SerializeField] private SpriteRenderer _sArms;
    //[SerializeField] private SpriteRenderer _sHands;
    //[SerializeField] private SpriteRenderer _sLegs;
    //[SerializeField] private SpriteRenderer _sFeet;
    //[SerializeField] private SpriteRenderer _sNeck; // for jewlery and such "amulet of bleebleborp" or smth

    [Header("References")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshPath _path;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private List<ItemData> _itemsWanted = new List<ItemData>();
    [SerializeField] private QuestData _currentQuest;
    [SerializeField] private Transform _walkTarget;

    void Start()
    {
        // initialize in game manager later
        //AdventurerData data = new AdventurerData();

        // keep here
        //AdventurerData.RandomizeHero(this);
        _hp = Random.Range(1, 100);
        _walkSpeed = Random.Range(1.0f, 2.5f); // tank vs scout i guess
        _reputation = Random.Range(-5000, 5000);


        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _walkSpeed;
        _agent.angularSpeed = _lookSpeed;
        _agent.stoppingDistance = _stoppingDistance;

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
        //Quaternion q = Quaternion.LookRotation((Camera.main.transform.position - transform.position), transform.up);
        //Vector3 eulerAngles = q.eulerAngles;
        //transform.localEulerAngles = new Vector3(0, eulerAngles.y, 0);
        if(_agent.hasPath && Vector3.Distance(transform.position, _agent.pathEndPosition) > _stoppingDistance)
        {
            transform.position += (_agent.nextPosition - transform.position).normalized * Time.deltaTime * _walkSpeed;
        }

        WalkToTarget(_walkTarget.position);
    }

    public void WalkToTarget(Vector3 location)
    {
        _agent.SetDestination(location);
    }

    //public void SetHead(Sprite s) { _sHead.sprite = s; }
    //public void SetTorso(Sprite s) { _sTorso.sprite = s; }
    //public void SetArms(Sprite s) { _sArms.sprite = s; }
    //public void SetHands(Sprite s) {_sHands.sprite = s; }
    //public void SetLegs(Sprite s) { _sLegs.sprite = s; }
    //public void SetFeet(Sprite s) { _sFeet.sprite = s; }
    //public void SetNeck(Sprite s) { _sNeck.sprite = s; }

    public bool HandItem(ItemData item)
    {
        return _itemsWanted.Remove(item);
    }

    public void AddWantedItem(ItemData item)
    {
        _itemsWanted.Add(item);
    }

    public bool TradeActive()
    {
        return _itemsWanted.Count > 0;
    }

    public void Interact(Player player)
    {
        // idk, put stuff here
    }

    public string GetInteractionText(Player player) { return "Talk to Adventurer"; }
    public Vector3 GetInteractionPosition(Player player) { return Vector3.zero; }
}