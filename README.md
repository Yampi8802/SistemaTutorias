# Sistema de Gestión de Tutorías

## Propósito

Este proyecto consiste en un sistema para gestionar las tutorías entre estudiantes y docentes.

El sistema permite representar a los estudiantes, docentes, tutorías, horarios y reservas. También cuenta con diferentes formas para enviar notificaciones y permite gestionar la creación de tutorías virtuales mediante un servicio de videoconferencia.

El proyecto utiliza diferentes patrones de diseño para mejorar la flexibilidad, reducir el acoplamiento y facilitar que el sistema pueda seguir creciendo.

---

## Descripción del problema

Los estudiantes necesitan poder solicitar tutorías de acuerdo con los horarios disponibles de los docentes.

Por esta razón, el sistema permite registrar la información necesaria para realizar una reserva y controlar el estado en el que se encuentra.

Además, el sistema necesita manejar diferentes formas de notificación y permitir la integración con proveedores externos de videoconferencia.

Para evitar dependencias innecesarias entre los componentes, se utilizan abstracciones como `INotificador` y `Videoconferencia`.

El incremento actual incorpora los patrones **Factory Method, Builder, Adapter y Facade**, aplicados a diferentes problemas del sistema.

---

## Clases principales

* **Administrador:** representa al administrador encargado de gestionar la información del sistema.
* **Estudiante:** representa al estudiante que solicita una tutoría.
* **Docente:** representa al docente que ofrece las tutorías.
* **Tutoria:** representa el tema y la descripción de una tutoría.
* **HorarioTutoria:** representa la fecha, horario y disponibilidad de una tutoría.
* **Reserva:** contiene la información de la reserva y su estado.
* **ServicioReservas:** se encarga de gestionar la confirmación de reservas, incluyendo la validación de la disponibilidad del horario.
* **INotificador:** define el contrato que se utiliza para enviar notificaciones.
* **Notificador:** implementa el envío de las notificaciones.

---

## Decisiones de diseño

Se separaron las responsabilidades en diferentes clases para mantener una mayor cohesión y evitar colocar demasiada lógica en una sola clase.

Por ejemplo, `Reserva` representa la información de una reserva, mientras que `ServicioReservas` se encarga del proceso relacionado con su creación y confirmación.

También se utilizaron abstracciones para reducir el acoplamiento.

`ServicioReservas` depende de `INotificador` en lugar de depender directamente de `Notificador`.

De igual manera, `TutoriasFacade` trabaja con la interfaz `Videoconferencia` y no directamente con `ProveedorZoom`.

La confirmación de una reserva también verifica que el horario seleccionado esté disponible antes de cambiar su estado a `"Confirmada"`.

---

# Principios SOLID

## SRP — Single Responsibility Principle

Las clases tienen responsabilidades diferentes.

Por ejemplo:

* `Reserva` representa los datos de una reserva.
* `ServicioReservas` gestiona el proceso relacionado con las reservas.
* `Notificador` se encarga del envío de notificaciones.
* `ZoomAdapter` se encarga de adaptar la interfaz del proveedor Zoom.
* `TutoriasFacade` coordina el proceso de creación de una tutoría virtual.
* `ReservaBuilder` se encarga de construir y validar objetos `Reserva`.

De esta forma se evita concentrar diferentes responsabilidades en una sola clase.

## DIP — Dependency Inversion Principle

`ServicioReservas` depende de la abstracción `INotificador` en lugar de depender directamente de `Notificador`.

La dependencia se recibe mediante el constructor.

De esta forma, la implementación concreta de las notificaciones puede cambiar sin tener que modificar la lógica principal de `ServicioReservas`.

De manera similar, `TutoriasFacade` depende de la abstracción `Videoconferencia` y no directamente de `ProveedorZoom`.

Esto permite cambiar el proveedor de videoconferencia por otra implementación que sea compatible con la interfaz.

---

## Cohesión y acoplamiento

Las responsabilidades se mantienen agrupadas de acuerdo con lo que hace cada componente.

