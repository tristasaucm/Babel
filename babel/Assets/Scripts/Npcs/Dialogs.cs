//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Dialogs : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Array que contiene los textos del npc
    /// </summary>
    [SerializeField] private string[] Texts;
    /// <summary>
    /// Sitio para  escribir
    /// </summary>
    [SerializeField] private TextMeshProUGUI UIText;
    /// <summary>
    /// Gameobject del panel del texto
    /// </summary>
    [SerializeField] private GameObject PanelDialogs;

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
    /// Numero de dialogos del npc
    /// </summary>
    private int _textLenght;
    /// <summary>
    /// Contiene el script del player movement
    /// </summary>
    private PlayerMovement _playerMovement;
    /// <summary>
    /// bool que indica si estamos hablando
    /// </summary>
    private bool _isTalking;
    /// <summary>
    /// bool que indica si podemos hablar
    /// </summary>
    private bool _canTalk;
    /// <summary>
    /// numero del texto en el que se encuentra
    /// </summary>
    private int _curentText;
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
        _textLenght = Texts.Length;
        _isTalking = false;
        _canTalk = false;
        _curentText = 0;
        PanelDialogs.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Miramos si puedes hablar para enmpezar
        if (_canTalk)
        {
            if (!_isTalking)
            {
                if (InputManager.Instance.InteracWasPressedThisFrame())
                {
                    _isTalking = true;
                    PanelDialogs.SetActive(true);
                    //desactivamos movimiento_ 
                    _playerMovement.ActivateDesactivateMovement(false);
                }
            }
            else if (_isTalking)
            {
                UpdateText();
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
        //Miramos si es el player para ver si puede hablar
        _playerMovement = collision.GetComponentInParent<PlayerMovement>();
        if (_playerMovement != null)
        {
            _canTalk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Miramos si el player se ha ido
        _playerMovement = collision.GetComponentInParent<PlayerMovement>();
        if (_playerMovement != null)
        {
            _canTalk = false;
        }
    }
    private void UpdateText()
    {
        if (_curentText < _textLenght)
        {
            //ponemos el texto que corresponde
            UIText.text = Texts[_curentText];

            if (InputManager.Instance.InteracWasPressedThisFrame())
            {
                _curentText++;
            }
        }
        if (_curentText == _textLenght)
        {
            PanelDialogs.SetActive(false);
            //Activamos el movimiento
            _playerMovement.ActivateDesactivateMovement(true);
            //Reseteamos el texto por si quieres volver a hablar
            _curentText = 0;
            _isTalking = false;
        }
    }
    #endregion   

} // class Dialogs 
// namespace
