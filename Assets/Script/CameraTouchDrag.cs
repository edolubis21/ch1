using UnityEngine;

public class CameraTouchDrag : MonoBehaviour
{
    private Vector2 touchStart;
    private Camera cam;

    private Vector3 camStartPos;
    public float maxDragDistance = 2f;  // maksimal geseran kamera dalam world unit
    public float bounceSpeed = 5f;      // kecepatan bounce kembali ke batas

    public bool isDraggingEnabled = true; // ✅ Tambahkan flag ini

    void Start()
    {
        cam = Camera.main;
        camStartPos = cam.transform.position;
    }

    void Update()
    {
        if (!isDraggingEnabled) return; // ✅ Cegah drag saat modal aktif

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = cam.ScreenToWorldPoint(touch.position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPosition = cam.ScreenToWorldPoint(touch.position);
                Vector2 direction = touchStart - touchPosition;

                Vector3 desiredPos = cam.transform.position + new Vector3(direction.x, direction.y, 0);
                Vector3 offset = desiredPos - camStartPos;

                if (offset.magnitude > maxDragDistance)
                {
                    offset = offset.normalized * maxDragDistance;
                }

                cam.transform.position = camStartPos + offset;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                StartCoroutine(BounceBack());
            }
        }
    }

    System.Collections.IEnumerator BounceBack()
    {
        while (Vector3.Distance(cam.transform.position, camStartPos) > 0.01f)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camStartPos, bounceSpeed * Time.deltaTime);
            yield return null;
        }
        cam.transform.position = camStartPos;
    }
}
