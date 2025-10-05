using UnityEngine;

public class Drag3DObject : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    public string targetTag = "Jar";
    private bool isOverJar = false;

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;

            // Check if over Jar during drag
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);
            isOverJar = false;
            foreach (var hit in hits)
            {
                if (hit.CompareTag(targetTag))
                {
                    isOverJar = true;
                    break;
                }
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (isOverJar)
        {
            Transform jar = FindNearestJar();
            if (jar != null)
            {
                if (CompareTag("Seeds"))
                {
                    // Move seeds under the jar using the jar reference
                    transform.position = jar.position + Vector3.down * 0.2f; // Underneath
                    transform.parent = jar; // Parent to the jar
                    Debug.Log(gameObject.name + " placed under Jar!");
                }
                else if (CompareTag("Thermometer"))
                {
                    // Stand thermometer upright in cork position
                    transform.position = jar.position + Vector3.up * 0.3f; // Adjust height for cork
                    transform.rotation = Quaternion.Euler(0, 0, 0); // Upright
                    transform.parent = jar;
                    TemperatureSimulator tempSim = GetComponent<TemperatureSimulator>();
                    if (tempSim != null)
                    {
                        tempSim.enabled = true;
                    }
                    Debug.Log(gameObject.name + " inserted into Jar cork!");
                }
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, 0.5f); // Counter height
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position; // Fallback
    }

    Transform FindNearestJar()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag(targetTag))
            {
                return hit.transform;
            }
        }
        return null;
    }
}