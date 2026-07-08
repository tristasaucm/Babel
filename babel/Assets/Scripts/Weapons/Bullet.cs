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
public class Bullet : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private int Damage;
    [SerializeField] private float Speed;
    [SerializeField] private float Maxdistance;
    [SerializeField] private GameObject Shoter;
    [SerializeField] private GameObject Proyectile;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private EnemiesHealth _enemieHealth;
    private bool _hasbeenShoot;
    private Vector3 _inicialPos;
    private ShootAttack _shootAttack;

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
        _shootAttack = Shoter.GetComponent<ShootAttack>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_hasbeenShoot)
        {
           this.transform.position += transform.right* Speed *Time.deltaTime;
            if (Maxdistance < Vector3.Distance(_inicialPos,this.transform.position) )
            {
                Debug.Log("llego al maximo");
                Proyectile.SetActive(false);
                _shootAttack.HasFinishAttack();
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
    /// Metodo que prepara el proyectil y lo lanza
    /// </summary>
    /// <param name="point"></param>
    public void Shoot(Vector3 point,Vector3 PlayerPos)
    {

        this.transform.position = PlayerPos;
        Vector2 dir = point - this.transform.position;
        float AnguloZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, AnguloZ);
        _hasbeenShoot = true;
        _inicialPos = this.transform.position;
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
        _enemieHealth = collision.GetComponent<EnemiesHealth>();
        if(_enemieHealth != null)
        {
            _enemieHealth.GetDamage(Damage);
        }
    }
    #endregion   

} // class Bullet 
// namespace
