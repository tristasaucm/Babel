//---------------------------------------------------------
// Codigo encargado de las armas a mele
// Tristan
//---------------------------------------------------------

using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class MeleWeapon : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Daño que produce el arma
    /// </summary>
    [SerializeField] private int Damage;
    /// <summary>
    /// duracion del arma mele
    /// </summary>
    [SerializeField] private float MeleDuration;
    /// <summary>
    /// Cooldown del arma mele
    /// </summary>
    [SerializeField] private float MeleCooldown;
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
    /// variable que contiene el script de vida de los enemigos
    /// </summary>
    private UniversalHealth _enemieHealth;
    /// <summary>
    /// bool que indica si esta equipado
    /// </summary>
    private bool _equip;
    /// <summary>
    /// bool que indica si es un enemigo
    /// </summary>
    private bool _enemie;
 
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
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
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
    /// metodo para equipar un arma
    /// </summary>
    /// <param name="est"> estado de la equipacion</param>
    /// <param name="enemie"> si es enemigo o no</param>
    public void Equip(bool est,bool enemie)
    {
        _equip = est;
        _enemie = enemie;
    }
    public void GetMeleDuration(ref float _duration)
    {
        _duration = MeleDuration;
    }
    public void GetMeleCooldown(ref float _cooldown)
    {
        _cooldown = MeleCooldown;
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
        //Mira quien ataco para si pueden recivir daño
        _enemieHealth = collision.GetComponentInParent<UniversalHealth>();
        if(_enemieHealth != null && _equip)
        {
            if (_enemieHealth.IsPlayer()&& _enemie)
            {
                _enemieHealth.GetDamage(Damage);

            }
            else if (!_enemieHealth.IsPlayer() && !_enemie)
            {
                _enemieHealth.GetDamage(Damage);
                this.gameObject.SetActive(false);
            }
        }
    }
    #endregion   

} // class MeleWeapon 
// namespace
