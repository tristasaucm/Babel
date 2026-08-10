//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Drawing;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemiesChase : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Gameobjetc del player para perseguirlo
    /// </summary>
    [SerializeField] private GameObject Player;
    /// <summary>
    /// Boost de velocidad del enemigo
    /// </summary>
    [SerializeField] private float BostSpeed;
    /// <summary>
    /// GameObject del collider
    /// </summary>
    [SerializeField] private GameObject Collider;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    /// <summary>
    /// componente colllider
    /// </summary>
    private Collider _collider;
    /// <summary>
    /// Movimiento de los enemigos
    /// </summary>
    private EnemiesMovement _movement;
    /// <summary>
    /// bool que indica si detecta al player
    /// </summary>
    private bool _playerDetected;
    /// <summary>
    /// bool que indica si esta persiguiendo al jugador
    /// </summary>
    private bool _chassing;
    /// <summary>
    /// componente del rigidbody
    /// </summary>
    private Rigidbody2D _rb;
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        //Inicializacion
        _chassing = false;
        _playerDetected = false;
        _collider = Collider.GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.Log("No hay Collider del enemigo por tanto no funcionara");
        }
        _movement = this.gameObject.GetComponent<EnemiesMovement>();
        if(_movement == null)
        {
            Debug.Log("No hay movimiento del enemigo por tanto no funcionara");
        }
        _chassing = false;
        _rb = this.GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.Log("No hay rigidbody por tanto no se puede mover el enemigo");
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        _playerDetected = _collider.GetPlayerDetected();
        //si ha detectado al player
        if (_playerDetected&& ! _chassing)
        {
            //Debug.Log("detectado");
            //Desactiva el patron de movimiento 
            _movement.DesactiveActivate(false);
            _chassing = true;
           
        }
       else if (_playerDetected && _chassing)
        {
            //Seguir al player
            //apuntamos al player
           // Debug.Log("persiguiendo");
            Vector3 point = Player.transform.position;
            Vector2 dir = point - this.transform.position;
            //Solucionar cuando mas cerca estas mas lento va 
            _rb.linearVelocity = dir * BostSpeed;

        }
       else  if (!_playerDetected && _chassing)
        {
           // Debug.Log("saliendo");
            //si el player se sale volvemos a activar el movimiento
            _movement.DesactiveActivate(true);
            _chassing = false;
            _rb.linearVelocity = new Vector2(0, 0);
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    #endregion   

} // class EnemiesChase 
// namespace
