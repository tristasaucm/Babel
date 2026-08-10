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
    [SerializeField] private float Speed;
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
    /// Velocidad cuando no esta activo el sprint
    /// </summary>
    private float _inicialSpeed;
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
    /// <summary>
    /// float que es el numero de energia que queda
    /// </summary>
    private float _energy;
    /// <summary>
    /// bool que indica si se ha terminado el coldown
    /// </summary>
    private bool _finishCooldown;
    /// <summary>
    /// Rigidbody del player
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
        //Inicializacion de variables
        _inicialSpeed = Speed;
        _sprintFinishTime = 0;
        _sprintStartTime = 0;
        _sprintActive = false;
        _movementActive = true;
        _finishCooldown = true;
        _rb = this.GetComponent<Rigidbody2D>();
        if(_rb == null)
        {
            Debug.Log("No hay rigidbody por tanto no se puede mover");
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
       
        //presiono tecla sprint si no esta activo 
        if (InputManager.Instance.FireWasPressedThisFrame() && !_sprintActive)
        {
            Debug.Log("pulsado");
            ChangeModeSpeed();
        }
        if (Time.time - _sprintStartTime > SprintDuration && _sprintActive)
        {
            ChangeModeSpeed();
        }
    }
    private void FixedUpdate()
    {
        Vector2 dir = InputManager.Instance.MovementVector;
        if (_movementActive)
        {
            Movement(dir);
        }
        else
        {
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
    /// <summary>
    /// Metodo que desactiva el movimiento
    /// </summary>
    public void ActivateDesactivateMovement(bool next)
    {
        _movementActive = next;
       
    }
    /// <summary>
    /// Metodo que se encarga de saber cuanta energia queda
    /// </summary>
    /// <returns> La energia del jugador </returns>
    public float GetEnergy()
    {
        if (_sprintActive)
        {
            _energy = SprintDuration - (Time.time - _sprintStartTime);
            _energy /= SprintDuration;
        }else if(Time.time - _sprintFinishTime < CooldownSprint && _sprintFinishTime != 0)
        {
            _energy = (Time.time - _sprintFinishTime);
            _energy /= CooldownSprint;
        }
        else
        {
            _energy = 1f;
        }
            
        return _energy;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    /// Metodo encargado de poner el sprint si se debe
    /// </summary>
    private void ChangeModeSpeed()
    {

        _finishCooldown = Time.time - _sprintFinishTime > CooldownSprint || _sprintFinishTime == 0;
        if (_finishCooldown)
        {
            //Debug.Log("coldown termindo");
            _sprintActive = false;
        }
        if (Speed == _inicialSpeed && _finishCooldown)
        {
            //Debug.Log("pone sprint");
            Speed = Sprint;
            _sprintStartTime = Time.time;
            _sprintActive = true;
        }
        else
        {
            if(Speed != _inicialSpeed)
            {
                Speed = _inicialSpeed;
                _sprintFinishTime = Time.time;
            }
        }
    }
    /// <summary>
    /// Metodo encargado del movimiento del jugador
    /// </summary>
    /// <param name="dir">direcion del movimiento </param>
    private void Movement(Vector2 dir)
    {
        _rb.linearVelocity = dir * Speed;

    }
    #endregion

} // class PlayerMovement 
// namespace
