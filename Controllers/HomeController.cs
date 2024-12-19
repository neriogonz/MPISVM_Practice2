using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTeploobmenApp.Data;
using WebTeploobmenApp.Models;

namespace WebTeploobmenApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TeploobmenContext _context;

        public HomeController(ILogger<HomeController> logger, TeploobmenContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Variants.ToList());
        }

        public IActionResult Parameters(int id)
        {
            Variant variant = _context.Variants.Find(id) ?? new Variant();
            Formulas.Data data = variant;
            return View(data);
        }

        public IActionResult Calc(CalcViewModel viewModel)
        {
            int yCount = viewModel.H0 * 2 + 1;
            double[,] result = new double[yCount, 8];

            for (int y = 0; y < yCount; y++)
            {
                double y2 = y / 2.0;
                result[y, 0] = Formulas.CalcY(viewModel, y2);
                result[y, 1] = Formulas.Calc1exp(viewModel, y2);
                result[y, 2] = Formulas.Calc1mexp(viewModel, y2);
                result[y, 3] = Formulas.CalcV(viewModel, y2);
                result[y, 4] = Formulas.CalcO(viewModel, y2);
                result[y, 5] = Formulas.CalcT1(viewModel, y2);
                result[y, 6] = Formulas.CalcT2(viewModel, y2);
                result[y, 7] = Formulas.CalcTDifference(viewModel, y2);
                for (int i = 0; i < 8; i++)
                {
                    result[y, i] = Math.Round(result[y, i], 2);
                }
            }

            viewModel.ResultTable = result;

            _context.Variants.Add(new Variant(viewModel));
            _context.SaveChanges();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
