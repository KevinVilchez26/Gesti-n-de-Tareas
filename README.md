# Gestión de Tareas (To-Do App) - Reflexión y Explicación Técnica

Este repositorio contiene una aplicación de consola en C# .NET que ilustra la implementación de **5 patrones de diseño Gang of Four (GoF)**. A continuación, se presenta una **reflexión teórica** sobre los patrones de diseño en general y una **explicación técnica detallada** de cómo cada patrón se implementó en este proyecto específico.

---

## 🧐 Reflexión Teórica sobre los Patrones de Diseño

Los patrones de diseño representan soluciones probadas a problemas concurrentes en el desarrollo de software. Introducidos por el *Gang of Four* en 1994, proveen un lenguaje común para los desarrolladores y catalogan las "buenas ideas" en la arquitectura de un sistema.

Adoptar patrones de diseño promueve la creación de software altamente escalable y mantenible. En lugar de resolver problemas desde cero mediante heurísticas impredecibles, los patrones proporcionan plantillas estructurales que dictan cómo los objetos interactúan. Esta aproximación minimiza la deuda técnica:
1. **Promueven el principio Abierto/Cerrado (OCP)**: Permitiendo añadir nuevas funcionalidades extendiendo, no modificando código existente.
2. **Alta Cohesión y Bajo Acoplamiento**: Los patrones separan responsabilidades en múltiples clases y minimizan la dependencia, lo que facilita el Testing y la mantenibilidad.

Su gran ventaja radica en la reutilización de conceptos en lugar de simplemente reutilizar código. Por consiguiente, fomentan un ecosistema estructurado e inteligible, esencial al operar en equipos a gran escala.

---

## 🛠️ Explicación Técnica de las Implementaciones

### 1. Singleton (Patrón Creacional)
**Concepto Teórico:** Garantiza que una clase posea únicamente una instancia y provee un punto de acceso global a esta. 
**Implementación Técnica:**
En este proyecto, se implementa a través de la clase `TaskManager` (ubicada en `Patterns/Singleton/`). Al ser el corazón de la aplicación, el `TaskManager` posee la lista exhaustiva de las tareas.
* **Componentes clave:**
  * Constructor estricto como `private` para impedir `new TaskManager()`.
  * Objeto dinámico `_instance` con control sincrónico (`lock (_padlock)`) para ser estrictamente *thread-safe* y evitar el fenómeno de *Race Condition* en accesos concurrentes.
  * Punto global controlado: `public static TaskManager Instance { get; }`.
El patrón otorga a la UI (`ConsoleView`) el acceso único sin inyecciones complejas, coordinando uniformemente el almacenamiento y carga en JSON (`StorageService`).

### 2. Factory Method (Patrón Creacional)
**Concepto Teórico:** Oculta la lógica de instanciación al cliente y delega la creación a una estructura predefinida.
**Implementación Técnica:**
Implementado en `TaskFactory` (`Patterns/Factory/`). Se expone el método estático `CreateTask`.
* **Componentes clave:**
  * Crea orgánicamente la instanciación principal de la interfaz `ITask` originando explícitamente `SimpleTask`.
  * La magia funcional estriba en que esta Factory está acoplada al patrón **Decorator**. Dependiendo de los parámetros de `CreateTask` (ej. si se proporciona `priority` o `tags`), la Factory crea las envolventes subyacentes. 
  * Por ejemplo: si hay múltiples configuraciones, se evita que `ConsoleView` maneje los flujos de decoración (`new TagDecorator(new PriorityDecorator(...))`), la interconexión se abstrae dentro de `TaskFactory`.

### 3. Observer (Patrón de Comportamiento)
**Concepto Teórico:** Establece una dependencia de 1 a N. Si el estado del Sujeto cambia, todos sus observadores dependientes son notificados y actualizados autónomamente.
**Implementación Técnica:**
El **Sujeto** es el `TaskManager`. Los observadores implementan `ITaskObserver` (`Patterns/Observer/`).
* **Componentes clave:**
  * `ITaskObserver`: Interfaz con el método `Update(string message)`.
  * El Sujeto implementa `Attach()`, `Detach()` y `Notify()`.
  * `ConsoleNotificationObserver`: Es el observador concreto adscrito al Singleton desde la UI. Cada vez que el gestor cambia su estado (se añade, completa o elimina una tarea), invoca un bucle notificando un mensaje con la letra cian en la consola sin interrumpir el flujo imperativo y previniendo el acoplamiento cruzado de notificación estricta.

### 4. Strategy (Patrón de Comportamiento)
**Concepto Teórico:** Encapsula diferentes familias de algoritmos y los hace intercambiables dentro del contexto de manera polimórfica sin modificar el objeto consumidor.
**Implementación Técnica:**
Aplica a la visualización organizada de las tareas en consola.
* **Componentes clave:**
  * Contrato `ISortStrategy` en `Patterns/Strategy/` con `Sort(IEnumerable<ITask> tasks)`.
  * Tres estrategias concretas: `SortByDateStrategy` (por defecto de LINQ), `SortByStatusStrategy` (concede el orden basado en bool `IsCompleted`) y `SortByPriorityStrategy` (algoritmo numérico custom).
  * El método `GetTasks(ISortStrategy sortStrategy)` en el contexto (`TaskManager`) infiere polimorfismo dinámico en tiempo de ejecución. Así, la UI escoge en la consola enviar "X" estrategia y la lista es procesada matemáticamente de formas distintas abstraídas.

### 5. Decorator (Patrón Estructural)
**Concepto Teórico:** Agrega dinámicamente nuevas responsabilidades o estado a un objeto sin requerir herencia directa mutando sus componentes abstractos.
**Implementación Técnica:**
* **Componentes clave:**
  * Componente base: Interfaz de abstracción `ITask`.
  * Componente concreto base: `SimpleTask`.
  * Clase Abstracta Decoradora intermedia: `TaskDecorator`, que mantiene internamente un campo `protected internal ITask _task;` (Asegurando encapsulamiento base).
  * Decoradores concretos: `PriorityDecorator` y `TagDecorator`.
  * **Uso real:** Al interceptar un método o una propiedad getter, el decorador infiere sobre el estado original. Por ejemplo, en `GetDetails()`, `TagDecorator` añade la interpolación `#{_tags}` a la invocación del objeto atado, resultando en composiciones limpias como `[Alta] [ ] Mi Tarea | Descripción #Trabajo`. Las responsabilidades se empaquetan de tal manera que ninguna de las nuevas lógicas corrompe o satura la clase fundamental natural de `SimpleTask`.