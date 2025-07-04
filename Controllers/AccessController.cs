using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1stModule_PIPremises.Data;
using _1stModule_PIPremises.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _1stModule_PIPremises.Controllers
{
    public class AccessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Access/
        public IActionResult Index()
        {
            var locations = _context.Locations
                .OrderBy(l => l.LocationName)
                .Select(l => new SelectListItem
                {
                    Value = l.LocationID.ToString(),
                    Text = l.LocationName
                }).ToList();

            ViewBag.Locations = new SelectList(locations, "Value", "Text");
            return View();
        }

        // GET: /Access/GetLiveData?locationId=1&showAll=true&flag=Intern
        [HttpGet]
        public async Task<IActionResult> GetLiveData(int locationId, bool showAll, string? flag)
        {
            var peopleQuery = _context.Users
                .Include(u => u.Location)
                .Include(u => u.SwipeLogs)
                .Where(u => u.LocationID == locationId);

            if (!string.IsNullOrEmpty(flag))
            {
                peopleQuery = peopleQuery.Where(u => u.Flag == flag);
            }

            var now = DateTime.Now;

            var filteredPeople = await peopleQuery
                .Where(u => u.SwipeLogs.Any(log =>
                    log.SwipeIN.Date == now.Date &&
                    (showAll || log.SwipeOUT == null || log.SwipeOUT > now)))
                .ToListAsync();

            // Chart data
            var chartData = filteredPeople
                .GroupBy(p => p.Flag)
                .Select(g => new
                {
                    flag = g.Key,
                    count = g.Count()
                }).ToList();

            // Table data
            var tableData = filteredPeople
                .Select(p => new
                {
                    name = p.EmployeeName,
                    flag = p.Flag,
                    designation = p.Designation ?? "",
                    remarks = p.Remarks ?? ""
                }).ToList();

            return Json(new { chartData, tableData });
        }

        // CSV Export
        public IActionResult ExportCsv(int locationId)
        {
            var users = _context.Users
                .Include(u => u.Location)
                .Where(u => u.LocationID == locationId)
                .ToList();

            var csv = "Name,Flag,Designation,Remarks\n";
            foreach (var user in users)
            {
                csv += $"{user.EmployeeName},{user.Flag},{user.Designation},{user.Remarks}\n";
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "premises_data.csv");
        }

        // ✅ PDF Export using QuestPDF
        public IActionResult ExportPdf(int locationId)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var users = _context.Users
                .Include(u => u.Location)
                .Where(u => u.LocationID == locationId)
                .ToList();

            var locationName = users.FirstOrDefault()?.Location.LocationName ?? "Unknown Location";

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text($"Premises Report - {locationName}")
                        .SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            // Header
                            table.Header(header =>
                            {
                                header.Cell().Element(CellHeaderStyle).Text("Name");
                                header.Cell().Element(CellHeaderStyle).Text("Flag");
                                header.Cell().Element(CellHeaderStyle).Text("Designation");
                                header.Cell().Element(CellHeaderStyle).Text("Remarks");

                                static IContainer CellHeaderStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold())
                                                    .Padding(5)
                                                    .Background(Colors.Grey.Lighten3)
                                                    .BorderBottom(1)
                                                    .BorderColor(Colors.Grey.Medium);
                                }
                            });

                            // Rows
                            foreach (var user in users)
                            {
                                table.Cell().Element(CellStyle).Text(user.EmployeeName);
                                table.Cell().Element(CellStyle).Text(user.Flag);
                                table.Cell().Element(CellStyle).Text(user.Designation ?? "-");
                                table.Cell().Element(CellStyle).Text(user.Remarks ?? "-");
                            }

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated on {DateTime.Now:dd MMM yyyy, HH:mm}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Medium);
                });
            });

            byte[] pdfBytes = pdf.GeneratePdf();
            return File(pdfBytes, "application/pdf", "premises_data.pdf");
        }
    }
}
