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

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private bool _canShoot;
    private bool _isShooting;
    private bool _isUsingShoot;
    private Bullet _bullet;
    private Transform _peepholeTransform;
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
        _peepholeTransform = Peephole.GetComponent<Transform>();
        _bullet = Proyectile.GetComponent<Bullet>();
        _isShooting = false;
        _canShoot = true;
        if (!_bullet)
        {
            Debug.Log("No hay bala");
        }
        Proyectile.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_canShoot && _isUsingShoot )
        {
            if (!_isShooting && InputManager.Instance.AttackWasPressedThisFrame())
            {
                Proyectile.SetActive(true);
                _bullet.Shoot(_peepholeTransform.position,this.transform.position);
                _isShooting = true;
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
    public void HasFinishAttack() 
    {
        _isShooting = false;
    }
    public void ActivateDesactivateShoot(bool status)
    {
        _isUsingShoot = status;
    }


    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class ShootAttack 
// namespace
