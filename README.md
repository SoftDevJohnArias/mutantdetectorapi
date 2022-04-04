# mutantdetectorapi

hola, actualmente esta api esta diseñada e implementada en una arquitectura limpia, desarrollada en C# con .NetCore 3.1., podras encontrar aqui tambien implementacion de metodologia TDD con Nunit para Netcore sobre las funcionalidades principales de deteccion de DNA, y aplicacion de algunos patrones  como principio Open closed y manejo de concurrencia para la implementacion de los algoritmos de busqueda , para persitencia a datos se usa una db azuresqlserver para acceso a data por repository mediante entity framework .
es un API Rest con dos metodos, 
uno 
# POST  /mutant
Se encarga de implementar la logica de deteccion de de la secuencia de ADN (horizontalmente, verticalmente, diagonalmente) y a su vez el guardado en una base de datos en caso de coincidencia para mutantes o no coincidencia para  humanos.
para esto se implementa una base de datos relacional (mutantdna) en azureSQL.

# GET /stats
Se encarga de presentar el ratio de cadenas de adn inspeccionadas vs cantidad de humanos detectados, para eso obtiene la informacion de la db para presentarla en el formato requerido


que igual estan documentados en el swagger 
https://mutantdetectorapi.azurewebsites.net/swagger/index.html


nota: actualmente la limitante que esta en el requisito de soportar cargas de 1 a 1000000 en un segundo, es un limitante del api, localmente con una dblocal tuve  un rendimiento de 70 mil pet por seg pruebas que se realizaron con jmeter.
al desplegarlo en azure el rendimiento bajo, inicialmente creeria que se podrian implementar escalamiento horizontal o vertical , la persistencia contra una base NOSQL, y algun patron que favorezca la latencia (la latencia tambien va sujeta a la región donde desplegue el appservice y la db), no esta implementado acá, ya que queria estar tranquilo con lo que conozco pero podria ser una validacion para una segunda instancia.

