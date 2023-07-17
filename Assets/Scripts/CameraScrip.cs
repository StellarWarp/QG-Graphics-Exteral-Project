using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
// [ExecuteInEditMode]
public class CameraScript : MonoBehaviour
{
    public Transform centerObject;
    public Vector3 center;

    //private bool centerRotation = true;
    public bool horizontalCamera = true;
    public bool orbitCamera = false;
    public bool freeMove = false;
    public bool editPosition = false;

    public float rotateSpeed;
    public float rotateLerpSpeed;

    public float moveSpeed;
    public float moveLerpSpeed;

    public float zoomSpeed;

    [SerializeField, HideInInspector] Vector3 targetPosition;
    [SerializeField, HideInInspector] Quaternion targetRotation;
    [SerializeField, HideInInspector] float targetDistance;

    public float minDistance = 1f;
    public float maxDistance = 100f;

    void OrbitCameraUpdate(Vector2 rotateChange, Vector3 moveChange, float zoomChange)
    {
        if (horizontalCamera)
        {
            targetRotation = Quaternion.Euler(0, rotateChange.x, 0) * targetRotation *
                             Quaternion.Euler(-rotateChange.y, 0, 0);
        }
        else
        {
            targetRotation = targetRotation * Quaternion.Euler(-rotateChange.y, rotateChange.x, 0);
        }
        Quaternion upAxisRotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
        center += upAxisRotation * moveChange;

        // 鼠标滚轮拉伸
        targetDistance -= zoomChange;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        targetPosition = center + targetRotation * (Vector3.back * targetDistance);
    }

    void FreeCameraUpdate(Vector2 rotateChange, Vector3 moveChange, float zoomChange)
    {
        if (horizontalCamera)
        {
            targetRotation = Quaternion.Euler(0, rotateChange.x, 0) * targetRotation *
                             Quaternion.Euler(-rotateChange.y, 0, 0);
        }
        else
        {
            targetRotation = targetRotation * Quaternion.Euler(-rotateChange.y, rotateChange.x, 0);
        }

        targetPosition += transform.rotation * moveChange;
    }

    private void OnEnable()
    {
        targetRotation = transform.rotation;
        targetPosition = transform.position;
        center = centerObject.position;
        targetDistance = (transform.position - center).magnitude;
    }

    void Update()
    {
        if (editPosition) EditorUpdate();

        Vector2 rotateChange = PlayerInput.rotate * (rotateSpeed * Time.deltaTime);
        Vector3 moveChange = PlayerInput.movement * (moveSpeed * Time.deltaTime);
        float zoomChange = PlayerInput.zoom * (zoomSpeed * Time.deltaTime);

        if(orbitCamera)
        {
            OrbitCameraUpdate(rotateChange, moveChange, zoomChange);
        }
        else
        {
            FreeCameraUpdate(rotateChange, moveChange, zoomChange);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateLerpSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveLerpSpeed * Time.deltaTime);
    }

    public void EditorUpdate()
    {
#if UNITY_EDITOR
        // if (editPosition)
        // {
        //     var dir = center - transform.position;
        //     distance = dir.magnitude;
        //     rotation = Quaternion.FromToRotation(Vector3.forward, dir);
        //     if (horizontalCamera)
        //     {
        //         var angle = rotation.eulerAngles;
        //         angle.z = 0;
        //         rotation.eulerAngles = angle;
        //     }
        //
        //     if (centerObject) targetCenter = centerObject.position;
        //     targetRotation = rotation;
        //     targetDistance = distance;
        // }
#endif
    }
}