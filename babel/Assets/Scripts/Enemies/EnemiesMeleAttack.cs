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
public class EnemiesMeleAttack : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Arma del enemigo
    /// </summary>
    [SerializeField] private GameObject Weapon;
    /// <summary>
    /// Coldown del arma 
    /// </summary>
    [SerializeField] private int Cooldown;
    /// <summary>
    /// Duracion del ataque del arma
    /// </summary>
    [SerializeField] private float AttackDuration;
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
    /// vida del enemigo
    /// </summary>
    private UniversalHealth _healthPlayer;
    /// <summary>
    /// bool que indica si atacamos
    /// </summary>
    private bool _attack;
    /// <summary>
    /// transform del player
    /// </summary>
    private Transform _playerTransform;
    /// <summary>
    /// Tiempo en el que se ha desactivado el ataque
    /// </summary>
    private float _AttackFinishTime;
    /// <summary>
    /// Tiempo en el que se ha activado el ataque
    /// </summary>
    private float _AttackStartTime;
    /// <summary>
    /// Bool que indica si estamos usando el arma mele
    /// </summary>
    private bool _isAttacking;
    /// <summary>
    /// Script del arma a mele
    /// </summary>
    private MeleWeapon _meleWeapon;
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
        //Reseteamos los coldowns y obtenemos los compoentes 
        _AttackFinishTime = Time.time;
        Weapon.SetActive(false);
        _attack = false;
        _meleWeapon = Weapon.GetComponent<MeleWeapon>();
        if (_meleWeapon)
        {
            _meleWeapon.Equip(true, true);
        }
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Si le podemos attacar
        if (_attack)
        {
            // Apuntamos y activamos
            Weapon.transform.position = this.transform.position;
            Vector3 point = _playerTransform.transform.position;
            Vector3 dir = point - this.transform.position;
            Weapon.transform.position += dir;
            //Si el tiempo pasado es mayor que el cooldown
            if(Time.time -_AttackFinishTime > Cooldown && !_isAttacking)
            {//Atacamos
                Weapon.SetActive(true);
                _isAttacking = true;
                _AttackStartTime = Time.time;
            }else if(_isAttacking && AttackDuration <Time.time -_AttackStartTime)
            {
                //si el tiempo se ha terminado terminamos el ataque
                _AttackFinishTime = Time.time;
                Weapon.SetActive(false);
                _isAttacking = false;
            }

           
        }
        if (!_attack)
        {
            //si no podemos atacar quitamos el ataque
            _AttackFinishTime = Time.time;
            Weapon.SetActive(false);
            _isAttacking = false;
        }
        //Atacamos 
        //en la direcion del player
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
    /// Metodo encargado de soltar las armas
    /// </summary>
    public void DropWeapons()
    {
        Weapon.transform.position = this.transform.position;
        Weapon.SetActive(true);
        _meleWeapon.Equip(false, false);
        Weapon = null;
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
        ///miramos si es el player para obtener su transform y decir que podemos atacar
        _healthPlayer = collision.GetComponent<UniversalHealth>();
        if(_healthPlayer!= null)
        {
            if (_healthPlayer.IsPlayer())
            {
                _attack = true;
                _playerTransform = collision.GetComponent<Transform>();
            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si el player se va dejamos de poder atacar
        _healthPlayer = collision.GetComponent<UniversalHealth>();
        if (_healthPlayer != null)
        {
            if (_healthPlayer.IsPlayer())
            {
                _attack = false;
            }
            
        }
    }
    #endregion   

} // class EnemiesMeleAttack 
// namespace
