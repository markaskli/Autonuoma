namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Paslauga;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// 'Paslauga' in create and edit forms
/// </summary>
public class PaslaugaCE
{

    public class PaslaugaM
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Pavadinimas")]
        [Required]
        public string Pavadinimas { get; set; }

        [DisplayName("Tipas")]
        [Required]
        public string Tipas { get; set; }

        [DisplayName("Aprašymas")]
        public string Aprasymas { get; set; }

        [DisplayName("Trukmė (akad. valand.)")]
        public int valanduSkaicius { get; set; }

    }


    public PaslaugaM Paslauga { get; set; } = new PaslaugaM();

}

/// <summary>
/// 'Paslauga' in list form.
/// </summary>
public class PaslaugaL 
{
	[DisplayName("Id")]
	public int Id { get; set; }

	[DisplayName("Pavadinimas")]
	[Required]
	public string Pavadinimas { get; set; }

	[DisplayName("Tipas")]
	[Required]
	public string Tipas { get; set; }	
}


