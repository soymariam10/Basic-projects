using System;

class Program
{
    static void Main()
    {
        // Solicitar la longitud en metros al usuario
        Console.WriteLine("Longitud en metros: ");

        // Leer la entrada del usuario y tratar de convertirla a un número
        if (double.TryParse(Console.ReadLine(), out double meters))
        {
            // Calcular la longitud en pies
            double feet = meters * 3.28084;

            // Mostrar el resultado al usuario
            Console.WriteLine($"{meters} metros son {feet} pies.");
        }
        else
        {
            // Manejar el caso en el que la entrada no es un número válido
            Console.WriteLine("Entrada no válida. Por favor, introduce un número válido.");
        }

        // Esperar a que el usuario presione Enter antes de cerrar la consola
        Console.ReadLine();
    }
}
