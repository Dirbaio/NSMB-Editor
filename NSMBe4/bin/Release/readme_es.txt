Treeki's editor para New Super Mario Bros
Version 5.0 - publicado 29 de Noviembre de 2009
http://jul.rustedlogic.net/thread.php?id=5814
----------
Gracias por haber bajado mi nuevo editor.
Este editor te deja editar niveles y el sistema de archivos en New Super Mario Bros.
Soporta edición de objetos, sprites/enemigos, entradas/salidas, vistas, rutas, y mas.

Desde versión 4.3, puedes usar el editor en español también. Ejecuta el editor, haz clic en "Options" y escoge Español como el idioma. Tendrás que cerrar y abrir el editor para que el idioma cambie.
Ten en cuenta que mi español no es perfecto - probablemente habrá faltas en la traducción, pero se podra usar.

----------
Cambios:

v4.0 - 13 de Abril 2009:
- Editor publicado.
v4.1 - 13 de Abril 2009:
- Ahora soporta ROMs de diferentes regiones.
v4.2 - 19 de Abril 2009:
- Un problema con ROMs de diferentes regiones arreglado.
- Sprites/enemigos estan en una lista. Algunos estarán puestos mal; por favor informarme si encuentras una falta. (Pueden haber mas en la lista español, porque esta traduccida de ingles.) Gracias a Pirahnaplant para la lista!
v4.3 - 26 de Abril 2009:
- ¡La actualización más grande hasta ahora!
- Edición de entradas/salidas ahora trabaja.
- Puedes ver vistas, pero no cambiarlas.
- Soporte para diferente idiomas añadido; español es usable.
- Opciones del nivel se pueden cambiar, incluso serie de objetos y fondos.
- Puedes usar el botón derecho del raton para mover lo que puedes ver del nivel.
v4.4 - 13 de Mayo 2009:
- Nueva opción para poder hacer niveles como 7-2 y los de Mario vs. Luigi donde puede pasar por los lados del nivel.
- Versión alternativa incluida que trabaja con Mono, para Linux y Mac OS.
v4.5 - 23 de Mayo 2009:
- Buscador de data añadido bajo Opciones. Esta herramienta hace que buscando mas data sobre el juego sea mas facil para hackeadores avanzado.
- Surtidos de sprites ahora se pueden escoger, para que puedas escoger que sprites trabajan en un nivel. Quieres poner Bowser en 1-1? Hacerlo, pero no digas nada cuando el juego se parte en medio batalla ;)
- Graficos en Mono ya trabajan bien.
v4.6 - 14 de Junio 2009:
- Todos los niveles deberian de trabajar sin problemas.
- Archivos del nivel ahora se pueden editar en hex sin otro programa.
- Ahora puedes borrar todos los objetos o sprites en un nivel (bajo Opciones en el editor).
v4.7 - 29 de Junio 2009:
- La función para comprimir un archivo en LZ ahora trabaja.
- Data para algunos objetos actualizados.
- Puedes cambiar el tamaño de un objeto si tienes la tecla Shift pulsada.
- Puedes hacer una copia de un objeto si tienes la tecla Ctrl pulsada.
v5 - 29 de Noviembre 2009:
- Ya puedes bajar el codigo del editor; gracias a Dirbaio para casi todas las cosas en esta version!
- Vistas se pueden cambiar.
- Rutas se pueden cambiar.
- Rutas de progreso se pueden cambiar.
- Las Zonas de accion de sprites se pueden editar.
- Editor de Rutas de Objetos, con edicion de objetos, map16, graficos y importar desde png
- Nuevo codigo de manejar archivos. Ahora mucho mas rapido
- Muchos cambios menores... Prueba el editor y descubrelos...

----------
Preguntas:
¿Cómo puedo editar niveles?
- Abrir un ROM, escoger un nivel, y cambiar cosas. Si no sabes hacer esto, no deberias estar hackeando ROMs.

