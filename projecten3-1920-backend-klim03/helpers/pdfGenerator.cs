using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs.CustomDTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.helpers
{
    public class PdfGenerator
    {
        private IConverter _converter;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHostingEnvironment _env;

        public PdfGenerator(IConverter converter, IHostingEnvironment environment, IHostingEnvironment env)
        {
            _converter = converter;
            _hostingEnvironment = environment;
            _env = env;
        }


        public byte[] GenerateCustomPdf(Project project, PdfSettings pdfsettings)
        {

            var sb = new StringBuilder();

            string klimImageStyle = "float: right; width: 350px; height 350px;";
            string pageSize = "width: 28cm; height: 410.8mm;";

            //string themeColor= "color:#C3004A;";

            sb.Append(@"<head></head>");
            sb.Append(@"<body>");
            

            foreach (var group in project.Groups) 
            {
                if (pdfsettings.GroupsToShow.Contains(group.GroupId))
                {
                    if (pdfsettings.GroupsToShow.Contains(group.GroupId))
                    {
                        sb.AppendFormat(@"<img style='{0}' src='{1}'>", klimImageStyle, _env.WebRootPath.ToString() + "\\images\\logo_klimaatmobiel.JPG");
                        sb.AppendFormat(@"<div style='{0}'>", pageSize);
                        sb.Append(@"<div style=''>");

                        sb.AppendFormat(@"<table>
                        <tr>
                        <td><h1 style='color:#C3004A; font-size:65px; padding-top:50px;'>{0}</h1></td>
                        <td rowspan='2' style='padding-left:60px;'>", group.GroupName);

                        foreach (var pupil in group.PupilGroups)
                        {
                            sb.AppendFormat(@"<span style='font-size:25px'>{0}</span><br>", pupil.Pupil.FirstName);
                        }

                        sb.Append("</td>");
                        sb.AppendFormat(@"
                    <tr><td align='left'>
                        <span style='color:#C3004A;font-size:35px;'>{2}{3}<br>{0}{1}</span>
                    </td></tr>", "score: ", group.Order.GetOrderScore, "Gespendeerd: ", group.Order.GetOrderPrice);

                        sb.Append("</tr></table>");


                        sb.Append(@"</div>");
                        sb.Append(@"<hr>");

                        foreach (var item in group.Evaluations)
                        {
                            sb.Append(@"<div style='width=100%;margin-top:10px;margin-bottom:10px'>");

                            sb.AppendFormat(@"<span style='font-size:40px; '>{0}</span><br>", (!item.Extra) ? item.EvaluationCriterea.Title : item.Title);

                            if (pdfsettings.ShowTeacher)
                            {
                                sb.Append(@"<span style='font-size:22px;font-weight: bold;'>Leerkracht</span><br>");
                                sb.AppendFormat(@"<span style='font-size:21px;'>{0}</span><br>", (item.DescriptionPrivate == "") ? "--" : item.DescriptionPrivate);
                                sb.Append("<div style='width:100%; height:10px;'></div>");
                            }
                            if (pdfsettings.ShowPupil)
                            {
                                sb.Append(@"<span style='font-size:22px;font-weight: bold;'>Leerling</span><br>");
                                sb.AppendFormat(@"<span style='font-size:21px;'>{0}</span><br>", (item.DescriptionPupil == "") ? "--" : item.DescriptionPupil);
                            }

                            sb.Append(@"</div>");
                            sb.Append(@"<hr>");

                        }
                        sb.Append(@"</div>");


                    }
                }   
            }
            sb.Append(@"</div>");
            sb.Append(@"</body>");


            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4

                },
                Objects = {
                new ObjectSettings() {
                PagesCount = true,
                HtmlContent = sb.ToString(),
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 16, Left = "Project: " + project.ProjectName + " - Budget: " + Math.Truncate(project.ProjectBudget), Line = true, Spacing = 0 }
                }
            }
            };

            byte[] pdf = _converter.Convert(doc);

            return pdf;
        }


    }
}
