namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Modelis' entity used in lists.
/// </summary>
public class SaskaitaL
{
	[DisplayName("Nr.")]
	public int Nr { get; set; }

	[DisplayName("Data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime Data { get; set; }		

	[DisplayName("Suma")]
	public string Suma { get; set; }

	[DisplayName("Sutartis")]
    public int FkSutartis { get; set; }
}


/// <summary>
/// Model of 'Modelis' entity used in creation and editing forms.
/// </summary>
public class SaskaitaCE
{
	/// <summary>
	/// Entity data
	/// </summary>
	public class SaskaitaM
	{
		[DisplayName("Nr. ")]
		public int Nr { get; set; }

		[DisplayName("Data")]
	    public DateTime Data { get; set; }		

	    [DisplayName("Suma")]
	    public decimal Suma { get; set; }

        [DisplayName("Sutartis")]
        public int? FkSutartis { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Sutartys { get; set; }
	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public SaskaitaM Model { get; set; } = new SaskaitaM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}

