    try
    {
        string[] arr = { };

        var val = arr.GetValue(0);
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("Out of range!");
    }
    catch (Exception)
    {
        Console.WriteLine("Errr, something bad happened!");
    }
