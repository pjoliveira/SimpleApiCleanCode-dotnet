using System;
using System.Linq;
using System.Text;

namespace SistemaAPI.Recursos.Extensao;


/// <summary>
/// Classes que extende os métodos Asp .Net
/// </summary>
public static class Metodos
{

    /// <summary>
    /// Coloca os traços no documento informado
    /// </summary>
    /// <param name="cpfcnpj">CPF ou CNPJ - Pode ser com pontos já ou não</param>
    /// <param name="colocaDescricao">Se é pra aparecer CPF ou CNPJ</param>
    /// <returns></returns>
    public static string FormatarCPFCNPJ(this string cpfcnpj, bool colocaDescricao = false)
    {
        try
        {
            var tmp = cpfcnpj.ClearString();

            if (tmp.Length == 14)
                return (colocaDescricao ? "CNPJ: " : "") + Convert.ToUInt64(tmp).ToString(@"00\.000\.000\/0000\-00");
            else if (tmp.Length == 11)
                return (colocaDescricao ? "CPF: " : "") + Convert.ToUInt64(tmp).ToString(@"000\.000\.000\-00");
            else
                return (colocaDescricao ? "CPF/CNPJ: " : "") + cpfcnpj;
        }
        catch { return (colocaDescricao ? "CPF/CNPJ: " : "") + cpfcnpj; }
    }

    /// <summary>
    /// Coloca os traços no documento informado
    /// </summary>
    /// <param name="cep">Cep desejado - Pode ser com pontos já ou não</param>
    /// <returns></returns>
    public static string FormatarCEP(this string cep)
    {
        try
        {
            var tmp = cep.ClearString();

            if (tmp.Length == 8)
                return Convert.ToUInt64(tmp).ToString(@"00000\-000");
            else
                return cep;
        }
        catch { return cep; }
    }

