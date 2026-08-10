//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemiesMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Distancia que se mueve en hacia cada lado
    /// </summary>
    [SerializeField] private int DistanceMove;
    /// <summary>
    /// Velocidad del enemigo
    /// </summary>
    [SerializeField] private int Speed;
    /// <summary>
    /// Bool  que indica si se desplaza en la X
    /// </summary>
    [SerializeField] private bool DirX;
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
    /// Vector que indica la posicion inicial
    /// </summary>
    private Vector2 _inicialPos;
    /// <summary>
    /// bool que indica si va
    /// </summary>
    private bool _moivingRight;
    /// <summary>
    /// Bool que indica si puede moverse
    /// </summary>
    private bool _canMove;
    /// <summary>
    /// Componente que contiene el rigidbody del enemigo
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
        _inicialPos = this.transform.position;
        _canMove = true;
        _moivingRight = true;
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
        //miramos si se puede mover 
        if (_canMove)
        {
            //Avanzar
            //Debug.Log("mueve");
            Move();
            //Mirar si se ha pasado del limite o a chocado y volver
        }
        else
        {
            //Si no puedes es que has detectado el jugador y quitamos la velocidad al eje que corresponda
            //Debug.Log("qUITANDO PATRON");
            if (DirX)
            {
                _rb.linearVelocityX = 0;
            }
            else
            {
                _rb.linearVelocityY = 0;
            }
            
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
    /// <summary>
    /// Metodo que desactiva el patron de movimiento
    /// </summary>
    /// <param name="act">modo en el que se queda</param>
    public void DesactiveActivate(bool act)
    {
        _canMove = act;
        if(act == true)
        {
            _inicialPos = this.transform.position;
        }
        
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //miramos si no es el player para cambiar de direccion
        if (!collision.gameObject.GetComponent<PlayerMovement>())
        {
            //Debug.Log("cambio");
            _moivingRight = !_moivingRight;
        }
       
    }
    /// <summary>
    /// Metodo encargado del patron movimiento del enemigo
    /// </summary>
    private void Move()
    {
       //Nos movemos dependidendo de la direccion y la distancia maxima que podemos llegar
        if (DirX )
        {
            if (this.transform.position.x < _inicialPos.x + DistanceMove && _moivingRight)
            {
                _rb.linearVelocityX = Speed;
                //Debug.Log("1"); 
            }else if (this.transform.position.x > _inicialPos.x - DistanceMove && !_moivingRight)
            {
                _rb.linearVelocityX = -Speed;
                //Debug.Log("2");

            }
            else if (this.transform.position.x >= _inicialPos.x + DistanceMove)
            {
                _moivingRight = false;
                //Debug.Log("3");

            }
            else if ((this.transform.position.x <= _inicialPos.x - DistanceMove))
            {
                _moivingRight = true;
                //Debug.Log("4");

            }
        }
        else
        {
            if (this.transform.position.y < _inicialPos.y + DistanceMove && _moivingRight)
            {
                _rb.linearVelocityY = Speed*1;
                //Debug.Log("5");

            }
            else if (this.transform.position.y > _inicialPos.y - DistanceMove && !_moivingRight)
            {
                _rb.linearVelocityY = -Speed;
                //Debug.Log("6");
            }
            else if (this.transform.position.y >= _inicialPos.y + DistanceMove)
            {
                _moivingRight = false;
                //Debug.Log("7");


            }
            else if ((this.transform.position.y <= _inicialPos.y - DistanceMove))
            {
                _moivingRight = true;
                //Debug.Log("8");

            }
        }

        
    }
    #endregion   

} // class EnemiesMeleController 
// namespace
