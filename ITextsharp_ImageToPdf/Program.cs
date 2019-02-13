using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITextsharp_ImageToPdf
{
    class Program
    {
        static string pdfPath = @"E:\images\TC\Reports\test.pdf";
        static void Main(string[] args)
        {
            WritePdf(@"E:\images\TC", "*TC.PNG");
            Console.ReadLine();
        }

        private static void WritePdf(string path, string extension)
        {
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document(PageSize.A4, 0, 0, 0, 0))
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();

                        doc.NewPage();
                        Image jpg1 = Image.GetInstance($@"E:\images\TC\01_Consent.png");
                        jpg1.ScaleAbsolute(PageSize.A4.Width, PageSize.A4.Height);
                        jpg1.SetAbsolutePosition(0, 0);
                        jpg1.Alignment = Image.UNDERLYING;

                        PdfContentByte canvas1 = writer.DirectContent;
                        canvas1.AddImage(jpg1);


                        //string[] files = Directory.GetFiles(@"E:\images\TC", "*TC.PNG");
                        string[] files = Directory.GetFiles(path, extension).OrderBy(f => f).ToArray();

                        foreach (var file in files)
                        {
                            doc.NewPage();
                            Console.WriteLine($"{file}");
                            Image jpg = Image.GetInstance(file);
                            jpg.ScaleAbsolute(PageSize.A4.Width, PageSize.A4.Height);
                            jpg.SetAbsolutePosition(0, 0);
                            jpg.Alignment = Image.UNDERLYING;

                            PdfContentByte canvas = writer.DirectContent;
                            canvas.AddImage(jpg);
                        }

                        doc.Close();
                    }
                }

                WriteFile(pdfPath, ms.ToArray());
            }
        }

        private static void WriteFile(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }
    }
}
