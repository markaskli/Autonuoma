namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Model of 'Filialas' entity.
/// </summary>
public class FilialasL
{
    [DisplayName("Id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Adresas")]
    public string Adresas { get; set; }

    [DisplayName("Telefono Nr.")]
    public string TelefonoNr { get; set; }

    [DisplayName("Darbo laikas")]
    public string DarboLaikas { get; set; }

    [DisplayName("Vairavimo mokykla")]
    public string FkVairavimoMokykla { get; set; }

}

public class FilialasCE 
{
	public class FilialasM 
	{
		[DisplayName("Id")]
		[Required]
		public int Id { get; set; }

		[DisplayName("Adresas")]
		public string Adresas { get; set; }
	
		[DisplayName("Telefono Nr.")]
		public string TelefonoNr { get; set; }

		[DisplayName("El. paštas")]
		public string ElPastas { get; set; }

		[DisplayName("Darbo laikas")]
		public string DarboLaikas { get; set; }
	
		[DisplayName("Vairavimo mokykla")]
		public string FkVairavimoMokykla { get; set; }
	}

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Mokyklos { get; set; }
	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public FilialasM Model { get; set; } = new FilialasM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}