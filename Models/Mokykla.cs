namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Mokykla;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Modelis' entity.
/// </summary>
public class Mokykla
{
	[DisplayName("Įmonės kodas")]
	public string Kodas { get; set; }

	[DisplayName("Pavadinimas")]
	public string Pavadinimas { get; set; }

	//Markė
	[DisplayName("Vadovas")]
	public string Vadovas { get; set; }

	[DisplayName("El. paštas")]
	public string ElPastas { get; set; }   
}


/// <summary>
/// Model of 'Mokykla' entity used in lists.
/// </summary>
public class MokyklaL
{
	[DisplayName("Įmonės kodas")]
	public string Kodas { get; set; }

	[DisplayName("Pavadinimas")]
	public string Pavadinimas { get; set; }		

	[DisplayName("Vadovas")]
	public string Vadovas { get; set; }
}


/// <summary>
/// Model of 'Modelis' entity used in creation and editing forms.
/// </summary>
public class MokyklaCE
{
	/// <summary>
	/// Entity data
	/// </summary>
	public class MokyklaM
	{
		[DisplayName("Įmonės kodas")]
		public string Kodas { get; set; }

		[DisplayName("Pavadinimas")]
		[MaxLength(30)]
		[Required]
		public string Pavadinimas { get; set; }

		[DisplayName("Vadovas")]
        [MaxLength(40)]
		[Required]
		public string Vadovas { get; set; }

        [DisplayName("Telefono Nr.")]
        [MaxLength(20)]
		[Required]
		public string telefono_nr { get; set; }

        [DisplayName("El. paštas")]
		[Required]
		public string elpastas { get; set; }

        [DisplayName("Darbuotojų skaičius")]
		[Required]
		public int darbuotoju_skaicius { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Markes { get; set; }
	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public MokyklaM Mokykla { get; set; } = new MokyklaM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}

