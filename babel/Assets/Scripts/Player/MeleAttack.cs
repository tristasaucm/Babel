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
public class MeleAttack : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// GameObject del Puntero 
    /// </summary>
    [SerializeField] private GameObject Peephole;
    /// <summary>
    /// GameObject del arma mele
    /// </summary>
    [SerializeField] private GameObject MeleWeapon;
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
    /// Posccion del puntero 
    /// </summary>
    private Vector2 _peepholePosition;
    /// <summary>
    /// El transform del puntero
    /// </summary>
    private Transform _tranformPeephole;
    /// <summary>
    /// El transform del arma
    /// </summary>
    private Transform _transformWeapon;
    /// <summary>
    /// Bool que indica si puede atacar
    /// </summary>
    private bool _canAttack;
    /// <summary>
    /// Bool que indica si el jugador se encuentra atacando
    /// </summary>
    private bool _isAttaking;
    /// <summary>
    /// Dirección a la que apunta el mouse
    /// </summary>
    private Vector2 _dir;
    /// <summary>
    /// Tiempo en el que se ha desactivado el sprint
    /// </summary>
    private float _AttackFinishTime;
    /// <summary>
    /// Tiempo en el que se ha activado el sprint
    /// </summary>
    private float _AttackStartTime;
    /// <summary>
    /// bool que indica si el sprint esta activo
    /// </summary>
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
        _tranformPeephole = Peephole.GetComponent<Transform>();
        _transformWeapon = MeleWeapon.GetComponent<Transform>();
        _isAttaking = false;
        _canAttack = true;
        MeleWeapon.SetActive(false);

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        PeepholeUpdate();
        if (_canAttack)
        {
            if (MeleCooldown < Time.time - _AttackFinishTime)
            {
                Atack();

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

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    /// Actualiza la posición del puntero en el mundo
    /// </summary>
    private void PeepholeUpdate()
    {
        //Miramos posicion del mouse
        _peepholePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Por si hay problemas de donde se encuentra
        //Debug.Log(_cursorPosition.x);
        //Debug.Log(_cursorPosition.y);
        //Ponemos la mira en la posicion obtenida
        _tranformPeephole.position = _peepholePosition;
    }
    /// <summary>
    /// Activa el ataque del Jugador
    /// </summary>
    private void Atack()
    {
        //Miramos la direccion que queremos atacar
        _dir = Input.mousePosition;
        //Metodo a revisar para que quede mejor a futuro
        int Quadrant = GetQuadrant(_dir);
        //Debug.Log(Quadrant);
        //Generamos el ataque en la dirección
        if (!_isAttaking && InputManager.Instance.AttackWasPressedThisFrame())
        {
            _isAttaking = true;
            _AttackStartTime = Time.time;
            GenerateWeapon(Quadrant);
        }
        else if (_isAttaking && MeleDuration < Time.time - _AttackStartTime)
        {
            _AttackFinishTime = Time.time;
            _isAttaking = false;
            MeleWeapon.SetActive(false);
        }

    }
    /// <summary>
    /// Metodo que representa y coloca el arma en la posicion correspondiente con el jugador
    /// </summary>
    /// <param name="Quadrant">Quadrante en el que se encuentra el arma </param>
    private void GenerateWeapon(int Quadrant)
    {
        MeleWeapon.SetActive(true);
        Vector2 tranform = this.transform.position;
        Quaternion Rotation = Quaternion.Euler(0, 0, 0);
        switch (Quadrant)
        {
            case 1: tranform.y++; Rotation = Quaternion.Euler(0, 0, 0); break;
            case 2: tranform.y += 0.5f; tranform.x -= 0.5f; Rotation = Quaternion.Euler(0, 0, 45); break;
            case 3: tranform.x--; Rotation = Quaternion.Euler(0, 0, 90); break;
            case 4: tranform.y -= 0.5f; tranform.x -= 0.5f; Rotation = Quaternion.Euler(0, 0, 135); break;
            case 5: tranform.y--; Rotation = Quaternion.Euler(0, 0, 180); break;
            case 6: tranform.y -= 0.5f; tranform.x += 0.5f; Rotation = Quaternion.Euler(0, 0, -135); break;
            case 7: tranform.x++; Rotation = Quaternion.Euler(0, 0, -90); break;
            case 8: tranform.y += 0.5f; tranform.x += 0.5f; Rotation = Quaternion.Euler(0, 0, -45); break;


        }
        _transformWeapon.position = tranform;
        _transformWeapon.rotation = Rotation;
    }
    /// <summary>
    /// Metodo para obtener en que cuadrante se encuentra el puntero
    /// </summary>
    /// <param name="dir">Dirección del puntero</param>
    /// <returns></returns>
    private int GetQuadrant(Vector2 dir)
    {
        int dedzoneLat = 150;
        int dedzoneUp = 50;
        int Quadrant = 0;
        int ScreenWidth = Screen.width;
        int ScreenHeight = Screen.height;
        if (dir.y - dedzoneUp > ScreenHeight / 2)
        {
            if (dir.x - dedzoneLat > ScreenWidth / 2)
            {
                Quadrant = 8;
            }
            else if (dir.x + dedzoneLat < ScreenWidth / 2)
            {
                Quadrant = 2;
            }
            else
            {
                Quadrant = 1;
            }
        }
        else if (dir.y + 3 * dedzoneUp < ScreenHeight / 2)
        {
            if (dir.x - dedzoneLat > ScreenWidth / 2)
            {
                Quadrant = 6;
            }
            else if (dir.x + dedzoneLat < ScreenWidth / 2)
            {
                Quadrant = 4;
            }
            else
            {
                Quadrant = 5;
            }
        }
        else
        {
            if (dir.x > ScreenWidth / 2)
            {
                Quadrant = 7;
            }
            else
            {
                Quadrant = 3;
            }
        }

        return Quadrant;
    }
    #endregion

} // class MeleAttack 
// namespace
