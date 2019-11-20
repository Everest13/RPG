using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float pitch = 2f;
    public float zoomSpeed = 4f;

    public float minZoom = 5f; //интервал ограничения зума
    public float maxZoom = 15f;

    public float yawSpeed = 100f; //скорость вращения?

    private float currentZoom = 10f;
    private float currentYaw = 0f;

    private void Update()
    {
        //рассчитываем координаты положения камеры
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; //увеличение, уменьшение скролом колесика
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); //ограничение зума, считает зум

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime; //получает значение вращения через a/d buttons
    }

    //после всех update, устанавливаем положение камеры
    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;  //следовать за player
        transform.LookAt(target.position + Vector3.up * pitch); // - хз, пока не понятно

        transform.RotateAround(target.position, Vector3.up, currentYaw); //ИЗМЕНЯЕТ угол вращения камеры 
    }
}
