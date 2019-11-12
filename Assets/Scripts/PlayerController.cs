using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] //обязателен компонент скрипта
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask; //масква слоя для определения коллайдеров при рейкастинге
    public Interactable focus;

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //главная камера
        motor = GetComponent<PlayerMotor>(); //получаем ссылку на др скрипт
    }

    // Update is called once per frame
    void Update()
    {
        //Нажата левая кнопка мыши
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //текущая позиция мыши в пиксельных координатах
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //move our player what we hit
                motor.MoveToPoint(hit.point); //передваем поинт пересечения луча с коллайдером Ground маски слоя

                //Stop focusing any objects
                RemoveFocus();
            }
        }

        //Нажата правая кнопка мыши
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //текущая позиция мыши в пиксельных координатах
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Check если есть пересечение с enemy (interactable)
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }
    
    /*
     * Проверка объекта фокуса
     * Вызов ф-ции следования за enemy
     * 
     * Interactable newFocus - int-ble enemy полученный при рейкастинге
     */
    void SetFocus (Interactable newFocus)
    {
        if (newFocus != focus) //если выбран другой объект
        {
            if (focus != null)
                focus.onDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus); //передать inter-le enemy для преследования в player
        }

        newFocus.onFocused(transform); //передать параметры player в int-ble enemy
    }

    /*
     * Прекратить следование
     */
    void RemoveFocus()
    {
        if (focus != null)
            focus.onDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}
