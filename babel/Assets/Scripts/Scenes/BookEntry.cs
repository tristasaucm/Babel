//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class BookEntry : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// El panel que te lleva al libro
    /// </summary>
    [SerializeField]private GameObject PanelBookEntry;
    /// <summary>
    /// El boton de play
    /// </summary>
    [SerializeField] private GameObject Button;
    /// <summary>
    /// Texto del siguiente nivel
    /// </summary>
    [SerializeField] private TextMeshProUGUI TextNivel;
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
    /// Componente que lleva los libros recogidos
    /// </summary>
    private PickUp _pickUp;
    /// <summary>
    /// Indica si puede interactuar
    /// </summary>
    private bool _canInterac;
    /// <summary>
    /// Script del player movement
    /// </summary>
    private PlayerMovement _playerMovement;
    /// <summary>
    /// Indica si el panel esta abierto
    /// </summary>
    private bool _hasPanelOpen;
    /// <summary>
    /// Script para cambiar de escena
    /// </summary>
    private ChangeScene _changeScene;
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
        _canInterac = false;
        if(PanelBookEntry == null)
        {
            Debug.Log("Falta el panel de entrar al libro");
        }
        else
        {
            PanelBookEntry.SetActive(false);
        }
        _changeScene = Button.GetComponent<ChangeScene>();
        if(_changeScene == null)
        {
            Debug.Log("El boton no tiene el change scene");
        }
        _hasPanelOpen = false;
       
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(_canInterac && InputManager.Instance.InteracWasPressedThisFrame())
        {
            //Miramos si tiene libros 
            if (_pickUp.HasSomething())
            {
                int scene = 0;
                string name ="";
                _pickUp.GetInfo(0, ref scene, ref name);
                TextNivel.text = name;
                //Si tiene ponemos el panel
                PanelBookEntry.SetActive(true);
                _changeScene.SetScene(scene);
                _playerMovement.ActivateDesactivateMovement(false);
                _hasPanelOpen = true;
            }
            
        }
        if(_hasPanelOpen && InputManager.Instance.EscapeWasPressedThisFrame())
        {
            PanelBookEntry.SetActive(false);
            _playerMovement.ActivateDesactivateMovement(true);
            _hasPanelOpen = false;
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
        _pickUp = collision.GetComponent<PickUp>();
        if(_pickUp != null)
        {
            _playerMovement = collision.GetComponentInParent<PlayerMovement>();
            //El jugador puede interactuar
            _canInterac = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pickUp = collision.GetComponent<PickUp>();
        if (_pickUp != null)
        {
            //El jugador no puede interactuar
            _canInterac = false;
        }
    }
    #endregion   

} // class BookEntry 
// namespace
