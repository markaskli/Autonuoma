namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using BranchesReport = Org.Ktu.Isk.P175B602.Autonuoma.Models.BranchReport;



/// <summary>
/// Controller for producing reports.
/// </summary>
public class ReportsController : Controller
{
	/// <summary>
	/// Produce reports of branches for driving schools
	/// </summary>
	/// <param name="dateFrom"></param>
	/// <param name="dateTo"></param>
	/// <returns></returns>
	[HttpGet]
	public ActionResult ContractsOfBranch(DateTime? dateFrom, DateTime? dateTo, string fAdresas, string imonesPavadinimas)
	{
		var report = new BranchesReport.Report();
		report.DateFrom = dateFrom;
		report.DateTo = dateTo?.AddHours(23).AddMinutes(59).AddSeconds(59);
		report.fAdresas = fAdresas;
		report.imonesPavadinimas = imonesPavadinimas;

		report.Sutartys = AtaskaitaRepo.GetBranchReport(report.DateFrom, report.DateTo, report.fAdresas, report.imonesPavadinimas);
		

		foreach(var item in report.Sutartys)
		{
			report.VisoKiekisSutarciu += item.Kiekis;
			report.VisoSumaSutarciu += item.PaslauguKaina;
		}

		return View(report);
	}

}