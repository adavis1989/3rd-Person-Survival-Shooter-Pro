using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bloodSplatPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shot();
    }
    private void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 9 | 1 << 0))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);

                Health health = hitInfo.collider.GetComponent<Health>();
                
                if (health != null)
                {

                    Instantiate(_bloodSplatPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    health.Damage(10); 
                }
            }

        }
    }
}
