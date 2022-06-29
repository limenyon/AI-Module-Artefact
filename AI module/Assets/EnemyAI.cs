using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//The script respinsible for taking all of the branches and deciding what to do with them
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float regenerationSpeed;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float startingHealth;
    [SerializeField] private float chasingRange;
    [SerializeField] private float attackingRange;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Wall[] availableWalls;
    private float currentHealth;
    private Material enemyMaterial;
    private Transform coverSpot;
    private Branch topBranch;
    private NavMeshAgent agent;

    //when the program starts we get the enemy agent
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyMaterial = GetComponent<MeshRenderer>().material;
    }

    //initialising the tree and the starting health of the enemy
    private void Start()
    {
        currentHealth = startingHealth;
        ConstructBehaviourTree();
    }

    //the behaviour tree must start with the bottom branch and move on up as the top nodes depend on the bottom nodes
    private void ConstructBehaviourTree()
    {
        CoverAvailablityCheck coverAvailabilityCheck = new CoverAvailablityCheck(availableWalls, playerTransform, this);
        TakeCover takeCover = new TakeCover(agent, this);
        Health health = new Health(this, lowHealthThreshold);
        Covered covered = new Covered(playerTransform, transform);
        Chase chase = new Chase(playerTransform, agent, this);
        Range chaseRange = new Range(chasingRange, playerTransform, transform);
        Range attackRange = new Range(attackingRange, playerTransform, transform);
        Attack attack = new Attack(agent, this, playerTransform);


        Order chaseOrder = new Order(new List<Branch> { chaseRange, chase});
        Order attackOrder = new Order(new List<Branch> { attackRange, attack });
        Order takeCoverOrder = new Order(new List<Branch> { coverAvailabilityCheck, takeCover });
        Selector coverAvailabilityCheckSelector = new Selector(new List<Branch> { takeCoverOrder, chase });
        Selector tryToTakeCoverSelector = new Selector(new List<Branch> { covered, coverAvailabilityCheckSelector });
        Order coverOrder = new Order(new List<Branch> { health, tryToTakeCoverSelector});
        topBranch = new Selector(new List<Branch> { coverOrder, attackOrder, chaseOrder });
    }

    public Transform GetBestCover()
    {
        return coverSpot;
    }

    public void SetCover(Transform bestCoverSpot)
    {
        this.coverSpot = bestCoverSpot;
    }

    public float GetCurrentHealth()
    {
        return currentHealth; 
    }
    public void SetCurrentHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, startingHealth);
    }
    private void Update()
    {
        //add a counter so this doesn't get called each frame, very wasteful
        topBranch.Observe();
        if(topBranch.branchState == BranchState.failure)
        {
            SetColour(Color.magenta);
        }
        currentHealth += Time.deltaTime * regenerationSpeed;
    }
    //whenever the enemy is clicked remove this much health
    private void OnMouseDown()
    {
        currentHealth = -10.0f;
    }
    //changing colours to identify states
    public void SetColour(Color colour)
    {
        enemyMaterial.color = colour;
    }
}
