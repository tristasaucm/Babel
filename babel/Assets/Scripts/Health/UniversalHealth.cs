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
public class UniversalHealth : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Vida del enemigo
    /// </summary>
    [SerializeField] private int Health;

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
    /// Usado para el ataque a mele del enemigo
    /// </summary>
    private EnemiesMeleAttack _mele;
    /// <summary>
    /// Usado para el disparo del enemigo
    /// </summary>
    private EnemiesShoot _shoot;
    /// <summary>
    /// Indica si eres un jugador o no
    /// </summary>
    private bool _player;
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
        //Chekea si eres jugador o enemigo
        _player = this.GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Siempre vamos mirando si hemos muerto
        DidIDie();
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
    /// Metodo que se encarga de hacer daño a la entidad
    /// </summary>
    /// <param name="damage">Daño recivido </param>
    public void GetDamage(int damage)
    {
        /* Debug.Log(
     "¡¡¡ GET DAMAGE !!! " +
     gameObject.name +
     " | llamado desde: " +
     new System.Diagnostics.StackTrace().ToString());*/
        Health -= damage;
    }
    /// <summary>
    /// Metodo encargado de Poner regeneracion al jugador
    /// </summary>
    /// <param name="regen">Vida regenerada</param>
    public void RegenHealth(int regen)
    {
        Health += regen;
    }
    /// <summary>
    /// Metodo para saber la vida que tenemos
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return Health;
    }
    public void ChangeHealth(float buff)
    {
        float extraHealth = buff * Health;
        Health += (int)extraHealth;
        Debug.Log(Health);
    }
    /// <summary>
    /// Metodo que indica si somos el player
    /// </summary>
    /// <returns></returns>
    public bool IsPlayer()
    {
        return _player;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    /// Metodo privado encargado de cuando morimos dropemos las cosas tanto como jugador como enemigo
    /// </summary>
    private void DidIDie()
    {
        if(Health <= 0)
        {
            if (_player)
            {
                GetComponentInChildren<AttackController>().DropWeapons();
            }
            else
            {
                _mele = GetComponentInChildren<EnemiesMeleAttack>();
                _shoot = GetComponentInChildren<EnemiesShoot>();
               if (_mele != null)
                {
                    _mele.DropWeapons();
                }
               if(_shoot != null)
                {
                    _shoot.DropWeapons();
                }
                
            }
           this.gameObject.SetActive(false);
        }
    }
    #endregion   

} // class EnemiesHealth 
// namespace
