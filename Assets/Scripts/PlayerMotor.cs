using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target; //target to follow
    NavMeshAgent agent; //references to our target

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position); //преследовать enemy
            FaceTarget(); //поворачиваться лицом enemy
        }
    }

    /*
     * Движение к выбранному объекту
     * 
     */
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point); //устанавливаем поинт пункта назначения для перемещения (поиска пути)
    }

    /*
     * Следовать за 
     * 
     * Interactable newTarget - int-ble enemy полученный при рейкастинге
     */
    public void FollowTarget(Interactable newTarget)
    {
        //соблюдать дистанцию от player до enemy
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    /*
     * Прекратить следовать за 
     */
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    /*
     * Player поворачивается вслед за перемещением enemy
     */
    void FaceTarget ()
    {
        //получить вектор 
        Vector3 direction = (target.position - transform.position).normalized;
        //поворот в указ направлении
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        //интерполяция, с уклоном в float t
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
