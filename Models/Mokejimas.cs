namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Modelis' entity used in lists.
/// </summary>
public class MokejimasL
{
	[DisplayName("Id")]
	public int Id { get; set; }

    [DisplayName("Data")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime Data { get; set; }

	[DisplayName("Suma")]
	public decimal Suma { get; set; }		

	[DisplayName("Klientas")]
    public string fk_KLIENTAS { get; set; }

    [DisplayName("Sąskaita")]
    public int fk_SASKAITA { get; set; }
}


/// <summary>
/// Model of 'Modelis' entity used in creation and editing forms.
/// </summary>
public class MokejimasCE
{
	/// <summary>
	/// Entity data
	/// </summary>
	public class MokejimasM
	{
		[DisplayName("Id")]
	    public int Id { get; set; }

	    [DisplayName("Suma")]
	    public decimal Suma { get; set; }	
        
        [DisplayName("Data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Data { get; set; }        	

	    [DisplayName("Atsiskaitymo tipas")]
	    public int Tipas { get; set; }

        [DisplayName("Sąskaita")]
        public int fk_SASKAITA { get; set; }

        [DisplayName("Klientas")]
        public string fk_KLIENTAS { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Saskaitos { get; set; }
		public IList<SelectListItem> Klientai { get; set; }
        public IList<SelectListItem> AtsiskaitymoTipai { get; set; }

	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public MokejimasM Mokejimas { get; set; } = new MokejimasM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


public class AtsiskaitymoTipas
{
    public int Id { get; set; }
    public string Pavadinimas { get; set; }
}

