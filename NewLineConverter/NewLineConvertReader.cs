using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLineConverter;
public enum EOLType : uint
{
    Unknown = 0,
    CRLF = 1,
    LF = 2,
    CR = 3
}

public static class NewLineConvertReader
{
    public const char CR = '\r';
    public const char LF = '\n';
    public static EOLType GetEOLType(string value) => value switch
    {
        "\r\n" => EOLType.CRLF,
        "\n" => EOLType.LF,
        "\r" => EOLType.CR,
        _ => EOLType.Unknown
    };
    public static IEnumerable<char> Convert(string text, EOLType type)
        =>Convert(new StringReader(text), type);

    public static IEnumerable<char> Convert(TextReader reader, EOLType type)
    {
        while (true)
        {
            int p = reader.Peek();
            if (p == -1)
            {
                reader.Read();
                yield break;
            }
            char c = (char)p;
            if (type != EOLType.Unknown)
            {
                if(c == CR)
                {
                    reader.Read();
                    int q = reader.Peek();
                    if (q == -1)
                    {
                        switch (type)
                        {
                            case EOLType.CRLF:
                                yield return CR;
                                yield return LF;
                                yield break;
                            case EOLType.LF:
                                yield return LF;
                                yield break;
                            case EOLType.CR:
                                yield return CR;
                                yield break;
                        }
                    }else if(q == LF)
                    {
                        reader.Read();
                        switch (type)
                        {
                            case EOLType.CRLF:
                                yield return CR;
                                yield return LF;
                                continue;
                            case EOLType.LF:
                                yield return LF;
                                continue;
                            case EOLType.CR:
                                yield return CR;
                                continue;
                        }
                    }
                }else if(c == LF)
                {
                    reader.Read();
                    int q = reader.Peek();
                    if (q == -1)
                    {
                        switch (type)
                        {
                            case EOLType.CRLF:
                                yield return CR;
                                yield return LF;
                                yield break;
                            case EOLType.LF:
                                yield return LF;
                                yield break;
                            case EOLType.CR:
                                yield return CR;
                                yield break;
                        }
                    }
                    else
                    {
                        switch (type)
                        {
                            case EOLType.CRLF:
                                yield return CR;
                                yield return LF;
                                continue;
                            case EOLType.LF:
                                yield return LF;
                                continue;
                            case EOLType.CR:
                                yield return CR;
                                continue;
                        }
                    }
                }
                else
                {
                    reader.Read();
                }
            }
            yield return c;
        }
    }
    public static string ConvertAll(TextReader reader, EOLType type) => new (Convert(reader, type).ToArray());
    public static string ConvertAll(string text, EOLType type) => new (Convert(text, type).ToArray());

}
