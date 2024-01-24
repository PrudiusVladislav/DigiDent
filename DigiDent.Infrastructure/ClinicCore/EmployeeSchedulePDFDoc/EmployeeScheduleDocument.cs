using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DigiDent.Infrastructure.ClinicCore.EmployeeSchedulePDFDoc;

public class EmployeeScheduleDocument: IDocument
{
    private readonly ScheduleDocumentDataModel _dataModel;

    public EmployeeScheduleDocument(ScheduleDocumentDataModel dataModel)
    {
        _dataModel = dataModel;
    }

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
            
                page.Header().AlignCenter().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                    
                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
    }
    
    private void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default
            .FontSize(20)
            .SemiBold()
            .FontColor(Colors.Blue.Medium);
    
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .Text(_dataModel.ClinicName)
                    .Style(titleStyle);
                
                column.Item()
                    .Text($"Working schedule for: {_dataModel.EmployeeFullName}")
                    .FontSize(18)
                    .SemiBold();

                column.Item().Text(text =>
                {
                    text.Span("From date: ").SemiBold();
                    text.Span($"{_dataModel.FromDate:d}");
                });
                
                column.Item().Text(text =>
                {
                    text.Span("To date: ").SemiBold();
                    text.Span($"{_dataModel.ToDate:d}");
                });
            });
        });
    }
    
    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);
            column.Item().Element(ComposeTable);
        });
    }
    
    private void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(15);
                columns.RelativeColumn(2);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });
            
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#");
                header.Cell().Element(CellStyle).Text("Date");
                header.Cell().Element(CellStyle).AlignRight().Text("Start time"); 
                header.Cell().Element(CellStyle).AlignRight().Text("End time");
                header.Cell().Element(CellStyle).AlignRight().Text("Total hours");
                
                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold())
                        .PaddingVertical(5)
                        .BorderBottom(1)
                        .BorderColor(Colors.Black);
                }
            });
            
            foreach (var item in _dataModel.WorkingDays)
            {
                table.Cell().Element(CellStyle)
                    .Text((_dataModel.WorkingDays.IndexOf(item) + 1).ToString());
                table.Cell().PaddingLeft(10).Element(CellStyle)
                    .Text(item.Date.ToString("d"));
                table.Cell().Element(CellStyle).AlignRight()
                    .Text(item.Start.ToString("t"));
                table.Cell().Element(CellStyle).AlignRight()
                    .Text(item.End.ToString("t"));
                table.Cell().Element(CellStyle).AlignRight()
                    .Text((item.End - item.Start).ToString("h\\:mm"));
                
                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(3)
                        .BorderColor(Colors.Grey.Lighten2)
                        .PaddingVertical(2);
                }
            }
        });
    }
}