El sistema utiliza interfaces como `INotificador` y `Videoconferencia` para reducir el acoplamiento entre las clases.

Por ejemplo, `ServicioReservas` no necesita conocer cómo funciona internamente `Notificador`.

De igual manera, `TutoriasFacade` no necesita conocer directamente cómo `ProveedorZoom` inicia una reunión, ya que trabaja mediante `Videoconferencia`.

Esto facilita cambiar las implementaciones y seguir agregando nuevas funcionalidades al sistema.

---

# Patrones de diseño utilizados

## Factory Method

Se utiliza para crear diferentes tipos de notificaciones sin que el código cliente tenga que depender directamente de las clases concretas.

Actualmente se incluyen:

* Email
* SMS
* WhatsApp
* Telegram

La estructura utiliza `Notificacion` como producto común y `NotificacionCreator` como creador abstracto.

Las clases concretas son:

* `EmailCreator`
* `SmsCreator`
* `WhatsAppCreator`
* `TelegramCreator`

La incorporación de Telegram demuestra que se puede agregar una nueva variante sin modificar las implementaciones que ya existen del patrón.

---

## Builder

Se utiliza para construir objetos `Reserva` mediante una interfaz fluida.

`ReservaBuilder` permite configurar la reserva paso a paso mediante métodos como:

* `ConId()`
* `ConEstudiante()`
* `ConDocente()`
* `ConTutoria()`
* `ConHorario()`
* `ConEstado()`

Los campos obligatorios son:

* `Estudiante`
* `Docente`
* `Tutoria`
* `Horario`

Los campos `Id` y `Estado` son opcionales.

El estado utiliza `"Pendiente"` como valor predeterminado.

El método `Build()` verifica que los campos obligatorios hayan sido proporcionados antes de crear la reserva.

---

## Adapter

Se utiliza para integrar un proveedor externo de videoconferencias cuyo método de trabajo es diferente al de la interfaz que utiliza el sistema.

La estructura está formada por:

* `Videoconferencia`
* `ProveedorZoom`
* `ZoomAdapter`

`Videoconferencia` define el contrato que necesita el sistema.

`ProveedorZoom` representa al proveedor externo.

`ZoomAdapter` adapta la operación de `ProveedorZoom` al contrato `Videoconferencia`.

De esta forma, el código cliente trabaja con la abstracción `Videoconferencia` sin depender directamente de la implementación del proveedor externo.

---

## Facade

Se utiliza para simplificar el proceso de creación de una tutoría virtual.

La clase `TutoriasFacade` coordina los componentes que se necesitan para realizar este proceso.

Actualmente coordina:

* `ServicioReservas`
* `Videoconferencia`

El cliente utiliza una sola operación:

`CrearTutoriaVirtual(Reserva reserva)`

Internamente, el Facade:

1. Solicita la confirmación de la reserva.
2. Solicita la creación de una reunión mediante `Videoconferencia`.
3. Obtiene el enlace de videoconferencia.
4. Devuelve el enlace al cliente.

De esta forma se oculta parte de la complejidad interna del proceso.

---

# Pertinencia de los patrones

Los patrones fueron seleccionados de acuerdo con los problemas que se identificaron en el sistema.

| Patrón         | Problema real                                                          | Qué cambia                                | Qué permanece estable                            | Principio relacionado | Beneficio esperado                                            | Costo                                   |
| -------------- | ---------------------------------------------------------------------- | ----------------------------------------- | ------------------------------------------------ | --------------------- | ------------------------------------------------------------- | --------------------------------------- |
| Factory Method | Se necesitan diferentes tipos de notificación.                         | El tipo concreto de notificación.         | El contrato de creación y uso de notificaciones. | OCP                   | Facilita agregar nuevos tipos.                                | Agrega creadores y productos concretos. |
| Builder        | `Reserva` contiene varios atributos y algunos son obligatorios.        | La forma en que se construye la reserva.  | La clase `Reserva` y su estructura.              | SRP                   | Facilita una construcción clara y validada.                   | Agrega una clase Builder.               |
| Adapter        | El proveedor externo de videoconferencia tiene una interfaz diferente. | La forma de acceder al proveedor externo. | El contrato `Videoconferencia`.                  | DIP                   | Permite integrar proveedores sin modificar su implementación. | Agrega una clase Adapter.               |
| Facade         | Crear una tutoría virtual requiere coordinar varios componentes.       | La forma de acceder al proceso completo.  | Los servicios internos.                          | SRP                   | Simplifica el uso del sistema.                                | Agrega una clase Facade.                |




