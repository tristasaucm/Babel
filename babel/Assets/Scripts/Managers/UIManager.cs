//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Data;
using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class UIManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Imagen de la barra de vida
    /// </summary>
    [SerializeField] private Image Life;
    /// <summary>
    /// Imagen de la barra de energia
    /// </summary>
    [SerializeField] private Image Energy;
    /// <summary>
    /// Gameobject del player
    /// </summary>
    [SerializeField] private GameObject Player;


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
    /// Script que lleva el movimiento del jugador
    /// </summary>
    private PlayerMovement _playerEnergy;
    /// <summary>
    /// Script que lleva la vida del jugador
    /// </summary>
    private UniversalHealth _playerHealth;
    /// <summary>
    /// float que indica la vida maxima
    /// </summary>
    private float _maxHealth;
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
        _playerEnergy = Player.GetComponent<PlayerMovement>();
        _playerHealth = Player.GetComponent<UniversalHealth>();
        _maxHealth = _playerHealth.GetHealth();
    }

    /// <summary>.
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //altualizamos el UI
        UpdateUI();
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
    /// Metodo que atualiza el UI
    /// </summary>
    private void UpdateUI()
    {
        //Debug.Log("actualizando");
       float health = _playerHealth.GetHealth();
       float energy = _playerEnergy.GetEnergy();
       Energy.fillAmount = energy;
        Life.fillAmount = health / _maxHealth;
       
    }
    #endregion   

} // class UIManager 
// namespace
