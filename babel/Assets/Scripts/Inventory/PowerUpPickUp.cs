// Breve descripción del contenido del archivo
// Tristan Sanchez
// Babel
//---------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PowerUpPickUp : MonoBehaviour
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
    /// Indica si puedes recoger un Power Up
    /// </summary>
    private bool _canPick;
    /// <summary>
    /// Mochila que lleva los Power Ups recogidos
    /// </summary>
    private PowerUp[] _bag;
    /// <summary>
    /// LLeva el ultimo sitio libre
    /// </summary>
    private int _currentLastPlace;
    /// <summary>
    /// Componente del script powerUp
    /// </summary>
    private PowerUp _powerUp;
    /// <summary>
    /// Indica si el panel esta activo
    /// </summary>
    private bool _panelActive;
    /// <summary>
    /// El momento en el que se inicia el panel
    /// </summary>
    private float _startTime;
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
        _bag = new PowerUp[10];
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_canPick && InputManager.Instance.InteracWasPressedThisFrame())
        {
            //Miramos si ya ha sido recogido
            if (!HasPowerUp(_powerUp))
            {//Si no ha sido recogido lo cogemos
                PickingUp(_powerUp);
                //Ponemos el menseaje ha sido encontrado un libro .....
                string name = _powerUp.GetName();
                PanelRecogidos.SetActive(true);
                ActivatePowerUp();
                TextoRecompensas.text = " ha sido encontrado un powerUp de " + name;
                //Lo marcamos como recogido 
                _powerUp.gameObject.SetActive(false);
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
    /// <summary>
    /// Indica si tenememos algo en la bolsa
    /// </summary>
    /// <returns> Devuelve true si tienes algo</returns>
    public bool HasSomething()
    {
        return _currentLastPlace > 0;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    /// Metodo para activar los power ups
    /// </summary>
    private void ActivatePowerUp()
    {
        //Obtenemos que power up somos 
        string name = _bag[_currentLastPlace - 1].GetName();
        float buff = _bag[_currentLastPlace - 1].GetBuff();
        //Miramos si existe su tipo Y lo activamos
        switch (name)
        {
            case "Speed": this.gameObject.GetComponent<PlayerMovement>().ChangeSpeed(buff); break;
            case "Life": this.gameObject.GetComponent<UniversalHealth>().ChangeHealth(buff);break;
            case "SprintDuration": this.gameObject.GetComponent<PlayerMovement>().ChangeSprintDuration(buff); break;
            case "Sprint": this.gameObject.GetComponent<PlayerMovement>().ChangeSprint(buff); break;
                //TODO
                //Aumenta el daño con los arcos/armas de larga distancia
                // case:"RangeDamage":this.gameObject.GetComponentInChildren<AttackController>.ChangeDamage(buff);break;
                //Aumenta el daño con las espadas/armasCuerpo a cuerpo
                //case: "RangeDamage":this.gameObject.GetComponentInChildren<MeleAttack>.ChangeDamage(buff); break;


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _powerUp = collision.GetComponent<PowerUp>();
        if (_powerUp)
        {
            //se puede recoger
            _canPick = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _powerUp = collision.GetComponent<PowerUp>();
        if (_powerUp)
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
    private bool HasPowerUp(PowerUp Name)
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
    private void PickingUp(PowerUp name)
    {

        _bag[_currentLastPlace] = name;
        //Obtenemos la escena destinada
        _currentLastPlace++;
    }

    #endregion   

} // class PowerUpPIckUp 
// namespace

