using JIWGrandAlpha.Domain.Entity;
using JIWGrandAlpha.Domain.Enum;
using Microsoft.AspNetCore.Html;

namespace JIWGrandAlpha.Helpers;

public static class CellHelper
{
    public static HtmlString Type(Cell cell)
    {
        var result = "";
        switch (cell.Type)
        {
            case TypeValue.Оценка:
                result = $"<p>{cell.Value}</p>";
                break;

            case TypeValue.Опоздание:
                result = $"<p style=color:lightskyblue;>{cell.Value}</p>";
                break;

            case TypeValue.Отсуствие:
                result = $"<p>Н</p>";
                break;
        }
        return new HtmlString(result);
    }
}