using System;
using System.IO;

/// <summary>
///   La clase pruebaNumerosLetras que sirve pa' saludar a ed-mundo!!
/// </summary>
public class pruebaNumerosLetras
{
    public static void Main(String[] args)
    {
        int cant = 0, cante = 0;
        String linea, numero;
        String[] partes;
        double valor;

        using (StreamReader fichero = new StreamReader
               (Path.Combine("..", "pruebas.csv")))
        {
            linea = fichero.ReadLine();

            Console.WriteLine
                ("\nNúmero\nEn letras\nLetras esperadas\nEstado\n");

            while (linea != null)
            {
                partes = linea.Split(',');

                if (partes[1].Replace(" ", "").Length > 28)
                {
                    Console.WriteLine
                        ("Esta cifra es demasiado grande será "
                         + "ignorada {0}.\n", partes[1]);
                    linea = fichero.ReadLine();
                    continue;
                }
                
                valor = double.Parse(partes[1].Replace(" ", ""));
                
                if (partes[0] == "moneda")
                    numero = NumerosLetras.monedaLetras
                        (valor, "pesos", "centavos");
                else
                    numero = NumerosLetras.decimalLetras(valor);

                Console.Write
                    ("{0}\n\"{1}\"\n\"{2}\"",
                     valor.ToString("#,##0.00"),
                     numero,
                     partes[2]);
                if (numero == partes[2])
                    Console.WriteLine("\ncorrecto\n");
                else
                {
                    Console.WriteLine("\nincorrecto\n");
                    cante ++;
                }
                cant ++;
                linea = fichero.ReadLine();
            }
        }

        Console.WriteLine("{0} pruebas con errores de {1} pruebas.",
                          cante, cant);
        Console.WriteLine("Listo!!");
    }
}

// Local Variables:
// compile-command: "mcs /t:exe /out:pruebaNumerosLetras.exe NumerosLetras.cs pruebaNumerosLetras.cs && ./pruebaNumerosLetras.exe"
// End:
