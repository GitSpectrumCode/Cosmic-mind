using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool isOverShadow = false;
    private GameObject shadowTarget;

    public Transform sombraFinal;
    public float distanciaParaEncaixe = 0.5f;

    public bool estaPosicionadaCorretamente = false;

    public AudioClip somCorreto;
    public AudioClip somIncorreto;
    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (isOverShadow)
        {
            float distancia = Vector3.Distance(transform.position, sombraFinal.position);

            if (distancia <= distanciaParaEncaixe)
            {
                transform.position = sombraFinal.position;
                estaPosicionadaCorretamente = true;
                TocarSom(somCorreto);
            }
            else
            {
                transform.position = startPosition;
                estaPosicionadaCorretamente = false;
                TocarSom(somIncorreto);
            }
        }
        else
        {
            transform.position = startPosition;
            estaPosicionadaCorretamente = false;
            TocarSom(somIncorreto);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sombra" && collision.gameObject.name == this.gameObject.name.Replace("Figura", "Sombra"))
        {
            isOverShadow = true;
            shadowTarget = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == shadowTarget)
        {
            isOverShadow = false;
            shadowTarget = null;
        }
    }

    void TocarSom(AudioClip som)
    {
        if (som != null && audioSource != null)
        {
            audioSource.PlayOneShot(som);
        }
    }

    public void ResetarPosicao()
    {
        transform.position = startPosition;
        estaPosicionadaCorretamente = false;
    }
}