### Verificación de los patrones

Los patrones fueron verificados mediante pruebas ejecutadas desde `Program.cs`.

* **Factory Method:** se probaron Email, SMS, WhatsApp y Telegram.
* **Builder:** se probaron reservas válidas y la validación de los campos obligatorios.
* **Adapter:** se verificó la creación de una reunión mediante `ZoomAdapter`.
* **Facade:** se verificó la coordinación entre `ServicioReservas` y `Videoconferencia`.

---

# Diagrama UML

El diagrama actualizado del sistema se encuentra en:

* `Documentacion/modelo-clases.puml`
* `Documentacion/modelo-clases.png`

El diagrama representa las principales clases del dominio, servicios, abstracciones y patrones de diseño utilizados en el sistema.

---

# Documentación de patrones

Los diagramas individuales de los patrones se encuentran en:

### Factory Method

* `Documentacion/Patrones/FactoryMethod.puml`
* `Documentacion/Patrones/FactoryMethod.png`

### Builder

* `Documentacion/Patrones/Builder.puml`
* `Documentacion/Patrones/Builder.png`

### Adapter

* `Documentacion/Patrones/Adapter/Adapter.puml`
* `Documentacion/Patrones/Adapter/Adapter.png`

### Facade

* `Documentacion/Patrones/Facade/Facade.puml`
* `Documentacion/Patrones/Facade/Facade.png`

La documentación y comparación de los patrones se encuentra en:

* `Documentacion/ComparacionPatrones.md`

---

# Estructura del proyecto

```text
SistemaTutoria/
│
├── Program.cs
├── README.md
├── SistemaTutoria.csproj
│
├── Dominio/
│   ├── Administrador.cs
│   ├── Docente.cs
│   ├── Estudiante.cs
│   ├── HorarioTutoria.cs
│   ├── Reserva.cs
│   └── Tutoria.cs
│
├── Servicios/
│   └── ServicioReservas.cs
│
├── Notificaciones/
│   ├── INotificador.cs
│   └── Notificador.cs
│
├── Patrones/
│   ├── Builder/
│   │   └── ReservaBuilder.cs
│   │
│   └── Factory/
│       ├── Notificacion.cs
│       ├── NotificacionCreator.cs
│       ├── EmailCreator.cs
│       ├── SmsCreator.cs
│       ├── WhatsAppCreator.cs
│       ├── TelegramCreator.cs
│       ├── NotificacionEmail.cs
│       ├── NotificacionSMS.cs
│       ├── NotificacionWhatsApp.cs
│       └── NotificacionTelegram.cs
│
└── Documentacion/
    ├── AnalisisDominio.md
    ├── ComparacionPatrones.md
    ├── modelo-clases.puml
    ├── modelo-clases.png
    │
    └── Patrones/
        ├── Builder.png
        ├── Builder.puml
        ├── FactoryMethod.png
        ├── FactoryMethod.puml
        │
        ├── Adapter/
        │   ├── Adapter.puml
        │   ├── Adapter.png
        │   ├── ProveedorZoom.cs
        │   ├── Videoconferencia.cs
        │   └── ZoomAdapter.cs
        │
        └── Facade/
            ├── Facade.puml
            ├── Facade.png
            └── TutoriasFacade.cs

```

---

# Requisitos

Para ejecutar el proyecto se necesita:

* Windows 10/11.
* .NET SDK 10.0 o superior compatible con el proyecto.
* Visual Studio Code, Visual Studio u otro editor compatible con C#.

