namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Database operations related to 'Automobilis'.
/// </summary>
public class SaskaitaRepo
{
	public static List<SaskaitaL> List() 
	{
		var query =
			$@"SELECT
				sas.nr,
				sas.data,
				sas.suma,
				su.nr AS sutartis
			FROM
				{Config.TblPrefix}saskaitos sas
				LEFT JOIN `{Config.TblPrefix}sutartys` su ON su.nr = sas.fk_SUTARTIS
			ORDER BY sas.nr ASC";
	
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<SaskaitaL>(drc, (dre, t) => {
				t.Nr = dre.From<int>("nr");
				t.Data = dre.From<DateTime>("data");
				t.Suma = dre.From<string>("suma");
				t.FkSutartis = dre.From<int>("sutartis");
			});

		return result;
	}

	public static List<SaskaitaL> ListForKlientas(string id)
	{
		var query = $@"SELECT
							s.nr, s.suma
						FROM `{Config.TblPrefix}saskaitos` s
						LEFT JOIN `{Config.TblPrefix}mokejimai` m ON m.fk_SASKAITA = s.nr
						WHERE m.fk_KLIENTAS = ?Id
						ORDER BY s.nr ASC";

		var drc = Sql.Query(query, args => {
			args.Add("?Id", id);
		});

		var result = 
			Sql.MapAll<SaskaitaL>(drc, (dre, t) => {
				t.Nr = dre.From<int>("nr");
				t.Suma = dre.From<string>("suma");
			});

		return result;
	}

	public static SaskaitaCE FindSaskaitaCE(int Nr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}saskaitos` WHERE nr=?Nr";

		var drc =
			Sql.Query(query, args => {
				args.Add("?Nr", Nr);
			});

		var result =
			Sql.MapOne<SaskaitaCE>(drc, (dre, t) => {
				//make a shortcut
				var saskaita = t.Model;

				//
                saskaita.Nr = dre.From<int>("nr");
                saskaita.Data = dre.From<DateTime>("data");
                saskaita.Suma = dre.From<decimal>("suma");
                saskaita.FkSutartis = dre.From<int>("fk_SUTARTIS");
			});

		return result;
	}

	public static void InsertSaskaita(SaskaitaCE saskCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}saskaitos`
			(
				`nr`,
                `data`,
                `suma`,
                `fk_SUTARTIS`
			)
			VALUES (
				?Nr,
				?Data,
				?Suma,
				?fk_sutartis
			)";

		Sql.Insert(query, args => {
			//make a shortcut
			var sask = saskCE.Model;

			//
            args.Add("?Nr", sask.Nr);
            args.Add("?Data", sask.Data);
            args.Add("?Suma", sask.Suma);
            args.Add("?fk_sutartis", sask.FkSutartis);
		});
	}

	public static void UpdateSaskaita(SaskaitaCE saskCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}saskaitos`
			SET
				`data` = ?Data,
				`suma` = ?Suma,
				`fk_SUTARTIS` = ?fk_sutartis
			WHERE
				nr=?Nr";

		Sql.Update(query, args => {
			//make a shortcut
			var sask = saskCE.Model;

			//
            args.Add("?Data", sask.Data);
            args.Add("?Suma", sask.Suma);
            args.Add("?fk_sutartis", sask.FkSutartis);
			args.Add("?Nr", sask.Nr);
		});
	}

	public static void DeleteSaskaita(int Nr)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}automobiliai` WHERE nr=?Nr";
		Sql.Delete(query, args => {
			args.Add("?Nr", Nr);
		});
	}

}
