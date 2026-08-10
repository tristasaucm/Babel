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
public class PickUp : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private int InventorySpace;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private GameObject[] _bag;
    private bool _canPick;
    private int _currentNextEmtySpace;
    private GameObject _posiblePickObject;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    private Pickable _pickable;
    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _bag = new GameObject[InventorySpace];
        _currentNextEmtySpace = 0;

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_canPick)
        {
            //Revisamos si ha pulsado la tecla de recoger objetos
            if (InputManager.Instance.InteracWasPressedThisFrame())
            {
                //Miramos si tenemos espacio en la mochila para guardarlo
                if (!IsFull())
                {
                    //Si podemos guardarlo lo metemos en la lista y lo desactivamos
                    _bag[_currentNextEmtySpace] = _posiblePickObject;
                    _currentNextEmtySpace++;
                    _posiblePickObject.SetActive(false);
                    Debug.Log("objeto recogido");
                }
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
        //Revisamos que sea un objeto recogible
        _pickable = collision.GetComponent<Pickable>();
        if (_pickable)
        {
            _canPick = true;
            _posiblePickObject = collision.gameObject; 
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pickable = collision.GetComponent<Pickable>();
        if (_pickable)
        {
            _canPick = false;
        }
    }
    private bool IsFull()
    {
        bool full = true;
        full = _currentNextEmtySpace >= _bag.Length;
        return full;
    }
    #endregion   

} // class PickUp 
// namespace
