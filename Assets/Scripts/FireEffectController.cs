using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectController : MonoBehaviour
{
    [SerializeField] private float SpeedSize;
    [SerializeField] private LineRenderer _line;
    private float Zcon;
    private void LateUpdate()
    {
        _line.transform.localScale = new Vector3(_line.transform.localScale.x, _line.transform.localScale.y, Zcon);
        Zcon += SpeedSize;
        if (_line.transform.localScale.z > 600) Destroy(this.gameObject);
    }
}
