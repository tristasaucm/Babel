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
    /// <summary>
    /// Daño de la bala
    /// </summary>
    [SerializeField] private int Damage;
    /// <summary>
    /// Velocidad de la bala
    /// </summary>
    [SerializeField] private float Speed;
    //Distancia maxima 
    [SerializeField] private float Maxdistance;
    //Arma que lo dispara
    [SerializeField] private GameObject Shoter;
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
    /// script de vida
    /// </summary>
    private UniversalHealth _enemieHealth;
    /// <summary>
    /// ha sido disparado
    /// </summary>
    private bool _hasbeenShoot;
    /// <summary>
    /// posicion inicial de la bala
    /// </summary>
    private Vector3 _inicialPos;
    /// <summary>
    /// Ataque a distancia
    /// </summary>
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
        //conseguimos el componente del disparo
        _shootAttack = Shoter.GetComponent<ShootAttack>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Miramos si ha sido disparado
        if (_hasbeenShoot)
        {
           this.transform.position += transform.right* Speed *Time.deltaTime;
            if (Maxdistance < Vector3.Distance(_inicialPos,this.transform.position) )
            {
               // Debug.Log("llego al maximo");
                this.gameObject.SetActive(false);
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
    /// <param name="point"> punto de llegada</param>
    /// <param name="PlayerPos">punto de partida</param>
    public void Shoot(Vector3 point,Vector3 PlayerPos)
    {
       // Debug.Log("¡¡¡BULLET SHOOT!!!");
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
        //Miramos si es un enemigo o un player y dependiendo de que seamos le hacemos daño o no
        if (!collision.isTrigger)
        {
            _enemieHealth = collision.GetComponent<UniversalHealth>();
            //miramos si tiene vida
            if (_enemieHealth != null)
            {
                if (_enemieHealth.IsPlayer())
                {
                    //Debug.Log("player");
                    if (_shootAttack.IsUsedByEnemie())
                    {
                        _enemieHealth.GetDamage(Damage);
                        _shootAttack.HasFinishAttack();
                    }
                }
                else
                {
                   // Debug.Log("Enemie");
                    if (!_shootAttack.IsUsedByEnemie())
                    {
                        //Debug.Log("dañando enemigo");
                        _enemieHealth.GetDamage(Damage);
                        _shootAttack.HasFinishAttack();
                    }

                }
            }
      
            
                
        }
    }
    #endregion   

} // class Bullet 
// namespace
