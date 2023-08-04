using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class CollisionAreaScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtCollisionInfo;
    [SerializeField] GameObject panelInfo;
    [SerializeField]  GameObject objetoSeleccionado;
    [SerializeField] AudioClip pisadaPasto1;
    [SerializeField] AudioClip pisadaPasto2;
    [SerializeField] AudioClip pisadaNormal1;
    [SerializeField] AudioClip pisadaNormal2;
    [SerializeField] FirstPersonController fps;
    float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        panelInfo.SetActive(false);
        txtCollisionInfo.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            fps.transform.position += Vector3.up;
            tiempo = 0;
            if (objetoSeleccionado)
            {
                objetoSeleccionado.SetActive(false);
                fps.m_FootstepSounds[0] = pisadaPasto1;
                fps.m_FootstepSounds[1] = pisadaPasto2;
            }
            else
            {
                fps.m_FootstepSounds[0] = pisadaNormal1;
                fps.m_FootstepSounds[1] = pisadaNormal2;
            }
        }
        tiempo += Time.deltaTime;
        txtCollisionInfo.text = tiempo.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        InteractiveObjectScript interactive = collision.gameObject.GetComponent<InteractiveObjectScript>();
        if (interactive)
        {
            objetoSeleccionado = collision.gameObject;
            panelInfo.SetActive(true);
            txtCollisionInfo.text = interactive.message;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        panelInfo.SetActive(false);
        objetoSeleccionado = null;
    }
}