¿Data de sprite? ¿Qué es esto?
- Algunos enemigos/sprites tienen opciones que estan configurados aquí - los colores de Koopas, por ejemplo.
  Esta página tiene una lista del data que ya tenemos, pero esta en inglés: http://treeki.shacknet.nu/nsmbsprites.html

¿Por qué están algunos sprites rojos y no en azul?
- Por los límites de memoria en el DS, todos los sprites no trabajan en todos los niveles a la misma vez. Los surtidos específicos que se pueden usar se pueden escoger bajo "Surtidos de Sprites" en "Configuración del Nivel".
  Los sprites que estan en rojo no se pueden escoger en los surtidos de sprites que tienes en el nivel ahora.

Cambie los surtidos de sprites, y ahora el juego se parte en la pantalla "Mundo 1-1", ¿por qué?
- Algunas combinaciones pueden hacer que el juego no trabaje bien - especialmente los jefes.
  Cambia surtidos a 0 hasta que encuentres una combinación que trabaje.

¿Cómo cambio la serie de objetos o el fondo de un nivel?
- Ir a la pantalla del Configuracion de Nivel y escojer los opciones que tu quieras.

¿Cómo cambio entradas/salidas?
- ¡Haz clic en el botón 'Entrances', y cambialos! El formato es confuso, si no entiendes algo o no te trabajan bien, mira en otros niveles para ver como trabaja lo que tu quieres. (Por ahora, me parecen que tuberias conectadas (como en 7-A) no trabajan en otros niveles.)

He movido una tuberia; ¿por qué no trabaja?
- Crear una entrada/salida para la tuberia.

Cambie el tamaño del nivel; ¿por qué no puedo entrar en la parte nueva?
- Desde la version 4.8, ya puedes cambiar vistas. Mira los rectangulos azul en el nivel - marcan la parte del nivel que puedes entrar. Escojer "Views" arriba en el editor y puedes editarlos para cambiar donde puedes entrar.

¿Como cambio rutas? ¿Para que se pueden usar?
- Rutas estan usadas para algunas cosas en el juego - la camara que mueve automaticamente, algunos enemigos como "Dorrie" (el dinosaurio del Mundo 4), el tren de bloques y tuberias conectadas.
  Para editarlas: Todas las rutas deberian de tener un punto o mas. Pulsa el boton Ctrl y haz clic en un punto y mueve el raton para añadir un punto nuevo. Pulsa el boton Shift cuando mueves un punto para que se quede entre 8 pixels. Pulsa el boton Alt y haz clic en un punto para borrar el punto.

¿Hay trucos que puedo usar para editar niveles mas rapido?
- Pulsa el boton Ctrl y mueve un objeto, sprite o entrada para hacer una copia de el. Puedes mover la parte del nivel en la pantalla pronto pulsando el boton derecho en el raton y moviendolo.

¡No lo puedo ejecutar!
- Mira para ver si tienes el Microsoft .NET Framework 2.0 instalado. (Si estas en Vista, ya lo deberias tener.) Si no, baja esto: http://www.microsoft.com/downloads/details.aspx?displaylang=en&FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5

No tengo Windows; ¿puedo usarlo?
- Desde la versión 4.4, funciona en Mono: http://www.mono-project.com/Main_Page
En linux basado en Debian, puedes instalar mono así:
sudo apt-get install mono

¿Donde bajo un ROM de NSMB?
- http://www.google.es

Mi pregunta no esta aqui..
- Hay un foro en Ingles para el editor aqui: http://jul.rustedlogic.net/forum.php?id=11 - Si puedes escribir en Ingles, puedes enviar tus preguntas aqui.


----------
Creditos:

Treeki- Diseño y codigo
Dirbaio- Codigo para editar vistas, rutas y otras cosas
Blackhole89- Codigo para dibujar objetos original
Piranhaplant- Lista de sprites en ingles
Madman200, Tanks- Informacion en el data de sprites
Master01- Lista de sprites en español
Iconos- http://www.pinvoke.com
Reshef- Traduccion del readme