namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


public class DarbuotojasL
{
    [DisplayName("Tabelio Nr.")]
    [MaxLength(10)]
    [Required]
    public int Tabelis { get; set; }

    [DisplayName("Vardas")]
    [MaxLength(20)]
    [Required]
    public string Vardas { get; set; }

    [DisplayName("Pavardė")]
    [MaxLength(20)]
    [Required]
    public string Pavarde { get; set; }

    [DisplayName("Telefono Nr.")]
    [Required]
    public string TelefonoNr { get; set; }

    [DisplayName("Vairavimo mokykla")]
    [Required]
    public string fkVairavimoMokykla { get; set; }
}


/// <summary>
/// Model of 'Darbuotojas' entity.
/// </summary>
public class DarbuotojasCE
{
    public class DarbuotojasM
    {
        [DisplayName("Tabelio Nr.")]
        [Required]
        public int Tabelis { get; set; }

        [DisplayName("Vardas")]
        public string Vardas { get; set; }

        [DisplayName("Pavardė")]
        public string Pavarde { get; set; }

        [DisplayName("Telefono Nr.")]
        public string TelefonoNr { get; set; }

        [DisplayName("El. paštas")]
        public string Elpastas { get; set; }

        [DisplayName("Gimimo data")]
        public DateTime GimimoData { get; set; }

        [DisplayName("Vairavimo mokykla")]
        public string fkVairavimoMokykla { get; set; }

        [DisplayName("Filialas")]
        public int fkFilialas { get; set; }

    }

	/// <summary>
	/// Select lists for making drop downs for choosing values of entity fields.
	/// </summary>
	public class ListsM
	{
		public IList<SelectListItem> Mokyklos { get; set; }
        public IList<SelectListItem> Filialai { get; set; }
	}

	/// <summary>
	/// Entity view.
	/// </summary>
	public DarbuotojasM Darbuotojas { get; set; } = new DarbuotojasM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();



}
