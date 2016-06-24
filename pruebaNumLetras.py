#!/usr/bin/python3

import sys
from numLetras import *

# Esto es para probar la versión de python de la librería para convertir de números a letras.

fallo = False

with open("pruebas.csv") as pruebas:
    prueba = pruebas.readline()
    print ("tipo | numero | esperado | calculado | resultado")
    while prueba != "":
        partes = prueba.replace("\n", "").split("|")
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
        print ("{} | {} | {} | {} | {}".format(partes[0], partes[1],
                                               partes[2], calculado,
                                               resultado))
        prueba = pruebas.readline()

if fallo:
    print("Una o más pruebas fallaron, por favor revise su vaina!")
else:
    print("Todas las pruebas parecen haber sido satisfactorias!")
        
print("Listo!")

sys.exit(int(fallo))
