using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
    [SerializeField] private EntityHealth _health;
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _image.fillAmount = _health.health / _health.maxHealth;
    }
}
