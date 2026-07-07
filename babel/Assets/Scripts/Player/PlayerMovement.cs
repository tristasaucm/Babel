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
public class PlayerMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Velocidad a la que se deplaza el personaje
    /// </summary>
    [SerializeField] private int Speed;
    /// <summary>
    /// Boleano que decide si el jugador puede usar el sprint o no 
    /// </summary>
    [SerializeField] private int Sprint;
    /// <summary>
    /// Tiempo que tarda en recuperarse el sprint
    /// </summary>
    [SerializeField] private float CooldownSprint;
    /// <summary>
    /// Duracion del sprint
    /// </summary>
    [SerializeField] private float SprintDuration;

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
    /// Numero de libros que lleva el jugador
    /// </summary>
    private int _libros;
    /// <summary>
    /// Velocidad cuando no esta activo el sprint
    /// </summary>
    private int _inicialSpeed;
    /// <summary>
    /// Tiempo en el que se ha desactivado el sprint
    /// </summary>
    private float _sprintFinishTime;
    /// <summary>
    /// Tiempo en el que se ha activado el sprint
    /// </summary>
    private float _sprintStartTime;
    /// <summary>
    /// bool que indica si el sprint esta activo
    /// </summary>
    private bool _sprintActive;
    /// <summary>
    /// bool que indica si el movimiento esta activo
    /// </summary>
    private bool _movementActive;
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
        _libros = 0;
        _inicialSpeed = Speed;
        _sprintFinishTime = 0;
        _sprintStartTime = 0;
        _sprintActive = false;
        _movementActive = true;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector2 dir = InputManager.Instance.MovementVector;
        //Movimiento
        if (_movementActive)
        {
            Movement(dir);
        }
        if (InputManager.Instance.FireWasPressedThisFrame())
        {
            ChangeModeSpeed();
        }
        if (Time.time - _sprintStartTime > SprintDuration && _sprintActive)
        {
            ChangeModeSpeed();
            _sprintActive = false;
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
    public void CambiaLibros(int num)
    {
        _libros += num;
    }
    public void ActivateDesactivateMovement()
    {
        _movementActive = !_movementActive;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void ChangeModeSpeed()
    {
        if (Speed == _inicialSpeed && (Time.time - _sprintFinishTime > CooldownSprint || _sprintFinishTime == 0))
        {
            //Debug.Log("pone sprint");
            Speed = Sprint;
            _sprintStartTime = Time.time;
            _sprintActive = true;

        }
        else
        {
            //Debug.Log("desactiva sprint");
            Speed = _inicialSpeed;
            _sprintFinishTime = Time.time;
        }
    }
    //Metodo encargado del movimiento del jugador
    private void Movement(Vector2 dir)
    {

        if (dir.x < 0)
        {
            transform.position += new Vector3(Speed * dir.x * Time.deltaTime, 0, 0);
        }
        else if (dir.x > 0)
        {
            transform.position += new Vector3(Speed * dir.x * Time.deltaTime, 0, 0);
        }
        if (dir.y < 0)
        {
            transform.position += new Vector3(0, Speed * dir.y * Time.deltaTime, 0);
        }
        if (dir.y > 0)
        {
            transform.position += new Vector3(0, Speed * dir.y * Time.deltaTime, 0);
        }
    }
    #endregion

} // class PlayerMovement 
// namespace
