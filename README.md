# Backend-otp
Login unificado

Testeos:

json: http://s847081577.mialojamiento.es/api/values
string: http://s847081577.mialojamiento.es/api/values/5


Configurar BD MYSQL 5.7:
Archivo App.config del proyecto de biblioteca-net framework "LogicData", cambiar datos del elemento "DefaultConnection"
Arquitectura con ado.net referencia: https://docs.microsoft.com/es-es/dotnet/framework/data/adonet/ado-net-architecture

Carpeta INSTALL (Eliminar en producción), permite el uso en diferentes servidores.

Contiene:
- SQL con la estructura de login