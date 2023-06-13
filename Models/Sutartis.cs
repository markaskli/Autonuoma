namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Sutartis;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Sutartis' in list form.
/// </summary>
public class SutartisL
{
	[DisplayName("Nr.")]
	public int Nr { get; set; }

	[DisplayName("Sudarymo data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime Data { get; set; }

    [DisplayName("Sutarties būsena")]
    public string Busena { get; set; }

	[DisplayName("Darbuotojas")]
	public string FkDarbuotojas { get; set; }

    [DisplayName("Klientas")]
    public string FkKlientas { get; set; }
 
}


/// <summary>
/// 'Sutartis' in create and edit forms.
/// </summary>
public class SutartisCE
{
    /// <summary>
    /// Entity data.
    /// </summary>
    public class SutartisM
    {
        [DisplayName("Nr.")]
        public int Nr { get; set; }

        [DisplayName("Sudarymo data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Data { get; set; }

        [DisplayName("Galiojimo pradžios data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PradziosData { get; set; }

        [DisplayName("Galiojimo pabaigos data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PabaigosData { get; set; }

        [DisplayName("Kaina")]
        public decimal Kaina { get; set; }

        [DisplayName("Pradinė rida")]
        public int PradineRida { get; set; }

        [DisplayName("Galinė rida")]
        public int? GalineRida { get; set; }

        [DisplayName("Sutarties būsena")]
        public int Busena { get; set; }

        [DisplayName("Darbuotojas")]
        public int FkDarbuotojas { get; set; }


        [DisplayName("Instruktorius")]
        public int FkInstruktorius { get; set; }


        [DisplayName("Automobilis")]
        public string FkAutomobilis { get; set; }

        [DisplayName("Klientas")]
        public string FkKlientas { get; set; }

        [DisplayName("Filialas")]
        public int FkFilialas { get; set; }

        [DisplayName("Paslauga")]
        public int FkPaslauga { get; set; }
    }

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Busenos { get; set; }
		public IList<SelectListItem> Instruktoriai { get; set; }
		public IList<SelectListItem> Darbuotojai { get; set; }
		public IList<SelectListItem> Automobiliai { get; set; }
		public IList<SelectListItem> Klientai { get; set; }
		public IList<SelectListItem> Filialai { get; set; }
		public IList<SelectListItem> Paslaugos {get;set;}
	}


	/// <summary>
	/// Sutartis.
	/// </summary>
	public SutartisM Sutartis { get; set; } = new SutartisM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


/// <summary>
/// 'SutartiesBusena' enumerator in lists.
/// /// </summary>
public class SutartiesBusena
{
	public int Id { get; set; }

	public string Name { get; set; }
}