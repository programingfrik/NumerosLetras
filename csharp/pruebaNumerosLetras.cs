using System;

/// <summary>
///   La clase pruebaNumerosLetras que sirve pa' saludar a ed-mundo!!
/// </summary>
public class pruebaNumerosLetras
{
    public struct Prueba
    {
        public double valor;
        public String esperado;

        public override String ToString()
        {
            return String.Format("{valor = \"{0}\"; esperado = \"{1}\"",
                                 valor, esperado);
        }
    }
    
    public static void Main(String[] args)
    {
        Prueba[] pruebas = new Prueba[]{
            new Prueba{valor = 147986342342551, esperado = "ciento cuarenta y siete billones novecientos ochenta y seis mil trescientos cuarenta y dos millones trescientos cuarenta y dos mil quinientos cincuenta y uno"},
            new Prueba{valor = 211000, esperado = "doscientos once mil"},
            new Prueba{valor = 200000, esperado = "doscientos mil"},
            new Prueba{valor = 1000, esperado = "mil"},
            new Prueba{valor = 11000, esperado = "once mil"},
            new Prueba{valor = 1000000, esperado = "un millón"},
            new Prueba{valor = 11000000, esperado = "once millones"},
            new Prueba{valor = 21000000, esperado = "veintiun millones"},
            new Prueba{valor = 31000000, esperado = "treinta y un millones"},
            new Prueba{valor = 11000000, esperado = "once millones"},
            new Prueba{valor = 44.5, esperado = "cuarenta y cuatro punto cincuenta"},
            new Prueba{valor = 44.05, esperado = "cuarenta y cuatro punto cinco"},
            new Prueba{valor = 21, esperado = "veintiuno"},
            new Prueba{valor = 61, esperado = "sesenta y uno"},
            new Prueba{valor = 61000, esperado = "sesenta y un mil"},
            new Prueba{valor = 101000, esperado = "ciento un mil"}
        };
        int cant = 0, cante = 0;

        Console.WriteLine("Número\nEn letras\nLetras esperadas\nEstado\n");
        foreach (Prueba prueba in pruebas)
        {
            Console.Write
                ("{0}\n\"{1}\"\n\"{2}\"", prueba.valor.ToString("#,##0.00"),
                 NumerosLetras.decimalLetras(prueba.valor), prueba.esperado);
            if (NumerosLetras.decimalLetras(prueba.valor) == prueba.esperado)
                Console.WriteLine("\ncorrecto\n");
            else
            {
                Console.WriteLine("\nincorrecto\n");
                cante ++;
            }
            cant ++;
        }

        Console.WriteLine("{0} pruebas con errores de {1} pruebas.", cante, cant);
        Console.WriteLine("Listo!!");
    }
}

// Local Variables:
// compile-command: "mcs /t:exe /out:pruebaNumerosLetras.exe NumerosLetras.cs pruebaNumerosLetras.cs && ./pruebaNumerosLetras.exe"
// End:
