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
public class AttackController : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Arma a distancia
    /// </summary>
    [SerializeField] GameObject RangeWeapon = null;
    /// <summary>
    /// Mirilla del jugador
    /// </summary>
    [SerializeField] GameObject Peephole;
    /// <summary>
    /// Player
    /// </summary>
    [SerializeField] GameObject Player;
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
    /// Script de mele
    /// </summary>
    private MeleAttack _mele;
    /// <summary>
    /// Script de shoot
    /// </summary>
    private ShootAttack _shoot;
    /// <summary>
    /// bool que indica si puedes atacar
    /// </summary>
    private bool _canAttack;
    /// <summary>
    /// bool que indica si tienes arma
    /// </summary>
    private bool Weapon;
    /// <summary>
    /// bool que indica si puedes recoger el arma
    /// </summary>
    private bool _canPickRange;
    /// <summary>
    /// tarnsform del arma a distancia
    /// </summary>
    private Transform _rangeTransform;
    /// <summary>
    /// Posible arma a distancia
    /// </summary>
    private GameObject _posibleRange;
    /// <summary>
    /// bool que indica si no tenemos arma
    /// </summary>
    private bool _noWeapons;
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
        //Inicializacion y comprobaciones 
        _mele =GetComponent<MeleAttack>();
        if(RangeWeapon!= null)
        {
            _shoot = RangeWeapon.GetComponent<ShootAttack>();
           
            Weapon = true;
            ChangeWeapon();
        }
        _canAttack = true;
        if(!_mele.HasMele() && !_shoot)
        {
            _noWeapons = true;
        }
        else
        {
            _noWeapons = false;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Miramos si podemos atacar y si podemos cambiar de arma
        if (_canAttack)
        {
            //Añadir que no se pueda cambiar si atacamos con mele 
            if (InputManager.Instance.ChangeWeaponWasPressedThisFrame())
            {
                if (RangeWeapon != null)
                {
                    Debug.Log("cambiando de arma");
                    Weapon = !Weapon;
                    ChangeWeapon();
                }
            }
            //Miramos si podemos equipar armas
            EquipRangeWeapon();
            if (_noWeapons)
            {
                if (RangeWeapon && !_mele.HasMele())
                {
                    Weapon = false;
                    _shoot.ActivateDesactivateShoot(true);
                    _shoot.SetEnemyWeapon(false);
                    RangeWeapon.SetActive(true);
                    _noWeapons = false;
                }
                if (!RangeWeapon && _mele.HasMele())
                {
                    Weapon = true;
                    _mele.ActivateDesactivateMele(true);
                    _noWeapons = false;
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
    /// <summary>
    /// Metodo para soltar las armas que tenemos 
    /// </summary>
    public void DropWeapons()
    {
        _mele.DropWeapons();
        if(RangeWeapon != null)
        {
            _rangeTransform.position = this.transform.position;
            RangeWeapon.SetActive(true);
            _shoot.ActivateDesactivateShoot(false);
            RangeWeapon = null;
        }
        
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    /// Metodo encargado de cambiar de arma
    /// </summary>
    private void ChangeWeapon()
    {
       
            if (Weapon && _mele.HasMele())
            {
                _mele.ActivateDesactivateMele(true);
                _shoot.ActivateDesactivateShoot(false);
                RangeWeapon.SetActive(false);
             }
            else if (RangeWeapon != null)
            {
                 Debug.Log("disparo Activado");            
                _shoot.ActivateDesactivateShoot(true);
                RangeWeapon.SetActive(true);
                _mele.ActivateDesactivateMele(false);
            }
        

    }
    /// <summary>
    /// Metodo encargado de equipar arma a distancia
    /// </summary>
    private void EquipRangeWeapon()
    {
        //Entrar en el collider de una arma
        //Pulsar la tecla de interactuar
        if (_canPickRange && InputManager.Instance.InteracWasPressedThisFrame())
        {
            //Quitamos la antigua arma si tenemos
            if (RangeWeapon != null)
            {
                Debug.Log("dejando antigua arma a distancia");
                _rangeTransform.position = this.transform.position;
                _shoot.ActivateDesactivateShoot(false);
                RangeWeapon.SetActive(true);
                RangeWeapon = null;
            }
            //establecer como arma la recogida
            RangeWeapon = _posibleRange;
            _canPickRange = false;
            //Desactivar el arma recogida
            _rangeTransform = RangeWeapon.GetComponent<Transform>();
            _shoot = RangeWeapon.GetComponent<ShootAttack>();
            _shoot.ActivateDesactivateShoot(true);
            _shoot.PlayerConfig(Peephole, Player);
            RangeWeapon.SetActive(false);
            Debug.Log("Arma Cambiada");
            //Añadir que se ponga el arma cogida solo si no estamos atacando
            ChangeWeapon();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //miramos si es un arma a distancia para cogerla
        if (collision.GetComponent<ShootAttack>())
        {
            _canPickRange = true;
            _posibleRange = collision.gameObject;
            //si es la misma no la cogemos
            if (_posibleRange == RangeWeapon)
            {
                _canPickRange = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si se va ya no la podemos coger
        if (collision.GetComponent<ShootAttack>())
        {
            _canPickRange = false;
        }
    }


    #endregion   

} // class AttackController 
// namespace
