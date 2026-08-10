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
public class Interactive : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] bool ContieneLibro = false;
    PlayerMovement _player;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private bool _estaInteractuando;
    private bool _puedeInteractuar;
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

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _estaInteractuando = InputManager.Instance.InteracWasReleasedThisFrame();
        if(_puedeInteractuar && _estaInteractuando)
        {
            if (SoltarLibro())
            {
               
            }
            else if (PonLibro())
            {
                
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
    /// <summary>
    /// Metodo para sacar un libro de un sitio interactuable
    /// </summary>
    /// <returns>Devuelve si puede o no sacar un libro</returns>
    private bool SoltarLibro()
    {
        bool soltado = false;
        if (ContieneLibro)
        {
            soltado = true;
            Debug.Log("Libro obtenido");
            ContieneLibro = false;
        }
        return soltado;
    }
    /// <summary>
    /// Metodo para poner un libro de un sitio interactuable
    /// </summary>
    /// <returns>Devuelve si puede o no poner un libro</returns>
    private bool PonLibro()
    {
        bool puesto = false;
        if (!ContieneLibro)
        {
            puesto = true;
            Debug.Log("Libro Colocado");
            ContieneLibro = true;
        }
        return puesto;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<PlayerMovement>();
        if (_player != null)
        {

            _puedeInteractuar = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<PlayerMovement>();
        if (_player != null)
        {

            _puedeInteractuar = false;

        }
    }
    #endregion

} // class Interactive 
// namespace
