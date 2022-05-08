# Estudio_contable_springfield
Repositorio del Trabajo práctico final (Materia: Construcción de aplicaciones informáticas - FCE UBA) 

## Qué utilicé para hacerlo?

## Problema:
Tengo una Nomina y necesito modelar entre otras funciones el alta/modificación (es un solo botón -> grabar) y baja de empleados. También se necesita modelar 
la liquidación de sueldos de la nómina.

## caso de uso 0: el usuario pide el listado de Categorías.
  - resultado: se le muestra a todos los usuario siempre el mismo listado:
    + C00 - Jerarquicos - Fuera de Convenio
    + C01 - Maestranza y servicios
    + C02 - Administrativos
    + C03 - Auxiliar
    + C04 - Auxiliar especializado
    + C05 - Ventas
 
## caso de uso 1: usuario da de alta un empleado en la nómina.
  - Se le listan los códigos válidos de las categorías (include CU 1)
  - ingresa Nombre, apellido, dirección, teléfono , mail.
  - ingresa una fecha de nacimiento válida (+18 años)
  - ingresa un CUIL válido (cantidad de digitos y digito verificador)
  - ingresa código de categoría valido (dentro de los que estamos estableciendo)
  - (legajo lo calcula automaticamente de manera autoincremental)
  - (fecha de alta lo calcula automaticamente (fecha de hoy))
  - resultado: se suma el empleado a la Nomina de la empresa.
- flujo alt. 1 el código es inválido

##  caso de uso 2: usuario desea liquidar los sueldos mensuales
  - El usuario desea hacer la liquidación mensual.
  - El periodo es seleccinada mediante un codigo que ingresa el usuario. 
  - resultado: El usuario realiza la liquidación, Empleado acumula una liquidación a su lista.
- precondiciones 1: la nómina no está vacía.
- flujo alt. 1 el período es inválido

##  caso de uso 3: usuario desea modificar/dar de baja empleado
   - Se listan todos los numeros de legajo de la nómina, con el nombre y la categoría.
   - El usuario elije un legajo para modificar, se traen los datos modificables. Se graban los cambios.
   - resultado: el empleado es modificado
   - precondiciones 1: la nómina no está vacía.
   - flujo alt. 1 el código es inválido

##  caso de uso 4: el actor desea conocer la nómina y por cada empleado su descripcion completa
   - precondiciones 1: La nómina no está vacía.
   - resultado: se le muestra al usuario un string con el siguiente formato:"{legajo} - {nombre} {Apellido} - {categoría} / $ {sueldo bruto}".

##  caso de uso 5: usuario desea imprimir un recibo
   - El usuario ingresa el legajo del empleado y el periodo
   - resultado: muestra un string con los detalles del recibo (brutos, descuentos, periodo, numero liquidacion, neto)
   - flujo alt. 1 el código es inválido
   - flujo alt. 2 el periodo es inválido

##  caso de uso 6: usuario desea modificar salarios
   - El usuario ingresa el codigo de la categoría.
   - El usuario ingresa el nuevo monto.
   - resultado: Se modifica la escala salarial.
   - flujo alt. 1 el código es inválido
   
## Para el proyecto en vivo descargar el archivo xxxx.exe y ejecutar
