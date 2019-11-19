using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Преобразовать 
 */
public class ConvertToRegularMesh : MonoBehaviour
{
    [ContextMenu("Convert to regular mesh")]
    void Convert()
    {
        //TODO: просмотреть Unity документацию на поclедующие типы 
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>(); //рендерит анимацию bones
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>(); //берет geometry meshFilter и уст. в положение опред. Transfer
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>(); //берет mesh asset и передает его meshRender для вывода на экран

        //передаем данные из skinnedMeshRenderer и затем уничтожаем его
        meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
        meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;

        DestroyImmediate(skinnedMeshRenderer); //удаляет немедленно, не ждат окончания кадра
        DestroyImmediate(this);
    }
}
