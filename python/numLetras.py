#!/usr/bin/python3
# -*- coding: utf-8 -*-

# Funciones para convertir números a letras o sea al nombre en español
# de ese número según las reglas que explica el artículo de wikipedia
# https://es.wikipedia.org/wiki/Anexo:Nombres_de_los_n%C3%BAmeros_en_espa%C3%B1ol.

import sys
import math
import re

# un arreglo para nombrar las unidades 
# descenas y centenas
nomtrio = [["uno ", "dos ", "tres ", "cuatro ", "cinco ", "seis ",
            "siete ", "ocho ", "nueve "],
            ["dieci", "veinti", "treinta y ", "cuarenta y ",
             "cincuenta y ", "sesenta y ", "setenta y ",
            "ochenta y ","noventa y "],
            ["ciento ", "doscientos ", "trescientos ",
             "cuatrocientos ", "quinientos ", "seiscientos ",
            "setecientos ", "ochocientos ", "novecientos "]]

# estos son números que son excepciones que se 
# pueden combinar en tres digitos.
exc = {10: "diez ", 11: "once ", 12: "doce ", 13: "trece ",
       14: "catorce ", 15: "quince ", 16: "dieciséis ",
       20: "veinte ", 22: "veintidós ", 23: "veintitrés ",
       26: "veintiséis ", 30: "treinta ", 40: "cuarenta ",
       50: "cincuenta ", 60: "sesenta ", 70: "setenta ",
       80: "ochenta ", 90: "noventa ", 100: "cien "}

# Estos son los nombres para las combinaciones,
# estos nombres denotan el nivel.
llones = ["mi", "bi", "tri", "cuatri", "quinti", "sisti", "septi",
          "octi", "nuevi"]

# Una funcion para convertir una terna
# unidad, descena, centena a letras
# esta funcion no se debe usar directamente
# es para ser usada por la función numALetras
# lvalor: Número natural de 3 dígitos que se desea convertir.
def terna(lvalor):
    """Convierte cifras naturales de tres dígitos, unidad, decena y centena, en letras."""
    loc = 0
    vale = 0
    retorno = ""

    if (type(lvalor) != int) or (lvalor > 999):
        raise ValueError("terna recibe como parámetros naturales de"
                         + " 3 dígitos.")

    if (lvalor in exc.keys()):
        # el valor completo es una excepción?
        return exc[lvalor]
    elif (int(lvalor % 100) in exc.keys()):
        # las descenas y las unidades forman una excepción?
        retorno = exc[int(lvalor % 100)]
        lvalor = lvalor // 100
        loc = 2
    elif (int(lvalor % 10) in exc.keys()):
        # las unidades forman una excepción?
        retorno = exc[int(lvalor % 10)]
        lvalor = lvalor // 10
        loc = 1
    
    # después de resolver las excepciones, que continue con los nombres
    # estandares
    while (lvalor > 0):
        vale = int(lvalor % 10)
        lvalor = lvalor // 10
        if (vale > 0):
            retorno = nomtrio[loc][vale - 1] + retorno
        loc += 1
    return retorno