    /// <summary>
    /// Método que converte um texto para inteiro
    /// </summary>
    /// <param name="valor">Valor em string</param>
    /// <returns>Valor int32</returns>
    public static int ToInt32(this string valor)
    {
        try
        {
            return Convert.ToInt32(valor);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// Método que converte um objeto para um valor double
    /// </summary>
    /// <param name="obj">Número em texto a ser convertido</param>
    /// <returns>Retorna o número double convertido</returns>
    public static double ToDouble(this object obj)
    {
        try { return Convert.ToDouble(obj); }
        catch { throw new Exception(string.Format("O valor informado não é um valor double válido! Valor informado: '{0}'", obj)); }
    }

    /// <summary>
    /// Método que verifica se um texto é nulo. Já usa o Trim
    /// </summary>
    /// <param name="text">Texto a ser verificado</param>
    /// <returns>Retorna se é nulo ou não</returns>
    public static bool IsNullOrEmpty(this string text)
    {
        try
        {
            if (text == null)
                return true;

            return string.IsNullOrEmpty(text.Trim());
        }
        catch { throw new Exception(string.Format("O valor informado não é um valor texto válido! Valor informado: '{0}'", text)); }
    }


    /// <summary>
    /// Método que converte um valor para inteiro de 64 bits
    /// </summary>
    /// <param name="text">Valor original</param>
    /// <returns>Valor inteiro convertido</returns>
    public static long ToLong(this string text)
    {
        try { return Convert.ToInt64(text); }
        catch { throw new Exception(string.Format("O valor informado não é um valor inteiro válido! Valor informado: '{0}'", text)); }

    }

    /// <summary>
    /// Método que converte um texto para decimal
    /// </summary>
    /// <param name="text">Número em texto a ser convertido</param>
    /// <returns>Retorna o número decimal convertido</returns>
    public static decimal ToDecimal(this string text)
    {
        try { return Convert.ToDecimal(text); }
        catch { throw new Exception(string.Format("O valor informado não é um valor decimal válido! Valor informado: '{0}'", text)); }
    }

    /// <summary>
    /// Método que converte um objeto para um valor decimal
    /// </summary>
    /// <param name="obj">Objeto a ser convertido</param>
    /// <returns>Retorna o número decimal convertido</returns>
    public static decimal ToDecimal(this object obj)
    {
        try { return Convert.ToDecimal(obj); }
        catch { throw new Exception(string.Format("O valor informado não é um valor decimal válido! Valor informado: '{0}'", obj)); }
    }

    /// <summary>
    /// Método que converte um objeto para um valor booleano
    /// </summary>
    /// <param name="obj">Objeto a ser convertido</param>
    /// <returns>Retorna o valor booleano convertido</returns>
    public static bool ToBoolean(this object obj)
    {
        try { return Convert.ToBoolean(obj); }
        catch { throw new Exception(string.Format("O valor informado não é um valor booleano válido! Valor informado: '{0}'", obj)); }
    }

    /// <summary>
    /// Método que converte um texto para Data
    /// </summary>
    /// <param name="text">Número em texto a ser convertido</param>
    /// <returns>Retorna a Data convertida</returns>
    public static DateTime ToDate(this string text)
    {
        try { return Convert.ToDateTime(text); }
        catch { throw new Exception(string.Format("O valor informado não é um valor data válido! Valor informado: '{0}'", text)); }
    }

    /// <summary>
    /// Método que converte um texto para booleano
    /// </summary>
    /// <param name="text">Número em texto a ser convertido</param>
    /// <returns>Retorna o valor Booleano convertido</returns>
    public static bool ToBoolean(this string text)
    {
        try { return Convert.ToBoolean(text); }
        catch { throw new Exception(string.Format("O valor informado não é um valor verdadeiro ou falso válido! Valor informado: '{0}'", text)); }
    }

    /// <summary>
    /// Método que converte um número para Boolean
    /// </summary>
    /// <param name="number">Número em texto a ser convertido</param>
    /// <returns>Retorna o valor booleano referente</returns>
    public static bool ToBoolean(this int number)
    {
        try { return Convert.ToBoolean(number); }
        catch { throw new Exception(string.Format("O valor informado não é um valor verdadeiro ou falso válido! Valor informado: '{0}'", number)); }
    }

    /// <summary>
    /// Método que converte um texto para Guid
    /// </summary>
    /// <param name="text">Número em texto a ser convertido</param>
    /// <returns>Retorna a Guid convertida</returns>
    public static Guid ToGuid(this string text)
    {
        Guid guid = new Guid();
        try { guid = new Guid(text); }
        catch { guid = Guid.Empty; }
        return guid;
    }

    /// <summary>
    /// Método que converte um texto para Char
    /// </summary>
    /// <param name="text">Número em texto a ser convertido</param>
    /// <returns>Retorna o valor char convertido</returns>
    public static char ToChar(this string text)
    {
        try { return Convert.ToChar(text); }
        catch { throw new Exception(string.Format("O valor informado não é um valor char válido! Valor informado: '{0}'", text)); }

    }



    public static string CutNoDots(this string text, short size)
    {
        if (string.IsNullOrEmpty(text) || text.Length < size)
            return text;
        else
            return text.Substring(0, size);
    }


    /// <summary>
    /// Método que adiciona um texto a outro utilizando um StringBuilder 
    /// </summary>
    /// <param name="text">Texto original</param>
    /// <param name="valueToAdd">Valor a ser adicionado</param>
    /// <returns>Retorna o valor após concatenação</returns>
    public static string Add(this string text, string valueToAdd)
    {
        return new StringBuilder(text).Append(valueToAdd).ToString();
    }

    /// <summary>
    /// Método que Gera uma quantidade desejada de um mesmo caractere. Usado, por exemplo, para fazer uma linha
    /// </summary>
    /// <param name="character">Caracter desejado</param>
    /// <param name="length">Tamanho desejado</param>
    /// <returns>Retorna o valor após concatenação</returns>
    public static string Fill(this string character, int length)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
            sb.Append(character);

        return sb.ToString();
    }

    /// <summary>
    /// Metodo que retira de uma string: . - , / _
    /// </summary>
    /// <param name="text">Texto de entrada</param>
    /// <returns>Texto sem caracteres especiais</returns>
    public static string ClearString(this string text)
    {
        string inputString = ".-,/_";
        string outputString = "";
        for (int i = 0; i < inputString.Length; i++)
            text = text.Replace(inputString[i].ToString(), outputString);

        return text;
    }

    /// <summary>
    /// Metodo que retira os careacteres passados de uma string
    /// /// </summary>
    /// <param name="text">Texto de entrada</param>
    /// <param name="inputString">String que deseja que sejam substituidas</param>
    /// <returns>Texto sem caracteres especiais</returns>
    public static string ClearString(this string text, string inputString)
    {
        string outputString = "";
        for (int i = 0; i < inputString.Length; i++)
            text = text.Replace(inputString[i].ToString(), outputString);

        return text;
    }

    /// <summary>
    /// Metodo que retira os acentos
    /// </summary>
    /// <param name="text">Texto com acento</param>
    /// <returns>Texto sem acento</returns>
    public static string RemoveAcent(this string text)
    {
        string inputString = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        string outputString = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
        for (int i = 0; i < inputString.Length; i++)
            text = text.Replace(inputString[i].ToString(), outputString[i].ToString());
        return text;
    }

    /// <summary>
    /// Método que formata o nome retirando espaços em branco do meio do nome
    /// </summary>
    /// <param name="text">Texto a ser tratado</param>
    /// <returns>Retorna o nome formatado corretamente</returns>
    public static string FormatName(this string text)
    {
        if (text == null || text == string.Empty)
            return "";

        string[] nameArray = text.Split(' ');
        string nameNew = "";
        foreach (var item in nameArray)
            nameNew += (" " + item).TrimEnd();

        return nameNew.Trim();
    }

    /// <summary>
    /// Retorna a primeira letra de uma palavra (com tratamento)
    /// </summary>
    /// <param name="text">Palavra desejada</param>
    /// <returns>Retorna somente a primeira, segunda ou terceira letra em maiúsculo </returns>
    public static string FirstLetterUpper(this string text)
    {
        text = text.FormatName();

        switch (text.Length)
        {
            case 0: return text;
            case 1: return text.ToUpper();
            case 2: return text.ToUpper();
            default: return text[0].ToString().ToUpper() + text.Substring(1).ToLower();
        }
    }


    /// <summary>
    /// Método que formata uma sentença de texto com o padrão Camel Case
    /// </summary>
    /// <param name="text">Texto a ser convertido</param>
    /// <returns>Retorna o nome formatado</returns>
    public static string FormatNameCamelCase(this string text)
    {
        //pego no nome, retiro os espaços e jogo pra baixo
        text = text.FormatName().ToLower();
        //quebro e percorro, jogando a primeira de cada palavra
        string[] nameArray = text.Split(' ');
        string nameNew = "";
        foreach (var item in nameArray)
            nameNew += (" " + item.FirstLetterUpper()).TrimEnd();

        //faz tratamento das preposições
        nameArray = nameNew.Split(' ');
        string[] prep = new string[] { "de", "do", "da", "dos", "das", "e", "a", "em" };
        for (int i = 0; i < nameArray.Length; i++)
        {
            var exit = prep.SingleOrDefault(w => w == nameArray[i].ToLower());
            if (exit != null)
                nameArray[i] = exit;
        }
        nameNew = "";
        foreach (var item in nameArray)
            nameNew += (" " + item.Trim()).TrimEnd();

        return nameNew.Trim();
    }

    public static DateTime DateInitial(this DateTime date)
    {
        return Convert.ToDateTime(date.ToString("dd/MM/yyyy") + " 00:00:00");
    }

    public static DateTime DateFinal(this DateTime date)
    {
        return Convert.ToDateTime(date.ToString("dd/MM/yyyy") + " 23:59:59");
    }

}
