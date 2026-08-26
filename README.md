# Sistema de Gestión de Tutorías

## Propósito

Este proyecto consiste en un sistema para gestionar las tutorías entre estudiantes y docentes. La idea es poder registrar a los estudiantes, docentes, tutorías, horarios y reservas, además de enviar una notificación cuando una reserva sea confirmada.

## Descripción del problema

Los estudiantes necesitan poder solicitar una tutoría dependiendo de los horarios que tengan disponibles los docentes. Por eso, el sistema permite registrar la información necesaria para realizar una reserva y también controlar el estado en el que se encuentra.

Además, se separó la parte de las notificaciones mediante una interfaz, para que el servicio de reservas no tenga que depender directamente de una implementación específica.

## Clases principales

* **Administrador:** se encarga de gestionar a los estudiantes y docentes.
* **Estudiante:** representa al estudiante que solicita una tutoría.
* **Docente:** representa al docente que ofrece las tutorías.
* **Tutoria:** representa el tema y la descripción de la tutoría.
* **HorarioTutoria:** representa la fecha, el horario y si está disponible.
* **Reserva:** contiene la información de la reserva y su estado.
* **ServicioReservas:** se encarga de crear y confirmar las reservas.
* **INotificador:** define la operación que se utiliza para enviar las notificaciones.
* **Notificador:** se encarga de implementar el envío de las notificaciones.

## Decisiones de diseño

Se separaron las diferentes responsabilidades en varias clases para que cada una se encargue de una parte específica del sistema y así mantener una mayor cohesión.

En el caso de las reservas, `ServicioReservas` utiliza la interfaz `INotificador` en lugar de utilizar directamente `Notificador`. De esta forma, si después se necesita cambiar la forma en la que se envían las notificaciones, no sería necesario modificar toda la lógica del servicio de reservas.

## Principios SOLID

### SRP - Single Responsibility Principle

Se separaron las responsabilidades entre diferentes clases. Por ejemplo, `Reserva` se encarga de representar los datos de la reserva, `ServicioReservas` de gestionar las reservas y `Notificador` de enviar las notificaciones.

De esta manera cada clase tiene una responsabilidad más específica y no se termina teniendo una sola clase con demasiadas funciones diferentes.

### DIP - Dependency Inversion Principle

`ServicioReservas` no depende directamente de `Notificador`, sino de la interfaz `INotificador`.

Esta dependencia se recibe mediante el constructor, por lo que si más adelante se cambia la forma de enviar las notificaciones, se puede cambiar la implementación sin tener que modificar directamente la lógica de `ServicioReservas`.

## Diagrama UML

El diagrama de clases se encuentra en:

* `Documentacion/modelo-clases.puml`
* `Documentacion/modelo-clases.png`

## Requisitos

* C# / .NET 10
* Visual Studio Code o Visual Studio
* Git

## Ejecución

Para compilar el proyecto se utiliza:

```bash
dotnet build
```

Para ejecutar el proyecto:

```bash
dotnet run
```

## Git

El proyecto utiliza Git para ir registrando los cambios que se van realizando mediante commits descriptivos.

## Uso de inteligencia artificial

Durante el desarrollo de esta actividad utilicé ChatGPT como tutor o guía para poder entender mejor las instrucciones, los conceptos de diseño orientado a objetos, Git y la forma de organizar la documentación.