# Convierte de números a letras.
# numeroLetra: El número natural que se desea convertir.
def numALetras(numeroLetra):
    """Convierte un número a letras o sea a su nombre en español."""
    pos = 0
    vala = 0; valb = 0
    temp = ""
    retorno = ""
    
    if (((type(numeroLetra) == str) and (numeroLetra.isdigit()))
        or (type(numeroLetra) == float)):
        lvalor = int(numeroLetra)
    elif (type(numeroLetra) == int):
        lvalor = numeroLetra
    else:
        raise TypeError("Tipo de dato incorrecto, se requiere un "
                        + "entero, un flotante o un cadena solo "
                        + "con caracteres numéricos.")

    # se trata de alguna excepcion ?
    if (lvalor == 0):
        return "cero"
   
    # si no, se usa el algoritmo
    while (lvalor > 0):
        # tomo dos grupos de 3 digitos
        # vala toma unidades, descenas y centenas
        vala = int(lvalor % 1000)
        lvalor = lvalor // 1000
        # valb toma las tres cifras de miles
        valb = int(lvalor % 1000)
        lvalor = lvalor // 1000
        
        if ((pos > 0) and (vala == 1) and (valb == 0)
            and (lvalor == 0)):
            # si esta iteracion es mayor que 0 (llones) y
            # vala es 1 y valb es 0 ( 000 001 XXX ... )
            # y lvalor ya es 0 (no quedan mas cifras)
            retorno = llones[pos - 1] + "llón " + retorno
        elif ((pos > 0) and ((vala + valb) > 0)):
            # si no, si estamos en los llones y
            # vala o valb tienen lvalor es mas de un llon
            retorno = llones[pos - 1] + "llones " + retorno
            
        if ((pos > 0) and (int(vala % 10) == 1)
            and not (int(vala % 100) == 11)):
            # si estamos en los llones y vala tiene 1
            # y valb tiene 0 (000 001 XXX ...) "un millón XXX"
            retorno = terna(vala)[:-2] + " " + retorno
        elif (vala > 0):
            # si no, si vala tiene valor que busque el nombre de la
            # terna
            retorno = terna(vala) + retorno
       
        if (valb == 1):
            retorno = "mil " + retorno
        elif ((int(valb % 10) == 1) and ((valb % 100) != 11)):
            # si en los digitos de mil hay un 1 en las unidades y no se
            # trata de un once entonces hay que recortar el "uno" para
            # que diga "un"
            temp = terna(valb)
            retorno = temp[:-2] + " mil " + retorno           
        elif (valb > 1):
            # cualquier otra cosa en digitos de mil lleva "mil"
            retorno = terna(valb) + "mil " + retorno
        pos += 1
    return retorno[:-1]

# Convierte un número que expresa una cantidad de dinero con centavos
# en letras.
# cantidad: Cantidad de dinero que se desea convertir.
# nmoneda: Nombre de la moneda.
# ncentavo: Nombre de los centavos.
def monedaALetras(cantidad, nmoneda, ncentavo):
    """Convierte una cantidad de dinero a letras o sea su nombre en español."""
    entera = 0
    decimal = 0.0
    expdecimal = re.compile("^(\\d*\\.?\\d+|\\d+\\.?\\d*)$")
    retorno = ""

    if (((type(cantidad) == str) and expdecimal.match(cantidad))
        or (type(cantidad) == float) or (type(cantidad) == int)):
        cantidad = float(cantidad)
    else:
        raise TypeError("Tipo de dato incorrecto, se requiere un "
                        + "entero, un flotante, o una cadena que "
                        + "represente un número decimal.")
    decimal = cantidad % 1
    entera = cantidad // 1

    retorno = "{} {}".format(numALetras(entera), nmoneda)

    if decimal != 0.0:
        decimal = int(decimal * 100)
        retorno += "con {} {}".format(numALetras(decimal), ncentavo)
    
    retorno = retorno.replace("uno", "un")
    return retorno

# Convierte una cantidad con decimal en letras
# cantidad: Cadena que representa la cantidad que se desea convertir
def decimalALetras(cantidad):
    entera = ""
    decimal = ""
    expdecimal = re.compile("^(\\d*\\.?\\d+|\\d+\\.?\\d*)$")
    retorno = ""

    if (((type(cantidad) == str) and expdecimal.match(cantidad))
        or (type(cantidad) == float) or (type(cantidad) == int)):
        cantidadstr = str(cantidad)
    else:
        raise TypeError("Tipo de dato incorrecto, se requiere un "
                        + "entero, un flotante, o una cadena que "
                        + "represente un número decimal.")
    
    # si tiene un punto tiene parte decimal
    lvalor = cantidadstr.find(".")
    if lvalor != -1:
        # separo la parte decimal de la entera
        entera = cantidadstr[:lvalor]
        decimal = cantidadstr[lvalor + 1:]
        if len(decimal) > 2:
            decimal = decimal[:2]
        elif len(decimal) == 1:
            decimal = decimal + "0"
        
        retorno = numALetras(entera) + " punto " +  numALetras(decimal)
    else:
        retorno = numALetras(cantidadstr)
    return retorno
