using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    /*
     * Метод, доступный для переопределения
     */
    public virtual void Interacte()
    {
        //This method is mean to be overwritten
        Debug.Log("Interracting with " + transform.name);
    }

    private void Update()
    {
        //если выбран объект enemy
        if (isFocus && !hasInteracted)
        {
            //расстояние между enemy & player
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            //если дистанция "между" меньше радиуса
            if (distance <= radius)
            {
                Interacte();
                hasInteracted = true;

            }
        }
    }

    /*
     * Transform playerTransform - player transform, focused in curent enemy
     */
    public void onFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    
    //очистить свойства при смене выбранного player объекта
    public void onDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    /*
     * отрабатывает при выборе объекта
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
