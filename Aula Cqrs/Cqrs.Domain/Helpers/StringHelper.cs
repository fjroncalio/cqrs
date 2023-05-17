namespace Cqrs.Domain.Helpers;
public static class StringHelper
{
    public static bool IsCpf(this string cpf)
    {
        var multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var cpfValue = cpf?.Trim() ?? string.Empty;
        cpfValue = cpfValue.Trim().Replace(".", "").Replace("-", "");
        if (cpfValue.Length != 11)
            return false;

        for (var j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpfValue)
                return false;

        var hasCpf = cpfValue[..9];
        var sumCpf = 0;

        for (var i = 0; i < 9; i++)
            sumCpf += int.Parse(hasCpf[i].ToString()) * multiplier1[i];

        var rest = sumCpf % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        var digit = rest.ToString();
        hasCpf = hasCpf + digit;
        sumCpf = 0;
        for (var i = 0; i < 10; i++)
            sumCpf += int.Parse(hasCpf[i].ToString()) * multiplier2[i];

        rest = sumCpf % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        digit = digit + rest.ToString();

        return cpfValue.EndsWith(digit);
    }

    public static string RemoveMaskCpf(this string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return cpf;

        var formatCpf = cpf.Trim();
        formatCpf = formatCpf.Replace(".", "").Replace("-", "");
        return formatCpf;
    }

    public static string FormatCpf(this string cpf)
    {
        return string.IsNullOrWhiteSpace(cpf)
            ? cpf
            : Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }
}