using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CameraLook : MonoBehaviour
{
    [SerializeField]GameObject[] _scanStartPos;
    [SerializeField] VisualEffect _effect;
    float timer = 0;
    private void Update()
    {
        transform.localEulerAngles = new(Input.GetAxisRaw("Mouse Y") * -1 + transform.eulerAngles.x, 0, 0);
    }
    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timer <= 0)
        {
            timer = 0.1f;
            List<Vector3> list = new();
            foreach (var item in _scanStartPos)
            {
                list.Add(item.transform.position);
            }
            StartCoroutine(Scan(list, transform.forward));
        }
    }

    IEnumerator Scan(List<Vector3> posArr, Vector3 forward)
    {
        foreach (Vector3 go in posArr)
        {
            Physics.Raycast(go, forward, out RaycastHit hit);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    var enemyAi = hit.collider.gameObject.GetComponent<EnemyAi>();
                    enemyAi.DisplayParticle();
                }
                else
                {
                    _effect.SetVector3("SetPosition", hit.point);
                    _effect.SendEvent("OnPlay");
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
