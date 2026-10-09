//---------------------------------------------------------
// Scritp para armas que disparan que puede ser usado por quien porte ese arma en ese momento
// Tristán Sánchez 
// Babel
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class ShootAttack : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private GameObject Proyectile;
    /// <summary>
    /// GameObject del Puntero 
    /// </summary>
    [SerializeField] private GameObject Peephole;
    /// <summary>
    /// Usuario del arma
    /// </summary>
    [SerializeField] private GameObject Player;
    /// <summary>
    /// Número de balas en el "pool"
    /// </summary>
    [SerializeField] private int NumBulletsPool = 7;
    /// <summary>
    /// Contenedor del pool de balas
    /// </summary>
    [SerializeField] private Transform BulletsContainer;
    /// <summary>
    /// Tiempo que tarda en recargar
    /// </summary>
    [SerializeField] float TimeOfRecharge = 1f;

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
    /// bool que se encarga de si puedes disparar
    /// </summary>
    private bool _canShoot;
    /// <summary>
    /// bool que se encarga de si estas disparando
    /// </summary>
    private bool _outOfBullets;
    /// <summary>
    /// bool que indica si estamos usando el disparo
    /// </summary>
    private bool _isUsingShoot;
    /// <summary>
    /// componente de la bala
    /// </summary>
    private Bullet _bullet;
    /// <summary>
    /// Mirirlla
    /// </summary>
    private Transform _peepholeTransform;
    /// <summary>
    /// Transform del player
    /// </summary>
    private Transform _playerTransform;
    /// <summary>
    /// Disparo remoto para los enemigos
    /// </summary>
    private bool _remoteShoot;
    /// <summary>
    /// Dice si lo esta usando un enemigo
    /// </summary>
    private bool _enemyuUse;
    /// <summary>
    /// Pool de balas máximas que podrán aparecer.
    /// </summary>
    private GameObject[] _bulletsPool;
    /// <summary>
    /// Controla el número de balas que tiene el jugador.
    /// </summary>
    private int _ammo;
    /// <summary>
    /// Tiempo de cuando te quedaste sin balas
    /// </summary>
    private float _timeOfEndBullets;
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
        //Inicializamos todo
        _playerTransform = Player.GetComponent<Transform>();
        _peepholeTransform = Peephole.GetComponent<Transform>();
        _bullet = Proyectile.GetComponent<Bullet>();
        _outOfBullets = false;
        _remoteShoot = false;
        _ammo = NumBulletsPool;
        if (!_bullet)
        {
            Debug.Log("No hay bala");
        }
        Proyectile.SetActive(false);
        //Creamos el array de balas
        _bulletsPool = new GameObject[NumBulletsPool]; // Reserva el espacio en el array
        for (int i = 0; i < NumBulletsPool; i++) // Por cada hueco genera una bala
        {
            //Istanciamos la bala
            GameObject bullet = Instantiate(Proyectile);
            bullet.transform.SetParent(BulletsContainer, true);
            _bulletsPool[i] = bullet; // Guarda la bala en la pool
            bullet.SetActive(false);
        }
        _ammo = NumBulletsPool-1;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        /* Debug.Log(
        "canShoot: " + _canShoot +
        " | isUsingShoot: " + _isUsingShoot +
        " | remoteShoot: " + _remoteShoot +
        " | isShooting: " + _isShooting
    );*/
        //Si podemos disparar y estamos usando el disparo colocamos el arma sobre nosotros
        if (_canShoot && _isUsingShoot)
        {

            Vector3 playerpos = Player.transform.position;
            playerpos.y += 0.5f;
            this.transform.position = playerpos;
            Vector2 dir = _peepholeTransform.position - this.transform.position;
            float AnguloZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0, 0, AnguloZ);
            //Si podemos disparar disparamos
            if (!_outOfBullets && ((
        ((!_enemyuUse && InputManager.Instance.AttackWasPressedThisFrame())
        || (_enemyuUse && _remoteShoot))
    )))
            {
                //Debug.Log("disparo desde el arco");

                // -- Comprueba si hemos gastado todas las balas y hay que recargar --
                if (_ammo < 0)
                {
                    _outOfBullets = true;
                    _timeOfEndBullets = Time.time;
                }
                else if(!_outOfBullets) //Si todavia tenemos balas podemos realizar el disparo
                {
                    _bulletsPool[_ammo].SetActive(true);//Se activa la bala que corresponde
                    _bulletsPool[_ammo].GetComponent<Bullet>().Shoot(_peepholeTransform.position, this.transform.position, _ammo);//se dispara
                    //Debug.Log("bala " + _ammo);
                    //Debug.Log(_ammo);
                    _ammo--;//Y se baja el numero de balas que tenemos

                }
               
                    
            }
            if (_outOfBullets)//Si nos hemos quedado sin balas recargamos
            {
                Debug.Log("recargando");
                Recharge();
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
    /// Metodo que indica si el ataque ha terminado para poder hacer otro
    /// </summary>
    public void HasFinishAttack(int i)
    {
        //Debug.Log("========== FIN DEL DISPARO ==========");
        _remoteShoot = false;
        _outOfBullets = false;
        _bulletsPool[i].SetActive(false);
        if(i == NumBulletsPool)
        {
            _ammo = NumBulletsPool;
        }
    }
    /// <summary>
    /// Metodo que ativa y desactiva el disparo
    /// </summary>
    /// <param name="status"> true activo false desactivado</param>
    public void ActivateDesactivateShoot(bool status)
    {
        // Debug.Log("estado " + status);
        _isUsingShoot = status;
        _canShoot = status;
    }
    /// <summary>
    /// Metodo que devuelve si se usa por un enemigo
    /// </summary>
    /// <returns></returns>
    public bool IsUsedByEnemie()
    {
        /*Debug.Log(
       "ShootAttack: IsUsedByEnemie = " + _remoteShoot
   );*/
        return _enemyuUse;
    }
    /// <summary>
    /// Metodo para disparar siendo enemigo
    /// </summary>
    public void RemoteShoot(bool est)
    {
        //Debug.Log("ShootAttack: RemoteShoot recibido");
        _remoteShoot = est;
        _enemyuUse = true;

    }
    /// <summary>
    /// Metodo que indica si estamos disparando
    /// </summary>
    /// <returns></returns>
    public bool IsShooting()
    {
        return _outOfBullets;
    }
    /// <summary>
    /// Metodo que indica si lo usa un enemigo
    /// </summary>
    /// <returns></returns>
    public bool IsUsedEnemie()
    {
        return _enemyuUse;
    }
    /// <summary>
    /// metodo que pone el arma para enemigos
    /// </summary>
    /// <param name="value"></param>
    public void SetEnemyWeapon(bool value)
    {
        _enemyuUse = value;
    }
    /// <summary>
    /// Metodo que configura el arma para ser usada por un jugador
    /// </summary>
    /// <param name="Peephole">Mirilla del jugador</param>
    /// <param name="Player">Persona que lleva el arma </param>
    public void PlayerConfig(GameObject Peephole, GameObject Player)
    {
        this.Peephole = Peephole;
        this.Player = Player;
        _playerTransform = Player.GetComponent<Transform>();
        _peepholeTransform = Peephole.GetComponent<Transform>();
        SetEnemyWeapon(false);

    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private bool GetInput()
    {
        if (!_enemyuUse)
        {
            return InputManager.Instance.AttackWasPressedThisFrame();
        }
        else
        {
            return false;
        }
    }
    private void Recharge()
    {
        //Miramos si el tiempo de recarga paso y recargamos
        if(TimeOfRecharge < Time.time-_timeOfEndBullets)
        {
            _ammo = NumBulletsPool-1;
            _outOfBullets = false;
        }
    }
    #endregion   

} // class ShootAttack 
// namespace