El proyecto utiliza **C# y .NET 10**.

---

# Ejecución

Desde la carpeta raíz del proyecto, ejecutar:

```bash
dotnet build

```

Si la compilación termina correctamente, ejecutar:

```bash
dotnet run

```

El programa mostrará en consola las pruebas de los patrones Factory Method, Builder, Adapter y Facade.

---

# Verificación

Se verificó la compilación y ejecución del proyecto mediante:

```bash
dotnet build

```

La compilación finalizó correctamente.

También se verificó la ejecución mediante:

```bash
dotnet run

```

Durante la ejecución se comprobó:

* Envío de notificaciones mediante Factory Method.
* Creación de reservas mediante Builder.
* Validación de campos obligatorios del Builder.
* Integración del proveedor Zoom mediante Adapter.
* Coordinación del proceso de tutoría virtual mediante Facade.
* Generación del enlace de videoconferencia.
* Validación de disponibilidad del horario antes de confirmar una reserva.

### Prueba de horario disponible

Cuando el horario tiene:

```text
Disponible = true

```

la reserva puede confirmarse correctamente y se crea la tutoría virtual.

Resultado:

```text
Estado de la reserva: Confirmada
Tutoría virtual creada.
Enlace: https://zoom.us/j/123456789

```

### Prueba de horario no disponible

Cuando el horario tiene:

```text
Disponible = false

```

el sistema no permite confirmar la reserva y genera una excepción:

```text
System.InvalidOperationException:
No se puede confirmar la reserva porque el horario no está disponible.

```

Esta prueba permite comprobar que una de las reglas de negocio identificadas en el análisis del dominio está implementada en el sistema.

### Resultado de ejecución principal

```text
=== PRUEBA FACTORY METHOD ===
[EMAIL] Enviando a jean@email.com: Su tutoría ha sido confirmada.
[SMS] Enviando a 0999999999: Su tutoría ha sido confirmada.
[WHATSAPP] Enviando a 0999999999: Su tutoría ha sido confirmada.
[TELEGRAM] Enviando a usuario_telegram: Su tutoría ha sido confirmada.

=== PRUEBA BUILDER ===
Reserva creada con Builder. Id: 2
Estado: Pendiente
Segunda reserva creada con Builder. Id: 0
Estado: Pendiente
Validación: El docente es obligatorio.

=== PRUEBA FACADE + ADAPTER ===
Notificación enviada a jean@email.com: Su reserva de tutoría sobre Programación en C# ha sido confirmada.
Zoom: iniciando reunión 'Tutoria de Programación en C#'.
Estado de la reserva: Confirmada
Tutoría virtual creada.
Enlace: https://zoom.us/j/123456789

=== FIN DE PRUEBAS ===

```

También se ejecutó:

```bash
dotnet clean

```

para comprobar que el proyecto puede limpiarse correctamente.

---

# Git y GitHub

El proyecto utiliza Git para registrar la evolución del desarrollo.

Los cambios realizados durante las diferentes etapas se registran mediante commits, permitiendo identificar la incorporación progresiva de los patrones de diseño y las correcciones realizadas.

Entre los commits relevantes se encuentra:

```text
0051f9b fix: validar disponibilidad del horario

```

Este commit incorpora la validación que evita confirmar una reserva cuando el horario seleccionado no está disponible.

Repositorio:

https://github.com/Yampi8802/SistemaTutorias

---

# Uso de inteligencia artificial

Durante el desarrollo de esta actividad utilicé ChatGPT como herramienta de apoyo para comprender mejor las instrucciones, los conceptos de diseño orientado a objetos, los patrones de diseño, Git y la organización de la documentación.

Las implementaciones fueron revisadas, ejecutadas y verificadas en el entorno de desarrollo para comprobar que funcionaran correctamente.

La decisión final sobre la estructura del proyecto, las implementaciones y las modificaciones realizadas fue revisada durante el desarrollo del trabajo.
