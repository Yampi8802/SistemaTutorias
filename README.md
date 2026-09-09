# Sistema de Gestión de Tutorías

## Propósito

Este proyecto consiste en un sistema para gestionar las tutorías entre estudiantes y docentes.

El sistema permite representar estudiantes, docentes, tutorías, horarios y reservas. Además, incorpora mecanismos para enviar notificaciones y gestionar la creación de tutorías virtuales mediante una videoconferencia.

El proyecto integra diferentes patrones de diseño para mejorar la flexibilidad, reducir el acoplamiento y facilitar la evolución del sistema.

---

## Descripción del problema

Los estudiantes necesitan poder solicitar tutorías dependiendo de los horarios disponibles de los docentes.

Por esta razón, el sistema permite registrar la información necesaria para realizar una reserva y controlar el estado en el que se encuentra.

Además, el sistema necesita manejar diferentes formas de notificación y permitir la integración con proveedores externos de videoconferencia.

Para evitar dependencias innecesarias entre componentes, se utilizan abstracciones como `INotificador` y `Videoconferencia`.

---

## Clases principales

- **Administrador:** representa al administrador encargado de gestionar información del sistema.
- **Estudiante:** representa al estudiante que solicita una tutoría.
- **Docente:** representa al docente que ofrece las tutorías.
- **Tutoria:** representa el tema y la descripción de una tutoría.
- **HorarioTutoria:** representa la fecha, horario y disponibilidad de una tutoría.
- **Reserva:** contiene la información de la reserva y su estado.
- **ServicioReservas:** se encarga de gestionar la creación y confirmación de reservas.
- **INotificador:** define el contrato utilizado para enviar notificaciones.
- **Notificador:** implementa el envío de notificaciones.

---

## Decisiones de diseño

Se separaron las responsabilidades en diferentes clases para mantener una mayor cohesión y evitar concentrar demasiada lógica en una sola clase.

Por ejemplo, `Reserva` representa la información de una reserva, mientras que `ServicioReservas` gestiona el proceso relacionado con su creación.

También se utilizaron abstracciones para reducir el acoplamiento.

`ServicioReservas` depende de `INotificador` en lugar de depender directamente de `Notificador`.

De manera similar, `TutoriasFacade` trabaja con la interfaz `Videoconferencia` y no directamente con `ProveedorZoom`.

---

# Principios SOLID

## SRP — Single Responsibility Principle

Las clases tienen responsabilidades diferenciadas.

Por ejemplo:

- `Reserva` representa los datos de una reserva.
- `ServicioReservas` gestiona el proceso relacionado con las reservas.
- `Notificador` se encarga del envío de notificaciones.
- `ZoomAdapter` se encarga de adaptar la interfaz del proveedor Zoom.
- `TutoriasFacade` coordina el proceso de creación de una tutoría virtual.

De esta manera se evita concentrar diferentes responsabilidades en una sola clase.

## DIP — Dependency Inversion Principle

`ServicioReservas` depende de la abstracción `INotificador` en lugar de depender directamente de `Notificador`.

La dependencia se recibe mediante el constructor.

De esta manera, la implementación concreta de las notificaciones puede cambiar sin modificar la lógica principal de `ServicioReservas`.

De forma similar, `TutoriasFacade` depende de la abstracción `Videoconferencia` y no directamente de `ProveedorZoom`.

Esto permite sustituir el proveedor de videoconferencia mediante otra implementación compatible con la interfaz.

---

## Cohesión y acoplamiento

Las responsabilidades se mantienen agrupadas de acuerdo con el propósito de cada componente.

El sistema utiliza interfaces como `INotificador` y `Videoconferencia` para reducir el acoplamiento entre las clases.

Por ejemplo, `ServicioReservas` no necesita conocer los detalles internos de `Notificador`.

De igual manera, `TutoriasFacade` no necesita conocer directamente cómo `ProveedorZoom` inicia una reunión, ya que trabaja mediante `Videoconferencia`.

Esto facilita la sustitución y evolución de las implementaciones.

---

# Patrones de diseño utilizados

## Factory Method

Se utiliza para crear diferentes tipos de notificaciones sin que el código cliente tenga que depender directamente de las clases concretas.

Actualmente se incluyen:

- Email
- SMS
- WhatsApp
- Telegram

La estructura utiliza `Notificacion` como producto común y `NotificacionCreator` como creador abstracto.

Las clases concretas son:

- `EmailCreator`
- `SmsCreator`
- `WhatsAppCreator`
- `TelegramCreator`

La incorporación de Telegram demuestra que es posible agregar una nueva variante sin modificar las implementaciones existentes del patrón.

---

## Builder

Se utiliza para construir objetos `Reserva` mediante una interfaz fluida.

`ReservaBuilder` permite configurar la reserva paso a paso mediante métodos como:

- `ConId()`
- `ConEstudiante()`
- `ConDocente()`
- `ConTutoria()`
- `ConHorario()`
- `ConEstado()`

Los campos obligatorios son:

- `Estudiante`
- `Docente`
- `Tutoria`
- `Horario`

Los campos `Id` y `Estado` son opcionales.

El estado utiliza `"Pendiente"` como valor predeterminado.

El método `Build()` valida que los campos obligatorios hayan sido proporcionados antes de crear la reserva.

---

## Adapter

Se utiliza para integrar un proveedor externo de videoconferencias cuyo método de trabajo no coincide directamente con la interfaz utilizada por el sistema.

