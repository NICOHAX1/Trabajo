using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.UIElements.Cursor;

public class MovimientoJugador : MonoBehaviour {
    public float velocidad; 
    public float salto;
    public float velocidadRotacion;
    
    private Rigidbody fisicas;
    private bool enSuelo = true;


    // Start es llamado antes del primer frame (una sola vez)
    void Start() {

        fisicas = GetComponent<Rigidbody>();
    }

    // Update es llamado una vez por frame
    void Update() {
       
        //movimiento
        var horizontal = Input.GetAxis("Horizontal"); 
        var vertical = Input.GetAxis("Vertical");
       
        var movimiento = new Vector3(horizontal, 0, vertical).normalized *
                                (Time.deltaTime * velocidad);
        transform.Translate(movimiento);

        //movimiento camara
        var mouseX = Input.GetAxis("Mouse X");
        var rotacion = new Vector3(0, mouseX, 0) * (Time.deltaTime * velocidadRotacion);
        transform.Rotate(rotacion);


        //salto
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            fisicas.AddForce(new Vector3(0, salto, 0), ForceMode.Impulse);
            enSuelo = false; // Cambia el estado a "en el aire"
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta si el personaje está en el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true; // Cambia el estado a "en el suelo"
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Detecta si el personaje ha dejado de tocar el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false; // Cambia el estado a "en el aire"
        }



        
    }
}