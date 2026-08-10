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
public class Sitable : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

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
    /// El transform del player 
    /// </summary>
    private Rigidbody2D _playerRigidbody;
    /// <summary>
    /// El player movement del player
    /// </summary>
    private PlayerMovement _playerMovement;
    /// <summary>
    /// Bool que indica si se puede sentar
    /// </summary>
    private bool _canSit;
    /// <summary>
    /// Bool que indica si esta sentado
    /// </summary>
    private bool _isSit;
    /// <summary>
    /// Posicon inicial del jugador
    /// </summary>
    private Vector2 _inicialPosPlayer;
    /// <summary>
    /// Transform del collider
    /// </summary>
    private Transform _colliderTransform;
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
        //Inicializamos
        _canSit = false;
        _isSit = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Si te puedes sentar y presionas la tecla te sientas
        if (_canSit)
        {
            if (InputManager.Instance.InteracWasPressedThisFrame()&& !_isSit)
            {
                _inicialPosPlayer = _playerRigidbody.position;
                _playerMovement.ActivateDesactivateMovement(false);
                
                Debug.Log("sentado");
                _playerRigidbody.position = this.transform.position;
                _isSit = true;
            }
            else if (InputManager.Instance.InteracWasPressedThisFrame() && _isSit)
            {
                Debug.Log("levantado");
                _playerRigidbody.position = _inicialPosPlayer;
                _playerMovement.ActivateDesactivateMovement(true);
                _isSit = false;
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

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Miramos si es el player para que se pueda sentar
        _playerMovement = collision.GetComponentInParent<PlayerMovement>();
        _playerRigidbody = collision.GetComponentInParent<Rigidbody2D>();
        if(_playerRigidbody && _playerMovement)
        {
            _canSit = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Miramos si se ha ido el player
        _playerMovement = collision.GetComponentInParent<PlayerMovement>();
        _playerRigidbody = collision.GetComponentInParent<Rigidbody2D>();
        if (_playerRigidbody && _playerMovement)
        {
            _canSit = false;
        }
    }
    #endregion   

} // class Sitable 
// namespace
