using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using GreenRoom.Utils;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;

    #region Roaming Variables
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;

    private float roamingTimer;
    private Vector3 roamPosition;

    private float roamingSpeed;
    #endregion

    #region Chasing Variables
    [SerializeField] private bool isChasingEnemy = false;
    [SerializeField] private float chasingDistance = 4f;
    [SerializeField] private float chasingSpeedMultiplier = 2f;

    private float chasingSpeed;
    #endregion

    #region Attacking Variables
    [SerializeField] private bool isAttackingEnemy = false;
    [SerializeField] private float attackingDistance = 2f;
    [SerializeField] private float attackRate = 2f;
    private float nextAttackTime = 0f;

    public event EventHandler OnEnemyAttack;
    #endregion

    [SerializeField] private float waitTime = 10f;
    private bool isProvoked = false;
    private float lastAttackTime;
    private bool hasCoroutineStarted = false;

    private NavMeshAgent navMeshAgent;
    private State currentState;

    private Vector3 startingPosition;

    private float nextCheckDirectionTime = 0f;
    private float checkDirectionDuration = 0.1f;
    private Vector3 lastPosition;

    public bool IsRunning
    {
        get
        {
            return navMeshAgent.velocity != Vector3.zero;
        }
    }

    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        currentState = startingState;

        roamingSpeed = navMeshAgent.speed;
        chasingSpeed = navMeshAgent.speed * chasingSpeedMultiplier;
    }

    private void Start()
    {
        lastAttackTime = Time.time;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
        if (Time.time - lastAttackTime > waitTime && !hasCoroutineStarted)
        {
            StartCoroutine(ShowMessageAndReturnToMenu());
            hasCoroutineStarted = true;
        }
    }

    public void SetDeathState()
    {
        navMeshAgent.ResetPath();
        currentState = State.Death;
    }

    private void StateHandler()
    {
        switch (currentState)
        {
            case State.Roaming:
                roamingTimer -= Time.deltaTime;
                if (roamingTimer < 0)
                {
                    Roaming();
                    roamingTimer = roamingTimerMax;
                }
                CheckCurrentState();
                break;
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
            case State.Death:
                DeathState();
                break;
            default:
            case State.Idle:
                break;
        }
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Roaming;

        if (isChasingEnemy && isProvoked)
        {
            if (distanceToPlayer <= chasingDistance)
            {
                newState = State.Chasing;
            }
            else
            {
                isProvoked = false;
            }
        }

        if (isAttackingEnemy && isProvoked)
        {
            if (distanceToPlayer <= attackingDistance)
            {
                newState = State.Attacking;
            }
        }

        if (newState != currentState)
        {
            if (newState == State.Chasing)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.speed = chasingSpeed;
            }
            else if (newState == State.Roaming)
            {
                roamingTimer = 0f;
                navMeshAgent.speed = roamingSpeed;
            }
            else if (newState == State.Attacking)
            {
                navMeshAgent.ResetPath();
            }
            currentState = newState;
        }
    }

    #region State Targets
    private void Roaming()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
    }

    private void ChasingTarget()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    private void AttackingTarget()
    {
        if (Time.time > nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            nextAttackTime = Time.time + attackRate;

            lastAttackTime = Time.time;
            hasCoroutineStarted = false;
        }
    }

    private void DeathState()
    {
        SceneTransitionManager.Instance.LoadMenuWithDelay(4f);
    }

    #endregion

    private void MovementDirectionHandler()
    {
        if (Time.time > nextCheckDirectionTime)
        {
            if (IsRunning)
            {
                ChangeFasingDirection(lastPosition, transform.position);
            }
            else if (currentState == State.Attacking)
            {
                ChangeFasingDirection(transform.position, Player.Instance.transform.position);
            }
            lastPosition = transform.position;
            nextCheckDirectionTime = Time.time + checkDirectionDuration;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    public float GetRoamingAnimationSpeed()
    {
        return navMeshAgent.speed / roamingSpeed;
    }

    private void ChangeFasingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    public void Provoke()
    {
        isProvoked = true;
    }

    private IEnumerator ShowMessageAndReturnToMenu()
    {
        Debug.Log("Enemy unattacked for " + waitTime + " seconds");

        // Сообщение 1: ожидание
        ChatManager.Instance.AddMinMessage("Enemy unattacked for " + waitTime.ToString() + " seconds");

        yield return new WaitForSeconds(2);

        // Сообщение 2: победа
        ChatManager.Instance.AddMaxMessage("You're a WINNER! Press Escape");

        // Ждём нажатия Escape
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            yield return null;
        }

        SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.SceneName.Menu);
    }

}