using System;

class Program
{
    static void Main()
    {
        try
        {
            // Solicitar y leer el primer número
            double firstNumber = PedirNumero("Primer numero: ");

            // Solicitar y leer el segundo número
            double secondNumber = PedirNumero("Segundo numero: ");

            // Solicitar y leer la operación a realizar
            string operation = PedirOperacion();

            // Realizar la operación y obtener el resultado
            double result = RealizarOperacion(firstNumber, secondNumber, operation);

            // Mostrar el resultado
            Console.WriteLine($"Resultado: {result}");
        }
        catch (FormatException)
        {
            // Capturar excepción si la entrada no es un número válido
            Console.WriteLine("Error: Ingresa un número válido.");
        }
        catch (DivideByZeroException)
        {
            // Capturar excepción si se intenta dividir por cero
            Console.WriteLine("Error: No se puede dividir por 0.");
        }
        catch (InvalidOperationException ex)
        {
            // Capturar excepción para operaciones no válidas
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Capturar excepción para otros errores inesperados
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        // Esperar a que el usuario presione Enter antes de cerrar la consola
        Console.ReadLine();
    }

    static double PedirNumero(string mensaje)
    {
        double numero;

        while (true)
        {
            // Solicitar al usuario que ingrese un número
            Console.Write(mensaje);

            // Leer la entrada del usuario como una cadena
            string userInput = Console.ReadLine();

            // Intentar convertir la cadena a un número double
            if (double.TryParse(userInput, out numero))
            {
                // La conversión fue exitosa, salir del bucle
                break;
            }
            else
            {
                // Informar al usuario que la entrada no es válida y volver a solicitar
                Console.WriteLine("Entrada no válida. Por favor, ingrese un número válido.");
            }
        }

        // Devolver el número convertido
        return numero;
    }

    static string PedirOperacion()
    {
        // Solicitar al usuario que elija una operación
        Console.WriteLine("Elige operacion (+, -, *, /): ");
        string operation = Console.ReadLine();

        // Verificar si la operación ingresada es válida
        if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
        {
            // Lanzar una excepción si la operación no es válida
            throw new InvalidOperationException("Operacion invalida.");
        }

        // Devolver la operación válida
        return operation;
    }

    static double RealizarOperacion(double firstNumber, double secondNumber, string operation)
    {
        double result;

        // Realizar la operación según la elección del usuario
        switch (operation)
        {
            case "+":
                result = firstNumber + secondNumber;
                break;
            case "-":
                result = firstNumber - secondNumber;
                break;
            case "*":
                result = firstNumber * secondNumber;
                break;
            case "/":
                // Verificar si se está intentando dividir por cero
                if (secondNumber == 0)
                {
                    // Lanzar una excepción si se intenta dividir por cero
                    throw new DivideByZeroException();
                }
                result = firstNumber / secondNumber;
                break;
            default:
                // Lanzar una excepción si la operación no es reconocida
                throw new InvalidOperationException("Operacion invalida.");
        }

        // Devolver el resultado de la operación
        return result;
    }
}
