//---------------------------------------------------------
// Arvchivo encargado del disparo a distancia del enemigo
// Tristan
// Babel
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemiesShoot : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// El arma a distancia del enemigo
    /// </summary>
    [SerializeField] GameObject RangeWeapon = null;
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
    /// Script del disparo del arma
    /// </summary>
    private ShootAttack _shoot;
    /// <summary>
    /// Bool que indica si podemos atacar
    /// </summary>
    private bool _canAttack;
    /// <summary>
    /// El transform del arma para siempre ponerla sobre nosotros
    /// </summary>
    private Transform _rangeTransform;
    /// <summary>
    /// script para guardar la vida de los enemigos que entren en nuestro rango
    /// </summary>
    private UniversalHealth _enemieHealth;
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
        //miramos si tenemos arma a distancia
        if (RangeWeapon == null)
        {
            Debug.Log("al enemigo le falta un arma");
        }
        else
        {
            _shoot = RangeWeapon.GetComponent<ShootAttack>();
            _rangeTransform = RangeWeapon.GetComponent<Transform>();
            _shoot.ActivateDesactivateShoot(true);
        }
        _shoot.ActivateDesactivateShoot(true);
        _shoot.SetEnemyWeapon(true);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Debug.Log(_canAttack);
        //Miramos si podemos atacar y no estamos atacando ya 
        if (_canAttack && !_shoot.IsShooting())
        {
            //  Debug.Log("EnemiesShoot: llamando a RemoteShoot");
            //mandamos el disparo al arma
            _shoot.RemoteShoot(true);

        }
        if (!_canAttack && _shoot.IsShooting())
        {
            //Des
            // Debug.Log("quitando disparo");
            _shoot.RemoteShoot(false);
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
    /// Metodo publico que se llama cuando morimos para soltar el arma que tenemos
    /// </summary>
    public void DropWeapons()
    {
        //Miramos si tenemos arma y la soltamos
        if (RangeWeapon != null)
        {
            _rangeTransform.position = this.transform.position;
            _shoot.ActivateDesactivateShoot(false);
            RangeWeapon.SetActive(true);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("ENTRA AL TRIGGER: " + collision.gameObject.name);
        //Miramos si es el player para empezar a disparar
        _enemieHealth = collision.GetComponentInParent<UniversalHealth>();

        if (_enemieHealth != null && _enemieHealth.IsPlayer())
        {
            // Debug.Log("PLAYER DETECTADO PARA DISPARAR");

            _canAttack = true;
        }
        else
        {
            // Debug.Log("No tiene UniversalHealth o no es Player");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si perdemos al player dejamos de disparar
        _enemieHealth = collision.GetComponentInParent<UniversalHealth>();
        if (_enemieHealth != null && _enemieHealth.IsPlayer())
        {
            //Debug.Log("player perdido");
            _canAttack = false;
        }
    }
    #endregion   

}
// class EnemiesShoot 
// namespace
