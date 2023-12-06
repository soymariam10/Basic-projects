using System;

class Program
{
    static void Main()
    {
        try
        {
            JuegoDeAdivinanza();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        Console.ReadLine();
    }

    static void JuegoDeAdivinanza()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 101);

        int guess = 0;
        int numberOfGuesses = 0;

        while (guess != randomNumber)
        {
            try
            {
                Console.WriteLine("Adivina el número entre 1 y 100: ");

                // Intenta convertir la entrada del usuario a un número entero.
                guess = Convert.ToInt32(Console.ReadLine());

                // Incrementa el contador de intentos.
                numberOfGuesses++;

                // Verifica si el número ingresado está dentro del rango permitido.
                if (guess < 1 || guess > 100)
                {
                    throw new ArgumentOutOfRangeException("El número debe estar entre 1 y 100.");
                }

                // Proporciona pistas sobre la ubicación del intento.
                if (guess < randomNumber)
                {
                    Console.WriteLine("Muy bajo.");
                }
                else if (guess > randomNumber)
                {
                    Console.WriteLine("Muy alto.");
                }
                else
                {
                    // Muestra un mensaje de felicitación si el usuario adivina correctamente.
                    Console.WriteLine($"¡Felicidades! Has acertado en {numberOfGuesses} intentos.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Ingresa un número válido.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: El número ingresado es demasiado grande o demasiado pequeño.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
