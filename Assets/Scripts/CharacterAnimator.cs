using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float locomationAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Установить значение speedFloat в аниматоре для выбора State в Blend.Tree (player animator)
        float speedPersent = agent.velocity.magnitude / agent.speed; //текущая скорость / максимальн. скорость
        animator.SetFloat("speedPersent", speedPersent, locomationAnimationSmoothTime, Time.deltaTime);
    }
}
