using UnityEngine;

public static class GestorEntrada
{
    private static bool entradaBloqueada = false;

    public static bool EntradaBloqueada()
    {
        return entradaBloqueada;
    }

    public static void BloquearEntrada()
    {
        entradaBloqueada = true;
    }

    public static void DesbloquearEntrada()
    {
        entradaBloqueada = false;
    }
}
