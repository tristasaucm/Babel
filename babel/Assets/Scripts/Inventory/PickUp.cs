//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
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
    /// <summary>
    /// Panel que se activa
    /// </summary>
    [SerializeField] private GameObject PanelRecogidos;
    /// <summary>
    /// texto de recompensas
    /// </summary>
    [SerializeField] private TextMeshProUGUI TextoRecompensas;
    /// <summary>
    /// Tiempo que dura el panel
    /// </summary>
    [SerializeField] private float DuracionPanel;
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
    /// Indica si puedes recoger un libro
    /// </summary>
    private bool _canPick;
    /// <summary>
    /// Componente del script pickable
    /// </summary>
    private Pickable _pickable;
    /// <summary>
    /// Mochila que lleva los libros recogidos
    /// </summary>
    private string[] _bag;
    /// <summary>
    /// LLeva el ultimo sitio libre
    /// </summary>
    private int _currentLastPlace;
    /// <summary>
    /// Indica si el panel esta activo
    /// </summary>
    private bool _panelActive;
    /// <summary>
    /// El momento en el que se inicia el panel
    /// </summary>
    private float _startTime;
    private int[] _scenes;
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
        if (TextoRecompensas == null)
        {
            Debug.Log("No hay texto el programa no ira");
        }
        if (PanelRecogidos == null)
        {
            Debug.Log("No hay panel el programa no ira");
        }
        _bag = new string[10];
        _scenes = new int[10];
        _panelActive = false;

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_canPick && InputManager.Instance.InteracWasPressedThisFrame())
        {
            //Miramos que libro es 
            string name = _pickable.GetName();
            //Miramos si ya ha sido recogido
            if (!HasBook(name))
            {//Si no ha sido recogido lo cogemos
                PickingUp(name);
                //Ponemos el menseaje ha sido encontrado un libro .....
                PanelRecogidos.SetActive(true);
                TextoRecompensas.text = " ha sido encontrado un libro de " + name;
                //Lo marcamos como recogido 
                _pickable.gameObject.SetActive(false);
                _panelActive = true;
                _startTime = Time.time;
            }


        }
        if (_panelActive && Time.time - _startTime >= DuracionPanel)
        {
            _panelActive = false;
            PanelRecogidos.SetActive(false);
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
    public bool HasSomething()
    {
        return _currentLastPlace > 0;
    }
    public void GetInfo(ref int num, ref int scene, ref string Name)
    {
        if (0 <= num && num <= _currentLastPlace-1)
        {
            scene = _scenes[num];
            Name = _bag[num];
        }if(num < 0)
        {
            num = 0;
        }
        else if (num >= _currentLastPlace - 1)
        {
            num = _currentLastPlace - 1;
        }
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _pickable = collision.GetComponent<Pickable>();
        if (_pickable)
        {
            //se puede recoger
            _canPick = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pickable = collision.GetComponent<Pickable>();
        if (_pickable)
        {
            // no se puede recoger
            _canPick = false;
        }
    }
    /// <summary>
    /// Mira si ya tenemos el objeto del suelo
    /// </summary>
    /// <param name="Name">nombre del objeto </param>
    /// <returns></returns>
    private bool HasBook(string Name)
    {
        bool Own = false;
        if (_currentLastPlace > 0)
        {


            int i = 0;
            while (i <= _currentLastPlace && !Own)
            {
                Own = _bag[i] == Name;
                i++;
            }
        }
        return Own;

    }
    /// <summary>
    /// Recoge el objeto
    /// </summary>
    /// <param name="name">Objeto recogido </param>
    private void PickingUp(string name)
    {

        _bag[_currentLastPlace] = name;
        //Obtenemos la escena destinada
        _scenes[_currentLastPlace] = _pickable.GetWorldScene();
        _currentLastPlace++;
    }

    #endregion

} // class PickUp 
// namespace
