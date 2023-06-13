namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Sutartis;


/// <summary>
/// Database operations related to 'Sutartis' entity.
/// </summary>
public class SutartisRepo
{
	public static List<SutartisL> ListSutartis()
	{
		var query =
			$@"SELECT
				s.nr,
				s.sudarymo_data as data,
				CONCAT(ad.vardas,' ', ad.pavarde) as darbuotojas,
				CONCAT(k.vardas,' ', k.pavarde) as klientas,
				b.name as busena
			FROM
				`{Config.TblPrefix}sutartys` s
				LEFT JOIN `{Config.TblPrefix}administracijos_darbuotojai` ad ON s.fk_ADMINISTRACIJOS_DARBUOTOJAS=ad.tabelio_nr
				LEFT JOIN `{Config.TblPrefix}klientai` k ON s.fk_KLIENTAS=k.asmens_kodas
				LEFT JOIN `{Config.TblPrefix}sutarties_busenos` b ON s.busena=b.id
			ORDER BY s.nr DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<SutartisL>(drc, (dre, t) => {
				t.Nr = dre.From<int>("nr");
				t.FkDarbuotojas = dre.From<string>("darbuotojas");
				t.FkKlientas = dre.From<string>("klientas");
				t.Data = dre.From<DateTime>("data");
				t.Busena = dre.From<string>("busena");
			});

		return result;
	}

	public static SutartisCE Find(int nr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}sutartys` WHERE nr=?Nr";
		var drc =
			Sql.Query(query, args => {
				args.Add("?Nr", nr);
			});

		var result =
			Sql.MapOne<SutartisCE>(drc, (dre, t) => {
				//make a shortcut
				var sut = t.Sutartis;

				//
				sut.Nr = dre.From<int>("nr");
				sut.Data = dre.From<DateTime>("sudarymo_data");
				sut.PradziosData = dre.From<DateTime>("galiojimo_pradzios_data");
				sut.PabaigosData = dre.From<DateTime>("galiojimo_pabaigos_data");
				sut.Kaina = dre.From<decimal>("kaina");
				sut.PradineRida = dre.From<int>("pradine_rida");
				sut.GalineRida = dre.From<int?>("galine_rida");
				sut.Busena = dre.From<int>("busena");
				sut.FkDarbuotojas = dre.From<int>("fk_ADMINISTRACIJOS_DARBUOTOJAS");
				sut.FkInstruktorius = dre.From<int>("fk_INSTRUKTORIUS");
				sut.FkAutomobilis = dre.From<string>("fk_AUTOMOBILIS");
				sut.FkKlientas = dre.From<string>("fk_KLIENTAS");
				sut.FkFilialas = dre.From<int>("fk_FILIALAS");
				sut.FkPaslauga = dre.From<int>("fk_PASLAUGA");

			});

		return result;
	}

	public static void Insert(SutartisCE sutCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}sutartys`
			(
				`nr`,
				`sudarymo_data`,
				`galiojimo_pradzios_data`,
				`galiojimo_pabaigos_data`,
				`kaina`,
				`pradine_rida`,
				`galine_rida`,
				`busena`,
				`fk_ADMINISTRACIJOS_DARBUOTOJAS`,
				`fk_INSTRUKTORIUS`,
				`fk_AUTOMOBILIS`,
				`fk_KLIENTAS`,
				`fk_FILIALAS`,
				`fk_PASLAUGA`
			)
			VALUES(
				?Nr,
				?sudarymoData,
				?pradziosData,
				?pabaigosData,
				?kaina,
				?prrida,
				?grida,
				?busena,
				?fk_administracijos_darbuotojas,
				?fk_instruktorius,
				?fk_automobilis,
				?fk_klientas,
				?fk_filialas,
				?fk_paslauga
			)";

		var nr =
			Sql.Insert(query, args => {
				//make a shortcut
				var sut = sutCE.Sutartis;

				//
				args.Add("?Nr", sut.Nr);
				args.Add("?sudarymoData", sut.Data);
				args.Add("?pradziosData", sut.PradziosData);
				args.Add("?pabaigosData", sut.PabaigosData);
				args.Add("?kaina", sut.Kaina);
				args.Add("?prrida", sut.PradineRida);
				args.Add("?grida", sut.GalineRida);
				args.Add("?busena", sut.Busena);
				args.Add("?fk_administracijos_darbuotojas", sut.FkDarbuotojas);
				args.Add("?fk_instruktorius", sut.FkInstruktorius);
				args.Add("?fk_automobilis", sut.FkAutomobilis);
				args.Add("?fk_klientas", sut.FkKlientas);
				args.Add("?fk_filialas", sut.FkFilialas);
				args.Add("?fk_paslauga", sut.FkPaslauga);
			});

	}

	public static void Update(SutartisCE sutCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}sutartys`
			SET
				`nr` = ?Nr,
				`sudarymo_data` = ?sudarymoData,
				`galiojimo_pradzios_data` = ?pradziosData,
				`galiojimo_pabaigos_data` = ?pabaigosData,
				`kaina` = ?kaina,
				`pradine_rida` = ?prrida,
				`galine_rida` = ?grida,
				`busena` = ?busena,
				`fk_ADMINISTRACIJOS_DARBUOTOJAS` = ?fk_administracijos_darbuotojas,
				`fk_INSTRUKTORIUS` = ?fk_instruktorius,
				`fk_AUTOMOBILIS` = ?fk_automobilis,
				`fk_KLIENTAS` = ?fk_klientas,
				`fk_FILIALAS` = ?fk_filialas,
				`fk_PASLAUGA` = ?fk_paslauga
			WHERE nr=?Nr";

        Sql.Update(query, args =>
        {
            var sut = sutCE.Sutartis;
            args.Add("?Nr", sut.Nr);
            args.Add("?sudarymoData", sut.Data);
            args.Add("?pradziosData", sut.PradziosData);
            args.Add("?pabaigosData", sut.PabaigosData);
            args.Add("?kaina", sut.Kaina);
            args.Add("?prrida", sut.PradineRida);
            args.Add("?grida", sut.GalineRida);
            args.Add("?busena", sut.Busena);
            args.Add("?fk_administracijos_darbuotojas", sut.FkDarbuotojas);
            args.Add("?fk_instruktorius", sut.FkInstruktorius);
            args.Add("?fk_automobilis", sut.FkAutomobilis);
            args.Add("?fk_klientas", sut.FkKlientas);
            args.Add("?fk_filialas", sut.FkFilialas);
            args.Add("?fk_paslauga", sut.FkPaslauga);
        });
	}

	public static void Delete(int nr)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}sutartys` where nr=?Nr";
		Sql.Delete(query, args => {
			args.Add("?Nr", nr);
		});
	}

	public static List<SutartiesBusena> ListSutartiesBusena()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}sutarties_busenos` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<SutartiesBusena>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Name = dre.From<string>("name");
			});

		return result;
	}

}