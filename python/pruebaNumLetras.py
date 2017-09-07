#!/usr/bin/python3

import sys
from numLetras import *

# Esto es para probar la versión de python de la librería para convertir de números a letras.

fallo = False
cant = 0
cante = 0
with open("../pruebas.csv") as pruebas:
    prueba = pruebas.readline()
    print ("tipo\nnumero\nesperado\ncalculado\nresultado\n")
    while prueba != "":
        partes = prueba.replace("\n", "").split(",")
        numero = partes[1].replace(" ", "")
        if (partes[0] == "natural"):
            numero = int(numero)
            calculado = numALetras(numero)
        elif (partes[0] == "decimal"):
            numero = float(numero)
            calculado = decimalALetras(numero)
        elif (partes[0] == "moneda"):
            numero = float(numero)
            calculado = monedaALetras(numero, "pesos", "centavos")
        else:
            raise ValueError(("La prueba \"{}\" tiene tipo {} que"
                              + " no es un tipo valido, los tipos "
                              + "validos son natural, decimal y "
                              + "moneda.").format(prueba, partes[0]))
        if (partes[2] == calculado):
            resultado = "correcto"
        else:
            resultado = "incorrecto"
            fallo = True
            cante += 1
        print ("{}\n{:,}\n{}\n{}\n{}\n".format(partes[0], numero,
                                               partes[2], calculado,
                                               resultado))
        cant += 1
        prueba = pruebas.readline()

print("{} pruebas con errores de {} pruebas.".format(cante, cant))

print("Listo!!")

sys.exit(int(fallo))
