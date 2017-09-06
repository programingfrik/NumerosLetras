using System;
using System.Text;

/// <summary>
///   http://es.wikipedia.org/wiki/Nombres_de_los_n%C3%BAmeros_en_espa%C3%B1ol
/// </summary>
public class NumerosLetras {
    // un arreglo para nombrar las unidades 
    // descenas y centenas
    private static string[,] nomtrio = {
        {"uno ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ",
         "nueve "},
        {"dieci", "veinti", "treinta y ", "cuarenta y ", "cincuenta y ", "sesenta y ",
         "setenta y ", "ochenta y ", "noventa y "},
        {"ciento ", "doscientos ", "trescientos ", "cuatroscientos ", "quinientos ",
         "seiscientos ", "setecientos ", "ochocientos ", "novecientos "}
    };

    // estos son numeros que son excepciones que se pueden combinar en
    // tres digitos.
    private static long[] excnum = new long[]
        {10, 11, 12, 13, 14, 15, 16, 20, 22, 23, 26, 30, 40, 50, 60, 70, 80, 90, 100};
    private static string[] excnom = new string[]
        {"diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseís ",
         "veinte ", "veintidós ", "veintitrés ", "veintiséis ", "treinta ",
         "cuareinta ", "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa ",
         "cien "};
	
    // estos son los nombres para las combinaciones
    private static string[] llones = new string[]
        {"mi", "bi", "tri", "cuatri", "quinti", "sisti", "septi", "octi", "nuevi"};

    /// <summary>
    /// Una funcion que transforma un trio de digitos (unidades,
    /// decenas y centenas) en letras.
    /// </summary>
    /// <param name="cantidad">Un numero entero con la cantidad de
    /// este numero.</param>
    /// <returns>El numero que representa el trio en letras.</returns>
    private static String trio(int valor) {
        int pos = 0;
        int vale = 0;
        string retorno = "";

        // se trata de alguna excepcion ?
        for (int cont = 0; cont < excnum.Length; cont++) {
            if ((excnum[cont] < 99) && (((valor - excnum[cont]) % 100) == 0)) {
                // si se trata de una exepcion en las descenas
                // y ademas hay centenas
                retorno = excnom[cont];
                pos = 2;
                valor = valor / 100;
            }
            else if (valor == excnum[cont])
                // si no que use la excepcion y salga
                return excnom[cont];
        }

        // que continue con los nombres estandares
        while (valor > 0) {
            vale = (int)(valor % 10);
            valor = valor / 10;
            
            if (vale > 0)
                retorno = nomtrio[pos, vale - 1] + retorno;
			
            pos++;
        }               

        return retorno;
    }

    /// <summary>
    /// Una funcion que convierte una cantidad entera en letras.
    /// </summary>
    /// <param name="cantidad">La cantidad que se desea convertir.</param>
    /// <returns>El entero en letras.</returns>
    public static string ConvNumLetras(long valor) {
        int pos = 0;
        int vala = 0, valb = 0;
        StringBuilder retorno = new StringBuilder();
        string temp = "";

        // se trata de alguna excepcion ?
        if (valor == 0) return "cero";
		
        // si no, se usa el algoritmo
        while (valor > 0) {
            // tomo dos grupos de 3 digitos
            // vala toma unidades, descenas y centenas
            vala = (int)(valor % 1000);
            valor = valor / 1000;
            // valb toma las tres cifras de miles
            valb = (int)(valor % 1000);
            valor = valor / 1000;
            
            if ((pos > 0) && (vala == 1) && (valb == 0) && (valor == 0))
                // si esta es la iteracion mayor que 0 y
                // vala es 1 y valb es 0 ( 000 001 XXX ... )
                // y valor ya es 0 (no quedan mas cifras)
                retorno.Insert(0, String.Format("{0}llón ", llones[pos - 1]));
            else if ((pos > 0) && (vala + valb > 0))
                // si no, si estamos en los llones y
                // vala o valb tienen valor (es mas de un llon)
                retorno.Insert(0, String.Format("{0}llones ", llones[pos - 1]));

            if ((pos > 0) && ((vala % 10) == 1) && ((vala % 100) != 11)) {
                // si estamos en los llones y vala tiene 1 y no se
                // trata de un 11 (XXX XX1 XXX ...)
                temp = trio(vala);
                retorno.Insert(0, String.Format
                               ("{0} ", temp.Substring(0, temp.Length - 2)) );
            }
            else if (vala > 0)
                // si no, si vala tiene valor
                retorno.Insert(0, trio(vala));
			
            if (valb == 1)
                // si valb es 1
                retorno.Insert(0, "mil ");
            else if (((valb % 10) == 1) && ((valb % 100) != 11)) {
                temp = trio(valb);
                retorno.Insert(0, String.Format
                               ("{0} mil ", temp.Substring(0, temp.Length - 2)) );
            }
            else if (valb > 1)
                retorno.Insert(0, String.Format("{0}mil ", trio(valb)));
			
            pos++;
        }

        retorno.Remove(retorno.Length - 1, 1);
        return retorno.ToString();
    }
        
    /// <summary>
    ///   Para convertir un numero que representa un cantidad de
    ///   dinero en una cantidad de dinero en letras.
    /// </summary>
    /// <param name="cantidad">La cantidad de dinero que se desea
    /// transformar a letras.</param>
    /// <param name="nmoneda">El nombre de la moneda en la que se
    /// desea poner el valor. Por ejemplo "pesos".</param>
    /// <param name="ncentavo">El nombre de las centesimas menores
    /// a la unidad para esta moneda.</param>
    /// <returns>La cadena con el numero transformado en monedas
    /// en letras</returns>
    public static String monedaLetras(double cantidad, String nmoneda, String ncentavo)
    {
        String[] partes = cantidad.ToString().Split('.');
        String letras;
        long entera, fraccion;
            
        entera = Int64.Parse(partes[0]);
            
        letras = ConvNumLetras(entera) + nmoneda;
            
        // si tiene una parte decimal que no esta en blanco
        if ((partes.Length > 1) && (!partes[1].Equals(String.Empty)))
        {
            if (partes[1].Length == 1)
                fraccion = Int64.Parse(partes[1] + "0");
            else if (partes[1].Length > 2)
                fraccion = Int64.Parse(partes[1].Substring(0,2));
            else
                fraccion = Int64.Parse(partes[1]);
            letras += " con " + ConvNumLetras(fraccion) + ncentavo;
        }
            
        return letras.Replace("uno", "un");
    }
        
    /// <summary>
    ///   Para convertir una dinero en una moneda a letras.
    /// </summary
    /// <param name="cantidad"></param>
    /// <returns></returns>
    public static String decimalLetras(double cantidad)
    {
        String[] partes = cantidad.ToString("0.00").Split('.');
        String letras;
        long entera, fraccion;

        entera = long.Parse(partes[0]);
        
        letras = ConvNumLetras(entera);
            
        // si tiene una parte decimal que no esta en blanco
        if ((partes.Length > 1) && (!partes[1].Equals(String.Empty)))
        {            
            if (partes[1].Length == 1)
                fraccion = long.Parse(partes[1] + "0");
            else
                fraccion = long.Parse(partes[1]);

            if (fraccion > 0)
                letras += " punto " + ConvNumLetras(fraccion);
        }
            
        return letras;
    }
}
