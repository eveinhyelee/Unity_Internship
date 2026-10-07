using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : BaseController
{
    private float speed;
    private int maxHealth;
    private int rewardMaxExp;
    private Rigidbody2D target;
    private Rigidbody2D rigidbody;
    public float detectionRadius = 4; // 감지 반경
    public float hitCoolTime = 0.5f; //쿨타임

    private bool isLive;
    private bool isHit;
    private bool isTargetDetected = false;

    private Vector2 patrolDirection;
    private float patrolChangeTime = 2f;
    private float patrolTimer = 0f;

    [SerializeField] public Slider slider;
    [SerializeField] private TextMeshProUGUI hpTxt;

    public DropItem dropItem;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init() //초기화 오류개선 함수
    {
        GetComponent<Collider2D>().enabled = true;
        
        if (data != null)
        {
            speed = data.MoveSpeed;
            curHP = data.MaxHP;
            maxHealth = data.MaxHP;
            rewardMaxExp = data.MaxExp;
        }

        rigidbody = GetComponent<Rigidbody2D>();
        hpTxt.text = $"{curHP}/{data.MaxHP}";

        isLive = true;
    }

    protected override void Start()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        if (target == null) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        isTargetDetected = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                target = hit.attachedRigidbody;
                isTargetDetected = true;
                break;
            }
        }
        if (isTargetDetected)
        {
            lookDirection = (target.position - rigidbody.position).normalized;
            movementDirection = lookDirection; // 추적
        }
        else
        {
            Patrol();
        }

        UpdateHPBar(data);
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (data == null) return;

        if (isTargetDetected)
        {
            Vector2 direction = target.position - rigidbody.position;
            Vector2 nextdirection = direction.normalized * data.MoveSpeed * Time.fixedDeltaTime;
            rigidbody.MovePosition(rigidbody.position + nextdirection);
        }
        else
        {
            rigidbody.velocity = movementDirection * data.MoveSpeed;
        }
    }

    public void UpdateHPBar(Data data) //HPBar업데이트
    {
        slider.value = (float)curHP / data.MaxHP;
        if (curHP <= 0)
        {
            curHP = 0;
            hpTxt.text = $"{curHP}/{data.MaxHP}";
        }
        else
        {
            hpTxt.text = $"{curHP}/{data.MaxHP}";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLive) return;

        if (collision.CompareTag("Bullet"))
        {
            curHP -= (int)collision.GetComponent<Bullet>().damage;
            if (curHP > 0)
            {
                animationHandler?.Hit();
            }
            else
            {
                OnDead();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLive) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(data.Attack); // 몬스터 공격력만큼 데미지
                Debug.Log("플레이어와 충돌 발생!");
            }
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolChangeTime)
        {
            patrolTimer = 0f;
            // -1~1 사이 랜덤 방향 생성
            patrolDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        movementDirection = patrolDirection; // 순찰 방향으로 이동
        lookDirection = patrolDirection; // 회전할 때도 동일 방향을 바라보도록 설정
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnDead()
    {
        if (!isLive) return;
        isLive = false;

        GetComponent<Collider2D>().enabled = false; //충돌을 무시하기 위함.

        animationHandler.Dead();
        UpdateHPBar(data);

        if (data.DropItem != null && data.DropItem.Length > 0) //아이템 드롭
        {
            int randomIndex = Random.Range(0, data.DropItem.Length);
            int itemID = data.DropItem[randomIndex];
            ItemDropManager.instance.DropItem(itemID, transform.position);
        }

        Invoke("SetActiveFalse", 2f);

        GameManager.instance.player.GetComponent<PlayerController>().GetExp(rewardMaxExp);
    }

    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}