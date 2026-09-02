# Comparación de Patrones de Diseño

## Factory Method
En el sistema de gestión de tutorías se necesitan diferentes formas para enviar notificaciones, como correo electrónico, SMS, WhatsApp y Telegram. El problema aparece cuando el sistema tiene que decidir directamente qué clase crear dependiendo del tipo de notificación que se quiera enviar.

Si esta parte está mezclada con el resto del sistema, cada vez que se quiera agregar una nueva forma de enviar notificaciones habría que modificar el código que ya existe. Esto puede hacer que el sistema tenga más acoplamiento y que después sea más complicado agregar nuevas opciones.

Por eso se utiliza el patrón Factory Method, ya que permite separar la creación de las notificaciones y dejar que cada clase se encargue de crear el tipo de notificación que corresponde. De esta forma, el resto del sistema puede trabajar con el contrato común `Notificacion` sin tener que conocer directamente cómo se crea cada notificación.

### Implementación
Para implementar este patrón se creó la interfaz `Notificacion`, que sirve como un contrato común para los diferentes tipos de notificaciones. A partir de esta interfaz se crearon `NotificacionEmail`, `NotificacionSMS`, `NotificacionWhatsApp` y `NotificacionTelegram`. También se creó la clase abstracta `NotificacionCreator`, que **define** el método para crear la notificación y tiene la lógica común para enviarla. Luego, cada creador concreto (`EmailCreator`, `SmsCreator`, `WhatsAppCreator` y `TelegramCreator`) se encarga de crear el tipo de notificación que le corresponde.


### Extensibilidad

Como cuarta opción se agregó Telegram, para lo cual se crearon las clases NotificacionTelegram y TelegramCreator. Para agregar esta nueva opción no fue necesario modificar las clases que ya existían, sino crear una nueva implementación del contrato Notificacion y su respectivo creador. Las clases NotificacionCreator y las demás implementaciones se mantienen sin cambios. Esto demuestra que el patrón permite agregar nuevos tipos de notificación sin tener que modificar la lógica que ya existe.

### UML

El diagrama UML correspondiente al patrón Factory Method se encuentra en:

Documentacion/Patrones/FactoryMethod.puml
Documentacion/Patrones/FactoryMethod.png

## Builder

### Problema inicial

La clase `Reserva` contiene varios datos que deben proporcionarse para crear una reserva, como el estudiante, docente, tutoría y horario. Si todos estos datos se reciben directamente mediante un constructor, la creación de objetos puede volverse difícil de leer y mantener, especialmente cuando existen campos opcionales.

Por este motivo se utiliza el patrón Builder, que permite construir una `Reserva` paso a paso, indicando únicamente los datos necesarios y manteniendo la creación del objeto de una forma más clara.

### Implementación

Para implementar este patrón se creó la clase `ReservaBuilder`, que permite configurar la reserva mediante diferentes métodos antes de construirla. Se utilizó una Fluent API, por lo que cada método devuelve el mismo `ReservaBuilder` y permite encadenar las llamadas.

Los campos obligatorios son `Estudiante`, `Docente`, `Tutoria` y `Horario`. Los campos `Id` y `Estado` son opcionales. El estado tiene como valor por defecto `"Pendiente"` y el `Id` utiliza su valor predeterminado de `int`.

Antes de crear la reserva, el método `Build()` valida que todos los campos obligatorios hayan sido proporcionados. Si falta alguno, se genera una excepción indicando qué dato es necesario.

### Configuraciones utilizadas

Se probaron dos configuraciones diferentes de `Reserva`. La primera establece también un `Id`, mientras que la segunda utiliza los valores predeterminados para los campos opcionales. En ambos casos se proporcionaron los campos obligatorios.

También se realizó una prueba de validación intentando construir una reserva sin proporcionar todos los campos obligatorios. El Builder detectó la ausencia del docente y generó la excepción correspondiente.

### UML

El diagrama UML de Builder se encuentra en:

`Documentacion/Patrones/Builder.puml`
`Documentacion/Patrones/Builder.png`


## Comparación técnica

| Criterio                   | Factory Method                                                                                                         | Builder                                                                                                                |
| -------------------------- | ---------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------- |
| **Problema que resuelve**  | Permite controlar la creación de diferentes tipos de notificaciones sin depender directamente de sus clases concretas. | Permite construir una `Reserva` paso a paso cuando tiene varios campos obligatorios y opcionales.                      |
| **Variabilidad principal** | El tipo de objeto que se quiere crear.                                                                                 | La configuración y combinación de los datos del objeto.                                                                |
| **Participantes**          | `Notificacion`, `NotificacionCreator` y sus implementaciones concretas.                                                | `ReservaBuilder` y `Reserva`.                                                                                          |
| **Ventaja principal**      | Facilita agregar nuevos tipos de notificación sin tener que modificar las clases que ya existen.                       | Permite crear los objetos de una forma más clara, flexible y fácil de leer mediante una Fluent API.                    |
| **Costo / consecuencia**   | Aumenta la cantidad de clases, ya que cada variante necesita un producto y un creador concreto.                        | Se necesita una clase Builder adicional y más código para poder construir el objeto.                                   |
| **Cuándo utilizarlo**      | Cuando existen diferentes tipos de objetos y se necesita que su creación pueda cambiar o extenderse.                   | Cuando un objeto tiene varios atributos, especialmente si algunos son opcionales o existen diferentes configuraciones. |
| **Cuándo evitarlo**        | Cuando solo existe un tipo de objeto y no se espera agregar nuevas variantes.                                          | Cuando el objeto tiene pocos atributos y su construcción es sencilla.                                                  |


## Conclusiones

La implementación de Factory Method permitió separar la creación de las notificaciones de la lógica que se encarga de utilizarlas. Esto hace que sea más fácil agregar nuevas formas de notificación sin tener que modificar las implementaciones que ya existen.

Por otro lado, Builder permitió mejorar la forma en que se crean las reservas, evitando tener un constructor con demasiados parámetros y permitiendo agregar los datos poco a poco. Además, la validación que se realiza antes de construir el objeto permite comprobar que la reserva tenga toda la información necesaria.

Aunque los dos patrones ayudan a tener un diseño más flexible, cada uno resuelve un problema diferente. Factory Method se enfoca principalmente en **cómo crear diferentes tipos de objetos**, mientras que Builder se enfoca en **cómo construir un objeto con diferentes configuraciones**.

Con esta comparación se pudo identificar que la elección de un patrón depende del problema que se quiera resolver y no simplemente de agregar un patrón para tener más clases en el sistema.
