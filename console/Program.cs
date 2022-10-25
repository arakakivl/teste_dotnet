public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Sum of 8:" + Soma(8));

        string[] array = {"kelless","keenness","Alfalggo"};
        foreach(var s in Duplicados(array))
        {
            Console.WriteLine("Se liga: " + s);
        }
    }

    static long Soma(long n)
    {
        long r = 0L;
        for(long i = 1L; i <= n; ++i)
            r += i;
    
        return r;
    }

    static string[] Duplicados(string[] array)
    {
        string[] tor = new string[array.Count()];
        for(int i = 0; i < array.Count(); ++i)
        {
            string s = "";
            for(int m = '\0', n = 0; n < array[i].Count(); ++n)
            {
                // Comparing char values (based on ASCII)
                if (array[i][n] != m)
                    s += array[i][n];

                m = array[i][n];
            }

            tor[i] = s;
        }

        return tor;
    }
}


// DOOG
// DO