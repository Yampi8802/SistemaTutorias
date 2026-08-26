## Análisis del dominio

### Sistema de gestión de tutorías

El sistema va a permitir gestionar las tutorías entre los estudiantes y los docentes. Los estudiantes pueden solicitar una tutoría y hacer reservas, mientras que los docentes pueden administrar los horarios que tienen disponibles. Las reservas permiten registrar la tutoría y saber en qué estado se encuentra. Además, el sistema puede enviar notificaciones cuando ocurre algún evento importante relacionado con una reserva.

### Elementos del dominio

| Elemento / clase candidata | Responsabilidad | Información relevante | Reglas / colaboraciones |
|---|---|---|---|
| Administrador | Registrar y gestionar a los estudiantes y docentes. | Id, nombre | Se relaciona con estudiantes y docentes. |
| Estudiante | Solicitar tutorías y realizar reservas. | Id, nombre, correo | Puede hacer reservas y recibir notificaciones. |
| Docente | Ofrecer tutorías y administrar sus horarios disponibles. | Id, nombre, especialidad | Puede publicar sus horarios y participar en las reservas. |
| Tutoría | Representar el tema o servicio de tutoría que se va a realizar. | Id, tema, descripción | El estudiante la selecciona cuando realiza una reserva. |
| HorarioTutoría | Representar los horarios que están disponibles para una tutoría. | Id, fecha, hora inicio, hora fin, disponibilidad | El horario puede estar disponible o no disponible. |
| Reserva | Registrar la solicitud de una tutoría y saber en qué estado está. | Id, estudiante, docente, tutoría, horario, estado | Puede estar confirmada, cancelada o reprogramada. |
| Notificador | Enviar información cuando ocurre algún evento importante relacionado con una reserva. | Destinatario, mensaje | Se utiliza para las notificaciones sin depender directamente de una tecnología específica. |

### Reglas relevantes

1. Una reserva debe estar relacionada con un estudiante.
2. También debe estar relacionada con un docente.
3. La reserva debe indicar qué tutoría seleccionó el estudiante.
4. La reserva debe utilizar un horario de tutoría.
5. El horario debe indicar si está disponible o no.
6. Una reserva puede tener diferentes estados, por ejemplo, pendiente, confirmada o cancelada.
7. Para confirmar una reserva, el horario que se va a utilizar debe estar disponible.
8. Cuando ocurre algún evento importante en una reserva, se puede enviar una notificación al estudiante.
9. La lógica del sistema no debería depender directamente de una tecnología específica para enviar notificaciones o guardar la información.