La estructura está formada por:

- `Videoconferencia`
- `ProveedorZoom`
- `ZoomAdapter`

`Videoconferencia` define el contrato que espera el sistema.

`ProveedorZoom` representa el proveedor externo.

`ZoomAdapter` adapta la operación de `ProveedorZoom` al contrato `Videoconferencia`.

De esta forma, el código cliente trabaja con la abstracción `Videoconferencia` sin depender directamente de la implementación del proveedor externo.

---

## Facade

Se utiliza para simplificar el proceso de creación de una tutoría virtual.

La clase `TutoriasFacade` coordina los componentes necesarios para realizar el proceso.

Actualmente coordina:

- `ServicioReservas`
- `Videoconferencia`

El cliente utiliza una única operación:

`CrearTutoriaVirtual(Reserva reserva)`

Internamente, el Facade:

1. Crea y confirma la reserva.
2. Solicita la creación de una reunión.
3. Obtiene el enlace de videoconferencia.
4. Devuelve el enlace al cliente.

Esto permite ocultar parte de la complejidad interna del proceso.

---

# Pertinencia de los patrones

Los patrones fueron seleccionados de acuerdo con problemas concretos identificados en el sistema.

### Factory Method

Se utiliza porque el sistema necesita crear diferentes tipos de notificaciones. El patrón permite encapsular la creación de cada tipo de notificación y facilita agregar nuevas variantes.

### Builder

Se utiliza porque una `Reserva` contiene varios atributos y algunos son obligatorios. El patrón permite construir el objeto paso a paso y centralizar las validaciones necesarias.

### Adapter

Se utiliza porque un proveedor externo de videoconferencia puede tener una interfaz diferente a la que utiliza el sistema. El Adapter permite integrar dicho proveedor sin modificar su implementación.

### Facade

Se utiliza porque la creación de una tutoría virtual requiere coordinar diferentes componentes. El Facade proporciona una operación sencilla al cliente y oculta la complejidad interna.

---

# Diagrama UML

El diagrama actualizado del sistema se encuentra en:

- `Documentacion/modelo-clases.puml`
- `Documentacion/modelo-clases.png`

El diagrama representa las principales clases del dominio, servicios, abstracciones y patrones de diseño utilizados en el sistema.

---

# Documentación de patrones

Los diagramas individuales de los patrones se encuentran en:

### Factory Method

- `Documentacion/Patrones/FactoryMethod.puml`
- `Documentacion/Patrones/FactoryMethod.png`

### Builder

- `Documentacion/Patrones/Builder.puml`
- `Documentacion/Patrones/Builder.png`

### Adapter

- `Documentacion/Patrones/Adapter/Adapter.puml`
- `Documentacion/Patrones/Adapter/Adapter.png`

### Facade

- `Documentacion/Patrones/Facade/Facade.puml`
- `Documentacion/Patrones/Facade/Facade.png`

La documentación y comparación de los patrones se encuentra en:

- `Documentacion/ComparacionPatrones.md`

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
        ├── Adapter/
        │   ├── Adapter.puml
        │   ├── Adapter.png
        │   ├── Videoconferencia.cs
        │   ├── ProveedorZoom.cs
        │   └── ZoomAdapter.cs
        │
        ├── Facade/
        │   ├── Facade.puml
        │   ├── Facade.png
        │   └── TutoriasFacade.cs
        │
        ├── FactoryMethod.puml
        ├── FactoryMethod.png
        ├── Builder.puml
        └── Builder.png
```

---

# Requisitos

Para ejecutar el proyecto se necesita:

- Windows 10/11.
- .NET SDK 10.0 o superior compatible con el proyecto.
- Visual Studio Code, Visual Studio u otro editor compatible con C#.

El proyecto utiliza C# y .NET 10.

---

# Ejecución

Desde la carpeta raíz del proyecto, ejecutar:

```bash
dotnet build
```

Si la compilación finaliza correctamente, ejecutar:

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

Resultado:

```text
Restauración completada (0,4s)
SistemaTutoria net10.0 realizado correctamente

Compilación realizado correctamente en 1,2s
```

También se verificó la ejecución mediante:

```bash
dotnet run
```

Durante la ejecución se comprobó:

- Envío de notificaciones mediante Factory Method.
- Creación de reservas mediante Builder.
- Validación de campos obligatorios del Builder.
- Integración del proveedor Zoom mediante Adapter.
- Coordinación del proceso de tutoría virtual mediante Facade.
- Generación del enlace de videoconferencia.

La ejecución produjo, entre otros, los siguientes resultados:

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

Finalmente, se ejecutó:

```bash
dotnet clean
```

para comprobar que el proyecto puede limpiarse correctamente.

---

# Git y GitHub

El proyecto utiliza Git para registrar la evolución incremental del desarrollo.

Los cambios realizados durante las diferentes etapas se registran mediante commits, permitiendo identificar la incorporación progresiva de los patrones de diseño.

Repositorio:

https://github.com/Yampi8802/SistemaTutorias

---

# Uso de inteligencia artificial

Durante el desarrollo de esta actividad utilicé ChatGPT como tutor o guía para poder entender mejor las instrucciones, los conceptos de diseño orientado a objetos, los patrones de diseño, Git y la organización de la documentación.

Las implementaciones fueron revisadas y ejecutadas en el entorno de desarrollo para comprobar su funcionamiento.