//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Tristan Sanchez
// Babel
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class houseEnter : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// el gameobject del el otro lado al cual vamos a tparnos
    /// </summary>
    [SerializeField] private GameObject houseLink;
    /// <summary>
    /// el panel de loading para que el jugador no sepa que pasa
    /// </summary>
    [SerializeField] private GameObject PanelLoading;
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
    /// el transform de donde nos tpamos
    /// </summary>
    private Transform _tpTransform;
    /// <summary>
    /// Tiempo de espera antes de quitar el panel
    /// </summary>
    private float _waitTime;
    /// <summary>
    /// Indica si estamos en rango
    /// </summary>
    private bool _onReach;
    /// <summary>
    /// el transform del Player;
    /// </summary>
    private Rigidbody2D _playerRb;
    /// <summary>
    /// Tiempo en el cual se ha teletransportado
    /// </summary>
    private float _startTime;
    /// <summary>
    /// Indica si esta en camino
    /// </summary>
    private bool _onWay;

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
        _tpTransform = houseLink.GetComponent<Transform>();
        PanelLoading.SetActive(false);
        _waitTime = 1f;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_onReach)
        {
            if (InputManager.Instance.InteracWasPressedThisFrame())
            {
                Tp();
            }
        }
        if(_onWay&& _waitTime < Time.time - _startTime)
        {
            _onWay = false;
            PanelLoading.SetActive(false);
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
        _playerRb = collision.GetComponentInParent<Rigidbody2D>();
        if (collision.GetComponentInParent<PlayerMovement>()!= null&& _playerRb)
        {
            _onReach = true;
          
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<PlayerMovement>() != null)
        {
            _onReach = false;
        }
    }
    private void Tp()
    {
        PanelLoading.SetActive(true);
        Vector3 pos = _tpTransform.position;
        //pos.y++;
        _playerRb.position = pos;
        _startTime = Time.time;
        _onWay = true;
    }
    #endregion   

} // class houseEnter 
// namespace
