namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Automobilis;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Automobilis' in list form.
/// </summary>
public class AutomobilisL
{
	[DisplayName("VIN Nr.")]
	public string VinNr { get; set; }

	[DisplayName("Valstybinis Nr.")]
	public string ValstybinisNr { get; set; }

	[DisplayName("Modelis")]
	public string Modelis { get; set; }

	[DisplayName("Markė")]
	public string Marke { get; set; }

	[DisplayName("Filialas")]
	public string FkFilialas { get; set; }

	[DisplayName("Mokykla")]
	public string Mokykla { get; set; }
}

/// <summary>
/// 'Automobilis' in create and edit forms.
/// </summary>
public class AutomobilisCE
{
	/// <summary>
	/// Automobilis.
	/// </summary>
	public class AutomobilisM
	{
		[DisplayName("VIN Nr.")]
		[Required]
		public string VinNr { get; set; }

		[DisplayName("Valstybinis Nr.")]
		[MaxLength(6)]
		[Required]
		public string ValstybinisNr { get; set; }

		[DisplayName("Pagaminimo data")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime? PagaminimoData { get; set; }

		[DisplayName("Rida")]
		[Required]
		public int Rida { get; set; }

		[DisplayName("Registravimo data")]
		[Required]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime? RegistravimoData { get; set; }

		[DisplayName("Vertė")]
		[Required]
		[DataType(DataType.Currency)]
		public decimal Verte { get; set; }

		[DisplayName("Pavarų dėžė")]
		[Required]
		public int FkPavaruDeze { get; set; }

		[DisplayName("Kuro tipas")]
		[Required]
		public int FkKuroTipas { get; set; }	

		[DisplayName("Kėbulo tipas")]
		[Required]
		public int FkKebuloTipas { get; set; }	

		[DisplayName("Modelis")]
		[Required]
		public int FkModelis { get; set; }


		[DisplayName("Filialas")]
		[Required]
		public int FkFilialas { get; set; }		

	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Modeliai { get; set; }
		public IList<SelectListItem> PavaruDezes { get; set; }
		public IList<SelectListItem> KebuloTipai { get; set; }
		public IList<SelectListItem> KuroTipai { get; set; }
		public IList<SelectListItem> Filialai { get; set; }

	}


	/// <summary>
	/// Automobilis.
	/// </summary>
	public AutomobilisM Automobilis { get ; set; } = new AutomobilisM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


/// <summary>
/// 'PavaruDeze' enumerator in lists.
/// </summary>
public class PavaruDeze
{
	public int Id { get; set; }

	public string Pavadinimas { get; set; }
}

/// <summary>
/// 'KebuloTipas' enumerator in lists.
/// </summary>
public class KebuloTipas
{
	public int Id { get; set; }

	public string Pavadinimas { get; set; }
}

/// <summary>
/// 'DegaluTipas' enumerator in lists.
/// </summary>
public class KuroTipas
{
	public int Id { get; set; }

	public string Pavadinimas { get; set; }
}


