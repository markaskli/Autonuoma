namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


public class InstruktoriusL
{
    [DisplayName("ID ")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Vardas")]
    [MaxLength(20)]
    [Required]
    public string Vardas { get; set; }

    [DisplayName("Pavardė")]
    [MaxLength(20)]
    [Required]
    public string Pavarde { get; set; }

    [DisplayName("Darbo laikas")]
    public string DarboLaikas { get; set; }

    [DisplayName("Filialas")]
    public string fkFilialas { get; set; }

}

/// <summary>
/// Model of 'Darbuotojas' entity.
/// </summary>
public class InstruktoriusCE
{
    public class InstruktoriusM
    {
        [DisplayName("ID")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Vardas")]
        public string Vardas { get; set; }

        [DisplayName("Pavardė")]
        public string Pavarde { get; set; }

        [DisplayName("Patirtis (metai)")]
        public int Patirtis { get; set; }

        [DisplayName("Darbo laikas")]
        public string DarboLaikas { get; set; }

        [DisplayName("Telefono Nr.")]
        public string TelefonoNr { get; set; }

        [DisplayName("El. paštas")]
        public string Elpastas { get; set; }

        [DisplayName("Gimimo data")]
        public DateTime GimimoData { get; set; }

        [DisplayName("Filialas")]
        public int fkFilialas { get; set; }

    }

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Filialai { get; set; }
	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public InstruktoriusM Instruktorius { get; set; } = new InstruktoriusM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}


