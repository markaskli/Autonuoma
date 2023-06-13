namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.BranchReport;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// View model for single contract in a report.
/// </summary>
public class BranchReport
{
	public int Id {get; set;}

	[DisplayName("Filialo adresas")]
	public string fAdresas { get; set; }

	[DisplayName("El. paštas")]
	public string elPastas { get; set; }

	[DisplayName("Vairavimo mokyklos pavadinimas")]
	public string imonesPavadinimas { get; set; }

	[DisplayName("Paslaugos pavadinimas")]
	public string paslaugosPavadinimas {get; set;}

	[DisplayName("Sudarytų sutarčių kiekis")]
	public int Kiekis { get; set; }

	[DisplayName("Bendra sutarčių vertė")]
	public decimal PaslauguKaina { get; set; }

	public int BendrasKiekis { get; set; }

	public decimal BendraSumaSutarciu { get; set; }
}

/// <summary>
/// View model for whole report.
/// </summary>
public class Report
{
	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateFrom { get; set; }

	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateTo { get; set; }

	public string fAdresas {get; set;}

	public string imonesPavadinimas {get; set;}

	public List<BranchReport> Sutartys { get; set; }

	public int VisoKiekisSutarciu { get; set; }

	public decimal VisoSumaSutarciu { get; set; }
}
